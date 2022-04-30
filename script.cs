using UnityEngine;
using System;
using System.Collections.Generic;

// Show specific detail for machinery, right under the object.
// <summary>
// Spawn "Toggle detail view only" to toggle whether mod works only in detail view
// 1. On object spawned, search for the Components (Behaviours)
// class AttributeManifest:
// 2. Each frame, align the text objects to the object
// 3. Each second, update the text objects
// Destroy the text objects when the object is destroyed
// Make text objects invisible when detail view or onlyDetailVIew is off
// </summary>

namespace ShowMachineryInfo
{
    public class Mod
    {
        // TODO Machinery Adding procedure:
        // 1. Add new Getcomponent<>() to if statement in OnItemSpawned
        // 2. Add new behaviour at AttributeManifest
        // 3. numObjects++
        // 4. Add X = GetComponent<>(), and X to if statement in void Awake()
        // 5. Add case to UpdateInfo()

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
                    DescriptionOverride = "Toggle whether to show machinery info only in detail view",
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
                            ModAPI.Notify("show machinery info in detail mode only: on");
                        }
                        else
                        {
                            ModAPI.Notify("show machinery info in detail mode only: off");
                        }
                    }
                }
            );

            ModAPI.Register<AttributeManifest>();

            ModAPI.OnItemSpawned += (sender, obj) => {              
                // if (behaviours.Intersection(obj.Instance.GetComponents())
                if(obj.Instance.GetComponent<BoatMotorBehaviour>()
                || obj.Instance.GetComponent<ButtonBehaviour>() // Both big and small buttons
                || obj.Instance.GetComponent<LagboxBehaviour>()
                || obj.Instance.GetComponent<KeyTriggerBehaviour>()
                || obj.Instance.GetComponent<DetectorBehaviour>()
                || obj.Instance.GetComponent<MagnetBehaviour>()
                || obj.Instance.GetComponent<LEDBulbBehaviour>()
                || obj.Instance.GetComponent<GateBehaviour>())
                {
                    // Add AttributeManifest
                    obj.Instance.GetOrAddComponent<AttributeManifest>();
                }
            };

            // Delete text on item deleted
            ModAPI.OnItemRemoved += (sender, obj) => {
                if(obj.Instance.GetComponent<AttributeManifest>())
                {
                    AttributeManifest objInstance = obj.Instance.GetComponent<AttributeManifest>();
                    UnityEngine.Object.Destroy(objInstance.AttrObject);
                }
            };
        }
    }

    // Show the attributes of an object
    public class AttributeManifest : MonoBehaviour
    {
        // time until a check is made for changes in machinery attributes
        float timeUntilCheck = 0f;

        // number of objects to seek
        static int numObjects = 8;

        // GameObject index:
        // boatMotor, button, lagbox, keyTrigger, detector, magnet, ledBulb, gate
        int objIndex = -1;

        // LEDBulb color
        int c;

        // machinery behaviours
        BoatMotorBehaviour boatMotor; // .Force -   -25 = reverse, 25 = forward
        ButtonBehaviour button; // .TriggerOnExit - isdouble
        LagboxBehaviour lagbox; // .DelayModifier - delay
        KeyTriggerBehaviour keyTrigger; //.DoubleTrigger
        DetectorBehaviour detector; // .TriggerOnExit, .Range
        MagnetBehaviour magnet; // .Reversed
        LEDBulbBehaviour ledBulb; // .Color
        GateBehaviour gate; // .ThresholdPercentage .MaxPower .DoubleTrigger

        // machinery position, rotation, scale etc.
        Transform pos;


        // array for text objects for each any GameObject
        public GameObject AttrObject;
        

        // On borne
        // TODO FIX when two objects are spawned very quickly, the second one does not have the text?? 
        // TODO check if each component, make one behaviour for non-null component, work only with that behaviour 
        void Awake()
        {
            pos = GetComponent<Transform>();

            // get the behaviour
            boatMotor = GetComponent<BoatMotorBehaviour>();
            button = GetComponent<ButtonBehaviour>();
            lagbox = GetComponent<LagboxBehaviour>();
            keyTrigger = GetComponent<KeyTriggerBehaviour>();
            detector = GetComponent<DetectorBehaviour>();
            magnet = GetComponent<MagnetBehaviour>();
            ledBulb = GetComponent<LEDBulbBehaviour>();
            gate = GetComponent<GateBehaviour>();

            if (boatMotor) 
            { 
                objIndex = 0; 
                // funkcja = Utils.IsForward(boatMotor); 
            } 
            else if (button) { objIndex = 1; }
            else if (lagbox) { objIndex = 2; }
            else if (keyTrigger) { objIndex = 3; }
            else if (detector) { objIndex = 4; }
            else if (magnet) { objIndex = 5; }
            else if (ledBulb) { objIndex = 6; }
            else if (gate) { objIndex = 7; }

            // Initial empty text object setup
            AttrObject = new GameObject();
            AttrObject.transform.localScale = new Vector3(0.03f, 0.03f, 1f);
            AttrObject.AddComponent<TextMesh>();
            AttrObject.GetComponent<TextMesh>().fontSize = 32;
            AttrObject.GetComponent<TextMesh>().anchor = TextAnchor.MiddleCenter;
            AttrObject.layer = 16;
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
                if (AttrObject != null && AttrObject.GetComponent<TextMesh>().text != "")
                {
                    float height = pos.transform.localScale[1];
                    AttrObject.transform.position = pos.transform.position + new Vector3(0f, -0.03f - 0.2f * height, 0f);
                    AttrObject.transform.localScale = new Vector3(1f, 1f, 1f) *Mathf.Sqrt(height) / 50;
                    AttrObject.transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
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
                }
            
                // Further text object setup for each attribute
                switch (objIndex){
                    case 0: // BoatMotor
                        AttrObject.GetComponent<TextMesh>().text = Utils.IsForward(boatMotor);
                        // AttrObject.GetComponent<TextMesh>().text = funkcja;
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

                    case 4: // detector, 1.213636
                        AttrObject.GetComponent<TextMesh>().text = "Range: " + detector.GetComponent<DetectorBehaviour>().Range / 1.213636f
                                                                + "\n" + Utils.IsDoubleDetector(detector);
                        break;
                        
                    case 5: // magnet
                        AttrObject.GetComponent<TextMesh>().text = Utils.IsReversed(magnet);
                        break;
                    
                    case 6: // ledBulb
                        // color
                        Color c = ledBulb.GetComponent<LEDBulbBehaviour>().Color;
                        // rgb(r, g, b)
                        AttrObject.GetComponent<TextMesh>().text = "rgb(" 
                                                                    + c.r.ToString("F2") + ", " 
                                                                    + c.g.ToString("F2") + ", " 
                                                                    + c.b.ToString("F2") + ")";
                        break;
                    
                    case 7: // gate
                        AttrObject.GetComponent<TextMesh>().text = "Threshold%: " + gate.GetComponent<GateBehaviour>().ThresholdPercentage
                                                                + "\nMaxPower: " + gate.GetComponent<GateBehaviour>().MaxPower
                                                                + "\n" + Utils.IsDoubleGate(gate);
                        break;
                }
            }
            else if (AttrObject != null)
            {
                UnityEngine.Object.Destroy(AttrObject);
            }
        }
    }
}