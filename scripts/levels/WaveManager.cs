using Godot;
using System;
using System.Collections.Generic;
using wizardgame.characters;
using wizardgame.hud;
using wizardgame.utils;

namespace wizardgame.levels
{
    public class WaveManager
    {
        protected string WaveInfoText = "";
        protected Level level;
        protected Player player;
        protected List<Wave> waves;
        protected int WaveIndex { get; set; }
        protected Wave CurrentWave => waves[WaveIndex];
        protected event DebugTextUpdateHandler DebugTextUpdateEvent;
        public WaveManager(Level level, Player player, IEnumerable<Wave> waves)
        {
            WaveIndex = 0;
            this.level = level;
            this.waves = new List<Wave>(waves);
            this.player = player;
            DebugTextUpdateEvent += player.OnDebugTextEvent;
            foreach (Wave wave in waves)
            {
                wave.WaveFinished += OnWaveFinished;
            }
        }
        public WaveManager(Level level, Player player)
        {
            WaveIndex = 0;
            this.player = player;
            this.level = level;
            waves = new List<Wave>();
        }

        public void StartWaves()
        {
            WaveIndex = 0;
            NextWave();
        }

        private void OnWaveFinished(Wave wave)
        {
            NextWave();
        }

        private bool NextWave()
        {
            if (WaveIndex < waves.Count)
            {
                waves[WaveIndex].SpawnWave(level);
                UpdateWaveInfo();
                WaveIndex++;
                return true;
            }
            else
            {
                return false;
            }
        }

        //public void SpawnWaveInfo()
        //{
        //var info = new Label();
        //var name = "WaveInfoText";
        //if (level.HasNode(name)) // remove old label if it exists
        //{
        //    level.RemoveChild(level.GetNode<Label>(name));
        //}
        //info.Name = name;
        //level.AddChild(info);
        //info.Position = new Vector2(5, 5);
        //waveInfo = info;
        //UpdateWaveInfo();
        //}

        public void UpdateWaveInfo()
        {
            var newText = $"Enemies {CurrentWave.EnemiesLeft}/{CurrentWave.MaxEnemies}\nWave {WaveIndex + 1}/{waves.Count}";

            DebugTextUpdateEvent?.Invoke(this, new DebugTextUpdateArgs { Text = newText, OldText = WaveInfoText, Action = TextAction.Replace });

            WaveInfoText = newText;
        }



    }
}
