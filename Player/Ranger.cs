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
            isShooting = false;
        }

        base._Input(@event);
    }

    public virtual void Shoot()
    {
        if (isShooting && !shootReload)
        {

            shootReload = true;
            //AddChild(bullet.Instance());
            KinematicBody bullet_new = (KinematicBody)bullet.Instance();


            GetParent().AddChild(bullet_new);

            GetNode<Timer>("ShootReload").Start();



            var pos = GetNode<Position3D>("CollisionShape/ShootPos");



            bullet_new.Translation = pos.ToGlobal(Vector3.Zero);

            Vector3 directionBullet = new Vector3(bullet_new.Translation.x - Translation.x, 0, bullet_new.Translation.z - Translation.z);

            float directionAngle = Mathf.Deg2Rad(playerShape.RotationDegrees.y);

            bullet_new.RotateY(directionAngle);
            bullet_new.Set("moveVelocity", directionBullet.Normalized() * 25f);
        }


    }
    public override void _PhysicsProcess(float delta)
    {
        Shoot();

        base._PhysicsProcess(delta);
    }
    public override void _Ready()
    {
        base._Ready();

        ConnectSignalAndOnready();

       /* Godot.Collections.Dictionary<string, string> d = new Godot.Collections.Dictionary<string, string>();

        d.Add("msaa", "false");

        string v = JSON.Print(d);
        GD.Print(v);
        JSONParseResult dd = JSON.Parse(v);
        Godot.Collections.Dictionary<string, string> dic = new Godot.Collections.Dictionary<string, string>( (Godot.Collections.Dictionary)dd.Result );*/
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


    public Vector3 GetPlayerPosition()
    {
        return ToGlobal(playerShape.Translation);
    }

    public Vector3 GetPlayerRotation()
    {
        return playerShape.Rotation;
    }

    public Basis GetPlayerBasis()
    {
        return playerShape.Transform.basis;
    }

    public Spatial GetPlayer()
    {
        return playerShape;
    }

}
