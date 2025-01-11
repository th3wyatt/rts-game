using System;
using Godot;

public partial class ResourceLabel: Label
{
    [Export] private MaterialResource materialResource;

    public override void _Ready()
    {
        materialResource.OnUpdate += HandleMaterialResourceUpdated;

        Text = "0";
    }

    private void HandleMaterialResourceUpdated()
    {
        Text = materialResource.MaterialValue.ToString();
    }
}