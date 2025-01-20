using Godot;
using System;

public partial class PlayerBuildingBase : BuildingBase
{
    [Export] private Sprite2D selectionVisualSprite;

    public void ToggleSelectionVisual()
    {
        selectionVisualSprite.Visible = !selectionVisualSprite.Visible;
    }

}
