using Godot;
using System;
using System.Linq;

public partial class ResourceNode : StaticBody2D
{

    [Export] MaterialResource resourcePool;
    [Export] Area2D gatherArea;
    [Export] Timer gatherTimer;
    [Export] int amount = 100;
    [Export] int gatherRate = 10;
    [Export] int gatherTime = 1;
    private int gathererCount;

    public override void _Ready()
    {
        AddToGroup("resources");
        gatherArea.BodyEntered += HandleBodyEntered;
        gatherArea.BodyExited += HandleBodyExited;
        gatherTimer.Timeout += HandleTimeout;
    }

    private void HandleTimeout()
    {
        if(gatherArea.HasOverlappingBodies())
        {
            amount -= gatherRate * gathererCount;
            resourcePool.MaterialValue += gatherRate * gathererCount;
            GD.Print("Gathered " + resourcePool.materialType + " " + amount +" Remaining!");
            
            
            if(amount <= 0)
            {
                GameEvents.RaiseResourceExausted();
                QueueFree();
            }
        } else
        {
            gatherTimer.Stop();
        }
    }


    private void HandleBodyExited(Node2D body)
    {
        if(gathererCount > 0)
        {
            gathererCount--;
            return;
        }
        gatherTimer.Stop();
        
    }


    private void HandleBodyEntered(Node2D body)
    {
        GD.Print(body.Name + " Entered Area!");
        GD.Print("Groups: " + body.GetGroups());
        if(body.IsInGroup("villagers"))
        {
            gathererCount++;
            
            GD.Print("Gatherer added to " + Name);
            gatherTimer.Start();

        }
    }

}
