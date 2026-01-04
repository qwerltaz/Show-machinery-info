
namespace DisplayMachineryDetail.Readers;

[BehaviourReader(typeof(CarBehaviour))]
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
        return $"{GetDirectionText()}\n{GetBrakeText()}";
    }

    private string GetDirectionText()
    {
        return behaviour.MotorSpeed > 0 ? "Reverse" : "";
    }

    private string GetBrakeText()
    {
        return behaviour.IsBrakeEngaged ? "" : "No brake";
    }
}

