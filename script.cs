using UnityEngine;
using System;
using System.Collections.Generic;

// Show specific detail for machinery, right under the object.
// <summary>
// Spawn "Toggle detail view only" to toggle whether mod works only in detail view
// 1. On object spawned, search for the Components (Behaviours)
// class ShowAttributes:
// 2. Each frame, align the text objects to the object
// 3. Each second, update the text objects
// Destroy the text objects when the object is destroyed
// Make text objects invisible when detail view or onlyDetailVIew is off
// </summary>

namespace DisplayMachineryDetail
{
    public class Mod
    {
        // Machinery Adding procedure:
        // 1. Add new Getcomponent<>() to if statement in OnItemSpawned
        // 2. Add new behaviour at ShowAttributes
        // 3. Add X = GetComponent<>(), and X to if statement in void Awake()
        // 4. Add case to UpdateInfo()

        // Whether mod works only in detail view
        public static bool onlyDetailView = true;

        public static void Main()
        {
            ModAPI.Register(
                new Modification()
                {
                    // item to derive from (whatever)
                    OriginalItem = ModAPI.FindSpawnable("Brick"),
                    // new item name with a suffix to assure it is globally unique
                    NameOverride = "Toggle detail view only",
                    // order by this name
                    NameToOrderByOverride = "b",
                    // new item description
                    DescriptionOverride = "Toggle whether to show machinery info only in detail view (default), or always.",
                    // item category
                    CategoryOverride = ModAPI.FindCategory("Entities"),
                    // new item thumbnail (relative path)
                    ThumbnailOverride = ModAPI.LoadSprite("thumb_ondetailview.png"),
                    // all code in the AfterSpawn delegate will be executed when the item is spawned
                    AfterSpawn = (Instance) =>
                    {
                        // Destroy item and toggle onlyDetailView
                        Instance.GetComponent<PhysicalBehaviour>().Disintegrate();
                        UnityEngine.Object.Destroy(Instance, 3f);
                        onlyDetailView = !onlyDetailView;
                        if(onlyDetailView)
                        {
                            ModAPI.Notify("Display machinery attributes in detail mode only: on");
                        }
                        else
                        {
                            ModAPI.Notify("Display machinery attributes in detail mode only: off");
                        }
                    }
                }
            );

            ModAPI.Register<ShowAttributes>();

            ModAPI.OnItemSpawned += (sender, obj) => {              
                if(obj.Instance.GetComponent<BoatMotorBehaviour>()
                || obj.Instance.GetComponent<ButtonBehaviour>() // Both big and small buttons
                || obj.Instance.GetComponent<LagboxBehaviour>()
                || obj.Instance.GetComponent<KeyTriggerBehaviour>()
                || obj.Instance.GetComponent<DetectorBehaviour>()
                || obj.Instance.GetComponent<MagnetBehaviour>()
                || obj.Instance.GetComponent<LEDBulbBehaviour>()
                || obj.Instance.GetComponent<GateBehaviour>()
                || obj.Instance.GetComponent<RotorBehaviour>()
                || obj.Instance.GetComponent<ResistorBehaviour>()
                || obj.Instance.GetComponent<MetronomeBehaviour>()
                || obj.Instance.GetComponent<CarBehaviour>()
                || obj.Instance.GetComponent<LaserBehaviour>()
                || obj.Instance.GetComponent<HoverThrusterBehaviour>()
                || obj.Instance.GetComponent<DamagableMachineryBehaviour>())
                {
                    // Add ShowAttributes
                    obj.Instance.GetOrAddComponent<ShowAttributes>();
                }
                else if (obj.Instance.GetComponentInChildren<WinchBehaviour>())
                {
                    obj.Instance.GetComponentInChildren<WinchBehaviour>().gameObject.GetOrAddComponent<ShowAttributes>();
                }
            };

            // Delete text on item deleted
            ModAPI.OnItemRemoved += (sender, obj) => {
                if(obj.Instance.GetComponent<ShowAttributes>())
                {
                    ShowAttributes objInstance = obj.Instance.GetComponent<ShowAttributes>();
                    UnityEngine.Object.Destroy(objInstance.AttrObject);
                    UnityEngine.Object.Destroy(objInstance.damageabilityObject);
                }
            };
        }
    }

