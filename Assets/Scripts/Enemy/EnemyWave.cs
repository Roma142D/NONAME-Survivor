using UnityEngine;
using System.Collections.Generic;

namespace RomaDoliba.Enemy
{
    [CreateAssetMenu(fileName ="EnemyWave", menuName = "Enemy/EnemyWave", order = 0)]
    public class EnemyWave : ScriptableObject
    {
        [SerializeField] private List<EnemyWaveData> _enemyWaves;
        private int _currentWave;
        
        public EnemyWaveData GetFirstWave()
        {
            _currentWave = 0;
            return _enemyWaves[0];
        }
        public EnemyWaveData GetNextWave()
        {
            _currentWave += 1;
            if (_currentWave < _enemyWaves.Count)
            {
                return _enemyWaves[_currentWave];
            }
            else
            {
                return GetRandomWave();
            }
        }
        public EnemyWaveData GetRandomWave()
        {
            return _enemyWaves[Random.Range(0, _enemyWaves.Count)];
        }


        [System.Serializable]
        public struct EnemyWaveData
        {
            public List<EnemyMovement> EnemiesToSpawn;
            public int EnemyAmount;
        }
    }
}
