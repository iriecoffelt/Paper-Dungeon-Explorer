using Godot;
using System;

public partial class UICanvasLayer : CanvasLayer
{
    [Export] public ContainerType layer { get; private set; }
}