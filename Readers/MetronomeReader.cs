
namespace DisplayMachineryDetail.Readers
{
    [BehaviourReader(typeof(MetronomeBehaviour))]
    public class MetronomeReader : IAttributeReader
    {
        private readonly MetronomeBehaviour behaviour;

        public MetronomeReader(MetronomeBehaviour behaviour)
        {
            this.behaviour = behaviour;
        }

        public bool IsValid() => behaviour != null;

        public string GetDisplayText()
        {
            return "Hz: " + behaviour.TempoModifier;
        }
    }
}

