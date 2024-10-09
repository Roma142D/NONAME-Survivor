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
        public static GameManager Instance{get; private set;}
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
        [SerializeField] private float _coolDownForSpawn;
        [SerializeField] private Transform _enemiesCollector;
        private List<EnemyMovement> _spawnedEnemies;
        private List<EnemyMovement> _enemiesToPool;
        private List<Transform> _currentEnemiesSpawnPoints;
        private Coroutine _spawnEnemiesCoroutine;
        public List<EnemyMovement> EnemiesToPool => _enemiesToPool;
        [Space]
        [Header("Player")]
        [SerializeField] private PlayerControler _player;
        [Space]
        [Header("Items")]
        [SerializeField] private Transform _dropedItemsCollector;
        private List<GameObject> _dropedItems;
        public Transform DropedItemsCollector => _dropedItemsCollector;
        public List<GameObject> DropedItems => _dropedItems;
        [Space]
        [Header("")]
        [SerializeField] private AudioSource _actionAudioSource;
        public AudioSource ActionAudioSource {get => _actionAudioSource; set => _actionAudioSource = value;}
        
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
            _currentEnemiesSpawnPoints = new List<Transform>();

            _spawnedTiles = new List<TileBase>();
            _terrainTilesData.Init(_backgroundGrid.transform);
            SpawnTile(Vector3.zero);
            
            //_currentEnemiesPerSpawn = _enemiesSpawnPerOneTime;
            _spawnedEnemies = new List<EnemyMovement>();
            _enemiesToPool = new List<EnemyMovement>();
            var spawnedEnemies = _enemiesSpawner.SpawnEnemies(_currentEnemiesSpawnPoints, true, _enemiesCollector);
            _spawnedEnemies.AddRange(spawnedEnemies);

            _dropedItems = new List<GameObject>();
        }
        private void Update()
        {
            if (_spawnEnemiesCoroutine == null)
            {
                var ranPosition = _currentEnemiesSpawnPoints[Random.Range(0, _currentEnemiesSpawnPoints.Count)];
                _spawnEnemiesCoroutine = StartCoroutine(SpawnEnemiesByCoolDown(ranPosition.position));
            }
            
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
                    if (_currentEnemiesSpawnPoints.Count >= 16)
                        {
                            _currentEnemiesSpawnPoints.RemoveRange(0, _currentEnemiesSpawnPoints.Count - 8);
                        }
                        _currentEnemiesSpawnPoints.AddRange(poolTile.EnemiesSpawnPoints);
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
            
            if (_currentEnemiesSpawnPoints.Count >= 16)
            {
                _currentEnemiesSpawnPoints.RemoveRange(0, _currentEnemiesSpawnPoints.Count);
            }
            _currentEnemiesSpawnPoints.AddRange(spawnedTile.EnemiesSpawnPoints);
        }

        private IEnumerator SpawnEnemiesByCoolDown(Vector3 spawnPosition)
        {
            yield return new WaitForSeconds(_coolDownForSpawn);

            CheckEnemiesToPool();
            var spawnedEnemies = _enemiesSpawner.SpawnEnemies(_currentEnemiesSpawnPoints, false, _enemiesCollector);
            _spawnedEnemies.AddRange(spawnedEnemies);
            /*
            if (_enemiesToPool.Count <= _currentEnemiesPerSpawn || _enemiesToPool == null)
            {
            }
            else
            {
                for (int i = 0; i < _currentEnemiesPerSpawn; i++)
                {
                    var enemyToPool = _enemiesToPool[i];
                    enemyToPool.gameObject.transform.position = spawnPosition;
                    enemyToPool.gameObject.SetActive(true);
                    _enemiesToPool.Remove(enemyToPool);
                    _spawnedEnemies.Add(enemyToPool);
                }
            }
            */
            _spawnEnemiesCoroutine = null;
        }

        private void CheckEnemiesToPool()
        {
            for (int i = 0; i < _spawnedEnemies.Count; i++)
            {
                var enemy = _spawnedEnemies[i];
                if (enemy.gameObject.activeSelf == false)
                {
                    _spawnedEnemies.Remove(enemy);
                    _enemiesToPool.Add(enemy);
                }
            }
        }

        public void AddItemToPool(GameObject item)
        {
            _dropedItems.Add(item);
        }

    }
}