    // Show the attributes of an object
    public class ShowAttributes : MonoBehaviour
    {
        // time until a check is made for changes in machinery attributes
        float timeUntilCheck = 0f;

        // GameObject index:
        // boatMotor, button, lagbox, keyTrigger, detector, magnet, ledBulb, gate, rotor, resistor, metronome, wheel
        int objIndex = -1;

        // machinery behaviours
        BoatMotorBehaviour boatMotor; // .Force -   -25 = reverse, 25 = forward
        ButtonBehaviour button; // .TriggerOnExit - isdouble
        LagboxBehaviour lagbox; // .DelayModifier - delay
        KeyTriggerBehaviour keyTrigger; //.DoubleTrigger
        DetectorBehaviour detector; // .TriggerOnExit, .Range: game units
        MagnetBehaviour magnet; // .Reversed
        LEDBulbBehaviour ledBulb; // .Color r,g,b
        GateBehaviour gate; // .ThresholdPercentage .MaxPower .DoubleTrigger
        RotorBehaviour rotor; // .Speed in [-8k, 8k]
        ResistorBehaviour resistor; // .ResistorPower  in [0, 1]
        MetronomeBehaviour metronome; // .TempoModifier (Hz)
        CarBehaviour wheel; // .MotorSpeed -500 = forward, 500 = reverse
        WinchBehaviour winch; // .LowerLimit, .UpperLimit (m)
        LaserBehaviour laser; // .UserSetColour r,g,b
        HoverThrusterBehaviour hoverThruster; // .BaseHoverHeight: game units
        // General behaviour of machinery: damageability.
        DamagableMachineryBehaviour damageableMachinery; // .Destroyed .Indestructible

        // machinery position, rotation, scale etc.
        Transform pos;


        // text objects for each any GameObject
        public GameObject AttrObject;
        public GameObject damageabilityObject;

        // On borne
        void Awake()
        {
            // to transform position, scale etc. of object
            pos = GetComponent<Transform>();

            // get the behaviour.
            boatMotor = GetComponent<BoatMotorBehaviour>();
            button = GetComponent<ButtonBehaviour>();
            lagbox = GetComponent<LagboxBehaviour>();
            keyTrigger = GetComponent<KeyTriggerBehaviour>();
            detector = GetComponent<DetectorBehaviour>();
            magnet = GetComponent<MagnetBehaviour>();
            ledBulb = GetComponent<LEDBulbBehaviour>();
            gate = GetComponent<GateBehaviour>();
            rotor = GetComponent<RotorBehaviour>();
            resistor = GetComponent<ResistorBehaviour>();
            metronome = GetComponent<MetronomeBehaviour>();
            wheel = GetComponent<CarBehaviour>();
            winch = GetComponentInChildren<WinchBehaviour>();
            laser = GetComponent<LaserBehaviour>();
            hoverThruster = GetComponent<HoverThrusterBehaviour>();

            damageableMachinery = GetComponent<DamagableMachineryBehaviour>();

            if (boatMotor) {objIndex = 0;} 
            else if (button) { objIndex = 1; }
            else if (lagbox) { objIndex = 2; }
            else if (keyTrigger) { objIndex = 3; }
            else if (detector) { objIndex = 4; }
            else if (magnet) { objIndex = 5; }
            else if (ledBulb) { objIndex = 6; }
            else if (gate) { objIndex = 7; }
            else if (rotor) { objIndex = 8; }
            else if (resistor) { objIndex = 9; }
            else if (metronome) { objIndex = 10; }
            else if (wheel) { objIndex = 11; }
            else if (winch){objIndex = 12; }
            else if (laser) { objIndex = 13; }
            else if (hoverThruster) { objIndex = 14; }

            // Initial empty text object setup
            AttrObject = new GameObject();
            AttrObject.transform.localScale = new Vector3(0.03f, 0.03f, 1f);
            AttrObject.AddComponent<TextMesh>();
            AttrObject.GetComponent<TextMesh>().fontSize = 32;
            AttrObject.GetComponent<TextMesh>().anchor = TextAnchor.MiddleCenter;
            AttrObject.layer = 16;

            damageabilityObject = new GameObject();
            damageabilityObject.transform.localScale = new Vector3(0.03f, 0.03f, 1f);
            damageabilityObject.AddComponent<TextMesh>();
            damageabilityObject.GetComponent<TextMesh>().fontSize = 32;
            damageabilityObject.GetComponent<TextMesh>().anchor = TextAnchor.MiddleCenter;
            damageabilityObject.layer = 16;
            damageabilityObject.GetComponent<TextMesh>().color = Color.yellow;
        }

