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
    private List<VirtualController> virtualController = new List<VirtualController>();

    bool triggerDown = false;

    void Start()
    {

    }

    void Update()
    {
        if (virtualController.Count == 5)
        {
            if (Input.GetKey(KeyCode.Return))
            {
                virtualController[1].ButtonEvent("press", VRInputEmulatorWrapper.EVRButtonId.k_EButton_System, 50);
            }
            else if (Input.GetKey(KeyCode.Escape))
            {
                virtualController[0].ButtonEvent("press", VRInputEmulatorWrapper.EVRButtonId.k_EButton_ApplicationMenu, 50);
            }
            else if (Input.GetKey(KeyCode.Space))
            {
                if (!triggerDown)
                {
                    triggerDown = true;
                    virtualController[1].AxisEvent(1, "1", "0");
                }
                else
                {
                    triggerDown = false;
                    virtualController[1].AxisEvent(1, "0", "0");
                }
            }
        }
    }

    public void onAdd3Ctrl()
    {
        if (vrInputEmulator == null)
        {
            vrInputEmulator = new VRInputEmulatorWrapper();
            var res = vrInputEmulator.Connect();
            if (vrInputEmulator.Connect() > 0)
            {
                StartCoroutine(AddTrackedController(true));
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
                StartCoroutine(AddTrackedController(false));
            }
        }
    }

    IEnumerator AddTrackedController(bool isTrackerOnly)
    {
        var cnt = 5;

        if (!isTrackerOnly)
        {
            AddTrackedController("OVRIE_Controller_L", true, true);
            while (cnt > 0)
            {
                yield return new WaitForSeconds(1);
                if (virtualController[virtualController.Count - 1].GetOpenVRDeviceID() >= 0)
                {
                    break;
                }
                cnt--;
            }
            if (cnt == 0)
            {
                yield break;
            }

            AddTrackedController("OVRIE_Controller_R", true, false);
            cnt = 5;
            while (cnt > 0)
            {
                yield return new WaitForSeconds(1);
                if (virtualController[virtualController.Count - 1].GetOpenVRDeviceID() >= 0)
                {
                    break;
                }
                cnt--;
            }
            if (cnt == 0)
            {
                yield break;
            }
        }

        AddTrackedController("OVRIE_Tracker_1", false);
        cnt = 5;
        while (cnt > 0)
        {
            yield return new WaitForSeconds(1);
            if (virtualController[virtualController.Count - 1].GetOpenVRDeviceID() >= 0)
            {
                break;
            }
            cnt--;
        }
        if (cnt == 0)
        {
            yield break;
        }
        
        AddTrackedController("OVRIE_Tracker_2", false);
        cnt = 5;
        while (cnt > 0)
        {
            yield return new WaitForSeconds(1);
            if (virtualController[virtualController.Count - 1].GetOpenVRDeviceID() >= 0)
            {
                break;
            }
            cnt--;
        }
        if (cnt == 0)
        {
            yield break;
        }

        AddTrackedController("OVRIE_Tracker_3", false);
        cnt = 5;
        while (cnt > 0)
        {
            yield return new WaitForSeconds(1);
            if (virtualController[virtualController.Count - 1].GetOpenVRDeviceID() >= 0)
            {
                break;
            }
            cnt--;
        }
        if (cnt == 0)
        {
            yield break;
        }
        
        Controllers.value = 0;
        virtualController[0].SetDevicePosition(-0.8f, 1.3f, 0f);
        virtualController[0].SetDeviceRotation(90f, 0f, 0f);
        virtualController[1].SetDevicePosition(0.8f, 1.3f, 0f);
        virtualController[1].SetDeviceRotation(-90f, 0f, 0f);
        
        virtualController[2].SetDevicePosition(0f, 0.8f, 0f);
        virtualController[3].SetDevicePosition(-0.18f, 0f, 0f);
        virtualController[4].SetDevicePosition(0.18f, 0f, 0f);
    }


    private VirtualController AddTrackedController(string serial, bool isController, bool isL = true)
    {
        var ctrl = new VirtualController(vrInputEmulator);
        var idL = ctrl.AddTrackedController(serial);
        if (idL < 0)
        {
            idL = ctrl.GetDeviceID();
            ctrl.GetOpenVRDeviceID();
        }
        else
        {
            if(isController)
            {
                ctrl.SetControllerProperty(isL);
            }
            else
            {
                ctrl.SetTrackerProperty();
            }
        }

        virtualController.Add(ctrl);
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

        virtualController[Controllers.value].SetDevicePosition(x, y, z);
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

        virtualController[Controllers.value].SetDeviceRotation(yaw, pitch, roll);
    }

    public void onSystemDown()
    {
        virtualController[Controllers.value].ButtonEvent("press", VRInputEmulatorWrapper.EVRButtonId.k_EButton_System, 50);
    }

    public void onSystemUp()
    {
        triggerDown = false;
    }

    public void onAppDown()
    {
        virtualController[Controllers.value].ButtonEvent("press", VRInputEmulatorWrapper.EVRButtonId.k_EButton_ApplicationMenu, 50);
    }

    public void onTriggerDown()
    {
        triggerDown = true;
        virtualController[Controllers.value].AxisEvent(1, "1", "0");
    }

    public void onTriggerUp()
    {
        triggerDown = false;
        virtualController[Controllers.value].AxisEvent(1, "0", "0");
    }

    private void OnDestroy()
    {
        virtualController.ForEach(v => v.Disconnect());
        virtualController.Clear();
    }
}
