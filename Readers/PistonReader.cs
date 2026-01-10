using UnityEngine;

namespace DisplayMachineryAttributes.Readers
{
    [BehaviourReader(typeof(PistonBehaviour))]
    public class PistonReader : IAttributeReader
    {
        private readonly PistonBehaviour _behaviour;
        private readonly AudioBehaviour _audioBehaviour;

        public PistonReader(PistonBehaviour behaviour)
        {
            _behaviour = behaviour;
            _audioBehaviour = behaviour.gameObject.GetComponent<AudioBehaviour>();
        }

        public bool IsValid()
        {
            return _behaviour;
        }

        public string GetDisplayText()
        {
            return _audioBehaviour.enabled ? "" : "Muted";
        }
    }
}