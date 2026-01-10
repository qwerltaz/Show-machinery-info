namespace DisplayMachineryAttributes.Readers
{
    [BehaviourReader(typeof(CounterBehaviour))]
    public class CounterReader : IAttributeReader
    {
        private readonly CounterBehaviour _behaviour;

        public CounterReader(CounterBehaviour behaviour)
        {
            _behaviour = behaviour;
        }

        public bool IsValid()
        {
            return _behaviour;
        }

        public string GetDisplayText()
        {
            return $"Max: {_behaviour.ResetValue}";
        }
    }
}