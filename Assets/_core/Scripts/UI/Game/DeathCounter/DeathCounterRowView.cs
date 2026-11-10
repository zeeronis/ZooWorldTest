using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ZooWorld.UI
{
    public sealed class DeathCounterRowView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _nameLabel;
        [SerializeField] private TMP_Text _countLabel;
        [SerializeField] private Image _icon;

        [Header("Pulse")]
        [Tooltip("What scales on change. Not driven by layout, so scaling is safe.")]
        [SerializeField] private RectTransform _pulseTarget;
        [SerializeField, Min(0.01f)] private float _pulseDuration = 0.25f;
        [Tooltip("Peak extra scale: 0.35 means up to 135%.")]
        [SerializeField, Min(0f)] private float _pulseAmplitude = 0.35f;

        private Coroutine _pulse;

        public void SetKind(string displayName, Sprite icon)
        {
            _nameLabel.text = displayName;
            if (_icon != null)
            {
                _icon.sprite = icon;
                _icon.gameObject.SetActive(icon != null);
            }
        }

        public void SetCount(int count, bool pulse)
        {
            _countLabel.SetText("{0}", count);
            if (pulse)
            {
                Pulse();
            }
        }

        private void Pulse()
        {
            if (!isActiveAndEnabled || _pulseTarget == null)
            {
                return;
            }

            if (_pulse != null)
            {
                StopCoroutine(_pulse);
            }

            _pulse = StartCoroutine(PulseRoutine());
        }

        private IEnumerator PulseRoutine()
        {
            for (var time = 0f; time < _pulseDuration; time += Time.unscaledDeltaTime)
            {
                // Up and back down in one half-sine.
                var scale = 1f + _pulseAmplitude * Mathf.Sin(Mathf.PI * time / _pulseDuration);
                _pulseTarget.localScale = new Vector3(scale, scale, 1f);
                yield return null;
            }

            _pulseTarget.localScale = Vector3.one;
            _pulse = null;
        }

        private void OnDisable()
        {
            if (_pulseTarget != null)
            {
                _pulseTarget.localScale = Vector3.one;
            }

            _pulse = null;
        }
    }
}
