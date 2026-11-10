using ZooWorld.Animals;

namespace ZooWorld.Collisions
{
    public interface IAnimalCollisionSink
    {
        void Report(AnimalView self, AnimalView other);
    }
}
