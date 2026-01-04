
namespace DisplayMachineryDetail.Readers;

[BehaviourReader(typeof(ButtonBehaviour))]
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
        return GetDoubleTriggerText();
    }

    private string GetDoubleTriggerText()
    {
        return behaviour.TriggerOnExit ? "Double" : "Single";
    }
}

