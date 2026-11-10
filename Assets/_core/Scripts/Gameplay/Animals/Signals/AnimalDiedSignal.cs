namespace ZooWorld.Animals
{
    public readonly struct AnimalDiedSignal
    {
        public readonly AnimalView Animal;
        public readonly AnimalConfig Config;

        public AnimalDiedSignal(AnimalView animal)
        {
            Animal = animal;
            Config = animal.Config;
        }

        public AnimalRole Role => Config.Role;
    }
}
