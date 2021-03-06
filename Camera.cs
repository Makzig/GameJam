using Godot;
using System;

public class Camera : Spatial
{

    [Export]
    public float _speedMove = 10;
    [Export]
    public float _speedRotate = 1;
    [Export]
    public float _speedZoom = 1;


    public override void _Ready()
    {
        base._Ready();
    }

    private Spatial FindPlayer()
    {
        return (Spatial)GetParent().FindNode("Melee_Fighter");
    }

    public override void _Process(float delta)
    {
        base._Process(delta);


        float rotate = Input.GetAxis("rot_left", "rot_right");
        if (rotate != 0)
        {
            RotateY(rotate * _speedRotate * delta);
        }

    }

    public override void _Input(InputEvent @event)
    {
        base._Input(@event);


        float zoom = 0;
        if (@event.IsActionPressed("zoom_in"))
        {
            zoom = -1 * _speedZoom;
        }
        else if (@event.IsActionPressed("zoom_out"))
        {
            zoom = 1 * _speedZoom;
        }

        if (zoom != 0)
        {
            GetChild<Spatial>(0).GetChild<Spatial>(0).Translate(new Vector3(0, 0, zoom));
        }
    }
}
