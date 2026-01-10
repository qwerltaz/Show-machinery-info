namespace DisplayMachineryAttributes.Readers
{
    [BehaviourReader(typeof(PointerBehaviour))]
    public class PointerReader : IAttributeReader
    {
        private readonly PointerBehaviour _behaviour;

        public PointerReader(PointerBehaviour behaviour)
        {
            _behaviour = behaviour;
        }

        public bool IsValid()
        {
            return _behaviour;
        }

        public string GetDisplayText()
        {
            return $"Speed: {_behaviour.Speed}";
        }
    }
}