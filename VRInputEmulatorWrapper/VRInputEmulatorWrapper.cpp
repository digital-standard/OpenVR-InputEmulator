// VRInputEmulatorWrapper.cpp : DLL アプリケーション用にエクスポートされる関数を定義します。
//

#include "stdafx.h"
#include "VRInputEmulatorWrapper.h"
#include <openvr.h>
#include <openvr_math.h>

std::stringstream ss;



VRInputEmulatorWrapper::VRInputEmulatorWrapper() :
	m_value(0)
{
	inputEmulator = new vrinputemulator::VRInputEmulator();
}

VRInputEmulatorWrapper::~VRInputEmulatorWrapper()
{
}

void VRInputEmulatorWrapper::Destroy()
{
	delete this;
}


int32_t VRInputEmulatorWrapper::Connect() try
{
	inputEmulator->connect();
	return 1;
}
catch (const std::exception&) {
	return -1;
}

int32_t VRInputEmulatorWrapper::AddTrackedController(const char *serial) try
{
	return inputEmulator->addVirtualDevice(vrinputemulator::VirtualDeviceType::TrackedController, serial, false);
}
catch (const std::exception&) {
	return -1;
}

int32_t VRInputEmulatorWrapper::SetDeviceProperty(int32_t id, int32_t propertyNum, const char *valueType, const char *value) try
{
	uint32_t deviceId = id;
	vr::ETrackedDeviceProperty deviceProperty = (vr::ETrackedDeviceProperty)propertyNum;

	if (std::strcmp(valueType, "int32") == 0) {
		inputEmulator->setVirtualDeviceProperty(deviceId, deviceProperty, (int32_t)std::atoi(value));
	}
	else if (std::strcmp(valueType, "uint64") == 0) {
		inputEmulator->setVirtualDeviceProperty(deviceId, deviceProperty, (uint64_t)std::atoll(value));
	}
	else if (std::strcmp(valueType, "float") == 0) {
		inputEmulator->setVirtualDeviceProperty(deviceId, deviceProperty, (float)std::atof(value));
	}
	else if (std::strcmp(valueType, "bool") == 0) {
		inputEmulator->setVirtualDeviceProperty(deviceId, deviceProperty, std::atoi(value) != 0);
	}
	else if (std::strcmp(valueType, "string") == 0) {
		inputEmulator->setVirtualDeviceProperty(deviceId, deviceProperty, value);
	}
	else {
		return -1;
	}
	return 1;
}
catch (const std::exception&) {
	return -1;
}

void VRInputEmulatorWrapper::PublishTrackedDevice(int32_t id)
{
	uint32_t deviceId = id;
	inputEmulator->publishVirtualDevice(deviceId);
}

void VRInputEmulatorWrapper::SetDeviceConnection(int32_t id, int32_t cnn)
{
	uint32_t deviceId = id;
	bool connected = cnn != 0;

	auto pose = inputEmulator->getVirtualDevicePose(deviceId);
	if (pose.deviceIsConnected != connected) {
		pose.deviceIsConnected = connected;
		pose.poseIsValid = connected;
		inputEmulator->setVirtualDevicePose(deviceId, pose);
	}
}

int32_t VRInputEmulatorWrapper::SetDevicePosition(int32_t id, const char *argX, const char *argY, const char *argZ) try
{
	uint32_t deviceId = id;
	float x = (float)std::atof(argX);
	float y = (float)std::atof(argY);
	float z = (float)std::atof(argZ);

	auto pose = inputEmulator->getVirtualDevicePose(deviceId);
	pose.vecPosition[0] = x;
	pose.vecPosition[1] = y;
	pose.vecPosition[2] = z;
	pose.poseIsValid = true;
	pose.result = vr::TrackingResult_Running_OK;
	inputEmulator->setVirtualDevicePose(deviceId, pose);

	return 1;
}
catch (const std::exception&) {
	return -1;
}

