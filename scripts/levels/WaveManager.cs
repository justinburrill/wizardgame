using Godot;
using System.Collections.Generic;

namespace wizardgame.levels
{
    internal class WaveManager
    {
        List<Wave> waves;
        int waveIndex = 0;

        public void StartWaves()
        {
            while (waveIndex < waves.Count)
            {
                waves[waveIndex].SpawnWave();
            }


        }



    }
}
