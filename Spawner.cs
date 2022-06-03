using Godot;
using System;

public class Spawner : Position3D
{
    enum EnemyType
    {
        One,
        Twoo,
        Three,
    }

    [Export]
    EnemyType enemyType = EnemyType.One;

    [Export]
    float enemySpawnTime = 10;

    private ulong lastEnemtSpawnTime = 0;
    private int bodiesHitSpawner = 0;


    public override void _Ready()
    {
        base._Ready();

    }



    private String GetScenePath(EnemyType type)
    {
        switch (type)
        {
            case EnemyType.One:
                return "res://enemies/Enemy_1.tscn";
            case EnemyType.Twoo:
                return "res://enemies/Enemy_2.tscn";
            case EnemyType.Three:
                return "res://enemies/Enemy_3.tscn";
            default:
                throw new Exception("Bad Enemy type");
        }
    }

    public override void _PhysicsProcess(float delta)
    {
        base._PhysicsProcess(delta);


        if(OS.GetTicksUsec() - lastEnemtSpawnTime > enemySpawnTime * 1000000 && NotBodiesInArea())
        {
            Spawn();
        }
    }

    private bool NotBodiesInArea()
    {
        Area area = GetChild<Area>(0);
        Godot.Collections.Array bodies = area.GetOverlappingBodies();
        return bodies.Count == 0;
    }

    private void Spawn()
    {
        PackedScene scene = ResourceLoader.Load<PackedScene>(GetScenePath(enemyType));
        Spatial enemy = (Spatial)scene.Instance();

        enemy.Translation = ToGlobal(Vector3.Zero);

        GetParent().AddChild(enemy);

        lastEnemtSpawnTime = OS.GetTicksUsec();
    }
}
