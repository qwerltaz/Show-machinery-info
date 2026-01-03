using UnityEngine;

namespace DisplayMachineryDetail.Readers;

public class LagboxReader : IAttributeReader
{
    private readonly LagboxBehaviour behaviour;

    public LagboxReader(LagboxBehaviour behaviour)
    {
        this.behaviour = behaviour;
    }

    public bool IsValid() => behaviour != null;

    public string GetDisplayText()
    {
        return "Delay: " + behaviour.DelayModifier;
    }
}

