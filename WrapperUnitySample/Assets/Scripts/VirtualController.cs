using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualController
{
    private VRInputEmulatorWrapper vrInputEmulator;
    private string serial = "";
    private int deviceID = -1;
    private int openVRID = -1;
    private int fatalError = 0; // 0:success 1:error -1:fatal error

    private const float radK = Mathf.PI / 180f;

    public VirtualController(VRInputEmulatorWrapper wrapper)
    {
        vrInputEmulator = wrapper;
    }

    public int AddTrackedController(string name)
    {
        serial = name;
        deviceID = vrInputEmulator.AddTrackedController(name);

        return deviceID;
    }

    public void SetDeviceProperty(int propertyNum, string valueTypeStr, string valueStr)
    {
        vrInputEmulator.SetDeviceProperty(deviceID, propertyNum, valueTypeStr, valueStr);
    }

    public void SetControllerProperty(bool isL)
    {
        if (deviceID >= 0)
        {
            SetDeviceProperty(1000, "string", "psvr");
            SetDeviceProperty(1001, "string", "Vive Controller MV");
            SetDeviceProperty(1002, "string", serial);
            SetDeviceProperty(1003, "string", "vr_controller_vive_1_5");
            SetDeviceProperty(1004, "bool", "0");
            SetDeviceProperty(1005, "string", "HTC");
            SetDeviceProperty(1006, "string", "1465809478 htcvrsoftware@firmware-win32 2016-06-13 FPGA 1.6/0/0 VRC 1465809477 Radio 1466630404");
            SetDeviceProperty(1007, "string", "product 129 rev 1.5.0 lot 2000/0/0 0");
            SetDeviceProperty(1010, "bool", "1");
            SetDeviceProperty(1017, "uint64", "2164327680");
            SetDeviceProperty(1018, "uint64", "1465809478");
            SetDeviceProperty(1029, "int32", "2");
            SetDeviceProperty(3001, "uint64", "12884901895");
            SetDeviceProperty(3002, "int32", "1");
            SetDeviceProperty(3003, "int32", "3");
            SetDeviceProperty(3004, "int32", "0");
            SetDeviceProperty(3005, "int32", "0");
            SetDeviceProperty(3006, "int32", "0");
            if (isL)
            {
                // Left Controller
                SetDeviceProperty(3007, "int32", "1");
            }
            else
            {
                // right Controller
                SetDeviceProperty(3007, "int32", "2");
            }
            SetDeviceProperty(5000, "string", "icons");
            SetDeviceProperty(5001, "string", "{htc}controller_status_off.png");
            SetDeviceProperty(5002, "string", "{htc}controller_status_searching.gif");
            SetDeviceProperty(5003, "string", "{htc}controller_status_searching_alert.gif");
            SetDeviceProperty(5004, "string", "{htc}controller_status_ready.png");
            SetDeviceProperty(5005, "string", "{htc}controller_status_ready_alert.png");
            SetDeviceProperty(5006, "string", "{htc}controller_status_error.png");
            SetDeviceProperty(5007, "string", "{htc}controller_status_standby.png");
            SetDeviceProperty(5008, "string", "{htc}controller_status_ready_low.png");

            vrInputEmulator.PublishTrackedDevice(deviceID);
            vrInputEmulator.SetDeviceConnection(deviceID, 1);
        }
    }

    public void SetTrackerProperty()
    {
        if (deviceID >= 0)
        {
            SetDeviceProperty(1000, "string", "lighthouse");
            SetDeviceProperty(1001, "string", "Vive Controller MV");
            SetDeviceProperty(1002, "string", serial);
            SetDeviceProperty(1003, "string", "vr_controller_vive_1_5");
            SetDeviceProperty(1004, "bool", "0");
            SetDeviceProperty(1005, "string", "HTC");
            SetDeviceProperty(1006, "string", "1465809478 htcvrsoftware@firmware-win32 2016-06-13 FPGA 1.6/0/0 VRC 1465809477 Radio 1466630404");
            SetDeviceProperty(1007, "string", "product 129 rev 1.5.0 lot 2000/0/0 0");
            SetDeviceProperty(1010, "bool", "1");
            SetDeviceProperty(1017, "uint64", "2164327680");
            SetDeviceProperty(1018, "uint64", "1465809478");
            SetDeviceProperty(1029, "int32", "3");
            SetDeviceProperty(3001, "uint64", "0");
            SetDeviceProperty(3002, "int32", "1");
            SetDeviceProperty(3003, "int32", "3");
            SetDeviceProperty(3004, "int32", "0");
            SetDeviceProperty(3005, "int32", "0");
            SetDeviceProperty(3006, "int32", "0");
            SetDeviceProperty(3007, "int32", "3");
            SetDeviceProperty(5000, "string", "icons");
            SetDeviceProperty(5001, "string", "{htc}controller_status_off.png");
            SetDeviceProperty(5002, "string", "{htc}controller_status_searching.gif");
            SetDeviceProperty(5003, "string", "{htc}controller_status_searching_alert.gif");
            SetDeviceProperty(5004, "string", "{htc}controller_status_ready.png");
            SetDeviceProperty(5005, "string", "{htc}controller_status_ready_alert.png");
            SetDeviceProperty(5006, "string", "{htc}controller_status_error.png");
            SetDeviceProperty(5007, "string", "{htc}controller_status_standby.png");
            SetDeviceProperty(5008, "string", "{htc}controller_status_ready_low.png");

            vrInputEmulator.PublishTrackedDevice(deviceID);
            vrInputEmulator.SetDeviceConnection(deviceID, 1);
        }
    }


    public void SetDevicePosition(Vector3 pos)
    {
        if (deviceID >= 0 && fatalError != -1)
        {
            fatalError = vrInputEmulator.SetDevicePosition(deviceID, pos.x.ToString(), pos.y.ToString(), pos.z.ToString());
        }
    }

    public void SetDevicePosition(float x, float y, float z)
    {
        if (deviceID >= 0 && fatalError != -1)
        {
            fatalError = vrInputEmulator.SetDevicePosition(deviceID, x.ToString(), y.ToString(), z.ToString());
        }
    }

    public void SetDeviceRotation(Vector3 angles)
    {
        if (deviceID >= 0 && fatalError != -1)
        {
            fatalError = vrInputEmulator.SetDeviceRotation(deviceID, (-1f * angles.y * radK).ToString(), (angles.z * radK).ToString(), (angles.x * radK).ToString());
        }
    }

    public void SetDeviceRotation(float yaw, float pitch, float roll)
    {
        if (deviceID >= 0 && fatalError != -1)
        {
            fatalError = vrInputEmulator.SetDeviceRotation(deviceID, (yaw * radK).ToString(), (pitch * radK).ToString(), (roll * radK).ToString());
        }
    }

    public void ButtonEvent(string eventStr, VRInputEmulatorWrapper.EVRButtonId btnId, int holdT)
    {
        if (deviceID >= 0)
        {
            fatalError = vrInputEmulator.ButtonEvent(eventStr, openVRID, btnId, holdT);
        }
    }

    public void AxisEvent(int axis, string x, string y)
    {
        if (deviceID >= 0)
        {
            vrInputEmulator.AxisEvent(openVRID, axis, x, y);
        }
    }

    public int GetDeviceID()
    {
        deviceID = vrInputEmulator.GetDeviceID(serial);

        return deviceID;
    }

    public int GetOpenVRDeviceID()
    {
        var oid = vrInputEmulator.GetOpenVRDeviceID(serial);

        if (oid != -1)
        {
            openVRID = oid;
            return openVRID;
        }
        else
        {
            fatalError = -1;
            return fatalError;
        }

    }

    public void Disconnect()
    {
        if (deviceID >= 0)
        {
            vrInputEmulator.Disconnect();
        }
    }
}
