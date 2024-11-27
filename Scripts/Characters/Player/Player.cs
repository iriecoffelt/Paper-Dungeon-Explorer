using Godot;
using System;

/// <summary>
/// This class represents the player
/// </summary>
public partial class Player : CharacterBody3D
{
    [ExportGroup("Required Nodes")]
    [Export] internal AnimationPlayer AnimationPlayerNode { get; private set; }
    [Export] internal Sprite3D Sprite3DNode { get; private set; }
    [Export] internal StateMachine StateMachineNode { get; private set; }

    public Vector2 direction = new();

    /// <summary>
    /// This function is called when the node enters the scene tree for the first time.
    /// </summary>
    public override void _Ready()
    {
        
    }

    /// <summary>
    /// This function is called on input.
    /// </summary>
    /// <param name="event"></param>
    public override void _Input(InputEvent @event)
    {
        direction = Input.GetVector(
            GameConstants.INPUT_MOVE_LEFT,
            GameConstants.INPUT_MOVE_RIGHT,
            GameConstants.INPUT_MOVE_FORWARD,
            GameConstants.INPUT_MOVE_BACKWARD
        );
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
}
