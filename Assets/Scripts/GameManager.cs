using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;
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
        private Vector3 _noTerrainPosition;
        private List<TileBase> _spawnedTiles;
        [Space]
        [Header("Player")]
        [SerializeField] private PlayerControler _player;
        [SerializeField] private float _checkRadius;
        [SerializeField] private float _checkDistance;
        
        private void Awake()
        {
            _spawnedTiles = new List<TileBase>();
            _terrainTilesData.Init(_backgroundGrid.transform);
            var spawnedTile = _terrainTilesData.SpawnTile(Vector3.zero);
            _spawnedTiles.Add(spawnedTile);
        }
        private void Update()
        {
            CheckTiles();
        }

        private void CheckTiles()
        {
            var spawnDistance = new Vector3(_player.MoveDirection.x * 22f, _player.MoveDirection.y * 22f, 0);
            
            Debug.DrawLine(_player.transform.position, _player.transform.position + spawnDistance, Color.red);
            if (!Physics2D.CircleCast(_player.transform.position + spawnDistance, _checkRadius, _player.MoveDirection, _checkDistance, _terrainMask))
            {
                var currentTile = Physics2D.OverlapPoint(_player.transform.position);
                var nextPositionToSpawn = currentTile.transform.position + spawnDistance;
                //_noTerrainPosition = _player.transform.position + spawnDistance;
                SpawnTile(nextPositionToSpawn);
            }
        }
        private void SpawnTile(Vector3 spawnPosition)
        {
            var spawnedTile = _terrainTilesData.SpawnTile(spawnPosition);
            _spawnedTiles.Add(spawnedTile);
        }
    }
}
