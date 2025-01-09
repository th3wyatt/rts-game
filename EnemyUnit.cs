using Godot;
using System;
using System.Linq;

public partial class EnemyUnit : Unit
{
    [Export] private float detectRange = 100.0f;
    [Export] private Area2D chaseArea;
    private EnemyState state;


    public override void _Ready()
    {
        base._Ready();
        chaseArea.BodyEntered += HandleBodyEntered;
        chaseArea.BodyExited += HandleBodyExited;
        CollisionShape2D collisonShape = (CollisionShape2D)chaseArea.GetChild(0);
        CircleShape2D circle = (CircleShape2D)collisonShape.Shape;
        circle.Radius = detectRange;
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
    }

    protected override void HandleUnitDied()
    {
        if(chaseArea.HasOverlappingBodies())
        {
            SetTarget(chaseArea.GetOverlappingBodies().Where((element) => element is PlayerUnit).Cast<Unit>().FirstOrDefault());
        } else
        {
            SetTarget(null);
        }
    }


    private void HandleBodyExited(Node2D body)
    {
        GD.Print("body exited");
        if(chaseArea.HasOverlappingBodies())
        {
            foreach (CharacterBody2D item in chaseArea.GetOverlappingBodies())
            {
                if(item is PlayerUnit)
                {
                    GD.Print("still more in area: " + item.Name);
                    SetTarget((Unit)item);
                    return;
                }
            }
            
        }
        state = EnemyState.Idle;
        SetTarget(null);
        GD.Print("target left");
    }


    
    private void HandleBodyEntered(Node2D body)
    {
        
        if (body is PlayerUnit)
        {
            state = EnemyState.Chase;
            Unit newTarget = (Unit)body;
            SetTarget(newTarget);
            GD.Print("new target!");

        }
    }
}
