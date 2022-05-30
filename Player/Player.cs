using Godot;
using System;

public class Player : KinematicBody
{
    private void Movement()
    {
        Vector3 moveVelocity = Vector3.Zero;

        Vector2 inputVelocity = Input.GetVector("Move_left", "Move_right", "Move_up", "Move_down");

        float speed = 10f;

        moveVelocity = new Vector3(inputVelocity.x, 0, inputVelocity.y).Normalized();

        moveVelocity = MoveAndSlide(moveVelocity * speed);

    }

    public override void _PhysicsProcess(float delta)
    {
        base._PhysicsProcess(delta);
        Movement();
    }

    public override void _Ready()
    {
        
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
