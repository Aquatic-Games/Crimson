﻿using System;
using Euphoria.Engine;
using Euphoria.Engine.Debugging;
using Euphoria.Engine.InputSystem;
using Euphoria.Engine.Scenes;
using Euphoria.Render;
using Euphoria.Render.Renderers;
using ImGuiNET;

namespace Tests.Engine;

public class TestApp : Application
{
    public const string FileBase = "/home/aqua";

    private float _dtAccumulator;
    private int _tickAccumulator;
    
    public override void Initialize(Scene initialScene)
    {
        base.Initialize(initialScene);

        //Graphics.VSyncMode = VSyncMode.Off;
        
        ImGuiRenderer imGui = Graphics.ImGuiRenderer;
        imGui.AddFont($"{FileBase}/Downloads/Russo_One/RussoOne-Regular.ttf", 14, "RussoOne");

        EuphoriaDebug.IsOpen = true;
    }

    public override void Tick(float dt)
    {
        /*_dtAccumulator += dt;
        _tickAccumulator++;

        if (_dtAccumulator >= 1)
        {
            _dtAccumulator -= 1;
            _tickAccumulator = 0;
        }

        Console.WriteLine($"Second {_dtAccumulator} Tick {_tickAccumulator}");*/
        
        base.Tick(dt);
    }

    public override void Update(float dt)
    {
        base.Update(dt);
        
        if (Input.IsKeyPressed(Key.Escape))
            App.Close();
    }
}