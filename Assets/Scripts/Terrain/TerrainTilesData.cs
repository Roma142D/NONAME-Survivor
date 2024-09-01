using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RomaDoliba.Terrain
{
    [CreateAssetMenu(fileName = "TerrainTiles", menuName = "MyTerrain/TerrainTiles", order = 0)]
    public class TerrainTilesData : ScriptableObject
    {
        [SerializeField] private List<TileBase> _terrainTilesBackground;
        [SerializeField] private List<GameObject> _propPrefabs;
        private Transform _parent;

        private void Awake()
        {
            _terrainTilesBackground = new List<TileBase>();
            _propPrefabs = new List<GameObject>();

        }
        public void Init (Transform parent)
        {
            _parent = parent;
        }
        public TileBase SpawnTile(Vector3 spawnPosition)
        {
            var randomTile = _terrainTilesBackground[Random.Range(0, _terrainTilesBackground.Count)];
            var spawnedTile = Instantiate(randomTile, spawnPosition, Quaternion.identity, _parent);
            SpawnProps(spawnedTile.PropsSpawnPoints);
            return spawnedTile;
        }
        public void SpawnProps(Transform[] propsSpawnPoints)
        {
            foreach (var propPosition in propsSpawnPoints)
            {
                var randomProp = Random.Range(0, _propPrefabs.Count);
                Instantiate(_propPrefabs[randomProp], propPosition.transform.position, Quaternion.identity, propPosition);
            }
        }

        [System.Serializable]
        public struct PropsData
        {
            public List<GameObject> PropsSpawnPoints;
            public List<GameObject> PropsPrefabs;
        }
    }
}
