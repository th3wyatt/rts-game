using Godot;
using System;
using System.Linq;
using System.Linq.Expressions;

public partial class PlayerUnit : Unit
{
    [Export] private PackedScene uiSelectionItem;
    [Export] private Sprite2D selectionVisualSprite;
    [Export] private StatResource[] stats;
    public UiUnitSelection uiSelectionPanel;

    public override void _Ready()
    {
        base._Ready();
        uiSelectionPanel = uiSelectionItem.Instantiate<UiUnitSelection>();
        healthBar.ValueChanged += HandleHealthBarValueChanged;
        GameEvents.OnStatCardPressed += HandleStatCardPressed;
        uiSelectionPanel.texture.Texture = sprite.Texture;
        uiSelectionPanel.healthLabel.Text = healthComponent.CurrentHealth.ToString();
          
    }

    private void HandleStatCardPressed(StatResource resource)
    {
        StatResource statToChange = GetStatResource(resource.Stat);
        if (statToChange != null)
        {
            statToChange.StatValue += statToChange.StatValue * resource.StatValue;
        }
        else
        {
            GD.Print("Couldn't upgrade stat");
        }        
    }

    private void HandleHealthBarValueChanged(double value)
    {
        uiSelectionPanel.healthLabel.Text = healthComponent.CurrentHealth.ToString();
    }

    public void ToggleSelectionVisual()
    {
        selectionVisualSprite.Visible = !selectionVisualSprite.Visible;
    }

    public StatResource GetStatResource(Stats statResource)
    {
        StatResource resource = stats.Where((element) => statResource == element.Stat)
            .FirstOrDefault();        
        if (resource == null)
        {
            GD.Print("couldn't find stat resource on player");
            return null;
        }
        return resource;
    }
}
