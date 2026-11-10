using Zenject;
using ZooWorld.Cameras;

namespace ZooWorld.Levels
{
    public sealed class LevelBuilder
    {
        public Level Level { get; }

        public LevelBuilder(DiContainer container, Level levelPrefab, CameraCtrl camera)
        {
            Level = container.InstantiatePrefabForComponent<Level>(levelPrefab);
            camera.Setup(Level.transform.position, Level.ArenaSize);
        }
    }
}
