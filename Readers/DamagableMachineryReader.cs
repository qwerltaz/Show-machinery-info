namespace DisplayMachineryAttributes.Readers
{
    [BehaviourReader(typeof(DamagableMachineryBehaviour), true)]
    public class DamagableMachineryReader : IAttributeReader
    {
        private readonly DamagableMachineryBehaviour _behaviour;

        public DamagableMachineryReader(DamagableMachineryBehaviour behaviour)
        {
            _behaviour = behaviour;
        }

        public bool IsValid()
        {
            return _behaviour != null;
        }

        public string GetDisplayText()
        {
            return $"{GetIndestructibleText()}\n{GetDestroyedText()}";
        }

        private string GetIndestructibleText()
        {
            return _behaviour.Indestructible ? "∞" : "";
        }

        private string GetDestroyedText()
        {
            return _behaviour.Destroyed ? "Broken" : "";
        }
    }
}
