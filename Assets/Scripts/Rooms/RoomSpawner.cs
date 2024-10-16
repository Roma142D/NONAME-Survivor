using System.Collections;
using System.Collections.Generic;
using RomaDoliba.Manager;
using UnityEngine;
namespace RomaDoliba.Terrain
{
    public enum Direction
    {
        Top,
        Down,
        Right,
        Left,
        None
    }
    public class RoomSpawner : MonoBehaviour
    {
        [SerializeField] private AllRoomsData _roomsPrefabs;
        [SerializeField] private Direction _direction;
        public bool IsSpawned{get; private set;}

        private void Start()
        {
            Invoke("SpawnRoom", 0.1f);
            Destroy(gameObject, 3f);
            /*
            if (GameManager.Instance.SpawnedRooms == null 
                ||GameManager.Instance.SpawnedRooms.Count <= GameManager.Instance.MaxRooms)
            {
            }
            else
            {
            }
            */
        }
        
        public void SpawnRoom()
        {
            RoomBase newRoom = null;
            if (!IsSpawned)
            {
                switch (_direction)
                {
                    case Direction.Top:
                        var topRoom = FindRoomByDirection(Direction.Top);
                        newRoom = Instantiate(topRoom, transform.position, Quaternion.identity);
                        break;
                    case Direction.Down:
                        var downRoom = FindRoomByDirection(Direction.Down);
                        newRoom = Instantiate(downRoom, transform.position, Quaternion.identity);
                        break;
                    case Direction.Right:
                        var rightRoom = FindRoomByDirection(Direction.Right);
                        newRoom = Instantiate(rightRoom, transform.position, Quaternion.identity);
                        break;
                    case Direction.Left:
                        var leftRoom = FindRoomByDirection(Direction.Left);
                        newRoom = Instantiate(leftRoom, transform.position, Quaternion.identity);          
                        break;
                }
                IsSpawned = true;
            }
            GameManager.Instance.SpawnedRooms.Add(newRoom);
        }

        private RoomBase FindRoomByDirection(Direction direction)
        {
            List<RoomBase> suitableRooms = new List<RoomBase>();
            foreach (var room in _roomsPrefabs.RoomsPrefabs)
            {
                //if (room.RoomDirection == direction) suitableRooms.Add(room);
                foreach (var roomDirection in room.RoomDirection)
                {
                    if (roomDirection == direction) suitableRooms.Add(room);
                }
            }
            return suitableRooms[Random.Range(0, suitableRooms.Count)];
        }
        private void OnTriggerStay2D(Collider2D collider)
        {
            if (collider.CompareTag("NeighborPoint") && collider.GetComponent<RoomSpawner>().IsSpawned)
            {
                Destroy(gameObject);
            }
        }
    }
}
