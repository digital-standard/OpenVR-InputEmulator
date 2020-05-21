using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

public class VRInputEmulatorWrapper : IDisposable
{
    [DllImport("VRInputEmulatorWrapper.dll")]
    static extern int CreateVRInputEmulatorWrapperInstance(IntPtr[] buffer, int bufferSize);

    delegate void FnAction(IntPtr self);

    delegate int FnConnect(IntPtr self);
    delegate int FnAddTrackedController(IntPtr self, byte[] chars);
    delegate int FnSetDeviceProperty(IntPtr self, int id, int propertyNum, byte[] valueType, byte[] value);
    delegate void FnPublishTrackedDevice(IntPtr self, int id);
    delegate void FnSetDeviceConnection(IntPtr self, int id, int cnn);
    delegate int FnSetDevicePosition(IntPtr self, int id, byte[] argX, byte[] argY, byte[] argZ);
    delegate int FnSetDeviceRotation(IntPtr self, int id, byte[] argYaw, byte[] argPitch, byte[] argRoll);
    delegate int FnButtonEvent(IntPtr self, byte[] eventStr, int id, int btnId, int holdT);
    delegate int FnAxisEvent(IntPtr self, int id, int axis, byte[] x, byte[] y);
    delegate int FnGetDeviceID(IntPtr self, byte[] serial);
    delegate int FnGetOpenVRDeviceID(IntPtr self, byte[] serial);
    delegate int FnDisconnect(IntPtr self);

    IntPtr _self;
    FnAction _fnDestroy;

    FnConnect _fnConnect;
    FnAddTrackedController _fnAddTrackedController;
    FnSetDeviceProperty _fnSetDeviceProperty;
    FnPublishTrackedDevice _fnPublishTrackedDevice;
    FnSetDeviceConnection _fnSetDeviceConnection;
    FnSetDevicePosition _fnSetDevicePosition;
    FnSetDeviceRotation _fnSetDeviceRotation;
    FnButtonEvent _fnButtonEvent;
    FnAxisEvent _fnAxisEvent;
    FnGetDeviceID _fnGetDeviceID;
    FnGetOpenVRDeviceID _fnGetOpenVRDeviceID;
    FnDisconnect _fnDisconnect;

    public enum EVRButtonId
    {
        k_EButton_System = 0,
        k_EButton_ApplicationMenu = 1,
        k_EButton_Grip = 2,
        k_EButton_DPad_Left = 3,
        k_EButton_DPad_Up = 4,
        k_EButton_DPad_Right = 5,
        k_EButton_DPad_Down = 6,
        k_EButton_A = 7,

        k_EButton_ProximitySensor = 31,

        k_EButton_Axis0 = 32,
        k_EButton_Axis1 = 33,
        k_EButton_Axis2 = 34,
        k_EButton_Axis3 = 35,
        k_EButton_Axis4 = 36,

        // aliases for well known controllers
        k_EButton_SteamVR_Touchpad = k_EButton_Axis0,
        k_EButton_SteamVR_Trigger = k_EButton_Axis1,

        k_EButton_Dashboard_Back = k_EButton_Grip,

        k_EButton_IndexController_A = k_EButton_Grip,
        k_EButton_IndexController_B = k_EButton_ApplicationMenu,
        k_EButton_IndexController_JoyStick = k_EButton_Axis3,

        k_EButton_Max = 64
    };

