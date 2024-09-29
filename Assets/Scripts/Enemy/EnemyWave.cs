using UnityEngine;
using System.Collections.Generic;

namespace RomaDoliba.Enemy
{
    [CreateAssetMenu(fileName ="EnemyWave", menuName = "Enemy/EnemyWave", order = 0)]
    public class EnemyWave : ScriptableObject
    {
        [SerializeField] private List<EnemyWaveData> _enemyWaves;
        
        public EnemyWaveData GetFirstWave()
        {
            return _enemyWaves[0];
        }

        [System.Serializable]
        public struct EnemyWaveData
        {
            public List<EnemyMovement> EnemiesToSpawn;
            public int EnemyAmount;
            public float SpawnInterval;
            private readonly int SpawnCount;
        }
    }
}
