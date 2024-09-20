using Godot;

namespace wizardgame.scripts
{
    public partial class Bar : ProgressBar
    {
        public Color colour;
        public ProgressBar DmgBar;
        public Label Txt;
        public bool timing = false;
        public float damageTimer;
        public float damageTimerMax = 1.5f;
        public new float MaxValue;
        private float _amount;
        public float Amount
        {
            get { return _amount; }
            set
            {
                _amount = value;
                Txt.Text = ((int)value).ToString();
                Value = (value / MaxValue * 100);
                timing = true;
            }
        }
        public override void _Ready()
        {
            damageTimer = 0;
            DmgBar = GetChild<ProgressBar>(1);
            Txt = GetChild<Label>(2);
        }
        public override void _Process(double delta)
        {
            // TODO: consider resetting the timer when more damage is taken

            if (timing)
            {
                damageTimer += (float)delta;
            }
            if (damageTimer > damageTimerMax)
            {
                timing = false;
                damageTimer = 0;
                DmgBar.Value = Amount / MaxValue * 100;
            }
        }
    }
}
