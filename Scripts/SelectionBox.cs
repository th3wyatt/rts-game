using Godot;
using Godot.Collections;
using System;
using System.Diagnostics;
using System.Linq;

public partial class SelectionBox : Node2D
{
    [Export] private NinePatchRect ninePatchRect;

    private bool isSelecting = false;
    private Vector2 selectionStart;
    private Rect2 selectionRect;

        private PlayerUnit selectedUnit;


    public override void _Process(double delta)
    {
        if (isSelecting)
        {
            Vector2 mousePosition = GetGlobalMousePosition(); 
            selectionRect = new Rect2(selectionStart, mousePosition - selectionStart).Abs();
            ninePatchRect.Position = selectionRect.Position;
            ninePatchRect.Size = selectionRect.Size;
        }
    }

    public void StartSelection()
    {
        isSelecting = true;
        selectionStart = GetGlobalMousePosition();
        ninePatchRect.Position = selectionStart;
        ninePatchRect.Size = new Vector2();
        
    }

    public void EndSelection()
    {
        if(isSelecting)
        {
            isSelecting = false;
            ninePatchRect.Visible = false;
            //TrySelectUnit();
            ProcessSelection();
        }

    }

    private void ProcessSelection()
    {
        PhysicsDirectSpaceState2D space = GetWorld2D().DirectSpaceState;
        PhysicsPointQueryParameters2D query = new();
        query.Position = GetGlobalMousePosition();
        Array<Dictionary> intersection = space.IntersectPoint(query, 1);

        if (intersection.Count > 0)
        {
            GodotObject selectedObject = intersection[0]["collider"].AsGodotObject();
            GD.Print("Thing Clicked: " + selectedObject.GetType().BaseType);
            switch (selectedObject)
            {
                case PlayerBuildingBase buildingBase:
                    TrySelectItem(buildingBase);
                    break;
                case PlayerUnit playerUnit:
                    TrySelectItem(playerUnit);
                    break;

                default:
                    GD.Print("Nothing fit in seleciton categories");
                    break;

            }
        }
        ProcessSelectionBox();
        
    }

    private void ProcessSelectionBox()
    {
        foreach (Unit item in GetTree().GetNodesInGroup(GameConstants.UNITS).Cast<Unit>())
        {
            if (selectionRect.HasPoint(item.GlobalPosition))
            {
                item.AddToGroup(GameConstants.SELECTED_UNITS);
            }
        }
    }

    public void DragSelectionBox()
    {
        
        if (isSelecting)
        {
            if (selectionRect.Size.Length() > 32)
            {
                ninePatchRect.Visible = true;
            }else{
                ninePatchRect.Visible = false;
            }
        }
    }

    private void TrySelectItem(Node item)
    {
       
        item?.AddToGroup(GameConstants.SELECTED_UNITS); // for single unit selection

    }

    public Unit GetSelectedUnit()
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

}
