using Godot;
using System;

public partial class PlayerVillagerUnit : PlayerUnit
{
    public override void _Ready()
    {
        base._Ready();
        AddToGroup("villagers");
        GameEvents.OnResourceExausted += HandleResourceExausted;
    }

    private void HandleResourceExausted()
    {
        SetTarget(null);
    }

}
