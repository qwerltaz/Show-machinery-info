using UnityEngine;
namespace DisplayMachineryDetail.Readers;

[BehaviourReader(typeof(LaserBehaviour))]
public class LaserReader : IAttributeReader
{
    private readonly LaserBehaviour behaviour;

    public LaserReader(LaserBehaviour behaviour)
    {
        this.behaviour = behaviour;
    }

    public bool IsValid() => behaviour != null;

    public string GetDisplayText()
    {
        Color c = behaviour.UserSetColour;
        return $"rgb({c.r:F2}, {c.g:F2}, {c.b:F2})";
    }
}

