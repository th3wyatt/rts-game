using Godot;
using Godot.Collections;
using System;
using System.Linq;
using System.Net.Http.Headers;

public partial class ResourceNode : StaticBody2D
{

    [Export] MaterialResource resourcePool;
    [Export] Area2D gatherArea;
    [Export] Timer gatherTimer;
    [Export] float amount = 100;
    float gatherRate = 0;
    float gatherAmount = 0;
    private int gathererCount;
    private Array<PlayerVillagerUnit> gatherers = new Array<PlayerVillagerUnit>();

    public override void _Ready()
    {
        AddToGroup("resources");
        gatherArea.BodyEntered += HandleBodyEntered;
        gatherArea.BodyExited += HandleBodyExited;
        gatherTimer.Timeout += HandleTimeout;
        GameEvents.OnStatCardPressed += HandleStatCardPressed;
    }

    private void HandleTimeout()
    {
        if(gatherArea.HasOverlappingBodies())
        {
            amount -= gatherAmount;
            resourcePool.MaterialValue += (int)Math.Round(gatherAmount);
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
            gatherers.Remove((PlayerVillagerUnit)body);
            RecalculateGatherStats();
        }
        else
        {
            gatherTimer.Stop();
        }
        
        
    }


    private void HandleBodyEntered(Node2D body)
    {
        if(body.IsInGroup("villagers"))
        {

            gatherers.Add((PlayerVillagerUnit)body);
            RecalculateGatherStats();
            gatherTimer.WaitTime = gatherRate;
            gatherTimer.Start();
            

        }
    }

    private void HandleStatCardPressed(StatResource resource)
    {
        if (resource.Stat == Stats.GatherAmountWood || resource.Stat == Stats.GatherRateWood)
        {
            RecalculateGatherStats();
        }
    }

    private void RecalculateGatherStats()
    {
        gatherRate = 0;
        gatherAmount = 0;
        gathererCount = 0;
        foreach (PlayerVillagerUnit villager in gatherers)
        {
            gatherAmount += villager.PlayerController.GetStatResource(Stats.GatherAmountWood).StatValue;
            if(gatherRate <= 0)
            {
                gatherRate = villager.PlayerController.GetStatResource(Stats.GatherRateWood).StatValue;
                
            } else
            {
                gatherRate = ((gatherRate * gathererCount) 
                    + villager.PlayerController.GetStatResource(Stats.GatherRateWood).StatValue) / (gathererCount +1);
            }
            gathererCount++;
        }
        GD.Print("Gather Amount: " + gatherAmount);
        GD.Print("Gather Rate: "+ gatherRate);
    }
}
