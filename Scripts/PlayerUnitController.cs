using Godot;
using System;
using System.Linq;

public partial class PlayerUnitController : Node
{
    [Export] private StatResource[] stats;

    public override void _Ready()
    {
        GameEvents.OnStatCardPressed += HandleStatCardPressed;
        
    }

    private void HandleStatCardPressed(StatResource resource)
    {
        GD.Print("Upgrading " + resource.Stat);
        StatResource statToChange = GetStatResource(resource.Stat);
        if (statToChange != null)
        {
            GD.Print("Starting Stat: " + statToChange.StatValue);
            GD.Print("Stat being upgraded by " + resource.StatValue);
            statToChange.StatValue += statToChange.StatValue * resource.StatValue;
            GD.Print("New Stat Value: " + statToChange.StatValue);
        }
        else
        {
            GD.Print("Couldn't upgrade stat");
        }        
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
