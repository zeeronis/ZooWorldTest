using System.Collections.Generic;
using ZooWorld.Animals;
using ZooWorld.Stats;

namespace ZooWorld.UI
{
    public sealed class DeathCounterPresenter : Presenter<DeathCounterView>
    {
        private readonly UIGameCtrl _ui;
        private readonly DeathStats _stats;
        private readonly Dictionary<AnimalConfig, DeathCounterRowView> _rows = new();

        public DeathCounterPresenter(DeathCounterView view, UIGameCtrl ui, DeathStats stats) : base(view)
        {
            _ui = ui;
            _stats = stats;
        }

        public override void Initialize()
        {
            _ui.Open(GameScreenId.DeathCounter);

            foreach (var kind in _stats.Kinds)
            {
                GetRow(kind).SetCount(_stats.GetCount(kind), false);
            }

            _stats.Changed += OnChanged;
        }

        public override void Dispose()
        {
            _stats.Changed -= OnChanged;
            _rows.Clear();
        }

        private void OnChanged(AnimalConfig kind, int count)
        {
            GetRow(kind).SetCount(count, true);
        }

        private DeathCounterRowView GetRow(AnimalConfig kind)
        {
            if (!_rows.TryGetValue(kind, out var row))
            {
                row = View.AddRow();
                row.SetKind(kind.DisplayName, kind.Icon);
                _rows[kind] = row;
            }

            return row;
        }
    }
}
