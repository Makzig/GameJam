using Godot;
//using System;

public class Ranger : Player
{
    bool isShooting = false;
    bool shootReload = false;


    Position3D shootPosition = null;
    float shootSpeed = 0.25f;


    [Export]
    PackedScene bullet;



    public override void _Input(InputEvent @event)
    {

        if (@event.IsActionPressed("Attack_Shoot"))
        {
            isShooting = true;
        }
        else if (@event.IsActionReleased("Attack_Shoot"))
        {
            isShooting= false;
        }

        base._Input(@event);
    }

    public virtual void Shoot()
    {
        if (isShooting)
        {

            shootReload = true; 
            AddChild(bullet.Instance());

            GetNode<Timer>("ShootReload").Start();

            bullet.Set("GlobalTranslate", shootPosition);
        }

        
    }
    public override void _PhysicsProcess(float delta)
    {
        Shoot();

        base._PhysicsProcess(delta);
    }
    public override void _Ready()
    {
        GetNode<Timer>("ShootReload").Connect("timeout", this, "_OnShootReload");
        GetNode<Timer>("ShootReload").WaitTime = shootSpeed;
        shootPosition = GetNode<Position3D>("CollisionShape/ShootPos");
    }
    private void _OnShootReload()
    {
        shootReload = false;

    }

}
