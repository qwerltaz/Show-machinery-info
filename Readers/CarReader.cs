namespace DisplayMachineryAttributes.Readers
{
    [BehaviourReader(typeof(CarBehaviour))]
    public class CarReader : IAttributeReader
    {
        private readonly CarBehaviour _behaviour;

        public CarReader(CarBehaviour behaviour)
        {
            _behaviour = behaviour;
        }

        public bool IsValid()
        {
            return _behaviour != null;
        }

        public string GetDisplayText()
        {
            return $"{GetDirectionText()}\n{GetBrakeText()}";
        }

        private string GetDirectionText()
        {
            return _behaviour.MotorSpeed > 0 ? "Reverse" : "";
        }

        private string GetBrakeText()
        {
            return _behaviour.IsBrakeEngaged ? "" : "No brake";
        }
    }
}
