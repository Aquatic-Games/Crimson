using Crimson.Graphics.RHI.Sdl;

namespace Crimson.Graphics.RHI;

public abstract class GraphicsDevice : IDisposable
{
    
    
    public abstract void Dispose();
    
    public static GraphicsDevice Create(string name, IntPtr sdlWindow)
    {
        return new SdlDevice(sdlWindow, name);
    }
}