using UnityEngine;

namespace DisplayMachineryDetail.Readers
{
    public class GateReader : IAttributeReader
    {
        private readonly GateBehaviour behaviour;

        public GateReader(GateBehaviour behaviour)
        {
            this.behaviour = behaviour;
        }

        public bool IsValid() => behaviour != null;

        public string GetDisplayText()
        {
            return $"Threshold: {behaviour.ThresholdPercentage}\nMaxPower: {behaviour.MaxPower}\n{Utils.IsDoubleGate(behaviour)}";
        }
    }
}

