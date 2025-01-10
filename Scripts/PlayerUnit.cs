using Godot;
using System;

public partial class PlayerUnit : Unit
{
    [Export] private Sprite2D selectionVisual;

    public void ToggleSelectionVisual()
    {
        selectionVisual.Visible = !selectionVisual.Visible;
    }
}
