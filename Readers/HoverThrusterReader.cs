
namespace DisplayMachineryDetail.Readers
{
    [BehaviourReader(typeof(HoverThrusterBehaviour))]
    public class HoverThrusterReader : IAttributeReader
    {
        private readonly HoverThrusterBehaviour _behaviour;

        public HoverThrusterReader(HoverThrusterBehaviour behaviour)
        {
            _behaviour = behaviour;
        }

        public bool IsValid() => _behaviour != null;

        public string GetDisplayText()
        {
            return "Height:" + _behaviour.BaseHoverHeight * Global.MetricMultiplier;
        }
    }
}

