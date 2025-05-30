using Crimson.Core;
using Crimson.Graphics.Utils;
using SDL3;

namespace Crimson.Graphics.RHI.Sdl;

internal class SdlDevice : GraphicsDevice
{
    private readonly IntPtr _window;
    private readonly IntPtr _device;
    
    public SdlDevice(IntPtr window, string appName)
    {
        _window = window;
        
        SDL.SetAppMetadata(appName, null!, null!);

        uint props = SDL.CreateProperties();
        SDL.SetBooleanProperty(props, SDL.Props.GPUDeviceCreateDebugModeBoolean, true);
        SDL.SetBooleanProperty(props, SDL.Props.GPUDeviceCreateShadersSPIRVBoolean, true);
        
#if DEBUG
        SDL.SetBooleanProperty(props, SDL.Props.GPUDeviceCreatePreferLowPowerBoolean, true);
#endif

        if (OperatingSystem.IsWindows())
        {
            SDL.SetBooleanProperty(props, SDL.Props.GPUDeviceCreateShadersDXILBoolean, true);
            // Use D3D12 on windows
            SDL.SetStringProperty(props, SDL.Props.GPUDeviceCreateNameString, "direct3d12");
        }

        Logger.Trace("Creating device.");
        _device = SDL.CreateGPUDeviceWithProperties(props).Check("Create device");
        
        Console.WriteLine($"Using SDL backend: {SDL.GetGPUDeviceDriver(_device)}");
        
        Logger.Trace("Claiming window for device.");
        SDL.ClaimWindowForGPUDevice(_device, _window).Check("Claim window for device");
    }
    
    public override void Dispose()
    {
        SDL.ReleaseWindowFromGPUDevice(_device, _window);
        SDL.DestroyGPUDevice(_device);
    }
}