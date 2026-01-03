using UnityEngine;
using DisplayMachineryDetail.Core;
using DisplayMachineryDetail.Display;
using DisplayMachineryDetail.Readers;

namespace DisplayMachineryDetail;

public class Mod
{
    public static void Main()
    {
        ModInitializer.RegisterMod();
    }
}


public class ShowAttributes : MonoBehaviour
{
    private float timeUntilCheck = 0f;
    private const float UpdateInterval = 0.5f;

    private IAttributeReader machineryReader;
    private IAttributeReader damageReader;
    private AttributeDisplayController displayController;

    void Awake()
    {
        displayController = new AttributeDisplayController(transform);
        InitializeReaders();
    }

    private void InitializeReaders()
    {
        machineryReader = CreateMachineryReader();
        
        DamagableMachineryBehaviour damageableMachinery = GetComponent<DamagableMachineryBehaviour>();
        if (damageableMachinery != null)
        {
            damageReader = new DamagableMachineryReader(damageableMachinery);
        }
    }

    private IAttributeReader CreateMachineryReader()
    {
        BoatMotorBehaviour boatMotor = GetComponent<BoatMotorBehaviour>();
        if (boatMotor) return new BoatMotorReader(boatMotor);

        ButtonBehaviour button = GetComponent<ButtonBehaviour>();
        if (button) return new ButtonReader(button);

        LagboxBehaviour lagbox = GetComponent<LagboxBehaviour>();
        if (lagbox) return new LagboxReader(lagbox);

        KeyTriggerBehaviour keyTrigger = GetComponent<KeyTriggerBehaviour>();
        if (keyTrigger) return new KeyTriggerReader(keyTrigger);

        DetectorBehaviour detector = GetComponent<DetectorBehaviour>();
        if (detector) return new DetectorReader(detector);

        MagnetBehaviour magnet = GetComponent<MagnetBehaviour>();
        if (magnet) return new MagnetReader(magnet);

        LEDBulbBehaviour ledBulb = GetComponent<LEDBulbBehaviour>();
        if (ledBulb) return new LEDBulbReader(ledBulb);

        GateBehaviour gate = GetComponent<GateBehaviour>();
        if (gate) return new GateReader(gate);

        RotorBehaviour rotor = GetComponent<RotorBehaviour>();
        if (rotor) return new RotorReader(rotor);

        ResistorBehaviour resistor = GetComponent<ResistorBehaviour>();
        if (resistor) return new ResistorReader(resistor);

        MetronomeBehaviour metronome = GetComponent<MetronomeBehaviour>();
        if (metronome) return new MetronomeReader(metronome);

        CarBehaviour wheel = GetComponent<CarBehaviour>();
        if (wheel) return new CarReader(wheel);

        WinchBehaviour winch = GetComponentInChildren<WinchBehaviour>();
        if (winch) return new WinchReader(winch);

        LaserBehaviour laser = GetComponent<LaserBehaviour>();
        if (laser) return new LaserReader(laser);

        HoverThrusterBehaviour hoverThruster = GetComponent<HoverThrusterBehaviour>();
        if (hoverThruster) return new HoverThrusterReader(hoverThruster);

        return null;
    }

    void OnDestroy()
    {
        CleanupDisplay();
    }

    public void CleanupDisplay()
    {
        displayController?.Destroy();
    }

    void Update()
    {
        timeUntilCheck += Time.unscaledDeltaTime;
        if (timeUntilCheck > UpdateInterval)
        {
            timeUntilCheck = 0f;
            UpdateInfo();
        }

        bool shouldDisplay = !ModSettings.OnlyDetailView || Global.main.ShowLimbStatus;
        
        if (shouldDisplay && displayController.HasText())
        {
            displayController.UpdatePosition();
        }
    }

    void UpdateInfo()
    {
        bool shouldDisplay = !ModSettings.OnlyDetailView || Global.main.ShowLimbStatus;

        if (shouldDisplay)
        {
            displayController.SetVisible(true);

            string mainText = machineryReader?.IsValid() == true ? machineryReader.GetDisplayText() : "";
            string damageText = damageReader?.IsValid() == true ? damageReader.GetDisplayText() : "";

            displayController.UpdateText(mainText, damageText);
        }
        else
        {
            displayController.SetVisible(false);
        }
    }
}