using Godot;
using System;
using Godot.Collections;
using System.Linq;


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
            GD.Print("Starting unit selection");
            selectionBox.Visible = true;
            selectionBox.StartSelection();
            
            // TrySelectUnit();

        } else if (Input.IsActionJustPressed(GameConstants.COMMAND_UNIT))
        {
            TryCommandUnit();

        } else if (@event is InputEventMouseMotion)
        {
            selectionBox.DragSelectionBox();
            
        } else if(Input.IsActionJustReleased(GameConstants.SELECT_UNIT)){

            GD.Print("unselecting units");
            UnselectUnits();
            GD.Print("calling EndSeleciton()");
            selectionBox.EndSelection();
            GD.Print("selecting new units");
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
                try
                {
                    PlayerUnit unit = (PlayerUnit)item;
                    unit.ToggleSelectionVisual();
                }
                catch (Exception)
                {
                    
                    GD.Print("could not add selected visual to this unit");
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
