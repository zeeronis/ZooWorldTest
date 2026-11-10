using UnityEngine;

namespace ZooWorld.UI
{
    public sealed class DeathCounterView : View
    {
        [SerializeField] private RectTransform _rowsRoot;
        [SerializeField] private DeathCounterRowView _rowPrefab;

        public DeathCounterRowView AddRow()
        {
            return Instantiate(_rowPrefab, _rowsRoot, false);
        }
    }
}
