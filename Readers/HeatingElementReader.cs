namespace DisplayMachineryAttributes.Readers
{
    [BehaviourReader(typeof(HeatingElementBehaviour))]
    public class HeatingElementReader : IAttributeReader
    {
        private readonly HeatingElementBehaviour _behaviour;

        public HeatingElementReader(HeatingElementBehaviour behaviour)
        {
            _behaviour = behaviour;
        }

        public bool IsValid()
        {
            return _behaviour;
        }

        public string GetDisplayText()
        {
            return $"Target: {_behaviour.TargetTemperature}";
        }
    }
}