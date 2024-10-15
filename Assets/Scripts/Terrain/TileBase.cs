using System.Collections;
using System.Collections.Generic;
using RomaDoliba.Manager;
using RomaDoliba.Player;
using UnityEngine;

namespace RomaDoliba.Terrain
{
    public class TileBase : MonoBehaviour
    {
        [SerializeField] private Transform[] _propsSpawnPoints;
        [SerializeField] private Transform[] _enemiesSpawnPoints;
        [SerializeField] private NeighborSpawnPoints _neighborSpawnPoints;

        public Transform[] PropsSpawnPoints => _propsSpawnPoints;
        public Transform[] EnemiesSpawnPoints => _enemiesSpawnPoints;
        //public Transform[] NeighborSpawnPoints => _neighborSpawnPoints;

        public Transform GetNextSpawnPoint()
        {
            var moveDirection = PlayerControler.Instance.MoveDirection;
            var playerPosition = PlayerControler.Instance.transform.position;
            Transform nextSpawnPosition;
            
            nextSpawnPosition = moveDirection.x > playerPosition.x 
                ? _neighborSpawnPoints.RightPoint 
                : _neighborSpawnPoints.LeftPoint;
            if (Mathf.Abs(moveDirection.y - playerPosition.y) > 4f)
            {
                nextSpawnPosition = moveDirection.y > playerPosition.y 
                    ? _neighborSpawnPoints.TopPoint 
                    : _neighborSpawnPoints.BottomPoint;
            }
            Debug.Log(nextSpawnPosition);
            return nextSpawnPosition;
        }

        [System.Serializable]
        public struct NeighborSpawnPoints
        {
            public Transform LeftPoint;
            public Transform RightPoint;
            public Transform TopPoint;
            public Transform BottomPoint;
        }
    }
}
