namespace DisplayMachineryAttributes.Readers
{
    [BehaviourReader(typeof(LaserBehaviour))]
    public class LaserReader : IAttributeReader
    {
        private readonly LaserBehaviour _behaviour;

        public LaserReader(LaserBehaviour behaviour)
        {
            _behaviour = behaviour;
        }

        public bool IsValid()
        {
            return _behaviour != null;
        }

        public string GetDisplayText()
        {
            var c = _behaviour.UserSetColour;
            return $"RGB({c.r:F2}, {c.g:F2}, {c.b:F2})";
        }
    }
}
