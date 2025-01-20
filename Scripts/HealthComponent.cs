using Godot;
using System;

public partial class HealthComponent : Node
{
    public event Action OnZero;
    public event Action OnUpdate;
    
    private float _currentHealth = 100;
    private float _maxHealth = 100;
    [Export] public float CurrentHealth
    {
        get =>_currentHealth;
        set
        {
            _currentHealth = Math.Clamp(value, 0, _maxHealth);

            OnUpdate?.Invoke();
            
            if(_currentHealth == 0)
            {
                OnZero?.Invoke();
                GameEvents.RaiseUnitDied();
            }

        }
    }
    [Export] public float MaxHealth
    {
        get => _maxHealth;
        set
        {
            _currentHealth = Math.Clamp(value, 0, Mathf.Inf);

        }
    }

    public void Damage(float damageToTake)
    {
        
        CurrentHealth -= damageToTake;

        if (CurrentHealth <= 0)
        {
            GameEvents.RaiseUnitDied();
            GetOwner().QueueFree();
        }
    }
}
