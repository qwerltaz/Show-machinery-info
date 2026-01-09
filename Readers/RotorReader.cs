namespace DisplayMachineryAttributes.Readers
{
    [BehaviourReader(typeof(RotorBehaviour))]
    public class RotorReader : IAttributeReader
    {
        private readonly RotorBehaviour _behaviour;

        public RotorReader(RotorBehaviour behaviour)
        {
            _behaviour = behaviour;
        }

        public bool IsValid()
        {
            return _behaviour != null;
        }

        public string GetDisplayText()
        {
            return "Speed: " + _behaviour.Speed;
        }
    }
}