int32_t VRInputEmulatorWrapper::SetDeviceRotation(int32_t id, const char *argYaw, const char *argPitch, const char *argRoll) try
{
	uint32_t deviceId = id;
	float yaw = (float)std::atof(argYaw);
	float pitch = (float)std::atof(argPitch);
	float roll = (float)std::atof(argRoll);

	auto pose = inputEmulator->getVirtualDevicePose(deviceId);
	pose.qRotation = vrmath::quaternionFromYawPitchRoll(yaw, pitch, roll);
	pose.poseIsValid = true;
	pose.result = vr::TrackingResult_Running_OK;
	inputEmulator->setVirtualDevicePose(deviceId, pose);

	return 1;
}
catch (const std::exception&) {
	return -1;
}



int32_t VRInputEmulatorWrapper::ButtonEvent(const char * eventStr, int32_t id, int32_t btnId, int32_t holdT) try
{
	bool noHold = false;
	vrinputemulator::ButtonEventType eventType;

	if (std::strcmp(eventStr, "press") == 0) {
		eventType = vrinputemulator::ButtonEventType::ButtonPressed;
		noHold = true;
	}
	else if (std::strcmp(eventStr, "pressandhold") == 0) {
		eventType = vrinputemulator::ButtonEventType::ButtonPressed;
	}
	else if (std::strcmp(eventStr, "unpress") == 0) {
		eventType = vrinputemulator::ButtonEventType::ButtonUnpressed;
	}
	else if (std::strcmp(eventStr, "touch") == 0) {
		eventType = vrinputemulator::ButtonEventType::ButtonTouched;
		noHold = true;
	}
	else if (std::strcmp(eventStr, "touchandhold") == 0) {
		eventType = vrinputemulator::ButtonEventType::ButtonTouched;
	}
	else if (std::strcmp(eventStr, "untouch") == 0) {
		eventType = vrinputemulator::ButtonEventType::ButtonUntouched;
	}
	else {
		return 0;
	}

	uint32_t holdTime = 50;
	if (noHold) {
			holdTime = holdT;
	}
	uint32_t deviceId = id;
	vr::EVRButtonId buttonId = (vr::EVRButtonId)btnId;

	inputEmulator->openvrButtonEvent(eventType, deviceId, buttonId, 0.0);
	std::this_thread::sleep_for(std::chrono::milliseconds(holdTime));

	if (noHold) {
		if (eventType == vrinputemulator::ButtonEventType::ButtonPressed) {
			eventType = vrinputemulator::ButtonEventType::ButtonUnpressed;
		}
		else {
			eventType = vrinputemulator::ButtonEventType::ButtonUntouched;
		}
		inputEmulator->openvrButtonEvent(eventType, deviceId, buttonId, 0.0);
	}
	return 1;
}
catch (const std::exception&) {
	return -1;
}


int32_t VRInputEmulatorWrapper::AxisEvent(int32_t id, int32_t axis, const char *x, const char *y) try
{
	uint32_t deviceId = id;
	uint32_t axisId = axis;
	vr::VRControllerAxis_t axisState;
	axisState.x = (float)std::atof(x);
	axisState.y = (float)std::atof(y);

	inputEmulator->openvrAxisEvent(deviceId, axisId, axisState);

	return 1;
}
catch (const std::exception&) {
	return -1;
}

int32_t VRInputEmulatorWrapper::GetDeviceID(const char *serial)
{
	auto deviceCount = inputEmulator->getVirtualDeviceCount();
	for (unsigned i = 0; i < deviceCount; ++i) {
		auto deviceInfo = inputEmulator->getVirtualDeviceInfo(i);

		if (std::strcmp(deviceInfo.deviceSerial.c_str(), serial) == 0)
		{
			return i;
		}
	}
	return -1;
}

int32_t VRInputEmulatorWrapper::GetOpenVRDeviceID(const char *serial)
{
	auto deviceCount = inputEmulator->getVirtualDeviceCount();
	for (unsigned i = 0; i < deviceCount; ++i) {
		auto deviceInfo = inputEmulator->getVirtualDeviceInfo(i);

		if (std::strcmp(deviceInfo.deviceSerial.c_str(), serial) == 0)
		{
			return deviceInfo.openvrDeviceId;
		}
	}

	return -1;
}

void VRInputEmulatorWrapper::Disconnect()
{
	inputEmulator->disconnect();

	//vr::VR_Shutdown();
}
