// dllmain.cpp : DLL アプリケーションのエントリ ポイントを定義します。
#include "stdafx.h"
#include "VRInputEmulatorWrapper.h"
/*
extern "C" __declspec(dllexport) IVRInputEmulatorWrapper *STDMETHODCALLTYPE CreateVRInputEmulatorWrapperInstance() try
{
	return new VRInputEmulatorWrapper();
}
catch (const std::exception&) {
	return nullptr;
}
*/
template<class T, typename RetT, typename ... Args>
struct Proxy
{
	template<typename RetT(T::*func)(Args...)>
	static RetT STDMETHODCALLTYPE Func(T *self, Args... args)
	{
		return (self->*func)(args...);
	}
};

extern "C" __declspec(dllexport) int32_t STDMETHODCALLTYPE CreateVRInputEmulatorWrapperInstance(
	void ** buffer, int32_t bufferSize) try
{
	typedef void * void_ptr;

	static const void_ptr funcs[] =
	{
		Proxy<IVRInputEmulatorWrapper, void>::Func<&IVRInputEmulatorWrapper::Destroy>,

		Proxy<IVRInputEmulatorWrapper, int32_t>::Func<&IVRInputEmulatorWrapper::Connect>,
		Proxy<IVRInputEmulatorWrapper, int32_t, const char *>::Func<&IVRInputEmulatorWrapper::AddTrackedController>,
		Proxy<IVRInputEmulatorWrapper, int32_t, int32_t, int32_t, const char *, const char *>::Func<&IVRInputEmulatorWrapper::SetDeviceProperty>,
		Proxy<IVRInputEmulatorWrapper, void, int32_t>::Func<&IVRInputEmulatorWrapper::PublishTrackedDevice>,
		Proxy<IVRInputEmulatorWrapper, void, int32_t, int32_t>::Func<&IVRInputEmulatorWrapper::SetDeviceConnection>,
		Proxy<IVRInputEmulatorWrapper, int32_t, int32_t, const char *, const char *, const char *>::Func<&IVRInputEmulatorWrapper::SetDevicePosition>,
		Proxy<IVRInputEmulatorWrapper, int32_t, int32_t, const char *, const char *, const char *>::Func<&IVRInputEmulatorWrapper::SetDeviceRotation>,
		Proxy<IVRInputEmulatorWrapper, int32_t, const char *, int32_t, int32_t, int32_t>::Func<&IVRInputEmulatorWrapper::ButtonEvent>,
		Proxy<IVRInputEmulatorWrapper, int32_t, int32_t, int32_t, const char *, const char *>::Func<&IVRInputEmulatorWrapper::AxisEvent>,
		Proxy<IVRInputEmulatorWrapper, int32_t, const char *>::Func<&IVRInputEmulatorWrapper::GetDeviceID>,
		Proxy<IVRInputEmulatorWrapper, int32_t, const char *>::Func<&IVRInputEmulatorWrapper::GetOpenVRDeviceID>,
		Proxy<IVRInputEmulatorWrapper, void>::Func<&IVRInputEmulatorWrapper::Disconnect>,
	};

	static const int32_t requiredSize = sizeof(funcs) / sizeof(void *) + 1;

	if (buffer == nullptr || bufferSize < 1)
	{
		return requiredSize;
	}

	if (bufferSize < requiredSize)
	{
		return 0;
	}

	buffer[0] = new VRInputEmulatorWrapper();
	int32_t index = 1;

	for (auto it = std::begin(funcs); it != std::end(funcs); it++)
	{
		buffer[index] = *it;
		index++;
	}

	return requiredSize;
}
catch (const std::exception&) {
	return 0;
}

BOOL APIENTRY DllMain( HMODULE hModule,
                       DWORD  ul_reason_for_call,
                       LPVOID lpReserved
					 )
{
	switch (ul_reason_for_call)
	{
	case DLL_PROCESS_ATTACH:
	case DLL_THREAD_ATTACH:
	case DLL_THREAD_DETACH:
	case DLL_PROCESS_DETACH:
		break;
	}
	return TRUE;
}

