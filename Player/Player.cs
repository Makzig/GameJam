using Godot;
using System;

public class Player : KinematicBody
{

    [Export]
    float health = 100f;



    public void Movement()
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
        GetNode<Area>("HealthBox").Connect("area_entered", this, "_OnHit");
    }




    public void _OnHit(Area area)
    {
        if(area.IsInGroup("EnemyHit"))
        {
            health -= (float)area.Get("damage");

            if (health <= 0)
            {
                GD.Print("You killing!!!");

            }


        }
       

    }
    

}
