using Godot;
using System;
using Godot.Collections;
using System.Linq;


public partial class Main : Node2D
{
    private PlayerUnit selectedUnit;
    [Export] public Node players;
    [Export] public Node enemies;

    public override void _Ready()
    {
        base._Ready();
        
    }

    public override void _Input(InputEvent @event)
    {
        base._Input(@event);
        if (@event is InputEventMouseButton mouseEvent && @event.IsPressed())
        {
            if (mouseEvent.ButtonIndex == MouseButton.Left)
            {
                TrySelectUnit();
            }else if (mouseEvent.ButtonIndex == MouseButton.Right)
            {
                TryCommandUnit();
            }
        }
    }

    private Unit GetSelectedUnit()
    {
        PhysicsDirectSpaceState2D space = GetWorld2D().DirectSpaceState;
        PhysicsPointQueryParameters2D query = new();
        query.Position = GetGlobalMousePosition();
        Array<Dictionary> intersection = space.IntersectPoint(query, 1);

        try
        {
            if (intersection.Count > 0)
            {
                Unit collider = (Unit)intersection[0]["collider"];
                
                return collider;
            }
        }
        catch (System.Exception)
        {
            GD.Print("Not a selectable Item");
            return null;
        }
        
        return null;
    }

        private void TrySelectUnit()
    {
        Unit unit = (Unit)GetSelectedUnit();

        if (unit != null && unit.isPlayer)
        {
            SelectUnit((PlayerUnit)unit);
        }else
        {
            UnselectUnit();
        }
    }

    private void SelectUnit(PlayerUnit unit)
    {
        UnselectUnit();
        selectedUnit = unit;
        selectedUnit.ToggleSelectionVisual();
    }

    private void UnselectUnit()
    {
        if (selectedUnit != null)
        {
            selectedUnit.ToggleSelectionVisual();
        } 
        selectedUnit = null;
    }

    private void TryCommandUnit()
    {
        if (!IsInstanceValid(selectedUnit)) { return; }

        Unit target = GetSelectedUnit();

        if (target != null && !target.isPlayer )
        {
            GD.Print("targeting NPC!");
            selectedUnit.SetTarget(target);

        } else
        {
            GD.Print("Enemy not selected, moving to position");
            selectedUnit.MoveToLocation(GetGlobalMousePosition());
        }
    }
}
