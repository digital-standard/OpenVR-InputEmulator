using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViveController
{
    private VRInputEmulatorWrapper vrInputEmulator;
    private string serial = "";
    private int deviceID = -1;
    private int openVRID = -1;
    private int fatalError = 0; // 0:success 1:error -1:fatal error

    public ViveController(VRInputEmulatorWrapper wrapper)
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

    public void SetDeviceProperty(string typeStr = "L")
    {
        if (deviceID >= 0)
        {
            SetDeviceProperty(1000, "string", "psvr");
            SetDeviceProperty(1001, "string", "Vive Controller MV");
            SetDeviceProperty(1002, "string", serial);
            /*
            if (typeStr == "L")
            {
                SetDeviceProperty(1002, "string", "LHR-FF625FC3");
            }
            else if (typeStr == "R")
            {
                SetDeviceProperty(1002, "string", "LHR-FF273FC6");
            }
            */
            SetDeviceProperty(1003, "string", "vr_controller_vive_1_5");
            SetDeviceProperty(1004, "bool", "0");
            SetDeviceProperty(1005, "string", "HTC");
            SetDeviceProperty(1006, "string", "1465809478 htcvrsoftware@firmware-win32 2016-06-13 FPGA 1.6/0/0 VRC 1465809477 Radio 1466630404");
            SetDeviceProperty(1007, "string", "product 129 rev 1.5.0 lot 2000/0/0 0");
            SetDeviceProperty(1010, "bool", "1");
            SetDeviceProperty(1017, "uint64", "2164327680");
            SetDeviceProperty(1018, "uint64", "1465809478");
            SetDeviceProperty(1029, "int32", "2");
            //SetDeviceProperty(1029, "int32", "3");
            //SetDeviceProperty(3001, "uint64", "12884901894");
            SetDeviceProperty(3001, "uint64", "12884901895");
            SetDeviceProperty(3002, "int32", "1");
            SetDeviceProperty(3003, "int32", "3");
            SetDeviceProperty(3004, "int32", "0");
            SetDeviceProperty(3005, "int32", "0");
            SetDeviceProperty(3006, "int32", "0");
            if (typeStr == "L")
            {
                SetDeviceProperty(3007, "int32", "0");

            }
            else if (typeStr == "R")
            {
                SetDeviceProperty(3007, "int32", "0");

            }
            else
            {
                SetDeviceProperty(3007, "int32", "3");
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
            //SetDeviceProperty(2074, "string", "kinect_device");

            vrInputEmulator.PublishTrackedDevice(deviceID);
            vrInputEmulator.SetDeviceConnection(deviceID, 1);

            var oid = vrInputEmulator.GetOpenVRDeviceID(serial);
            if (oid != -1)
            {
                openVRID = oid;
                vrInputEmulator.SetDevicePosition(deviceID, "0", "0", "0");
            }
            else
            {
                fatalError = -1;
            }
        }
    }

    public void SetDeviceVIVEControllerProperty(string typeStr = "L")
    {
        if (deviceID >= 0)
        {
            /************/

            SetDeviceProperty(1000, "string", "lighthouse");
            SetDeviceProperty(1001, "string", "Vive. Controller MV");
            if(typeStr == "L")
            {
                SetDeviceProperty(1002, "string", "LHR-FF625FC3");
            }
            else
            {
                SetDeviceProperty(1002, "string", "LHR-FF273FC6");
            }
            /* Prop_RenderModelName_String */
            SetDeviceProperty(1003, "string", "vr_controller_vive_1_5");
            /* Prop_WillDriftInYaw_Bool */
            SetDeviceProperty(1004, "bool", "0");
            SetDeviceProperty(1005, "string", "HTC");
            /* Prop_TrackingFirmwareVersion_String */
            SetDeviceProperty(1006, "string", "1533720215 htcvrsoftware@firmware-win32 2018-08-08 FPGA 262(1.6/0/0) BL 0 VRC 1533720214 Radio 1532585738");
            /* Prop_HardwareRevision_String */
            SetDeviceProperty(1007, "string", "product 129 rev 1.5.0 lot 2000/0/0 0");
            /* Prop_ConnectedWirelessDongle_String */
            /*
            if (typeStr == "L")
            {
                SetDeviceProperty(1009, "string", "B7EF576A83");
            }
            else
            {
                SetDeviceProperty(1009, "string", "582E667C0C");
            }
            */
            /* Prop_DeviceIsWireless_Bool */
            SetDeviceProperty(1010, "bool", "1");
            /* Prop_DeviceIsCharging_Bool */
            SetDeviceProperty(1011, "bool", "0");
            /* Prop_DeviceBatteryPercentage_Float */
            SetDeviceProperty(1012, "float", "1");
            /* Prop_StatusDisplayTransform_Matrix34 */
            //SetDeviceProperty(1013, "Matrix34", "[ -1, 0, 0, 0, 0, 0, -1, 0, 0, -1, 0, 0 ]");
            /* Prop_Firmware_UpdateAvailable_Bool */
            SetDeviceProperty(1014, "bool", "0");
            /* Prop_Firmware_ManualUpdate_Bool */
            SetDeviceProperty(1015, "bool", "0");
            /* Prop_Firmware_ManualUpdateURL_String */
            SetDeviceProperty(1016, "string", "https://developer.valvesoftware.com/wiki/SteamVR/HowTo_Update_Firmware");
            /* Prop_HardwareRevision_Uint64 */
            SetDeviceProperty(1017, "uint64", "2164327680");
            /* Prop_FirmwareVersion_Uint64 */
            SetDeviceProperty(1018, "uint64", "1533720215");
            /* Prop_FPGAVersion_Uint64 */
            SetDeviceProperty(1019, "uint64", "262");
            /* Prop_VRCVersion_Uint64 */
            SetDeviceProperty(1020, "uint64", "1533720214");
            /* Prop_RadioVersion_Uint64 */
            SetDeviceProperty(1021, "uint64", "1532585738");
            /* Prop_DongleVersion_Uint64 */
            SetDeviceProperty(1022, "uint64", "1461100729");

            /* Prop_DeviceProvidesBatteryStatus_Bool */
            SetDeviceProperty(1026, "bool", "1");
            /* Prop_DeviceCanPowerOff_Bool */
            SetDeviceProperty(1027, "bool", "1");
            /* Prop_Firmware_ProgrammingTarget_String */
            if (typeStr == "L")
            {
                SetDeviceProperty(1028, "string", "LHR-FF625FC3");
            }
            else
            {
                SetDeviceProperty(1028, "string", "LHR-FF273FC6");
            }
            /* Prop_DeviceClass_Int32 */
            SetDeviceProperty(1029, "int32", "2");

            /* Prop_Firmware_ForceUpdateRequired_Bool */
            SetDeviceProperty(1032, "bool", "0");
            /* Prop_Unknown_1033 */
            //SetDeviceProperty(1033, "bool", "1");
            /* Prop_ParentDriver_Uint64 */
            SetDeviceProperty(1034, "uint64", "8589934594");
            /* Prop_ResourceRoot_String */
            SetDeviceProperty(1035, "string", "htc");
            /* Prop_RegisteredDeviceType_String */
            if (typeStr == "L")
            {
                SetDeviceProperty(1036, "string", "htc/vive_controllerLHR-FF625FC3");
            }
            else
            {
                SetDeviceProperty(1036, "string", "htc/vive_controllerLHR-FF273FC6");
            }
            /* Prop_InputProfilePath_String */
            SetDeviceProperty(1037, "string", "{htc}/input/vive_controller_profile.json");

            /* Prop_Identifiable_Bool */
            SetDeviceProperty(1043, "bool", "1");

            /* Prop_ReportsTimeSinceVSync_Bool */
            SetDeviceProperty(2000, "bool", "0");


            /* Prop_SupportedButtons_Uint64 */
            if (typeStr == "L")
            {
                SetDeviceProperty(3001, "uint64", "0");
            }
            else
            {
                SetDeviceProperty(3001, "uint64", "12884901894");
            }
            /* Prop_Axis0Type_Int32 */
            SetDeviceProperty(3002, "int32", "1");
            /* Prop_Axis1Type_Int32 */
            SetDeviceProperty(3003, "int32", "3");

            /* Prop_ControllerRoleHint_Int32 */
            SetDeviceProperty(3007, "int32", "0");
            /*
             * if (typeStr == "L")
            {
                SetDeviceProperty(3007, "int32", "1");
            }
            else
            {
                SetDeviceProperty(3007, "int32", "2");
            }
            */
            SetDeviceProperty(5001, "string", "{htc}controller_status_off.png");
            SetDeviceProperty(5002, "string", "{htc}controller_status_searching.gif");
            SetDeviceProperty(5003, "string", "{htc}controller_status_searching_alert.gif");
            SetDeviceProperty(5004, "string", "{htc}controller_status_ready.png");
            SetDeviceProperty(5005, "string", "{htc}controller_status_ready_alert.png");
            SetDeviceProperty(5006, "string", "{htc}controller_status_error.png");
            SetDeviceProperty(5007, "string", "{htc}controller_status_standby.png");
            SetDeviceProperty(5008, "string", "{htc}controller_status_ready_low.png");

            /* Prop_HasDisplayComponent_Bool */
            SetDeviceProperty(6002, "bool", "0");
            /* Prop_HasCameraComponent_Bool */
            SetDeviceProperty(6004, "bool", "0");
            /* Prop_HasDriverDirectModeComponent_Bool */
            SetDeviceProperty(6005, "bool", "0");
            /* Prop_HasVirtualDisplayComponent_Bool */
            SetDeviceProperty(6006, "bool", "0");


            /* Prop_ControllerType_String */
            SetDeviceProperty(7000, "string", "vive_controller");
            /* Prop_LegacyInputProfile_String */
            SetDeviceProperty(7001, "string", "vive_controller");
            /* Prop_Unknown_7002 */
            //SetDeviceProperty(7002, "bool", "0");

            /***********

            SetDeviceProperty(1000, "string", "lighthouse");
            SetDeviceProperty(1001, "string", "Vive Controller MV");
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
            if (typeStr == "L")
            {
                SetDeviceProperty(3007, "int32", "1");
            }
            else
            {
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
            //SetDeviceProperty(2074, "string", "kinect_device");

    ******************************/

            vrInputEmulator.PublishTrackedDevice(deviceID);
            vrInputEmulator.SetDeviceConnection(deviceID, 1);

            vrInputEmulator.SetDevicePosition(deviceID, "0", "0", "0");
        }
    }

    public void SetDeviceWinMRProperty(string typeStr = "L")
    {
        if (deviceID >= 0)
        {
            SetDeviceProperty(1000, "string", "holographic");
            if (typeStr == "L")
            {
                SetDeviceProperty(1001, "string", "WindowsMR: 0x045E/0x065B/0/1");
                SetDeviceProperty(1002, "string", "MRSOURCE0");
                SetDeviceProperty(1003, "string", "controller.obj");
                SetDeviceProperty(1005, "string", "WindowsMR: 0x045E");
                SetDeviceProperty(1011, "bool", "0");
                SetDeviceProperty(1012, "float", "0.1");
                SetDeviceProperty(1026, "bool", "1");
                SetDeviceProperty(1029, "int32", "2");
                SetDeviceProperty(1034, "uint64", "8589934594");
                SetDeviceProperty(1037, "string", "{holographic}/input/mixedreality_controller_profile.json");
                SetDeviceProperty(3002, "int32", "1");
                SetDeviceProperty(3003, "int32", "3");
                SetDeviceProperty(3004, "int32", "2");
                SetDeviceProperty(3007, "int32", "1");
            }
            else
            {
                SetDeviceProperty(1001, "string", "WindowsMR: 0x045E/0x065B/0/2");
                SetDeviceProperty(1002, "string", "MRSOURCE1");
                SetDeviceProperty(1003, "string", "controller.obj");
                SetDeviceProperty(1005, "string", "WindowsMR: 0x045E");
                SetDeviceProperty(1011, "bool", "0");
                SetDeviceProperty(1012, "float", "0.45");
                SetDeviceProperty(1026, "bool", "1");
                SetDeviceProperty(1029, "int32", "2");
                SetDeviceProperty(1034, "uint64", "8589934594");
                SetDeviceProperty(1037, "string", "{holographic}/input/mixedreality_controller_profile.json");
                SetDeviceProperty(3002, "int32", "1");
                SetDeviceProperty(3003, "int32", "3");
                SetDeviceProperty(3004, "int32", "2");
                SetDeviceProperty(3007, "int32", "2");
            }

            SetDeviceProperty(5000, "string", "icons");
            SetDeviceProperty(5001, "string", "{holographic}/icons/controller_right_status_off.png");
            SetDeviceProperty(5002, "string", "{holographic}/icons/controller_right_status_searching.gif");
            SetDeviceProperty(5003, "string", "{holographic}/icons/controller_right_status_searching_alert.gif");
            SetDeviceProperty(5004, "string", "{holographic}/icons/controller_right_status_ready.png");
            SetDeviceProperty(5005, "string", "{holographic}/icons/controller_right_status_ready_alert.png");
            SetDeviceProperty(5006, "string", "{holographic}/icons/controller_right_status_error.png");
            SetDeviceProperty(5007, "string", "{holographic}/icons/controller_right_status_standby.png");
            SetDeviceProperty(5008, "string", "{holographic}/icons/controller_right_status_ready_low.png");

            SetDeviceProperty(6002, "bool", "0");
            SetDeviceProperty(6004, "bool", "0");
            SetDeviceProperty(6005, "bool", "0");
            SetDeviceProperty(6006, "bool", "0");
            SetDeviceProperty(7000, "string", "holographic_controller");
            SetDeviceProperty(7002, "int32", "0");

            vrInputEmulator.PublishTrackedDevice(deviceID);
            vrInputEmulator.SetDeviceConnection(deviceID, 1);

            var oid = vrInputEmulator.GetOpenVRDeviceID(serial);
            if(oid != -1)
            {
                openVRID = oid;
                vrInputEmulator.SetDevicePosition(deviceID, "0", "0", "0");
            }
            else
            {
                fatalError = -1;
            }
        }
    }
    public void SetDevicePosition(Vector3 pos)
    {
        if (deviceID >= 0 && fatalError != -1)
        {
            //fatalError = vrInputEmulator.SetDevicePosition(deviceID, (-1f - pos.z).ToString(), (pos.y + 1f).ToString(), pos.x.ToString());
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

    private float radK = Mathf.PI / 180f;

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
