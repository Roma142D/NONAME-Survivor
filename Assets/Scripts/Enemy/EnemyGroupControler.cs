using System.Collections;
using System.Collections.Generic;
using RomaDoliba.Manager;
using UnityEngine;

namespace RomaDoliba.Enemy
{
    [CreateAssetMenu(fileName = "EnemiesGroup", menuName = "Enemy/EnemiesGroup", order = 1)]
    public class EnemyGroupControler : ScriptableObject
    {
        [SerializeField] private EnemyWave _enemyWavesData;
        private List<EnemyMovement> _enemiesGroup;
                
        public List<EnemyMovement> SpawnEnemies(List<Transform> spawnPoint, bool IsStartGame, Transform parent)
        {
            var waveToSpawn = _enemyWavesData.GetNextWave();
            _enemiesGroup = new List<EnemyMovement>();
            if (IsStartGame)
            {
                waveToSpawn = _enemyWavesData.GetFirstWave();
            }
            for (int i = 0; i < waveToSpawn.EnemyAmount; i++)
            {
                var nextSpawnPosition = spawnPoint[Random.Range(0, spawnPoint.Count)].position;
                var ranEnemy = waveToSpawn.EnemiesToSpawn[Random.Range(0, waveToSpawn.EnemiesToSpawn.Count)];
                if (IsStartGame)
                {
                    var spawnedEnemy = Instantiate(ranEnemy, nextSpawnPosition, Quaternion.identity, parent);
                    _enemiesGroup.Add(spawnedEnemy);
                    Debug.Log($"{_enemiesGroup.Count} OnGameStart");
                }
                else
                {
                    if (GameManager.Instance.EnemiesToPool == null || GameManager.Instance.EnemiesToPool.Count < 8)
                    {
                        var spawnEnemy = Instantiate(ranEnemy, nextSpawnPosition, Quaternion.identity, parent);
                        _enemiesGroup.Add(spawnEnemy);
                        Debug.Log($"{_enemiesGroup.Count} OnEnemiesPool<8");
                    }
                    else
                    {
                        EnemyMovement spawnEnemy = null;
                        for (int n = 0; n < GameManager.Instance.EnemiesToPool.Count - 1; n++)
                        {
                            var nameToCheck = $"{ranEnemy.name}(Clone)";
                            if (GameManager.Instance.EnemiesToPool[n].name == nameToCheck)
                            {
                                spawnEnemy = GameManager.Instance.EnemiesToPool[n];
                                spawnEnemy.transform.position = nextSpawnPosition;
                                spawnEnemy.gameObject.SetActive(true);
                                GameManager.Instance.EnemiesToPool.Remove(spawnEnemy);
                                Debug.Log($"{_enemiesGroup.Count} FromPool");
                                break;
                            }
                        }

                        if (spawnEnemy == null)
                        {
                            spawnEnemy = Instantiate(ranEnemy, nextSpawnPosition, Quaternion.identity, parent);
                            Debug.Log($"{_enemiesGroup.Count} CantFindInPool");
                        }

                        _enemiesGroup.Add(spawnEnemy);
                    }
                }
            }
            return _enemiesGroup;
        }

    }
}
