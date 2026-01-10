namespace DisplayMachineryAttributes.Readers
{
    [BehaviourReader(typeof(MotorisedHingeBehaviour))]
    public class ServoReader : IAttributeReader
    {
        private readonly MotorisedHingeBehaviour _behaviour;

        public ServoReader(MotorisedHingeBehaviour behaviour)
        {
            _behaviour = behaviour;
        }

        public bool IsValid()
        {
            return _behaviour;
        }

        public string GetDisplayText()
        {
            return $"Target angle:{_behaviour.Delta}\nSpeed: {_behaviour.Speed}";
        }
    }
}