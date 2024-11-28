using Godot;
using System;
using System.Reflection.Metadata;

public partial class EnemyPatrolState : EnemyState
{
    [Export] private Timer idleTImerNode;
    [Export(PropertyHint.Range, "0, 10, 0.1")] private float maxIdleTime = 2.5f;
    private int pointIndex = 0;

    protected override void EnterState()
    {
        characterNode.AnimationPlayerNode.Play(GameConstants.ANIM_MOVE);

        pointIndex = 1;

        destination = GetPointGlobalPosition(pointIndex);
        characterNode.Agent3DNode.TargetPosition = destination;

        characterNode.Agent3DNode.NavigationFinished += HandleNavigationFinished;
        idleTImerNode.Timeout += HandleTimeout;
    }

    protected override void ExitState()
    {
        characterNode.Agent3DNode.NavigationFinished -= HandleNavigationFinished;
        idleTImerNode.Timeout -= HandleTimeout;
    }

    public override void _PhysicsProcess(double delta)
    {
        if (!idleTImerNode.IsStopped()) 
        {
            return;
        }
        Move();
    }

    private void HandleNavigationFinished()
    {
        characterNode.AnimationPlayerNode.Play(GameConstants.ANIM_IDLE);

        RandomNumberGenerator rng = new();
        idleTImerNode.WaitTime = rng.RandfRange(0f, maxIdleTime);

        idleTImerNode.Start();
    }

    private void HandleTimeout()
    {
        characterNode.AnimationPlayerNode.Play(GameConstants.ANIM_MOVE);

        pointIndex = Mathf.Wrap(
            pointIndex + 1, 0, characterNode.PathNode.Curve.PointCount
        );

        destination = GetPointGlobalPosition(pointIndex);
        characterNode.Agent3DNode.TargetPosition = destination;
    }
}
