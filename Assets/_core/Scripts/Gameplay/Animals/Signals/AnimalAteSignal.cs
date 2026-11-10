namespace ZooWorld.Animals
{
    public readonly struct AnimalAteSignal
    {
        public readonly AnimalView Predator;
        public readonly AnimalConfig Prey;

        public AnimalAteSignal(AnimalView predator, AnimalConfig prey)
        {
            Predator = predator;
            Prey = prey;
        }
    }
}
