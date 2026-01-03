using UnityEngine;

namespace DisplayMachineryDetail.Readers;

public class DetectorReader : IAttributeReader
{
    private readonly DetectorBehaviour behaviour;

    public DetectorReader(DetectorBehaviour behaviour)
    {
        this.behaviour = behaviour;
    }

    public bool IsValid() => behaviour != null;

    public string GetDisplayText()
    {
        return $"Range: {behaviour.Range * Global.MetricMultiplier}\n{GetDoubleTriggerText()}";
    }

    private string GetDoubleTriggerText()
    {
        return behaviour.TriggerOnExit ? "Double" : "Single";
    }
}

