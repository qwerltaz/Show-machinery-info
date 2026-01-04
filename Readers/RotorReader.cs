
namespace DisplayMachineryDetail.Readers
{
    [BehaviourReader(typeof(RotorBehaviour))]
    public class RotorReader : IAttributeReader
    {
        private readonly RotorBehaviour behaviour;

        public RotorReader(RotorBehaviour behaviour)
        {
            this.behaviour = behaviour;
        }

        public bool IsValid() => behaviour != null;

        public string GetDisplayText()
        {
            return "Speed: " + behaviour.Speed;
        }
    }
}