        void OnDestroy()
        {
            // delete text object
            UnityEngine.Object.Destroy(AttrObject);
        }
        
        
        // Update each frame
        void Update()
        {
            // update attribute values, less frequent than Update()
            timeUntilCheck += Time.unscaledDeltaTime;
            if (timeUntilCheck > .5f)
            {
                timeUntilCheck = 0f;
                UpdateInfo();
            }

            if (!Mod.onlyDetailView || Mod.onlyDetailView && Global.main.ShowLimbStatus)
            {
                // update attribute position
                if (AttrObject != null && AttrObject.GetComponent<TextMesh>().text != ""
                    || damageabilityObject != null && damageabilityObject.GetComponent<TextMesh>().text != "")
                {
                    float height = pos.transform.localScale[1];
                    float width = pos.transform.localScale[0];

                    AttrObject.transform.position = pos.transform.position + new Vector3(0f, -0.03f - 0.2f * height, 0f);
                    AttrObject.transform.localScale = new Vector3(1f, 1f, 1f) *Mathf.Sqrt(height) / 50;
                    AttrObject.transform.rotation = new Quaternion(0f, 0f, 0f, 0f);

                    damageabilityObject.transform.position = pos.transform.position + new Vector3(0.03f + 0.2f * width ,0f , 0f);
                    damageabilityObject.transform.localScale = new Vector3(1f, 1f, 1f) *Mathf.Sqrt(width) / 50;
                    damageabilityObject.transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
                }
            }
        }


