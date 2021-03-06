
using Godot;
using System;

public class Arrow : KinematicBody
{
    public Vector3 moveVelocity = new Vector3();


    [Export]
    float damage = 25f; 

    private void Movement()
    {
        

        float moveSpeed = 25f;


        moveVelocity = MoveAndSlide(moveVelocity.Normalized() * moveSpeed);



    }

    public override void _Ready()
    {
        GetNode<Area>("DamageArea").Connect("body_entered", this, "_OnEnemyHitBody");
        GetNode<Area>("DamageArea").Connect("area_entered", this, "_OnEnemyHit");
        GetNode<Timer>("Timer").Connect("timeout", this, "_OnKill");
    }



    private void _OnKill()
    {
        QueueFree();
    }

    //***********************************************
    private void _OnEnemyHitBody(Node body)
    {
        if (body.HasMethod("OnHit"))
        {
            body.Call("OnHit", damage);
        }

        QueueFree();
    }
    //***********************************************

    private void _OnEnemyHit(Area area)
    {
        if (area.IsInGroup("Enemy"))
        {
            QueueFree();
        }
    }

    public override void _PhysicsProcess(float delta)
    {

        Movement();
        base._PhysicsProcess(delta);
    }
}
