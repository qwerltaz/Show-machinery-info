namespace DisplayMachineryAttributes.Readers
{
    [BehaviourReader(typeof(DetectorBehaviour))]
    public class DetectorReader : IAttributeReader
    {
        private readonly DetectorBehaviour _behaviour;

        public DetectorReader(DetectorBehaviour behaviour)
        {
            _behaviour = behaviour;
        }

        public bool IsValid()
        {
            return _behaviour != null;
        }

        public string GetDisplayText()
        {
            return $"Range: {_behaviour.Range * Global.MetricMultiplier}\n{GetDoubleTriggerText()}";
        }

        private string GetDoubleTriggerText()
        {
            return _behaviour.TriggerOnExit ? "Double" : "Single";
        }
    }
}
