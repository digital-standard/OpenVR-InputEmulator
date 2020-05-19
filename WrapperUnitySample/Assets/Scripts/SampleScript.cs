using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SampleScript : MonoBehaviour
{
    public Dropdown Controllers;
    public InputField PosX;
    public InputField PosY;
    public InputField PosZ;
    public InputField Yaw;
    public InputField Pitch;
    public InputField Roll;

    private VRInputEmulatorWrapper vrInputEmulator = null;
    private List<ViveController> viveControllers = new List<ViveController>();

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Return))
        {
            viveControllers[1].ButtonEvent("press", VRInputEmulatorWrapper.EVRButtonId.k_EButton_System, 100);
        }
        else if (Input.GetKey(KeyCode.Escape))
        {
            viveControllers[0].ButtonEvent("press", VRInputEmulatorWrapper.EVRButtonId.k_EButton_ApplicationMenu, 50);
        }
        else if (Input.GetKey(KeyCode.Space))
        {
            if (!triggerDown)
            {
                triggerDown = true;
                viveControllers[1].AxisEvent(1, "1", "0");
            }
            else
            {
                triggerDown = false;
                viveControllers[1].AxisEvent(1, "0", "0");
            }
        }
    }

    private void CreateControllers()
    {
    }

    public void onAdd3Ctrl()
    {
        if (vrInputEmulator == null)
        {
            vrInputEmulator = new VRInputEmulatorWrapper();
            var res = vrInputEmulator.Connect();
            if (vrInputEmulator.Connect() > 0)
            {
                AddTrackedController("OVRIE_Tracker_1");
                AddTrackedController("OVRIE_Tracker_2");
                AddTrackedController("OVRIE_Tracker_3");

                Controllers.value = 0;
                viveControllers[0].SetDevicePosition(0f, 0.8f, 0f);
                viveControllers[1].SetDevicePosition(-0.18f, 0f, 0f);
                viveControllers[2].SetDevicePosition(0.18f, 0f, 0f);
            }
        }

    }

    public void onAdd5Ctrl()
    {
        if (vrInputEmulator == null)
        {
            vrInputEmulator = new VRInputEmulatorWrapper();
            var res = vrInputEmulator.Connect();
            if (vrInputEmulator.Connect() > 0)
            {
                AddTrackedController("OVRIE_Controller_L");
                AddTrackedController("OVRIE_Controller_R");

                AddTrackedController("OVRIE_Tracker_1");
                AddTrackedController("OVRIE_Tracker_2");
                AddTrackedController("OVRIE_Tracker_3");

                Controllers.value = 0;
                viveControllers[0].SetDevicePosition(-0.8f, 1.3f, 0f);
                viveControllers[0].SetDeviceRotation(90f, 0f, 0f);
                viveControllers[1].SetDevicePosition(0.8f, 1.3f, 0f);
                viveControllers[1].SetDeviceRotation(-90f, 0f, 0f);

                viveControllers[2].SetDevicePosition(0f, 0.8f, 0f);
                viveControllers[3].SetDevicePosition(-0.18f, 0f, 0f);
                viveControllers[4].SetDevicePosition(0.18f, 0f, 0f);
            }
        }
    }

    private ViveController AddTrackedController(string serial)
    {
        var ctrl = new ViveController(vrInputEmulator);
        var idL = ctrl.AddTrackedController(serial);
        if (idL < 0)
        {
            idL = ctrl.GetDeviceID();
            ctrl.GetOpenVRDeviceID();
        }
        else
        {
            ctrl.SetDeviceProperty();
        }

        viveControllers.Add(ctrl);
        Controllers.options.Add(new Dropdown.OptionData(serial));

        return ctrl;
    }

    public void onMove()
    {
        var x = 0f;
        if (float.TryParse(PosX.text, out x))
        {
            Debug.Log("error PosX");
        }
        var y = 0f;
        if (float.TryParse(PosY.text, out y))
        {
            Debug.Log("error PosY");
        }
        var z = 0f;
        if (float.TryParse(PosZ.text, out z))
        {
            Debug.Log("error PosZ");
        }

        viveControllers[Controllers.value].SetDevicePosition(x, y, z);
    }

    public void onRotate()
    {
        var yaw = 0f;
        if (float.TryParse(Yaw.text, out yaw))
        {
            Debug.Log("error Yaw");
        }
        var pitch = 0f;
        if (float.TryParse(Pitch.text, out pitch))
        {
            Debug.Log("error Pitch");
        }
        var roll = 0f;
        if (float.TryParse(Roll.text, out roll))
        {
            Debug.Log("error Roll");
        }

        viveControllers[Controllers.value].SetDeviceRotation(yaw, pitch, roll);
    }

    bool systemDown = false;
    bool triggerDown = false;

    public void onSystemDown()
    {
        if (!systemDown)
        {
            systemDown = true;
            viveControllers[Controllers.value].ButtonEvent("press", VRInputEmulatorWrapper.EVRButtonId.k_EButton_System, 50);
        }
    }
    public void onSystemUp()
    {
        triggerDown = false;
        //viveControllers[Controllers.value].ButtonEvent("unpress", VRInputEmulatorWrapper.EVRButtonId.k_EButton_System, 50);
    }

    public void onTriggerDown()
    {
        //if (!triggerDown)
        {
            triggerDown = true;
            viveControllers[Controllers.value].AxisEvent(1, "1", "0");
        }
    }
    public void onTriggerUp()
    {
        triggerDown = false;
        viveControllers[Controllers.value].AxisEvent(1, "0", "0");
    }

    private void OnDestroy()
    {
        viveControllers.ForEach(v => v.Disconnect());
        viveControllers.Clear();
    }
}
