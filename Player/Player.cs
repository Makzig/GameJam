using Godot;
using System;

public class Player : KinematicBody
{

    [Export]
    float health = 100f;

    float rayRotate = 0f;

    Spatial camera;

    CollisionShape collisionRotate = null;
    public void Movement()
    {
        Vector3 moveVelocity = Vector3.Zero;

        Vector2 inputVelocity = Input.GetVector("Move_left", "Move_right", "Move_up", "Move_down").Rotated(-camera.Rotation.y);

        float speed = 10f;

        moveVelocity = new Vector3(inputVelocity.x, 0, inputVelocity.y).Normalized();

        moveVelocity = MoveAndSlide(moveVelocity * speed);

    }

    public override void _PhysicsProcess(float delta)
    {
        base._PhysicsProcess(delta);
        Movement();

        GetNode<CollisionShape>("CollisionShape").RotationDegrees = new Vector3(0, GetNode<Spatial>("LookRay/Ray").RotationDegrees.y, 0);
       
        
    }




    public override void _Ready()
    {
        //GD.Print(GetNode("LookRay/translate_rot_y/rot_x/translate_z"));
        camera = GetNode<Spatial>("LookRay/translate_rot_y");

        GetNode<Area>("CollisionShape/HealthBox").Connect("area_entered", this, "_OnHit");
        rayRotate = (GetNode<Spatial>("LookRay/Ray").RotationDegrees.y);
        collisionRotate = GetNode<CollisionShape>("CollisionShape");
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
