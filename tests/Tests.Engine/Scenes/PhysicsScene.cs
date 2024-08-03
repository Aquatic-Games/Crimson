﻿using System;
using System.Numerics;
using Euphoria.Engine.Entities;
using Euphoria.Engine.Entities.Components;
using Euphoria.Engine.Scenes;
using Euphoria.Physics;
using Euphoria.Physics.Shapes;
using Euphoria.Render;
using Euphoria.Render.Primitives;
using Tests.Engine.Components;

namespace Tests.Engine.Scenes;

public class PhysicsScene : Scene
{
    public override void Initialize()
    {
        base.Initialize();

        Camera.Transform.Position.Z = 3;
        Camera.AddComponent(new CameraMove());

        Cube cube = new Cube();
        Material material = new Material(new MaterialDescription(Texture.White));

        const bool interpolation = true;
        
        Entity staticCube = new Entity("StaticCube",
            new Transform(new Vector3(0, -2, 0), Quaternion.Identity, new Vector3(25, 1, 25), Vector3.Zero));
        staticCube.AddComponent(new MeshRenderer(new Mesh(cube.Vertices, cube.Indices), material));
        staticCube.AddComponent(new Rigidbody(new Box(1, 1, 1), 0, false));
        AddEntity(staticCube);

        Entity wall1 = new Entity("Wall1", new Transform(new Vector3(-12.5f, 0, 0)) { Scale = new Vector3(1, 10, 25) });
        wall1.AddComponent(new MeshRenderer(new Mesh(cube.Vertices, cube.Indices), material));
        wall1.AddComponent(new Rigidbody(new Box(1, 1, 1), 0, false));
        AddEntity(wall1);
        
        Entity wall2 = new Entity("Wall2", new Transform(new Vector3(12.5f, 0, 0)) { Scale = new Vector3(1, 10, 25) });
        wall2.AddComponent(new MeshRenderer(new Mesh(cube.Vertices, cube.Indices), material));
        wall2.AddComponent(new Rigidbody(new Box(1, 1, 1), 0, false));
        AddEntity(wall2);
        
        Entity wall3 = new Entity("Wall3", new Transform(new Vector3(0, 0, -12.5f)) { Scale = new Vector3(25, 10, 1) });
        wall3.AddComponent(new MeshRenderer(new Mesh(cube.Vertices, cube.Indices), material));
        wall3.AddComponent(new Rigidbody(new Box(1, 1, 1), 0, false));
        AddEntity(wall3);
        
        Entity wall4 = new Entity("Wall4", new Transform(new Vector3(0, 0, 12.5f)) { Scale = new Vector3(25, 10, 1) });
        wall4.AddComponent(new MeshRenderer(new Mesh(cube.Vertices, cube.Indices), material));
        wall4.AddComponent(new Rigidbody(new Box(1, 1, 1), 0, false));
        AddEntity(wall4);

        for (int i = 0; i < 2000; i++)
        {
            Entity dynamicCube = new Entity($"DynamicCube{i}", new Transform(new Vector3((i % 20) - 10, 15 + i, (i % 20) - 10)));
            dynamicCube.AddComponent(new MeshRenderer(new Mesh(cube.Vertices, cube.Indices), material));
            dynamicCube.AddComponent(new Rigidbody(new Box(1, 1, 1), 1, interpolation));
            
            AddEntity(dynamicCube);
        }
    }

    public override void Tick(float dt)
    {
        base.Tick(dt);

        if (PhysicsWorld.Raycast(Camera.Transform.Position, Camera.Transform.Forward, 100, out RayHit hit))
        {
            Console.WriteLine(
                $"Yes! Hit body {hit.Body.Id}, position: {hit.Body.Position} (HitPos: {hit.Position}, Normal: {hit.Normal})");
        }
        else
            Console.WriteLine("Nope");
    }
}