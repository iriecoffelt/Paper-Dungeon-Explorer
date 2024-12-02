using Godot;
using System;

public partial class PlayerAttackState : PlayerState
{
    [Export] private Timer comboTimerNode;
    private int comboCounter = 1;
    private int maxComboCount = 2;

    public override void _Ready()
    {
        base._Ready();
        comboTimerNode.Timeout += () => comboCounter = 1;
    }

    protected override void EnterState()
    {
        characterNode.AnimationPlayerNode.Play(GameConstants.ANIM_ATTACK + comboCounter, -1, 2.5f);
        characterNode.AnimationPlayerNode.AnimationFinished += HandleAnimationFinished;
    }

    protected override void ExitState()
    {
        characterNode.AnimationPlayerNode.AnimationFinished -= HandleAnimationFinished;
        comboTimerNode.Start();
    }

    private void HandleAnimationFinished(StringName animName)
    {
        comboCounter++;
        comboCounter = Mathf.Wrap(comboCounter, 1, maxComboCount + 1);
        characterNode.ToggleHitbox(true);
        characterNode.StateMachineNode.SwitchState<PlayerIdleState>();
    }

    private void PerformHit()
    {
        Vector3 newPosition = characterNode.Sprite3DNode.FlipH ? Vector3.Left : Vector3.Right; // Get the direction the sprite is facing and set the hit position accordingly
        float distanceMultiplier = 0.75f;
        newPosition *= distanceMultiplier;
        characterNode.HitboxNode.Position = newPosition;
        characterNode.ToggleHitbox(false);
    }
}
