using UnityEngine;
using Zenject;
using ZooWorld.Cameras;

namespace ZooWorld.UI
{
    [RequireComponent(typeof(Canvas))]
    public sealed class WorldPopupsCanvas : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;

        public Transform Root => _canvas.transform;

        [Inject]
        public void Construct(CameraCtrl camera)
        {
            _canvas.worldCamera = camera.Camera;
        }
    }
}
