using TMPro;
using UnityEngine;

namespace ZooWorld.UI
{
    [RequireComponent(typeof(RectTransform), typeof(CanvasGroup))]
    public sealed class TastyPopupView : MonoBehaviour
    {
        [SerializeField] private RectTransform _rect;
        [SerializeField] private CanvasGroup _group;
        [SerializeField] private TMP_Text _label;

        public void Setup(Transform parent, string text)
        {
            _rect.SetParent(parent, false);
            _rect.localScale = Vector3.one;
            _label.text = text;
            _group.alpha = 1f;
            _group.blocksRaycasts = false;
            _group.interactable = false;
        }

        public void SetPose(Vector3 worldPosition, Quaternion rotation)
        {
            _rect.SetPositionAndRotation(worldPosition, rotation);
        }

        public void SetAlpha(float alpha)
        {
            _group.alpha = alpha;
        }
    }
}
