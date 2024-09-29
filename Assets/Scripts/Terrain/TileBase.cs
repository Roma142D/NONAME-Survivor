using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RomaDoliba.Terrain
{
    public class TileBase : MonoBehaviour
    {
        [SerializeField] private Transform[] _propsSpawnPoints;
        [SerializeField] private Transform[] _enemiesSpawnPoints;

        public Transform[] PropsSpawnPoints => _propsSpawnPoints;
        public Transform[] EnemiesSpawnPoints => _enemiesSpawnPoints;
    }
}
