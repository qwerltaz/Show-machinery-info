
namespace DisplayMachineryDetail.Readers;

[BehaviourReader(typeof(DamagableMachineryBehaviour), isDamageReader: true)]
public class DamagableMachineryReader : IAttributeReader
{
    private readonly DamagableMachineryBehaviour behaviour;

    public DamagableMachineryReader(DamagableMachineryBehaviour behaviour)
    {
        this.behaviour = behaviour;
    }

    public bool IsValid() => behaviour != null;

    public string GetDisplayText()
    {
        return $"{GetIndestructibleText()}\n{GetDestroyedText()}";
    }

    private string GetIndestructibleText()
    {
        return behaviour.Indestructible ? "∞" : "";
    }

    private string GetDestroyedText()
    {
        return behaviour.Destroyed ? "Broken" : "";
    }
}

