using Godot;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.PortableExecutable;

public partial class PlayerUnit : Unit
{
    [Export] private PackedScene uiSelectionItem;
    [Export] private Sprite2D selectionVisualSprite;
    [Export] public PlayerUnitController PlayerController{get; private set;}
    [Export] protected PlayerStatComponent PlayerStatComponent;
    public UiUnitSelection uiSelectionPanel;

    public override void _Ready()
    {
        base._Ready();
        uiSelectionPanel = uiSelectionItem.Instantiate<UiUnitSelection>();
        healthBar.ValueChanged += HandleHealthBarValueChanged;
        uiSelectionPanel.texture.Texture = sprite.Texture;
        uiSelectionPanel.healthLabel.Text = healthComponent.CurrentHealth.ToString();
          
    }

    

    private void HandleHealthBarValueChanged(double value)
    {
        uiSelectionPanel.healthLabel.Text = healthComponent.CurrentHealth.ToString();
    }

    public void ToggleSelectionVisual()
    {
        selectionVisualSprite.Visible = !selectionVisualSprite.Visible;
    }

    
}
