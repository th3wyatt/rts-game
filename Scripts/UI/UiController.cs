using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class UiController : CanvasLayer
{
    [Export] public PanelContainer SelectionPanel {get; private set;}
    [Export] public HBoxContainer unitSelectionGrid {get; private set;}

    private Dictionary<ContainerType, UiContainer> containers;

    public override void _Ready()
    {
        containers = GetChildren()
            .Where((element) => element is UiContainer)
            .Cast<UiContainer>()
            .ToDictionary((element) => element.container);

        containers[ContainerType.TopPanel].ButtonNode.Pressed += HandleCardsPressed;
        containers[ContainerType.Cards].ButtonNode.Pressed += HandleCardsClosePressed;
    }

    private void HandleCardsClosePressed()
    {
        containers[ContainerType.Cards].Visible = false;
    }


    private void HandleCardsPressed()
    {
        containers[ContainerType.Cards].Visible = true;
    }

}
