namespace DisplayMachineryAttributes.Readers
{
    [BehaviourReader(typeof(BoatMotorBehaviour))]
    public class BoatMotorReader : IAttributeReader
    {
        private readonly BoatMotorBehaviour _behaviour;

        public BoatMotorReader(BoatMotorBehaviour behaviour)
        {
            _behaviour = behaviour;
        }

        public bool IsValid()
        {
            return _behaviour != null;
        }

        public string GetDisplayText()
        {
            return GetDirectionText();
        }

        private string GetDirectionText()
        {
            return _behaviour.Force < 0 ? "Reverse" : "";
        }
    }
}
