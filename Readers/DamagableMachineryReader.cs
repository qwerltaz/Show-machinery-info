using UnityEngine;

namespace DisplayMachineryDetail.Readers;

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
        return $"{Utils.IsIndestructible(behaviour)}\n{Utils.IsDestroyed(behaviour)}";
    }
}

