using Godot;
using System;

public partial class TreeResource : ResourceNode
{
    public override void _Ready()
    {
        base._Ready();
        AddToGroup("trees");
    }
}
