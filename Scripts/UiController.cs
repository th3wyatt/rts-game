using Godot;
using System;

public partial class UiController : CanvasLayer
{
    [Export] public PanelContainer SelectionPanel {get; private set;}
    [Export] public HBoxContainer unitSelectionGrid {get; private set;}
}
