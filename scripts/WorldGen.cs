using Godot;
using System;

public class WorldGen : Godot.Object
{
    private GridMap map;

    public WorldGen(GridMap map)
    {
        this.map = map;
    }

    public void GenerateGround(String name, AABB aabb, float seed)
    {
        int item = map.MeshLibrary.FindItemByName(name);
        if (item < 0)
        {
            GD.Print("Not find item: " + name);
            return;
        }
        aabb = aabb.Abs();
        OpenSimplexNoise n = new OpenSimplexNoise();
        //for (int y = 0; y < aabb.Size.y; y++)
        //{
        Vector3 s = new Vector3(aabb.Size)  / 2F;
        for (int z = 0; z < aabb.Size.z; z++)
        {
            for (int x = 0; x < aabb.Size.x; x++)
            {
                float noise = n.GetNoise3d(x, z, seed);
                int h = (int)(((noise + 1) / 2) * aabb.Size.y);
                map.SetCellItem(x - (int)s.x, h - (int)s.y, z - (int)s.z, item);
            }
        }
        //}
    }

}
