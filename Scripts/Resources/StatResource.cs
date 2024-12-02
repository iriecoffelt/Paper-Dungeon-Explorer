using System;
using Godot;

[GlobalClass] //tells godot to treat this as a global resource
public partial class StatResource : Resource
{
    public event Action OnZero;
    public event Action OnUpdate;
    [Export] public Stat StatType { get; private set; }

    private float _statValue;
    [Export] public float StatValue 
    { 
        get => _statValue;
        set 
        {
            _statValue = Mathf.Clamp( value, 0, Mathf.Inf); //clamps the value between 0 and infinity
            OnUpdate?.Invoke();
            if (_statValue == 0)
            {
                OnZero?.Invoke();
            }
        } 
    }
}