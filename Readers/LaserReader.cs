using UnityEngine;

namespace DisplayMachineryDetail.Readers
{
    [BehaviourReader(typeof(LaserBehaviour))]
    public class LaserReader : IAttributeReader
    {
        private readonly LaserBehaviour _behaviour;

        public LaserReader(LaserBehaviour behaviour)
        {
            _behaviour = behaviour;
        }

        public bool IsValid() => _behaviour != null;

        public string GetDisplayText()
        {
            Color c = _behaviour.UserSetColour;
            return $"rgb({c.r:F2}, {c.g:F2}, {c.b:F2})";
        }
    }
}

