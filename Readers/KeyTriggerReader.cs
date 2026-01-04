
namespace DisplayMachineryDetail.Readers;

[BehaviourReader(typeof(KeyTriggerBehaviour))]
public class KeyTriggerReader : IAttributeReader
{
    private readonly KeyTriggerBehaviour behaviour;

    public KeyTriggerReader(KeyTriggerBehaviour behaviour)
    {
        this.behaviour = behaviour;
    }

    public bool IsValid() => behaviour != null;

    public string GetDisplayText()
    {
        return GetDoubleTriggerText();
    }

    private string GetDoubleTriggerText()
    {
        return behaviour.DoubleTrigger ? "Double" : "Single";
    }
}

