using Godot;
using System;

public class Enemy : KinematicBody
{
    [Export]
    public float minDistShot = 500;
    [Export]
    public float shotInterval = 1F;
    [Export]
    public float speedRotation = 2;
    [Export]
    public float speedMove = 5;
    [Export]
    public int life = 90;

    protected ulong lastTimeShot = 0;
    protected Spatial player;
    protected PackedScene arrowPacked;
    protected float lerpRotate;

    public override void _Ready()
    {
        arrowPacked = ResourceLoader.Load<PackedScene>("res://enemies/Arraw.tscn");
        player = (Spatial)GetParent().FindNode("Player");
    }


    public override void _PhysicsProcess(float delta)
    {
        base._PhysicsProcess(delta);

        if (RotateToPlayer(delta))
        {
            if (OS.GetTicksUsec() - lastTimeShot > shotInterval * 1000000)
            {
                if ((GetPlayerPosition() - Translation).LengthSquared() < minDistShot)
                {
                    Shot(GetPlayerPosition());
                }
                else
                {
                    Move();
                }
            }
        }

        MoveDown();
    }

    private void MoveDown()
    {
        MoveAndCollide(Vector3.Down * 1);
    }

    private void Move()
    {
        MoveAndSlide((GetPlayerPosition() - Translation).Normalized() * speedMove, Vector3.Up);
    }

    private bool RotateToPlayer(float delta)
    {
        Vector3 pp = GetPlayerPosition();
        float atan = Mathf.Atan2(pp.z - Translation.z, pp.x - Translation.x);
        Rotation = new Vector3(Rotation.x, -atan - Mathf.Pi * 0.5F, Rotation.z);
        return true;
    }

    private Spatial GetPalyer()
    {
        return (Spatial)player.Call("GetPlayer");
    }

    protected virtual void Shot(Vector3 pos)
    {
        RigidBody arrow = arrowPacked.Instance<RigidBody>();
        Spatial arrowPosition = (Spatial)FindNode("PositionArrow");
        arrow.Translation = arrowPosition.ToGlobal(Vector3.Zero);

        Vector3 pp = GetPlayerPosition();
        float atan = Mathf.Atan2(pp.z - Translation.z, pp.x - Translation.x);
        arrow.Rotation = new Vector3(Rotation.x, -atan, Rotation.z);

        arrow.ApplyCentralImpulse(new Vector3(Mathf.Cos(atan), 0, Mathf.Sin(atan)) * Mathf.Pow(pp.DistanceTo(Translation), 1.1F));

        GetParent().AddChild(arrow);

        lastTimeShot = OS.GetTicksUsec();
    }


    public void OnHit(int damage)
    {
        life -= damage;
        if(life <= 0)
        {
            QueueFree();
        }
    }

    public Vector3 GetPlayerPosition()
    {
        return (Vector3)player.Call("GetPlayerPosition");
    }

    public Vector3 GetPlayerRotation()
    {
        return (Vector3)player.Call("GetPlayerRotation");
    }


    private Basis GetPlayerBasis()
    {
        return (Basis)player.Call("GetPlayerBasis");
    }
}