        // Occasional update of the attributes
        void UpdateInfo()
        {

            // If detail view is on
            if (!Mod.onlyDetailView || Mod.onlyDetailView && Global.main.ShowLimbStatus)
            {
                // Create text anew if it does not exist 
                if (AttrObject == null)
                {
                AttrObject = new GameObject();
                AttrObject.transform.localScale = new Vector3(0.03f, 0.03f, 1f);
                AttrObject.AddComponent<TextMesh>();
                AttrObject.GetComponent<TextMesh>().fontSize = 32;
                AttrObject.GetComponent<TextMesh>().anchor = TextAnchor.MiddleCenter;
                AttrObject.layer = 16;

                damageabilityObject = new GameObject();
                damageabilityObject.transform.localScale = new Vector3(0.06f, 0.06f, 1f);
                damageabilityObject.AddComponent<TextMesh>();
                damageabilityObject.GetComponent<TextMesh>().fontSize = 32;
                damageabilityObject.GetComponent<TextMesh>().anchor = TextAnchor.MiddleCenter;
                damageabilityObject.GetComponent<TextMesh>().color = Color.yellow;
                damageabilityObject.layer = 16;
                }

                // color for some behaviours
                Color c;
            
                // Further text object setup for each attribute
                switch (objIndex){
                    case 0: // BoatMotor
                        AttrObject.GetComponent<TextMesh>().text = Utils.IsForward(boatMotor);
                        break;
                    
                    case 1: // button
                        AttrObject.GetComponent<TextMesh>().text = Utils.IsDoubleButton(button);
                        break;
                    
                    case 2: // lagbox
                        AttrObject.GetComponent<TextMesh>().text = "Delay: " + lagbox.GetComponent<LagboxBehaviour>().DelayModifier;
                        break;
                    
                    case 3: // keyTrigger
                        AttrObject.GetComponent<TextMesh>().text = Utils.IsDoubleKey(keyTrigger);
                        break;

                    case 4: // detector, 1.213636f
                        AttrObject.GetComponent<TextMesh>().text = $"Range: {detector.GetComponent<DetectorBehaviour>().Range * Global.MetricMultiplier}"
                                                                + "\n" + Utils.IsDoubleDetector(detector);
                        break;
                        
                    case 5: // magnet
                        AttrObject.GetComponent<TextMesh>().text = Utils.IsReversed(magnet);
                        break;
                    
                    case 6: // ledBulb
                        // color
                        c = ledBulb.GetComponent<LEDBulbBehaviour>().Color;
                        // rgb(r, g, b)
                        AttrObject.GetComponent<TextMesh>().text = "rgb(" 
                                                                    + c.r.ToString("F2") + ", " 
                                                                    + c.g.ToString("F2") + ", " 
                                                                    + c.b.ToString("F2") + ")";
                        break;
                    
                    case 7: // gate
                        AttrObject.GetComponent<TextMesh>().text = "Threshold: " + gate.GetComponent<GateBehaviour>().ThresholdPercentage
                                                                + "\nMaxPower: " + gate.GetComponent<GateBehaviour>().MaxPower
                                                                + "\n" + Utils.IsDoubleGate(gate);
                        break;
                    
                    case 8: // rotor
                        AttrObject.GetComponent<TextMesh>().text = "Speed: " + rotor.GetComponent<RotorBehaviour>().Speed;
                        break;
                    
                    case 9: // resistor
                        AttrObject.GetComponent<TextMesh>().text = "Power: " + resistor.GetComponent<ResistorBehaviour>().ResistorPower.ToString("F2");
                        break;

                    case 10: // metronome
                        AttrObject.GetComponent<TextMesh>().text = "Hz: " + metronome.GetComponent<MetronomeBehaviour>().TempoModifier;
                        break;

                    case 11: // wheel
                        AttrObject.GetComponent<TextMesh>().text = Utils.IsReversedWheel(wheel)
                                                                + "\n" + Utils.IsBrakeEngaged(wheel);
                        break;

                    case 12: // winch
                        AttrObject.GetComponent<TextMesh>().text = $"in [{winch.GetComponentInChildren<WinchBehaviour>().LowerLimit}, {winch.GetComponentInChildren<WinchBehaviour>().UpperLimit}]";
                        break;

                    case 13: // laser
                        c = laser.GetComponent<LaserBehaviour>().UserSetColour;
                        AttrObject.GetComponent<TextMesh>().text = "rgb(" 
                                                                    + c.r.ToString("F2") + ", " 
                                                                    + c.g.ToString("F2") + ", " 
                                                                    + c.b.ToString("F2") + ")";
                        break;

                    case 14: // hoverThruster
                        AttrObject.GetComponent<TextMesh>().text = "Height:" + hoverThruster.GetComponent<HoverThrusterBehaviour>().BaseHoverHeight * Global.MetricMultiplier;
                        break;
                }

                if (GetComponent<DamagableMachineryBehaviour>())
                {
                    damageabilityObject.GetComponent<TextMesh>().text = Utils.IsIndestructible(damageableMachinery)
                                                                        + "\n" + Utils.IsDestroyed(damageableMachinery);
                }
            }
            else if (AttrObject != null)
            {
                UnityEngine.Object.Destroy(AttrObject);
                UnityEngine.Object.Destroy(damageabilityObject);
            }   
        }
    }
}