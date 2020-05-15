// VRInputEmulatorWrapper.cpp : DLL アプリケーション用にエクスポートされる関数を定義します。
//

#include "stdafx.h"
#include "VRInputEmulatorWrapper.h"
#include <openvr.h>
#include <openvr_math.h>



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

void VRInputEmulatorWrapper::SetDevicePosition(int32_t id, const char *argX, const char *argY, const char *argZ)
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
}

void VRInputEmulatorWrapper::SetDeviceRotation(int32_t id, const char *argYaw, const char *argPitch, const char *argRoll)
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
}



void VRInputEmulatorWrapper::ButtonEvent(const char * eventStr, int32_t id, int32_t btnId, int32_t holdT) {

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
		return;
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
}

void VRInputEmulatorWrapper::Disconnect()
{
	inputEmulator->disconnect();
}
