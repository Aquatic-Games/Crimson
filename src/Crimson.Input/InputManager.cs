using System.Numerics;
using Crimson.Platform;

namespace Crimson.Input;

public class InputManager
{
    private readonly HashSet<Key> _keysDown;
    private readonly HashSet<Key> _keysPressed;

    private readonly HashSet<MouseButton> _buttonsDown;
    private readonly HashSet<MouseButton> _buttonsPressed;

    private Vector2 _mousePosition;
    private Vector2 _mouseDelta;
    private Vector2 _scrollDelta;

    public Vector2 MousePosition => _mousePosition;

    public Vector2 MouseDelta => _mouseDelta;

    public Vector2 ScrollDelta => _scrollDelta;
    
    public InputManager(EventsManager events)
    {
        _keysDown = [];
        _keysPressed = [];
        _buttonsDown = [];
        _buttonsPressed = [];
        
        events.KeyDown += OnKeyDown;
        events.KeyUp += OnKeyUp;
        events.MouseButtonDown += OnMouseButtonDown;
        events.MouseButtonUp += OnMouseButtonUp;
        events.MouseMove += OnMouseMove;
        events.MouseScroll += OnMouseScroll;
    }

    public bool IsKeyDown(Key key)
        => _keysDown.Contains(key);

    public bool IsKeyPressed(Key key)
        => _keysPressed.Contains(key);

    public bool IsMouseButtonDown(MouseButton button)
        => _buttonsDown.Contains(button);

    public bool IsMouseButtonPressed(MouseButton button)
        => _buttonsPressed.Contains(button);

    public void Update()
    {
        _keysPressed.Clear();
        _buttonsPressed.Clear();
        _mouseDelta = Vector2.Zero;
        _scrollDelta = Vector2.Zero;
    }
    
    private void OnKeyDown(Key key)
    {
        _keysDown.Add(key);
        _keysPressed.Add(key);
    }

    private void OnKeyUp(Key key)
    {
        _keysDown.Remove(key);
        _keysPressed.Remove(key);
    }
    
    private void OnMouseButtonDown(MouseButton button)
    {
        _buttonsDown.Add(button);
        _buttonsPressed.Add(button);
    }
    
    private void OnMouseButtonUp(MouseButton button)
    {
        _buttonsDown.Remove(button);
        _buttonsPressed.Remove(button);
    }
    
    private void OnMouseMove(Vector2 position, Vector2 delta)
    {
        _mousePosition = position;
        _mouseDelta += delta;
    }
    
    private void OnMouseScroll(Vector2 scroll)
    {
        _scrollDelta += scroll;
    }
}