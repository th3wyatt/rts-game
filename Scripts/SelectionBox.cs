using Godot;
using Godot.Collections;
using System;

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
            GD.Print("updating rect");
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
            GD.Print("End selection start");
            isSelecting = false;
            ninePatchRect.Visible = false;
            TrySelectUnit();
        }

    }

    public void DragSelectionBox()
    {
        
        if (isSelecting)
        {
            GD.Print("Dragging!");
            if (selectionRect.Size.Length() > 32)
            {
                GD.Print("ITS VISIBLE!");
                ninePatchRect.Visible = true;
            }else{
                ninePatchRect.Visible = false;
            }
        }
    }

    private void TrySelectUnit()
    {
       
        GD.Print("trying to get a clicked unit");
        Unit unit = GetSelectedUnit();

        if (unit != null)
        {
            unit.AddToGroup(GameConstants.SELECTED_UNITS);
        }

        foreach (Unit item in GetTree().GetNodesInGroup(GameConstants.UNITS))
        {
            if (selectionRect.HasPoint(item.GlobalPosition))
            {
                item.AddToGroup(GameConstants.SELECTED_UNITS);
            }
        }
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
