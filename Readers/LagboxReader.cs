namespace DisplayMachineryAttributes.Readers
{
    [BehaviourReader(typeof(LagboxBehaviour))]
    public class LagboxReader : IAttributeReader
    {
        private readonly LagboxBehaviour _behaviour;

        public LagboxReader(LagboxBehaviour behaviour)
        {
            _behaviour = behaviour;
        }

        public bool IsValid()
        {
            return _behaviour != null;
        }

        public string GetDisplayText()
        {
            return "Delay: " + _behaviour.DelayModifier;
        }
    }
}
