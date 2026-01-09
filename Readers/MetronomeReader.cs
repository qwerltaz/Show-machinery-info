namespace DisplayMachineryAttributes.Readers
{
    [BehaviourReader(typeof(MetronomeBehaviour))]
    public class MetronomeReader : IAttributeReader
    {
        private readonly MetronomeBehaviour _behaviour;

        public MetronomeReader(MetronomeBehaviour behaviour)
        {
            _behaviour = behaviour;
        }

        public bool IsValid()
        {
            return _behaviour != null;
        }

        public string GetDisplayText()
        {
            return "Hz: " + _behaviour.TempoModifier;
        }
    }
}
