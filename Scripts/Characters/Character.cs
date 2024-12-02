using System;
using Godot;
using System.Linq;

/// <summary>
/// This class represents the player
/// </summary>
public abstract partial class Character : CharacterBody3D
{
    [Export] private StatResource[] stats;

    [ExportGroup("Required Nodes")]
    [Export] internal AnimationPlayer AnimationPlayerNode { get; private set; }
    [Export] internal Sprite3D Sprite3DNode { get; private set; }
    [Export] internal StateMachine StateMachineNode { get; private set; }
    [Export] internal Area3D HurtboxNode { get; private set; }
    [Export] internal Area3D HitboxNode { get; private set; }
    [Export] internal CollisionShape3D HitBoxShapeNode { get; private set; }

    [ExportGroup("AI Nodes")]
    [Export] internal Path3D PathNode { get; private set; }
    [Export] internal NavigationAgent3D Agent3DNode { get; private set; }
    [Export] internal Area3D ChaseAreaNode { get; private set; }
    [Export] internal Area3D AttackAreaNode { get; private set; }

    [ExportGroup("Movement")]
    [Export] public int JumpImpulse  { get; set; } = 30;

    public Vector2 direction = new();
    internal Vector3 targetVelocity = Vector3.Zero;

    public override void _Ready()
    {
        HurtboxNode.AreaEntered += HandleHurtboxEntered;
    }

    public override void _PhysicsProcess(double delta)
    {
        ApplyCharacterGravity(delta);
    }

    private void HandleHurtboxEntered(Area3D area)
    {
        StatResource health = GetStatResource(Stat.Health);
        Character player = area.GetOwner<Character>();
        health.StatValue -= player.GetStatResource(Stat.Strength).StatValue;
    }

    public StatResource GetStatResource(Stat stat)
    {
        StatResource result = stats.Where((element) => element.StatType == stat).FirstOrDefault();
        return result;
    }

    /// <summary>
    /// This function checks the velocity of the player and flips the sprite accordingly
    /// </summary>
    internal void Flip()
    {
        bool isNotMovingHorizontally = Velocity.X == 0;
        bool isMovingLeft = Velocity.X < 0;

        if (isNotMovingHorizontally) {return; }

        Sprite3DNode.FlipH = isMovingLeft;
    }

    public void ToggleHitbox(bool flag)
    {
        HitBoxShapeNode.Disabled = flag;
    }

    public void PlayerJump()
    {
        if (IsOnFloor() && Input.IsActionJustPressed(GameConstants.INPUT_JUMP))
        {
            targetVelocity.Y = JumpImpulse;
        }
    }

    public void CheckJump()
    {
        if (Velocity.Y > 0)
        {
            targetVelocity.Y = 0;
        }
    }

    public void ApplyCharacterGravity(double delta)
    {
        if (!IsOnFloor())
        {
            targetVelocity.Y -= (float)(GameConstants.PLAYER_GRAVITY * delta);   
        }
    }
}
