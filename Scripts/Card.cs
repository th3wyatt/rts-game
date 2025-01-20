using Godot;
using System;

public partial class Card : PanelContainer
{
    [Export] private Label cardName;
    [Export] private Label cardCost;
    [Export] private TextureRect cardPicture;
    [Export] private Label cardText;
}
