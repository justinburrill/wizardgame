using Godot;
using System;
using System.Collections.Generic;
using wizardgame.characters;

namespace wizardgame.levels
{

    public class Wave
    {
        public event Action<Wave> WaveFinished;

        private List<Enemy> enemies;
        public int EnemiesLeft => enemies.Count;


        public void AddEnemy(Enemy enemy)
        {
            enemy.Died += OnEnemyDied;
            enemies.Add(enemy);
        }

        private void OnEnemyDied(Character enemy)
        {
            if (enemy is not Enemy)
            {
                throw new ArgumentException("enemy in OnEnemyDied is not an Enemy");
            }
            enemies.Remove((Enemy)enemy);
            if (EnemiesLeft <= 0)
            {
                WaveFinished?.Invoke(this);
            }
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

        //public bool IsFinished()
        //{
        //    foreach (var enemy in enemies)
        //    {
        //        if (enemy.IsAlive)
        //        {
        //            return false;
        //        }
        //    }
        //    return true;
        //}
    }
}
