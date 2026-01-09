using UnityEngine;

namespace DisplayMachineryDetail.Readers
{
    [BehaviourReader(typeof(LEDBulbBehaviour))]
    public class LEDBulbReader : IAttributeReader
    {
        private readonly LEDBulbBehaviour _behaviour;

        public LEDBulbReader(LEDBulbBehaviour behaviour)
        {
            _behaviour = behaviour;
        }

        public bool IsValid() => _behaviour != null;

        public string GetDisplayText()
        {
            Color c = _behaviour.Color;
            return $"rgb({c.r:F2}, {c.g:F2}, {c.b:F2})";
        }
    }
}