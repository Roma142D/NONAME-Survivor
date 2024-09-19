using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RomaDoliba.Enemy
{
    [CreateAssetMenu(fileName = "EnemiesGroup", menuName = "Enemy/EnemiesGroup", order = 1)]
    public class EnemyGroupControler : ScriptableObject
    {
        [SerializeField] private EnemyMovement _enemyPrefab;
        private List<EnemyMovement> _enemiesGroup;

        private void Start()
        {
            _enemiesGroup = new List<EnemyMovement>();
        }
        public List<EnemyMovement> SpawnEnemies(Vector3 spawnPoint, int enemiesCount)
        {
            _enemiesGroup.Clear();
            var nextSpawnPosition = spawnPoint;
            for (int i = 0; i < enemiesCount; i++)
            {
                var spawnedEnemy = Instantiate(_enemyPrefab, nextSpawnPosition, Quaternion.identity);
                _enemiesGroup.Add(spawnedEnemy);
                nextSpawnPosition += spawnedEnemy.transform.localScale;
            }
            return _enemiesGroup;
        }

    }
}
