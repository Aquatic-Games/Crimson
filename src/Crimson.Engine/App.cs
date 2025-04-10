﻿using System.Diagnostics;
using Crimson.Core;
using Crimson.Render;
using Crimson.Platform;

namespace Crimson.Engine;

/// <summary>
/// The main application instance that runs the Crimson engine.
/// </summary>
public static class App
{
    private static string _appName;
    private static bool _isRunning;
    private static GlobalApp _globalApp;

    private static EventsManager _events;
    private static Surface _surface;
    private static Graphics _graphics;

    /// <summary>
    /// The app name.
    /// </summary>
    public static string Name => _appName;
    
    /// <summary>
    /// Is the app running? If false, the subsystems may not be initialized.
    /// You should only use engine features when this value is true.
    /// </summary>
    public static bool IsRunning => _isRunning;

    /// <summary>
    /// The global application instance.
    /// </summary>
    public static GlobalApp GlobalApp => _globalApp;

    /// <summary>
    /// The app's <see cref="EventsManager"/>.
    /// </summary>
    public static EventsManager Events => _events;

    /// <summary>
    /// The app's <see cref="Crimson.Platform.Surface"/>.
    /// </summary>
    public static Surface Surface => _surface;

    /// <summary>
    /// The app's <see cref="Crimson.Render.Graphics"/> instance.
    /// </summary>
    public static Graphics Graphics => _graphics;

    static App()
    {
        _appName = "";
        _isRunning = false;
        _globalApp = null!;
        _events = null!;
        _surface = null!;
        _graphics = null!;
    }
    
    /// <summary>
    /// Run the application.
    /// </summary>
    /// <param name="options">The <see cref="AppOptions"/> to use on startup.</param>
    /// <param name="globalApp">A <see cref="Crimson.Engine.GlobalApp"/> instance, if any.</param>
    public static void Run(in AppOptions options, GlobalApp? globalApp = null)
    {
        Debug.Assert(_isRunning == false);
        
        Logger.Info("Crimson 1.0.0");
        Logger.Info("Starting app:");
        Logger.Info($"    Name: {options.Name}");
        Logger.Info($"    Version: {options.Version}");
        
        _appName = options.Name;
        _globalApp = globalApp ?? new GlobalApp();
        
        Logger.Debug("Initializing events manager.");
        _events = new EventsManager();
        
        Logger.Debug("Creating window.");
        _surface = new Surface(in options.Window);
        _events.WindowClose += Close;
        
        Logger.Debug("Initializing graphics subsystem.");
        _graphics = new Graphics(_appName, Surface.Info, Surface.Size);
        
        _isRunning = true;
        
        Logger.Debug("Initializing user code.");
        _globalApp.Initialize();

        Logger.Debug("Entering main loop.");
        while (_isRunning)
        {
            _events.ProcessEvents();
            
            _globalApp.Update(1.0f / 60.0f);
            _globalApp.Draw();
            
            _graphics.Render();
        }
        
        Logger.Info("Cleaning up.");
        
        _globalApp.Dispose();
        _graphics.Dispose();
        _surface.Dispose();
        _events.Dispose();
    }

    /// <summary>
    /// Close the app.
    /// </summary>
    public static void Close()
    {
        Debug.Assert(_isRunning == true);
        
        _isRunning = false;
    }
}