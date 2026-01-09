
namespace DisplayMachineryDetail.Readers
{
    [BehaviourReader(typeof(KeyTriggerBehaviour))]
    public class KeyTriggerReader : IAttributeReader
    {
        private readonly KeyTriggerBehaviour _behaviour;

        public KeyTriggerReader(KeyTriggerBehaviour behaviour)
        {
            _behaviour = behaviour;
        }

        public bool IsValid() => _behaviour != null;

        public string GetDisplayText()
        {
            return GetDoubleTriggerText();
        }

        private string GetDoubleTriggerText()
        {
            return _behaviour.DoubleTrigger ? "Double" : "Single";
        }
    }
}

