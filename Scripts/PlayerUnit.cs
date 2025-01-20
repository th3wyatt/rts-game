using Godot;
using System;

public partial class PlayerUnit : Unit
{
    [Export] private PackedScene uiSelectionItem;
    [Export] private Sprite2D selectionVisualSprite;
    public UiUnitSelection uiSelectionPanel;

    public override void _Ready()
    {
        base._Ready();
        GD.Print("UI Panel Scene: " + uiSelectionItem.GetType()+" , "+ uiSelectionItem.ResourceName);
        uiSelectionPanel = uiSelectionItem.Instantiate<UiUnitSelection>();
        healthBar.ValueChanged += OnHealthBarValueChanged;
        uiSelectionPanel.texture.Texture = sprite.Texture;
        uiSelectionPanel.healthLabel.Text = healthComponent.CurrentHealth.ToString();
          
    }

    private void OnHealthBarValueChanged(double value)
    {
        uiSelectionPanel.healthLabel.Text = healthComponent.CurrentHealth.ToString();
    }

    public void ToggleSelectionVisual()
    {
        selectionVisualSprite.Visible = !selectionVisualSprite.Visible;
    }
}
