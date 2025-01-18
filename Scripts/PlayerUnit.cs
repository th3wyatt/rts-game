using Godot;
using System;

public partial class PlayerUnit : Unit
{
    [Export] private Sprite2D selectionVisual;
    [Export] private PackedScene uiSelectionItem;
    public UiUnitSelection uiSelectionPanel;

    public override void _Ready()
    {
        base._Ready();
        GD.Print("UI Panel Scene: " + uiSelectionItem.GetType()+" , "+ uiSelectionItem.ResourceName);
        uiSelectionPanel = uiSelectionItem.Instantiate<UiUnitSelection>();
        healthBar.ValueChanged += OnHealthBarValueChanged;
        uiSelectionPanel.texture.Texture = sprite.Texture;
        uiSelectionPanel.healthLabel.Text = health.ToString();
          
    }

    private void OnHealthBarValueChanged(double value)
    {
        uiSelectionPanel.healthLabel.Text = health.ToString();
    }


    public void ToggleSelectionVisual()
    {
        selectionVisual.Visible = !selectionVisual.Visible;
    }
}
