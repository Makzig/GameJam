using Godot;
using System;

public class GridMap : Godot.GridMap
{
    
    public override void _Ready()
    {
        Clear();
        WorldGen worldGen = new WorldGen(this);
        worldGen.GenerateGround("dark", new AABB(0, 0, 0, 150, 10, 150), 5F);

   
        
    }

}
