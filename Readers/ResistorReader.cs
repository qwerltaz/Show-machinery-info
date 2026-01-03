using UnityEngine;

namespace DisplayMachineryDetail.Readers
{
    public class ResistorReader : IAttributeReader
    {
        private readonly ResistorBehaviour behaviour;

        public ResistorReader(ResistorBehaviour behaviour)
        {
            this.behaviour = behaviour;
        }

        public bool IsValid() => behaviour != null;

        public string GetDisplayText()
        {
            return $"Power: {behaviour.ResistorPower:F2}";
        }
    }
}

