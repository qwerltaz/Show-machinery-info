namespace DisplayMachineryAttributes.Readers
{
    [BehaviourReader(typeof(LEDBulbBehaviour))]
    public class LEDBulbReader : IAttributeReader
    {
        private readonly LEDBulbBehaviour _behaviour;

        public LEDBulbReader(LEDBulbBehaviour behaviour)
        {
            _behaviour = behaviour;
        }

        public bool IsValid()
        {
            return _behaviour != null;
        }

        public string GetDisplayText()
        {
            var c = _behaviour.Color;
            return $"rgb({c.r:F2}, {c.g:F2}, {c.b:F2})";
        }
    }
}
