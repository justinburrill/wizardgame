using Godot;
using System.Collections.Generic;
using wizardgame.characters;

namespace wizardgame.levels
{
    public class Wave
    {
        List<Enemy> enemies;

        void AddEnemy(Enemy enemy)
        {
            enemies.Add(enemy);
        }

        void AddEnemies(Enemy enemy, int count)
        {
            for (int i = 0; i < count; i++)
            {
                AddEnemy(enemy);
            }
        }

        void SpawnWave()
        {
            foreach (var enemy in enemies)
            {
                enemy.Spawn();
            }
        }
        public bool IsFinished()
        {
            foreach (var enemy in enemies)
            {
                if (enemy.IsAlive)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
