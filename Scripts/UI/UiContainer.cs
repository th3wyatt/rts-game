using Godot;
using System;

public partial class UiContainer : Container
{
    [Export] public ContainerType container { get; private set; }

     [Export] public Button ButtonNode { get; private set; }
}