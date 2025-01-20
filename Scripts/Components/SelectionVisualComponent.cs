using Godot;
using System;

public partial class SelectionVisualComponent : Node
{
    [Export] public Sprite2D selectionVisual {get; private set;}
    public void ToggleSelectionVisual()
    {
        selectionVisual.Visible = !selectionVisual.Visible;
    }
}
