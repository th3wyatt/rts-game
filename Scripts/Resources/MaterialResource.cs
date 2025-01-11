using System;
using Godot;

[GlobalClass]

public partial class MaterialResource : Resource
{
    public event Action OnMaterialMax;
    public event Action OnUpdate;
    [Export] public MaterialType materialType {get; private set;}
    
    private int _materialValue;
    private int _materialMax;

    [Export] public int MaterialValue
    {
        get =>_materialValue;
        set
        {
            _materialValue = Math.Clamp(value, 0, _materialMax);

            OnUpdate?.Invoke();
            
            if(_materialValue == _materialMax)
            {
                OnMaterialMax?.Invoke();
            }

        }
    }

    [Export] public int MaterialMax
    {
        get => _materialMax;
        set
        {
            _materialMax = value;

            if(_materialMax < 0)
            {
                _materialMax = 0;
            }

        }
    }
}