using UnityEngine;

namespace ZooWorld.UI
{
    public sealed class DeathCounterView : View
    {
        [SerializeField] private RectTransform _rowsRoot;
        [SerializeField] private DeathCounterRowView _rowPrefab;

        [Tooltip("How many kinds the counter shows at most. The ones with the most deaths win.")]
        [SerializeField, Min(1)] private int _maxRows = 3;

        public int MaxRows => _maxRows;

        public DeathCounterRowView AddRow()
        {
            return Instantiate(_rowPrefab, _rowsRoot, false);
        }
    }
}
