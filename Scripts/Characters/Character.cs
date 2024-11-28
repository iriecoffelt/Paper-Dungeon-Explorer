using Godot;

/// <summary>
/// This class represents the player
/// </summary>
public abstract partial class Character : CharacterBody3D
{
    [ExportGroup("Required Nodes")]
    [Export] internal AnimationPlayer AnimationPlayerNode { get; private set; }
    [Export] internal Sprite3D Sprite3DNode { get; private set; }
    [Export] internal StateMachine StateMachineNode { get; private set; }

    [ExportGroup("AI Nodes")]
    [Export] public Path3D PathNode { get; private set; }
    [Export] public NavigationAgent3D Agent3DNode { get; private set; }

    public Vector2 direction = new();

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
