using Godot;
//using System;

public class Ranger : Player
{
    bool isShooting = false;
    bool shootReload = false;


    Position3D shootPosition = null;
    float shootSpeed = 0.25f;



    CollisionShape playerShape = null;


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
            //AddChild(bullet.Instance());
            KinematicBody bullet_new = (KinematicBody) bullet.Instance();


            GetParent().AddChild(bullet_new);
            
            GetNode<Timer>("ShootReload").Start();

            

            var pos = GetNode<Position3D>("CollisionShape/ShootPos");

           

            bullet_new.Translation = pos.ToGlobal(Vector3.Zero);

            Vector3 directionBullet = new Vector3(bullet_new.Translation.x - playerShape.Translation.x, 0 , bullet_new.Translation.z - playerShape.Translation.z);

            float directionAngle = Mathf.Deg2Rad(playerShape.RotationDegrees.y);

            bullet_new.RotateY(directionAngle);
            bullet_new.Set("moveVelocity", directionBullet.Normalized() * 25f);
            //GD.Print(RotationDegrees.y);
            //bullet<Spatial>
        }

        
    }
    public override void _PhysicsProcess(float delta)
    {
        Shoot();

        base._PhysicsProcess(delta);
    }
    public override void _Ready()
    {
        ConnectSignalAndOnready();
    }
    private void _OnShootReload()
    {
        shootReload = false;

    }

    private void ConnectSignalAndOnready()
    {



        GetNode<Timer>("ShootReload").Connect("timeout", this, "_OnShootReload");
        GetNode<Timer>("ShootReload").WaitTime = shootSpeed;


        playerShape = GetNode<CollisionShape>("CollisionShape");
        shootPosition = GetNode<Position3D>("CollisionShape/ShootPos");



    }


}
