using UnityEngine;

namespace ZooWorld.UI
{
    [CreateAssetMenu(menuName = "ZooWorld/UI/Tasty Popup", fileName = "TastyPopupConfig")]
    public sealed class TastyPopupConfig : ScriptableObject
    {
        [SerializeField] private TastyPopupView _prefab;
        [SerializeField] private string _text = "Tasty!";
        [SerializeField, Min(0.1f)] private float _duration = 1.2f;

        [Tooltip("World offset from the predator position at the moment it ate.")]
        [SerializeField] private Vector3 _spawnOffset = new(0f, 0.8f, 0f);

        [Tooltip("How high the text rises along world Y during its life, in world units.")]
        [SerializeField, Min(0f)] private float _riseHeight = 1.5f;

        [Tooltip("Part of the life at the end used to fade out.")]
        [SerializeField, Range(0f, 1f)] private float _fadePart = 0.35f;

        public TastyPopupView Prefab => _prefab;
        public string Text => _text;
        public float Duration => _duration;
        public Vector3 SpawnOffset => _spawnOffset;
        public float RiseHeight => _riseHeight;
        public float FadePart => _fadePart;
    }
}
