
namespace DisplayMachineryDetail.Readers
{
    [BehaviourReader(typeof(MagnetBehaviour))]
    public class MagnetReader : IAttributeReader
    {
        private readonly MagnetBehaviour behaviour;

        public MagnetReader(MagnetBehaviour behaviour)
        {
            this.behaviour = behaviour;
        }

        public bool IsValid() => behaviour != null;

        public string GetDisplayText()
        {
            return GetPolarityText();
        }

        private string GetPolarityText()
        {
            return behaviour.Reversed ? "Repell" : "Attract";
        }
    }
}

