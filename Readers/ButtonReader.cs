using UnityEngine;

namespace DisplayMachineryDetail.Readers
{
    public class ButtonReader : IAttributeReader
    {
        private readonly ButtonBehaviour behaviour;

        public ButtonReader(ButtonBehaviour behaviour)
        {
            this.behaviour = behaviour;
        }

        public bool IsValid() => behaviour != null;

        public string GetDisplayText()
        {
            return Utils.IsDoubleButton(behaviour);
        }
    }
}

