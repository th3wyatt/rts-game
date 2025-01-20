using System;
using System.Linq;
using Godot;
using Vector2 = Godot.Vector2;

public partial class Unit : CharacterBody2D
{
    protected Unit target = null;
    [Export] private NavigationAgent2D agent;
    [Export] public Sprite2D sprite {get; private set;}
    [Export] protected ProgressBar healthBar;
    [Export] protected HealthComponent healthComponent;
    [Export] private int damage = 20;
    [Export] private float moveSpeed = 50.0f;
    [Export] private float attackRange = 25.0f;
    [Export] private float attackRate = 0.5f;
    private double lastAttackTime;
    [Export] public bool isPlayer {get; private set;}
    [Export] private Timer flashTimer;
    protected Main GameManager;

    public override void _Ready()
    {
        healthBar.MaxValue = healthComponent.MaxHealth;
        healthBar.Value = healthComponent.CurrentHealth;
        flashTimer.Timeout += HandleTimeout;
        GameManager = (Main)GetTree().Root.GetChild(0);
        target = null;
        GameEvents.OnUnitDied += HandleUnitDied;
        healthComponent.OnUpdate += HandleHealthUpdated;
    

    }

    private void HandleHealthUpdated()
    {
        healthBar.Value = healthComponent.CurrentHealth;
    }


    public override void _Process(double delta)
    {
        TargetCheck();
    }

    public override void _PhysicsProcess(double delta)
    {
        if (agent.IsNavigationFinished()) { return; } 

        Vector2 direction = GlobalPosition.DirectionTo(agent.GetNextPathPosition());
        Velocity = direction * moveSpeed;
        MoveAndSlide();
    }
    
    protected virtual void HandleUnitDied(){}
    
    private void HandleTimeout()
    {
        sprite.Modulate = Colors.White;
    }


    public void SetTarget(Unit newTarget)
    {
        target = newTarget;
    }

    private void TargetCheck()
    {
        if (!IsInstanceValid(target)) { return;}

        float dist = GlobalPosition.DistanceTo(target.GlobalPosition);

        if (dist <= attackRange)
        {
            agent.TargetPosition = GlobalPosition;
            TryAttackTarget();

        }else
        {
                agent.TargetPosition = target.GlobalPosition;
        }
    }

    private void TakeDamage(float damageToTake)
    {
        
        healthComponent.Damage(damageToTake);

        sprite.Modulate = Colors.Red;
        flashTimer.Start();
    }

    private void TryAttackTarget()
    {
        double currentTime = Time.GetUnixTimeFromSystem();

        if (target == null) { return;}

        if (currentTime - lastAttackTime > attackRate)
        {
            target.TakeDamage(damage);
            lastAttackTime = currentTime;
        }
    }

    public void MoveToLocation(Vector2 location)
    {
        target = null;
        agent.TargetPosition = location;
    }

}
