
namespace DisplayMachineryDetail.Readers
{
    [BehaviourReader(typeof(GateBehaviour))]
    public class GateReader : IAttributeReader
    {
        private readonly GateBehaviour _behaviour;

        public GateReader(GateBehaviour behaviour)
        {
            _behaviour = behaviour;
        }

        public bool IsValid() => _behaviour != null;

        public string GetDisplayText()
        {
            return $"Threshold: {_behaviour.ThresholdPercentage}\nMaxPower: {_behaviour.MaxPower}\n{GetDoubleTriggerText()}";
        }

        private string GetDoubleTriggerText()
        {
            return _behaviour.DoubleTrigger ? "Double" : "Single";
        }
    }
}

