using System;
using Godot;

public abstract partial class EnemyState : CharacterState
{
    protected Vector3 destination;

    public override void _Ready()
    {
        base._Ready();
        characterNode.GetStatResource(Stat.Health).OnZero += HandleZeroHealth;
    }
    protected Vector3 GetPointGlobalPosition(int index) 
    {
        Vector3 localPosition = characterNode.PathNode.Curve.GetPointPosition(index);
        Vector3 globalPosition = characterNode.PathNode.GlobalPosition;
        return localPosition + globalPosition;
    }

    protected void Move()
    {
        characterNode.Agent3DNode.GetNextPathPosition();
        characterNode.Velocity = characterNode.GlobalPosition.DirectionTo(destination);

        characterNode.MoveAndSlide();
        characterNode.Flip();
    }

    protected void HandleChaseAreaBodyEntered(Node3D body)
    {
        characterNode.StateMachineNode.SwitchState<EnemyChaseState>();
    }

    private void HandleZeroHealth()
    {
        characterNode.StateMachineNode.SwitchState<EnemyDeathState>();
    }

}