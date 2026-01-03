using UnityEngine;

namespace DisplayMachineryDetail.Readers;

public class BoatMotorReader : IAttributeReader
{
    private readonly BoatMotorBehaviour behaviour;

    public BoatMotorReader(BoatMotorBehaviour behaviour)
    {
        this.behaviour = behaviour;
    }

    public bool IsValid() => behaviour != null;

    public string GetDisplayText()
    {
        return GetDirectionText();
    }

    private string GetDirectionText()
    {
        return behaviour.Force < 0 ? "Reverse" : "";
    }
}

