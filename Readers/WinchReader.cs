using UnityEngine;

namespace DisplayMachineryDetail.Readers
{
    public class WinchReader : IAttributeReader
    {
        private readonly WinchBehaviour behaviour;

        public WinchReader(WinchBehaviour behaviour)
        {
            this.behaviour = behaviour;
        }

        public bool IsValid() => behaviour != null;

        public string GetDisplayText()
        {
            return $"in [{behaviour.LowerLimit}, {behaviour.UpperLimit}]";
        }
    }
}

