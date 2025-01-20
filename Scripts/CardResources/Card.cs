using Godot;
using System;

public partial class Card : PanelContainer
{
    [Export] private Label cardName;
    [Export] private Label cardCostLabel;
    [Export] private TextureRect cardPicture;
    [Export] private Label cardText;
    [Export] private int cardCost;
    [Export] protected Button cardbutton;
}
