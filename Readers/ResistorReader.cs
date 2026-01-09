namespace DisplayMachineryAttributes.Readers
{
    [BehaviourReader(typeof(ResistorBehaviour))]
    public class ResistorReader : IAttributeReader
    {
        private readonly ResistorBehaviour _behaviour;

        public ResistorReader(ResistorBehaviour behaviour)
        {
            _behaviour = behaviour;
        }

        public bool IsValid()
        {
            return _behaviour != null;
        }

        public string GetDisplayText()
        {
            return $"Power: {_behaviour.ResistorPower:F2}";
        }
    }
}
