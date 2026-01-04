using UnityEngine;

namespace DisplayMachineryDetail.Readers
{
    [BehaviourReader(typeof(LEDBulbBehaviour))]
    public class LEDBulbReader : IAttributeReader
    {
        private readonly LEDBulbBehaviour behaviour;

        public LEDBulbReader(LEDBulbBehaviour behaviour)
        {
            this.behaviour = behaviour;
        }

        public bool IsValid() => behaviour != null;

        public string GetDisplayText()
        {
            Color c = behaviour.Color;
            return $"rgb({c.r:F2}, {c.g:F2}, {c.b:F2})";
        }
    }
}