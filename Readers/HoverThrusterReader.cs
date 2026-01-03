using UnityEngine;

namespace DisplayMachineryDetail.Readers
{
    public class HoverThrusterReader : IAttributeReader
    {
        private readonly HoverThrusterBehaviour behaviour;

        public HoverThrusterReader(HoverThrusterBehaviour behaviour)
        {
            this.behaviour = behaviour;
        }

        public bool IsValid() => behaviour != null;

        public string GetDisplayText()
        {
            return "Height:" + behaviour.BaseHoverHeight * Global.MetricMultiplier;
        }
    }
}

