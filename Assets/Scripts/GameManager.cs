using System.Collections;
using System.Collections.Generic;
using System.Linq;
using RomaDoliba.Enemy;
using RomaDoliba.Player;
using RomaDoliba.Terrain;
using UnityEngine;

namespace RomaDoliba.Manager
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance{get; private set;}
        [Space]
        [Header("RoomsSpawn")]
        [SerializeField] private int _maxRooms;
        private RoomBase _startRoom;
        public int MaxRooms => _maxRooms;
        public RoomBase CurrentRoom {get; set;}
        public List<RoomBase> SpawnedRooms {get; set;}
        /*
        [Header("TerrainSpawn")]
        [SerializeField] private TerrainTilesData _terrainTilesData;
        [SerializeField] private GameObject _backgroundGrid;
        [SerializeField] private LayerMask _terrainMask;
        private List<TileBase> _spawnedTiles;
        [SerializeField] private float _checkRadius;
        [SerializeField] private float _checkDistance;
        [SerializeField] private float _distanceToDisable;
        */
        public TileBase LastSpawnedTile {get;private set;}
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
        public Coroutine IsEnemyDefeated {get; private set;}
        public bool IsWaveDefeated {get; set;}
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
        [Header("Audio")]
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
            SpawnedRooms = new List<RoomBase>();
            _currentEnemiesSpawnPoints = new List<Transform>();
            _spawnedEnemies = new List<EnemyMovement>();
            _enemiesToPool = new List<EnemyMovement>();
            _dropedItems = new List<GameObject>();
            
            /*
            _spawnedTiles = new List<TileBase>();
            _terrainTilesData.Init(_backgroundGrid.transform);
            SpawnTile(Vector3.zero, true);
            */
            //_currentEnemiesPerSpawn = _enemiesSpawnPerOneTime;
        }
        private IEnumerator Start()
        {
            yield return new WaitForSecondsRealtime(2f);
            //_currentEnemiesSpawnPoints.AddRange(SpawnedRooms[0].EnemiesSpawnPoints);
            //SpawnedRooms[0].gameObject.SetActive(false);
            CurrentRoom = SpawnedRooms[0];
            //_currentEnemiesSpawnPoints.AddRange(CurrentRoom.EnemiesSpawnPoints);
            if (SpawnedRooms.Count > MaxRooms)
            {
                while (SpawnedRooms.Count - 1 > MaxRooms)
                {
                    Destroy(SpawnedRooms[MaxRooms + 1].gameObject);
                    SpawnedRooms.RemoveAt(MaxRooms + 1);
                }
            }
            yield return new WaitForSecondsRealtime(1f);
            var spawnedEnemies = _enemiesSpawner.SpawnEnemies(CurrentRoom.EnemiesSpawnPoints.ToList(), true, _enemiesCollector);
            _spawnedEnemies.AddRange(spawnedEnemies);
        }
        private void FixedUpdate()
        {
            if (_spawnedEnemies.Count > 0) CheckEnemiesToPool();
            if (_spawnEnemiesCoroutine == null && IsWaveDefeated)
            {
                _spawnEnemiesCoroutine = StartCoroutine(SpawnEnemiesByCoolDown());
            }
            
            
            //CheckTilesToSpawn();
        }
        /*
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
                    SpawnTile(nextPositionToSpawn, false);
                }
                else if (CheckTilesToPool() != null)
                {
                    var poolTile = CheckTilesToPool();
                    poolTile.gameObject.SetActive(true);
                    poolTile.gameObject.transform.position = poolTile.GetNextSpawnPoint().position;
                    LastSpawnedTile = poolTile;
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
        private void SpawnTile(Vector3 spawnPosition, bool isStart)
        {
            var spawnedTile = _terrainTilesData.SpawnTile(spawnPosition, isStart);
            _spawnedTiles.Add(spawnedTile);
            LastSpawnedTile = spawnedTile;
            
            if (_currentEnemiesSpawnPoints.Count >= 16)
            {
                _currentEnemiesSpawnPoints.RemoveRange(0, _currentEnemiesSpawnPoints.Count);
            }
            _currentEnemiesSpawnPoints.AddRange(spawnedTile.EnemiesSpawnPoints);
        }
        */
        
        private IEnumerator SpawnEnemiesByCoolDown()
        {
            //yield return new WaitUntil(() => _spawnedEnemies.Count == 0);
            //IsWaveDefeated = _spawnedEnemies.Count == 0;
            yield return new WaitForSeconds(_coolDownForSpawn);
            IsWaveDefeated = false;
            var spawnedEnemies = _enemiesSpawner.SpawnEnemies(CurrentRoom.EnemiesSpawnPoints.ToList(), false, _enemiesCollector);
            _spawnedEnemies.AddRange(spawnedEnemies);
            _spawnEnemiesCoroutine = null;
        }
        private void CheckEnemiesToPool()
        {
            for (int i = _spawnedEnemies.Count - 1; i >= 0; i--)
            {
                var enemy = _spawnedEnemies[i];
                if (enemy.gameObject.activeSelf == false)
                {
                    _spawnedEnemies.Remove(enemy);
                    _enemiesToPool.Add(enemy);
                }
            }
            IsWaveDefeated = _spawnedEnemies.Count == 0;
        }
        

        public void AddItemToPool(GameObject item)
        {
            _dropedItems.Add(item);
        }

    }
}
