using Godot;
using System.Collections.Generic;

namespace wizardgame.levels
{
    internal class WaveManager
    {
        Label waveInfo;
        List<Wave> waves;
        public Level level;

        private int WaveIndex { get; set; }
        public WaveManager(Level level, IEnumerable<Wave> waves)
        {
            WaveIndex = 0;
            this.level = level;
            this.waves = new List<Wave>(waves);
            foreach (Wave wave in waves)
            {
                wave.WaveFinished += OnWaveFinished;
            }
        }

        private void InitWaves()
        {
            WaveIndex = 0;
            SpawnWaveInfo();
        }

        private void OnWaveFinished(Wave wave)
        {
            NextWave();
        }

        public bool NextWave()
        {
            if (WaveIndex == 0) { InitWaves(); }
            if (WaveIndex < waves.Count)
            {
                waves[WaveIndex].SpawnWave();
                UpdateWaveInfo();
                WaveIndex++;
                return true;
            }
            else
            {
                return false;
            }
        }

        public void SpawnWaveInfo()
        {
            var info = new Label();
            var name = "WaveInfoText";
            if (level.GetNode<Label>(name) is not null) // remove old label if it exists
            {
                level.RemoveChild(level.GetNode<Label>(name));
            }
            info.Name = name;
            level.AddChild(info);
            info.Position = new Vector2(5, 5);
            waveInfo = info;
            UpdateWaveInfo();
        }

        public void UpdateWaveInfo()
        {
            waveInfo.Text = $"Wave {WaveIndex + 1}/{waves.Count}";
        }



    }
}
