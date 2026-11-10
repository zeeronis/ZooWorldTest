using System.Collections.Generic;
using ZooWorld.Animals;
using ZooWorld.Stats;

namespace ZooWorld.UI
{
    // Shows the top View.MaxRows kinds by death count. On a tie the kind that died first stays higher.
    public sealed class DeathCounterPresenter : Presenter<DeathCounterView>
    {
        private readonly UIGameCtrl _ui;
        private readonly DeathStats _stats;
        private readonly List<DeathCounterRowView> _rows = new();
        private readonly List<AnimalConfig> _shown = new();
        private readonly List<AnimalConfig> _top = new();

        public DeathCounterPresenter(DeathCounterView view, UIGameCtrl ui, DeathStats stats) : base(view)
        {
            _ui = ui;
            _stats = stats;
        }

        public override void Initialize()
        {
            _ui.Open(GameScreenId.DeathCounter);
            Refresh(null);
            _stats.Changed += OnChanged;
        }

        public override void Dispose()
        {
            _stats.Changed -= OnChanged;
            _rows.Clear();
            _shown.Clear();
            _top.Clear();
        }

        private void OnChanged(AnimalConfig kind, int count)
        {
            Refresh(kind);
        }

        private void Refresh(AnimalConfig changed)
        {
            CollectTop(View.MaxRows);

            for (var i = 0; i < _top.Count; i++)
            {
                var kind = _top[i];
                var row = GetRow(i);
                if (_shown[i] != kind)
                {
                    row.SetKind(kind.DisplayName, kind.Icon);
                    _shown[i] = kind;
                }

                row.SetCount(_stats.GetCount(kind), kind == changed);
            }
        }

        // Partial insertion sort: O(kinds * limit), keeps first-died order on ties.
        private void CollectTop(int limit)
        {
            _top.Clear();
            var kinds = _stats.Kinds;
            for (var i = 0; i < kinds.Count; i++)
            {
                var kind = kinds[i];
                var count = _stats.GetCount(kind);

                var index = _top.Count;
                while (index > 0 && _stats.GetCount(_top[index - 1]) < count)
                {
                    index--;
                }

                if (index >= limit)
                {
                    continue;
                }

                _top.Insert(index, kind);
                if (_top.Count > limit)
                {
                    _top.RemoveAt(limit);
                }
            }
        }

        // Kinds never disappear from the stats, so rows only grow up to MaxRows.
        private DeathCounterRowView GetRow(int index)
        {
            while (_rows.Count <= index)
            {
                _rows.Add(View.AddRow());
                _shown.Add(null);
            }

            return _rows[index];
        }
    }
}
