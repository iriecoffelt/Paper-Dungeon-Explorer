using Godot;
using System;

public abstract partial class PlayerState : Node
{
    protected Player characterNode;

    public override void _Ready()
    {
        characterNode = GetOwner<Player>();
        SetPhysicsProcess(false); // disable physics
        SetProcessInput(false); // disable input
    }

    public override void _Notification(int what)
    {
        base._Notification(what);

        if (what == GameConstants.NOTIFICATION_ENTER_STATE)
        {
            EnterState();
            SetPhysicsProcess(true); // enable physics
            SetProcessInput(true); // enable input
        }
        else if (what == GameConstants.NOTIFICATION_EXIT_STATE)
        {
            SetPhysicsProcess(false); // disable physics
            SetProcessInput(false); // disable input
        }
    }

    protected virtual void EnterState() 
    {
    }
}