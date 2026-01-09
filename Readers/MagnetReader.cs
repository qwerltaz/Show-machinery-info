
namespace DisplayMachineryDetail.Readers
{
    [BehaviourReader(typeof(MagnetBehaviour))]
    public class MagnetReader : IAttributeReader
    {
        private readonly MagnetBehaviour _behaviour;

        public MagnetReader(MagnetBehaviour behaviour)
        {
            _behaviour = behaviour;
        }

        public bool IsValid() => _behaviour != null;

        public string GetDisplayText()
        {
            return GetPolarityText();
        }

        private string GetPolarityText()
        {
            return _behaviour.Reversed ? "Repell" : "Attract";
        }
    }
}

