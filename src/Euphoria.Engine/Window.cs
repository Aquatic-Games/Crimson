﻿using Silk.NET.SDL;
using SdlWindow = Silk.NET.SDL.Window;

namespace u4.Engine;

public unsafe class Window : IDisposable
{
    private Sdl _sdl;
    private SdlWindow* _window;
    private void* _glContext;

    public Window(GraphicsOptions options)
    {
        _sdl = Sdl.GetApi();

        if (_sdl.Init(Sdl.InitVideo | Sdl.InitEvents) < 0)
            throw new Exception($"Failed to initialize SDL: {_sdl.GetErrorS()}");

        _sdl.GLSetAttribute(GLattr.ContextProfileMask, (int) GLprofile.Core);
        _sdl.GLSetAttribute(GLattr.ContextMajorVersion, 4);
        _sdl.GLSetAttribute(GLattr.ContextMinorVersion, 3);
        _sdl.GLSetAttribute(GLattr.AlphaSize, 0);

        _window = _sdl.CreateWindow(options.WindowTitle, Sdl.WindowposCentered, Sdl.WindowposCentered,
            options.WindowSize.Width, options.WindowSize.Height, (uint) WindowFlags.Opengl);

        if (_window == null)
            throw new Exception("Failed to create SDL window.");

        _glContext = _sdl.GLCreateContext(_window);
        
    }

    public void Dispose()
    {
        if (_glContext != null)
            _sdl.GLDeleteContext(_glContext);
        
        _sdl.DestroyWindow(_window);
        _sdl.Quit();
        _sdl.Dispose();
    }
}