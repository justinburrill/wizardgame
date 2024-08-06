using Godot;
using System;

public partial class HealthBar : ProgressBar
{
    private float _health;
    public float Health
    {
        get { return _health; }
        set
        {
            _health = value;
            Txt.Text = value.ToString();
            Value = value / MaxHealthValue * 100;
        }
    }
    ProgressBar DmgBar;
    Label Txt;
    Timer timer;
    public float MaxHealthValue;

    public override void _Ready()
    {
        timer = new Timer();
        DmgBar = GetChild<ProgressBar>(0);
        Txt = GetChild<Label>(1);
    }

    public override void _Process(double delta)
    {
    }
}
