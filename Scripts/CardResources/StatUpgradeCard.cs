using Godot;
using System;

public partial class StatUpgradeCard : Card
{
    [Export] private StatResource stat;

    public override void _Ready()
    {
        base._Ready();
        cardbutton.Pressed += OnCardButtonPressed;
        
    }

    private void OnCardButtonPressed()
    {
        GD.Print("raising stat card pressed");
        GameEvents.RaiseStatCardPressed(stat);
        
    }
}
