
namespace DisplayMachineryDetail.Readers
{
    [BehaviourReader(typeof(WinchBehaviour))]
    public class WinchReader : IAttributeReader
    {
        private readonly WinchBehaviour _behaviour;

        public WinchReader(WinchBehaviour behaviour)
        {
            _behaviour = behaviour;
        }

        public bool IsValid() => _behaviour != null;

        public string GetDisplayText()
        {
            return $"in [{_behaviour.LowerLimit}, {_behaviour.UpperLimit}]";
        }
    }
}

