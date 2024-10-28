using System;
using System.Collections;
using System.Collections.Generic;
using RomaDoliba.Manager;
using UnityEngine;

namespace RomaDoliba.Terrain
{
    public class RoomBase : TileBase
    {
        [SerializeField] private Doors _doors;
        [SerializeField] private Pentagram _pentagram;
        [SerializeField] private List<Direction> _roomDirection;
        [SerializeField] private ContactFilter2D _doorFilter;
        [SerializeField] private PropsData _propsData;
        public Doors RoomDoors => _doors;
        //public Pentagram RoomPentagram {get => _pentagram; set {_pentagram = value;}}
        public List<Direction> RoomDirection => _roomDirection;
        public List<DoorControler> NeighborDoors{get; set;}
        public bool IsRoomCleared {get; set;}
        
        private IEnumerator Start()
        {
            NeighborDoors = new List<DoorControler>();
            
            yield return new WaitForSecondsRealtime(3.1f);
            CheckDoors();
            _propsData.SpawnProps(_propsSpawnPoints, 3);
            IsRoomCleared = false;
        }

        
        public void OnWaveDefete()
        {
            _pentagram.SpriteRenderer.sprite = _pentagram.ActiveSprite;
            OpenDoors();
        }
        public IEnumerator CloseAfterDelay(float delay)
        {
            GameManager.Instance.CurrentRoom = this;
            GameManager.Instance.IsWaveDefeated = false;
            yield return new WaitForSecondsRealtime(delay);

            if (_doors.LeftDoor.Collider != null)
            {
                _doors.LeftDoor.Collider.isTrigger = false;
                //_doors.LeftDoor.Collider.GetContacts(_doorFilter, NeighborDoors);
                _doors.LeftDoor.SpriteRenderer.color = Color.white;
                
            }
            if (_doors.RightDoor.Collider != null)
            {
                _doors.RightDoor.Collider.isTrigger = false;
                //_doors.RightDoor.Collider.GetContacts(_doorFilter, NeighborDoors);
                _doors.RightDoor.SpriteRenderer.color =Color.white;
                
            }
            if (_doors.TopDoor.Collider != null)
            {
                _doors.TopDoor.Collider.isTrigger = false;
                //_doors.TopDoor.Collider.GetContacts(_doorFilter, NeighborDoors);
                _doors.TopDoor.SpriteRenderer.color = Color.white;
                
            }
            if (_doors.BottomDoor.Collider != null)
            {
                _doors.BottomDoor.Collider.isTrigger = false;
                //_doors.BottomDoor.Collider.GetContacts(_doorFilter, NeighborDoors);
                _doors.BottomDoor.SpriteRenderer.color = Color.white;
                
            }
        }

        private void OpenDoors()                                    //Test
        {
            if (_doors.LeftDoor.Collider != null)
            {
                _doors.LeftDoor.Collider.isTrigger = true;
                //_doors.LeftDoor.Collider.GetContacts(_doorFilter, NeighborDoors);
                _doors.LeftDoor.SpriteRenderer.color = Color.black;
                
            }
            if (_doors.RightDoor.Collider != null)
            {
                _doors.RightDoor.Collider.isTrigger = true;
                //_doors.RightDoor.Collider.GetContacts(_doorFilter, NeighborDoors);
                _doors.RightDoor.SpriteRenderer.color = Color.black;
                
            }
            if (_doors.TopDoor.Collider != null)
            {
                _doors.TopDoor.Collider.isTrigger = true;
                //_doors.TopDoor.Collider.GetContacts(_doorFilter, NeighborDoors);
                _doors.TopDoor.SpriteRenderer.color = Color.black;
                
            }
            if (_doors.BottomDoor.Collider != null)
            {
                _doors.BottomDoor.Collider.isTrigger = true;
                //_doors.BottomDoor.Collider.GetContacts(_doorFilter, NeighborDoors);
                _doors.BottomDoor.SpriteRenderer.color = Color.black;
                
            }
            foreach (var door in NeighborDoors)
            {
                door.DoorCollider.isTrigger = true;
                door.DoorRenderer.color = Color.black;
            }
        }
        
        private void CheckDoors()
        {
            if (_doors.LeftDoor.Collider != null)
            {
                List<Collider2D> colliders = new List<Collider2D>();
                _doors.LeftDoor.SpriteRenderer.enabled = _doors.LeftDoor.Collider.GetContacts(_doorFilter, colliders) != 0 
                ? true
                : false;
                NeighborDoors.AddRange(colliders.ConvertAll<DoorControler>(door => door.GetComponent<DoorControler>()));
            }
            if (_doors.RightDoor.Collider != null)
            {
                List<Collider2D> colliders = new List<Collider2D>();
                _doors.RightDoor.SpriteRenderer.enabled = _doors.RightDoor.Collider.GetContacts(_doorFilter, colliders) != 0 
                ? true
                : false;
                NeighborDoors.AddRange(colliders.ConvertAll<DoorControler>(door => door.GetComponent<DoorControler>()));
            }
            if (_doors.TopDoor.Collider != null)
            {
                List<Collider2D> colliders = new List<Collider2D>();
                _doors.TopDoor.SpriteRenderer.enabled = _doors.TopDoor.Collider.GetContacts(_doorFilter, colliders) != 0 
                ? true
                : false;
                NeighborDoors.AddRange(colliders.ConvertAll<DoorControler>(door => door.GetComponent<DoorControler>()));
            }
            if (_doors.BottomDoor.Collider != null)
            {
                List<Collider2D> colliders = new List<Collider2D>();
                _doors.BottomDoor.SpriteRenderer.enabled = _doors.BottomDoor.Collider.GetContacts(_doorFilter, colliders) != 0 
                ? true
                : false;
                NeighborDoors.AddRange(colliders.ConvertAll<DoorControler>(door => door.GetComponent<DoorControler>()));
            }
            
            if(NeighborDoors.Count == 0)
            {
                GameManager.Instance.SpawnedRooms.Remove(this);
                gameObject.SetActive(false);
            } 
        }
        [System.Serializable]
        public struct Pentagram
        {
            public SpriteRenderer SpriteRenderer;
            public Collider2D Collider;
            public Sprite DefoltSprite;
            public Sprite ActiveSprite;
        }
        [System.Serializable]
        public struct Doors
        {
            public DoorData LeftDoor;
            public DoorData RightDoor;
            public DoorData TopDoor;
            public DoorData BottomDoor;
        }
        [System.Serializable]
        public struct DoorData
        {
            public SpriteRenderer SpriteRenderer;
            public Collider2D Collider;
        }
    }
}
