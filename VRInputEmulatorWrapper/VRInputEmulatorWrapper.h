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
	virtual void SetDevicePosition(int32_t id, const char *argX, const char *argY, const char *argZ) = 0;
	virtual void SetDeviceRotation(int32_t id, const char *argYaw, const char *argPitch, const char *argRoll) = 0;
	virtual void ButtonEvent(const char * eventStr, int32_t id, int32_t btnId, int32_t holdT) = 0;
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
	virtual void SetDevicePosition(int32_t id, const char *argX, const char *argY, const char *argZ) override;
	virtual void SetDeviceRotation(int32_t id, const char *argYaw, const char *argPitch, const char *argRoll) override;
	virtual void ButtonEvent(const char * eventStr, int32_t id, int32_t btnId, int32_t holdT) override;
	virtual void Disconnect() override;
};