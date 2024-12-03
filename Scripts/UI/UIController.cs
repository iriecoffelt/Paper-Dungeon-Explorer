using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class UIController : Control
{
    private Dictionary<ContainerType, UIContainer> containers;
    private Dictionary<ContainerType, UICanvasLayer> layers;

    private bool canPause = false;

    public override void _Ready()
    {
        containers = GetChildren().Where((element) => element is UIContainer).Cast<UIContainer>().ToDictionary((element) => element.container);
        layers = GetChildren().Where((element) => element is UICanvasLayer).Cast<UICanvasLayer>().ToDictionary((element) => element.layer);

        containers[ContainerType.Start].Visible = true;
        containers[ContainerType.Start].ButtonNode.Pressed += HandleStartPressed;
        GameEvents.OnEndGame += HandleEndGame;
        GameEvents.OnVictory += HandleVictory;
        layers[ContainerType.Buttons].Visible = false;
        containers[ContainerType.Pause].ButtonNode.Pressed += HandlePausePressed;
    }

    private void HandlePausePressed()
    {
        GetTree().Paused = false;
        containers[ContainerType.Pause].Visible = false;
        containers[ContainerType.Stats].Visible = true;
        layers[ContainerType.Move].Visible = true;
        layers[ContainerType.Buttons].Visible = true;
    }

    public override void _Input(InputEvent @event)
    {
        if (!canPause)
        {
            return;
        }
        if (!Input.IsActionJustPressed(GameConstants.INPUT_PAUSE))
        {
            return;
        }
        containers[ContainerType.Stats].Visible = GetTree().Paused;
        layers[ContainerType.Move].Visible = GetTree().Paused;
        layers[ContainerType.Buttons].Visible = GetTree().Paused;
        GetTree().Paused = !GetTree().Paused;
        containers[ContainerType.Pause].Visible = GetTree().Paused;
    }

    private void HandleVictory()
    {
        canPause = false;
        containers[ContainerType.Stats].Visible = false;
        layers[ContainerType.Move].Visible = false;
        layers[ContainerType.Buttons].Visible = false;
        containers[ContainerType.Victory].Visible = true;
        GetTree().Paused = true;
    }

    private void HandleEndGame()
    {
        canPause = false;
        containers[ContainerType.Stats].Visible = false;
        layers[ContainerType.Move].Visible = false;
        layers[ContainerType.Buttons].Visible = false;
        containers[ContainerType.Defeat].Visible = true;
    }

    private void HandleStartPressed()
    {
        canPause = true;
        GetTree().Paused = false;
        containers[ContainerType.Start].Visible = false;
        containers[ContainerType.Stats].Visible = true;
        layers[ContainerType.Move].Visible = true;
        layers[ContainerType.Buttons].Visible = true;
        GameEvents.RaiseStartGame();
    }
}
