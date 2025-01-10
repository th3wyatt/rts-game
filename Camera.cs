using Godot;
using System;
using System.ComponentModel.DataAnnotations;
using System.Numerics;
using Vector2 = Godot.Vector2;

public partial class Camera : Camera2D
{
    [Export] Camera2D camera;
    [Export,Range(0,1000)] private float movementSpeed = 64;
    [Export,Range(1,5)] private float minZoom = 1;
    [Export,Range(1,5)] private float maxZoom = 3;
    private bool isPanning = false;
    private bool allowZoom = true;
    private Vector2 zoomLevel;
    private Vector2 zoomSpeed = new Vector2(.3f,.3f);

    Vector2 lastMousePosition;

    public override void _Ready()
    {
        zoomLevel = camera.Zoom;
    }
    public override void _Process(double delta)
    {
        if(!isPanning)
        {
            HandleKeyboardMovement(delta);
            if(allowZoom)
            {
                HandleZoom(delta);
            }
        }

    }

    private void HandleZoom(double delta)
    {
        zoomLevel = zoomLevel.Clamp(minZoom, maxZoom);
        camera.Zoom = camera.Zoom.Lerp(zoomLevel, 0.1f);
    }


    public override void _UnhandledInput(InputEvent @event)
    {
        if(Input.IsActionPressed(GameConstants.ZOOM_IN))
        {
            zoomLevel += zoomSpeed;
        } else if(Input.IsActionPressed(GameConstants.ZOOM_OUT)){
            zoomLevel -= zoomSpeed;
        }
    }

    private void HandleKeyboardMovement(double delta)
    {
        Vector2 direction = Vector2.Zero;

        if (Input.IsActionPressed(GameConstants.PAN_LEFT))
        {
            direction.X -= 1;
        }
        if (Input.IsActionPressed(GameConstants.PAN_RIGHT))
        {
            direction.X += 1;
        }
        if (Input.IsActionPressed(GameConstants.PAN_UP))
        {
            direction.Y -= 1;
        }
        if (Input.IsActionPressed(GameConstants.PAN_DOWN))
        {
            direction.Y += 1;
        }
        if(direction != Vector2.Zero)
        {
            direction = direction.Normalized();
            camera.GlobalTranslate(direction * movementSpeed * (float)delta);
        }
    }


}
