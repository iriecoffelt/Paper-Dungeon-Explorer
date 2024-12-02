using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class UIController : Control
{
    private Dictionary<ContainerType, UIContainer> containers;
    private Dictionary<CanvasLayerType, UICanvasLayer> canvasLayers;

    public override void _Ready()
    {
        containers = GetChildren().Where((element) => element is UIContainer).Cast<UIContainer>().ToDictionary((element) => element.container);
        /*canvasLayers = GetChildren().Where((element) => element is UICanvasLayer).Cast<UICanvasLayer>().ToDictionary((element) => element.canvasLayer);
        foreach (UICanvasLayer canvasLayer in GetChildren().Where((element) => element is UICanvasLayer).Cast<UICanvasLayer>())
        {
            GD.Print("Canvas Layer: " + canvasLayer.Name + ", CanvasLayer: " + canvasLayer.canvasLayer);
        }*/


        containers[ContainerType.Start].Visible = true;

        containers[ContainerType.Start].ButtonNode.Pressed += HandleStartPressed;
    }

    private void HandleStartPressed()
    {
        GetTree().Paused = false;
        containers[ContainerType.Start].Visible = false;
        containers[ContainerType.Stats].Visible = true;
        /*canvasLayers[CanvasLayerType.TouchControls].Visible = true;
        canvasLayers[CanvasLayerType.ButtonControls].Visible = true;*/
        GameEvents.RaiseStartGame();
    }
}
