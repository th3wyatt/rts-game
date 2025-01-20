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
        GameEvents.RaiseStatCardPressed(stat);
        GD.Print("raising stat card pressed");
    }
}
