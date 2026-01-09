
namespace DisplayMachineryDetail.Readers
{
    [BehaviourReader(typeof(ButtonBehaviour))]
    public class ButtonReader : IAttributeReader
    {
        private readonly ButtonBehaviour _behaviour;

        public ButtonReader(ButtonBehaviour behaviour)
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
            return _behaviour.TriggerOnExit ? "Double" : "Single";
        }
    }
}

