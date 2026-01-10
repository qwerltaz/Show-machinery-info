namespace DisplayMachineryAttributes.Readers
{
    [BehaviourReader(typeof(AdhesiveCouplerBehaviour))]
    public class AdhesiveCouplerReader : IAttributeReader
    {
        private readonly AdhesiveCouplerBehaviour _behaviour;

        public AdhesiveCouplerReader(AdhesiveCouplerBehaviour behaviour)
        {
            _behaviour = behaviour;
        }

        public bool IsValid()
        {
            return _behaviour != null;
        }

        public string GetDisplayText()
        {
            return $"Range: {_behaviour.StickDistance}";
        }
    }
}