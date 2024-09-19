using System.Collections;
using System.Collections.Generic;
using RomaDoliba.Enemy;
using RomaDoliba.Player;
using RomaDoliba.Terrain;
using UnityEngine;

namespace RomaDoliba.Manager
{
    public class GameManager : MonoBehaviour
    {
        [Header("TerrainSpawn")]
        [SerializeField] private TerrainTilesData _terrainTilesData;
        [SerializeField] private GameObject _backgroundGrid;
        [SerializeField] private LayerMask _terrainMask;
        private List<TileBase> _spawnedTiles;
        [SerializeField] private float _checkRadius;
        [SerializeField] private float _checkDistance;
        [SerializeField] private float _distanceToDisable;
        [Space]
        [Header("EnemySpawn")]
        [SerializeField] private EnemyGroupControler _enemiesSpawner;
        [SerializeField] private Transform _spawnEnemiesPoint; //TEST 
        [SerializeField] private int _enemiesSpawnPerOneTime;
        private List<EnemyMovement> _spawnedEnemies;
        [Space]
        [Header("Player")]
        [SerializeField] private PlayerControler _player;
        
        private void Awake()
        {
            _spawnedTiles = new List<TileBase>();
            _terrainTilesData.Init(_backgroundGrid.transform);
            SpawnTile(Vector3.zero);

            _spawnedEnemies = new List<EnemyMovement>();
            _enemiesSpawner.SpawnEnemies(_spawnEnemiesPoint.position, _enemiesSpawnPerOneTime);
        }
        private void Update()
        {
            CheckTilesToSpawn();
        }

        private void CheckTilesToSpawn()
        {
            var spawnDistance = new Vector3(_player.MoveDirection.x * 22f, _player.MoveDirection.y * 22f, 0);
            
            Debug.DrawLine(_player.transform.position, _player.transform.position + spawnDistance, Color.red);
            if (!Physics2D.CircleCast(_player.transform.position + spawnDistance, _checkRadius, _player.MoveDirection, _checkDistance, _terrainMask))
            {
                var currentTile = Physics2D.OverlapPoint(_player.transform.position);
                var nextPositionToSpawn = currentTile.transform.position + spawnDistance;
                if (_spawnedTiles.Count < 15 || CheckTilesToPool() == null)
                {
                    SpawnTile(nextPositionToSpawn);
                }
                else if (CheckTilesToPool() != null)
                {
                    var poolTile = CheckTilesToPool();
                    poolTile.gameObject.SetActive(true);
                    poolTile.gameObject.transform.position = nextPositionToSpawn;
                }
            }
        }
        private TileBase CheckTilesToPool()
        {
            if (_spawnedTiles.Count > 10)
            {
                foreach (var tileToDisable in _spawnedTiles)
                {
                    var distanceToPlayer = Vector3.Distance(_player.transform.position, tileToDisable.transform.position);
                    if (distanceToPlayer > _distanceToDisable)
                    {
                        _spawnedTiles.Remove(tileToDisable);
                        tileToDisable.gameObject.SetActive(false);
                        _spawnedTiles.Add(tileToDisable);
                        return tileToDisable;
                    }
                }
            }
            return null;
        }
        private void SpawnTile(Vector3 spawnPosition)
        {
            var spawnedTile = _terrainTilesData.SpawnTile(spawnPosition);
            _spawnedTiles.Add(spawnedTile);
        }
    }
}
