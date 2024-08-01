﻿using BepuPhysics;
using BepuPhysics.Collidables;
using BepuUtilities;
using BepuUtilities.Memory;
using Euphoria.Physics.Callbacks;
using IShape = Euphoria.Physics.Shapes.IShape;

namespace Euphoria.Physics;

public static class PhysicsWorld
{
    private static NarrowPhaseCallbacks _narrowPhaseCallbacks;
    private static PoseIntegratorCallbacks _poseIntegratorCallbacks;
    
    private static BufferPool _bufferPool;
    private static ThreadDispatcher _threadDispatcher;
    
    internal static Simulation Simulation;

    public static void Initialize()
    {
        _narrowPhaseCallbacks = new NarrowPhaseCallbacks();
        _poseIntegratorCallbacks = new PoseIntegratorCallbacks();
        
        _bufferPool = new BufferPool();
        _threadDispatcher = new ThreadDispatcher(Environment.ProcessorCount);

        Simulation = Simulation.Create(_bufferPool, _narrowPhaseCallbacks, _poseIntegratorCallbacks,
            new SolveDescription(8, 1));
    }

    public static Body CreateBody(BodyDescription description, IShape shape)
    {
        BodyInertia inertia = shape.CalculateInertia(description.Mass);
        TypedIndex index = shape.AddToSimulation(Simulation);
        
        switch (description.BodyType)
        {
            case BodyType.Dynamic:
            {
                BepuPhysics.BodyDescription desc = BepuPhysics.BodyDescription.CreateDynamic(
                    new RigidPose(description.Position, description.Rotation), inertia, index,
                    new BodyActivityDescription(0.01f));

                BodyHandle handle = Simulation.Bodies.Add(desc);
                return new Body(new CollidableReference(CollidableMobility.Dynamic, handle));
            }
            
            case BodyType.Kinematic:
                throw new NotImplementedException();
            
            case BodyType.Static:
                throw new NotImplementedException();
            
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public static void Tick(float dt)
    {
        Simulation.Timestep(dt, _threadDispatcher);
    }
}