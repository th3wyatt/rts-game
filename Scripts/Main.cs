using Godot;
using System;
using Godot.Collections;
using System.Linq;
using System.Collections;


public partial class Main : Node2D
{
    private PlayerUnit selectedUnit;
    [Export] public Node players;
    [Export] public Node enemies;
    [Export] private UiController uiController;
    [Export] private SelectionBox selectionBox;
    

    public override void _Ready()
    {
        base._Ready();
        
    }

    public override void _Input(InputEvent @event)
    {
        if(Input.IsActionJustPressed(GameConstants.SELECT_UNIT))
        {
            selectionBox.Visible = true;
            selectionBox.StartSelection();
            
        } else if (Input.IsActionJustPressed(GameConstants.COMMAND_UNIT))
        {
            TryCommandUnit();

        } else if (@event is InputEventMouseMotion)
        {
            selectionBox.DragSelectionBox();
            
        } else if(Input.IsActionJustReleased(GameConstants.SELECT_UNIT)){

            UnselectUnits();
            selectionBox.EndSelection();
            SelectUnits();
        }
        
    }

    private void SelectUnits()
    {
        Array<Node> selectedUnits = GetTree().GetNodesInGroup(GameConstants.SELECTED_UNITS);
        
        if(selectedUnits.Count > 0)
        {
            foreach (Node item in selectedUnits)
            {
                switch (item)
                {
                    case PlayerBuildingBase building:
                        building.ToggleSelectionVisual();
                        break;
                    case PlayerUnit unit:
                        unit.ToggleSelectionVisual();
                        uiController.unitSelectionGrid.AddChild(unit.uiSelectionPanel);
                        uiController.SelectionPanel.Visible = true;
                        break;
                    default:
                        GD.Print("could not add selected visual to this unit");
                        break;
                }
              
            }
        } 
    }

    private void UnselectUnits()
    {
        Array<Node> selectedUnits = GetTree().GetNodesInGroup(GameConstants.SELECTED_UNITS);
        
        if(selectedUnits.Count > 0)
        {
            foreach (Node item in selectedUnits)
            {
                try
                {
                    PlayerUnit unit = (PlayerUnit)item;
                    unit.ToggleSelectionVisual();
                    unit.RemoveFromGroup(GameConstants.SELECTED_UNITS);

                }
                catch (System.Exception)
                {
                    
                    GD.Print("could not remove selected visual to this unit");
                }
              
            }
            foreach (Node item in uiController.unitSelectionGrid.GetChildren())
            {
                uiController.unitSelectionGrid.RemoveChild(item);
            }
            uiController.SelectionPanel.Visible = false;
        }
        uiController.SelectionPanel.Visible = false;

    }

    private void TryCommandUnit()
    {
        Array<Node> selectedUnits = GetTree().GetNodesInGroup(GameConstants.SELECTED_UNITS);
        
        if(selectedUnits.Count > 0)
        {
            foreach (Node item in selectedUnits)
            {
                try
                {
                    Unit target = selectionBox.GetSelectedUnit();
                    PlayerUnit unit = (PlayerUnit)item;
                    if (target != null && !target.isPlayer )
                    {
                        GD.Print("targeting NPC!");
                        unit.SetTarget(target);

                    } else
                    {
                        GD.Print("Enemy not selected, moving to position");
                        unit.MoveToLocation(GetGlobalMousePosition());
                    }
                }
                catch (System.Exception)
                {
                    
                    GD.Print("could not remove selected visual to this unit");
                }
              
            }
        }
       
    }
}