    public VRInputEmulatorWrapper()
    {
        int bufferSize = CreateVRInputEmulatorWrapperInstance(null, 0);
        var buffer = new IntPtr[bufferSize];
        if (CreateVRInputEmulatorWrapperInstance(buffer, bufferSize) != bufferSize)
        {
            throw new Exception();
        }

        var bufferIdx = 0;
        _self = buffer[bufferIdx];
        _fnDestroy = Marshal.GetDelegateForFunctionPointer<FnAction>(buffer[++bufferIdx]);

        _fnConnect = Marshal.GetDelegateForFunctionPointer<FnConnect>(buffer[++bufferIdx]);
        _fnAddTrackedController = Marshal.GetDelegateForFunctionPointer<FnAddTrackedController>(buffer[++bufferIdx]);
        _fnSetDeviceProperty = Marshal.GetDelegateForFunctionPointer<FnSetDeviceProperty>(buffer[++bufferIdx]);
        _fnPublishTrackedDevice = Marshal.GetDelegateForFunctionPointer<FnPublishTrackedDevice>(buffer[++bufferIdx]);
        _fnSetDeviceConnection = Marshal.GetDelegateForFunctionPointer<FnSetDeviceConnection>(buffer[++bufferIdx]);
        _fnSetDevicePosition = Marshal.GetDelegateForFunctionPointer<FnSetDevicePosition>(buffer[++bufferIdx]);
        _fnSetDeviceRotation = Marshal.GetDelegateForFunctionPointer<FnSetDeviceRotation>(buffer[++bufferIdx]);
        _fnButtonEvent = Marshal.GetDelegateForFunctionPointer<FnButtonEvent>(buffer[++bufferIdx]);
        _fnAxisEvent = Marshal.GetDelegateForFunctionPointer<FnAxisEvent>(buffer[++bufferIdx]);
        _fnGetDeviceID = Marshal.GetDelegateForFunctionPointer<FnGetDeviceID>(buffer[++bufferIdx]);
        _fnGetOpenVRDeviceID = Marshal.GetDelegateForFunctionPointer<FnGetOpenVRDeviceID>(buffer[++bufferIdx]);
        _fnDisconnect = Marshal.GetDelegateForFunctionPointer<FnDisconnect>(buffer[++bufferIdx]);
    }

    public void Dispose()
    {
        _fnDestroy?.Invoke(_self);
        _fnDestroy = null;
        _self = IntPtr.Zero;
    }

    public int Connect()
    {
        return _fnConnect(_self);
    }

    public int AddTrackedController(string str)
    {
        return _fnAddTrackedController(_self, ToByte(str));
    }

    public int SetDeviceProperty(int id, int propertyNum, string valueTypeStr, string valueStr)
    {
        return _fnSetDeviceProperty(_self, id, propertyNum, ToByte(valueTypeStr), ToByte(valueStr));
    }

    public void PublishTrackedDevice(int id)
    {
        _fnPublishTrackedDevice(_self, id);
    }

    public void SetDeviceConnection(int id, int cnn)
    {
        _fnSetDeviceConnection(_self, id, cnn);
    }

    public int SetDevicePosition(int id, string argXStr, string argYStr, string argZStr)
    {
        return _fnSetDevicePosition(_self, id, ToByte(argXStr), ToByte(argYStr), ToByte(argZStr));
    }

    public int SetDeviceRotation(int id, string argYawStr, string argPitchStr, string argRollStr)
    {
        return _fnSetDeviceRotation(_self, id, ToByte(argYawStr), ToByte(argPitchStr), ToByte(argRollStr));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="eventStr">press, pressandhold, unpress, touch, touchandhold, untouch</param>
    /// <param name="id"></param>
    /// <param name="btnId"></param>
    /// <param name="holdT"></param>
    public int ButtonEvent(string eventStr, int id, EVRButtonId btnId, int holdT)
    {
        return _fnButtonEvent(_self, ToByte(eventStr), id, (int)btnId, holdT);
    }

    public int AxisEvent(int id, int axis, string x, string y)
    {
        return _fnAxisEvent(_self, id, axis, ToByte(x), ToByte(y));
    }

    public int GetDeviceID(string serial)
    {
        return _fnGetDeviceID(_self, ToByte(serial));
    }

    public int GetOpenVRDeviceID(string serial)
    {
        return _fnGetOpenVRDeviceID(_self, ToByte(serial));
    }

    public int Disconnect()
    {
        return _fnDisconnect(_self);
    }

    private byte[] ToByte(string str)
    {
        return Encoding.ASCII.GetBytes(str + "\0");

    }
}
