using Godot;
using System.Collections.Generic;
using wizardgame.characters;

namespace wizardgame.levels
{
    public class Wave
    {
        List<Enemy> enemies;

        public void AddEnemy(Enemy enemy)
        {
            enemies.Add(enemy);
        }

        public void AddEnemies(Enemy enemy, int count)
        {
            for (int i = 0; i < count; i++)
            {
                AddEnemy(enemy);
            }
        }

        public void SpawnWave()
        {
            foreach (Enemy enemy in enemies)
            {
                enemy.SpawnFromOffScreen();
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
