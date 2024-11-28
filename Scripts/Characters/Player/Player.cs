using Godot;

/// <summary>
/// This class represents the player
/// </summary>
public partial class Player : Character
{
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
}
