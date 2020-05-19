#pragma once
#include "stdafx.h"
#include <vrinputemulator.h>


class IVRInputEmulatorWrapper
{
public:
	virtual void Destroy() = 0;

	virtual int32_t Connect() = 0;
	virtual int32_t AddTrackedController(const char *serial) = 0;
	virtual int32_t SetDeviceProperty(int32_t id, int32_t propertyNum, const char *valueType, const char *value) = 0;
	virtual void PublishTrackedDevice(int32_t id) = 0;
	virtual void SetDeviceConnection(int32_t id, int32_t cnn) = 0;
	virtual int32_t SetDevicePosition(int32_t id, const char *argX, const char *argY, const char *argZ) = 0;
	virtual int32_t SetDeviceRotation(int32_t id, const char *argYaw, const char *argPitch, const char *argRoll) = 0;
	virtual int32_t ButtonEvent(const char * eventStr, int32_t id, int32_t btnId, int32_t holdT) = 0;
	virtual int32_t AxisEvent(int32_t id, int32_t axis, const char *x, const char *y) = 0;
	virtual int32_t GetDeviceID(const char *serial) = 0;
	virtual int32_t GetOpenVRDeviceID(const char *serial) = 0;
	virtual void Disconnect() = 0;
};


class VRInputEmulatorWrapper :
	public IVRInputEmulatorWrapper
{
private:
	int32_t m_value;
	std::string m_str;

	vrinputemulator::VRInputEmulator *inputEmulator;

public:
	VRInputEmulatorWrapper();
	~VRInputEmulatorWrapper();

	virtual void Destroy() override;

	virtual int32_t Connect() override;
	virtual int32_t AddTrackedController(const char *serial) override;
	virtual int32_t SetDeviceProperty(int32_t id, int32_t propertyNum, const char *valueType, const char *value) override;
	virtual void PublishTrackedDevice(int32_t id) override;
	virtual void SetDeviceConnection(int32_t id, int32_t cnn) override;
	virtual int32_t SetDevicePosition(int32_t id, const char *argX, const char *argY, const char *argZ) override;
	virtual int32_t SetDeviceRotation(int32_t id, const char *argYaw, const char *argPitch, const char *argRoll) override;
	virtual int32_t ButtonEvent(const char * eventStr, int32_t id, int32_t btnId, int32_t holdT) override;
	virtual int32_t AxisEvent(int32_t id, int32_t axis, const char *x, const char *y) override;
	virtual int32_t GetDeviceID(const char *serial) override;
	virtual int32_t GetOpenVRDeviceID(const char *serial) override;
	virtual void Disconnect() override;
};