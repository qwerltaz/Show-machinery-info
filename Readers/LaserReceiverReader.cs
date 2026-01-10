namespace DisplayMachineryAttributes.Readers
{
    namespace DisplayMachineryAttributes.Readers
    {
        [BehaviourReader(typeof(LaserReceiverBehaviour))]
        public class LaserReceiverReader : IAttributeReader
        {
            private readonly LaserReceiverBehaviour _behaviour;

            public LaserReceiverReader(LaserReceiverBehaviour behaviour)
            {
                _behaviour = behaviour;
            }

            public bool IsValid()
            {
                return _behaviour;
            }

            public string GetDisplayText()
            {
                return _behaviour.DoubleTrigger ? "Double" : "Single";
            }
        }
    }
}