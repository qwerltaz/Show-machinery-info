using UnityEngine;

namespace DisplayMachineryDetail.Readers
{
    public class CarReader : IAttributeReader
    {
        private readonly CarBehaviour behaviour;

        public CarReader(CarBehaviour behaviour)
        {
            this.behaviour = behaviour;
        }

        public bool IsValid() => behaviour != null;

        public string GetDisplayText()
        {
            return $"{Utils.IsReversedWheel(behaviour)}\n{Utils.IsBrakeEngaged(behaviour)}";
        }
    }
}

