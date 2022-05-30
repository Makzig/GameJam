using Godot;
using System;

public class TestEnemy : KinematicBody
{

    public static float UPDATE_PATH_MIN_INTERVAL = 1F;
    public static float UPDATE_PATH_PLAYER_MOVE_DIST = 100;
    public static float MIN_DIST_PATH_POINT = 5;

    enum State
    {
        ATTACK
    }

    private State _state = State.ATTACK;
    private Vector3 _prevPlayerPos = new Vector3();
    private ulong _lastTimUpdatePath = 0;
    private Godot.Collections.Array<Vector3> _path = new Godot.Collections.Array<Vector3>();
    [Export]
    private float _moveSpeed = 10;

    public Vector3 GetPlayerPos()
    {
        return ((Spatial)GetParent().FindNode("Melee_Fighter")).Translation;
    }

    public override void _PhysicsProcess(float delta)
    {
        base._PhysicsProcess(delta);

        UpdatePath();

        Move();
    }

    private void Move()
    {
        if(_path.Count < 1)
        {
            return;
        }

        Vector3 pos = _path[0];
        if(Translation.DistanceSquaredTo(pos) < MIN_DIST_PATH_POINT)
        {
            _path.RemoveAt(0);
            return;
        }

        MoveAndSlide((pos - Translation).Normalized() * _moveSpeed, Vector3.Up);
    }

    private bool UpdatePath()
    {
        if(GetPlayerPos().DistanceSquaredTo(_prevPlayerPos) > UPDATE_PATH_PLAYER_MOVE_DIST)
        {
            FindPath(GetPlayerPos());
            return true;
        }else if(OS.GetTicksUsec() - _lastTimUpdatePath > UPDATE_PATH_MIN_INTERVAL * 1000000)
        {
            FindPath(GetPlayerPos());
            return true;
        }

        return false;
    }

    private void FindPath(Vector3 pos)
    {
        Navigation nav = GetNavigation();

        Vector3[] p = nav.GetSimplePath(Translation, pos);
        _path.Clear();
        foreach(Vector3 point in p)
        {
            _path.Add(point);
        }

        //GD.Print(_path);

        _lastTimUpdatePath = OS.GetTicksUsec();
    }

    private Navigation GetNavigation()
    {
        return (Navigation)GetParent().FindNode("Navigation");
    }
}
