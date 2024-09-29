using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RomaDoliba.Enemy
{
    [CreateAssetMenu(fileName = "EnemiesGroup", menuName = "Enemy/EnemiesGroup", order = 1)]
    public class EnemyGroupControler : ScriptableObject
    {
        //[SerializeField] private EnemyMovement _enemyPrefab;
        [SerializeField] private EnemyWave _enemyWavesData;
        private List<EnemyMovement> _enemiesGroup;
        
        public List<EnemyMovement> SpawnEnemies(List<Transform> spawnPoint)
        {
            var firstWave = _enemyWavesData.GetFirstWave();
            _enemiesGroup.Clear();
            for (int i = 0; i < firstWave.EnemyAmount; i++)
            {
                var nextSpawnPosition = spawnPoint[Random.Range(0, spawnPoint.Count)].position;
                var ranEnemy = firstWave.EnemiesToSpawn[Random.Range(0, firstWave.EnemiesToSpawn.Count)];
                var spawnedEnemy = Instantiate(ranEnemy, nextSpawnPosition, Quaternion.identity);
                _enemiesGroup.Add(spawnedEnemy);
            }
            return _enemiesGroup;
        }

    }
}
