using Godot;
using System;

[GlobalClass]

public partial class BuildingStatResource : Resource
{
    public event Action OnZero;
    public event Action OnUpdate;
    [Export] public BuildingStat BuildingStatType {get; private set;}

    private float _statValue;

    [Export] public float StatValue
    {
        get => _statValue;
        set
        {
            _statValue = Math.Clamp(value, 0, Mathf.Inf);

            OnUpdate?.Invoke();

            if (_statValue == 0)
            {
                OnZero?.Invoke();
            }
        }
    }
}
