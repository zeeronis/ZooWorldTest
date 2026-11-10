using Zenject;
using ZooWorld.Animals;
using ZooWorld.Collisions;
using ZooWorld.Infrastructure;

namespace ZooWorld.Feeding
{
    public sealed class FeedingSolver : IGameFixedUpdatable
    {
        private readonly AnimalContacts _contacts;
        private readonly AnimalBounce _bounce;
        private readonly AnimalLifecycle _lifecycle;
        private readonly IRandomService _random;
        private readonly SignalBus _signalBus;

        public FeedingSolver(AnimalContacts contacts, AnimalBounce bounce, AnimalLifecycle lifecycle, IRandomService random, SignalBus signalBus)
        {
            _contacts = contacts;
            _bounce = bounce;
            _lifecycle = lifecycle;
            _random = random;
            _signalBus = signalBus;
        }

        public void OnFixedUpdate(float fixedDeltaTime)
        {
            var pairs = _contacts.Pairs;
            for (var i = 0; i < pairs.Count; i++)
            {
                Solve(pairs[i].First, pairs[i].Second);
            }

            _contacts.Clear();
        }

        private void Solve(AnimalView a, AnimalView b)
        {
            if (!a.IsAlive || !b.IsAlive)
            {
                return;
            }

            var aEatsB = a.Config.Role.CanEat(b.Config.Role);
            var bEatsA = b.Config.Role.CanEat(a.Config.Role);

            if (!aEatsB && !bEatsA)
            {
                _bounce.Apply(a, b);
                return;
            }

            var aWins = aEatsB && (!bEatsA || _random.Chance(0.5f));
            if (aWins)
            {
                Eat(a, b);
            }
            else
            {
                Eat(b, a);
            }
        }

        private void Eat(AnimalView predator, AnimalView prey)
        {
            var preyConfig = prey.Config;
            _lifecycle.Despawn(prey);
            _signalBus.Fire(new AnimalAteSignal(predator, preyConfig));
        }
    }
}
