using Godot;
using System;

public class Enemy_2 : Enemy
{

    public override void _Ready()
    {
        base._Ready();

        minDistShot = 5;
    }

    protected override void Shot(Vector3 pos)
    {

    }
}
