using System;
using System.Collections;
using System.Collections.Generic;
using RomaDoliba.ActionSystem;
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
        public bool IsBossRoom {get; set;}
        
        private IEnumerator Start()
        {
            IsBossRoom = false;
            NeighborDoors = new List<DoorControler>();
            GlobalEventSender.OnEvent += OnLambSacrifice;
            yield return new WaitForSecondsRealtime(3.1f);
            CheckDoors();
            _propsData.SpawnProps(_propsSpawnPoints, 3);
            IsRoomCleared = false;
        }

        public void OnLambSacrifice(string eventName, float value)
        {
            if (eventName == GlobalData.LAMB_SACRIFICE && IsBossRoom)
            {
                OpenDoors();
            }
        }
        
        public void OnWaveDefete()
        {
            _pentagram.SpriteRenderer.sprite = _pentagram.ActiveSprite;
            if (!IsBossRoom) 
            {
                OpenDoors();
            }
            else
            {
                GameManager.Instance.OnLevelCompleted();
            }
        }
        public IEnumerator CloseAfterDelay(float delay)
        {
            GameManager.Instance.CurrentRoom = this;
            GameManager.Instance.IsWaveDefeated = false;
            yield return new WaitForSecondsRealtime(delay);

            if (_doors.LeftDoor != null)
            {
                _doors.LeftDoor.DoorCollider.isTrigger = false;
                //_doors.LeftDoor.Collider.GetContacts(_doorFilter, NeighborDoors);
                _doors.LeftDoor.DoorRenderer.color = Color.white;
                
            }
            if (_doors.RightDoor != null)
            {
                _doors.RightDoor.DoorCollider.isTrigger = false;
                //_doors.RightDoor.Collider.GetContacts(_doorFilter, NeighborDoors);
                _doors.RightDoor.DoorRenderer.color =Color.white;
                
            }
            if (_doors.TopDoor != null)
            {
                _doors.TopDoor.DoorCollider.isTrigger = false;
                //_doors.TopDoor.Collider.GetContacts(_doorFilter, NeighborDoors);
                _doors.TopDoor.DoorRenderer.color = Color.white;
                
            }
            if (_doors.BottomDoor != null)
            {
                _doors.BottomDoor.DoorCollider.isTrigger = false;
                //_doors.BottomDoor.Collider.GetContacts(_doorFilter, NeighborDoors);
                _doors.BottomDoor.DoorRenderer.color = Color.white;
                
            }
        }

        private void OpenDoors()                                    //Test
        {
            if (_doors.LeftDoor != null)
            {
                _doors.LeftDoor.DoorCollider.isTrigger = true;
                //_doors.LeftDoor.Collider.GetContacts(_doorFilter, NeighborDoors);
                _doors.LeftDoor.DoorRenderer.color = Color.black;
                
            }
            if (_doors.RightDoor != null)
            {
                _doors.RightDoor.DoorCollider.isTrigger = true;
                //_doors.RightDoor.Collider.GetContacts(_doorFilter, NeighborDoors);
                _doors.RightDoor.DoorRenderer.color = Color.black;
                
            }
            if (_doors.TopDoor != null)
            {
                _doors.TopDoor.DoorCollider.isTrigger = true;
                //_doors.TopDoor.Collider.GetContacts(_doorFilter, NeighborDoors);
                _doors.TopDoor.DoorRenderer.color = Color.black;
                
            }
            if (_doors.BottomDoor != null)
            {
                _doors.BottomDoor.DoorCollider.isTrigger = true;
                //_doors.BottomDoor.Collider.GetContacts(_doorFilter, NeighborDoors);
                _doors.BottomDoor.DoorRenderer.color = Color.black;
                
            }
            foreach (var door in NeighborDoors)
            {
                if (!door.IsBossRoomDoor)
                {
                    door.DoorCollider.isTrigger = true;
                    door.DoorRenderer.color = Color.black;
                }
            }
        }
        
        private void CheckDoors()
        {
            if (_doors.LeftDoor != null)
            {
                List<Collider2D> colliders = new List<Collider2D>();
                _doors.LeftDoor.DoorRenderer.enabled = _doors.LeftDoor.DoorCollider.GetContacts(_doorFilter, colliders) != 0 
                ? true
                : false;
                NeighborDoors.AddRange(colliders.ConvertAll<DoorControler>(door => door.GetComponent<DoorControler>()));
                _doors.LeftDoor.IsBossRoomDoor = IsBossRoom;
            }
            if (_doors.RightDoor != null)
            {
                List<Collider2D> colliders = new List<Collider2D>();
                _doors.RightDoor.DoorRenderer.enabled = _doors.RightDoor.DoorCollider.GetContacts(_doorFilter, colliders) != 0 
                ? true
                : false;
                NeighborDoors.AddRange(colliders.ConvertAll<DoorControler>(door => door.GetComponent<DoorControler>()));
                _doors.RightDoor.IsBossRoomDoor = IsBossRoom;
            }
            if (_doors.TopDoor != null)
            {
                List<Collider2D> colliders = new List<Collider2D>();
                _doors.TopDoor.DoorRenderer.enabled = _doors.TopDoor.DoorCollider.GetContacts(_doorFilter, colliders) != 0 
                ? true
                : false;
                NeighborDoors.AddRange(colliders.ConvertAll<DoorControler>(door => door.GetComponent<DoorControler>()));
                _doors.TopDoor.IsBossRoomDoor = IsBossRoom;
            }
            if (_doors.BottomDoor != null)
            {
                List<Collider2D> colliders = new List<Collider2D>();
                _doors.BottomDoor.DoorRenderer.enabled = _doors.BottomDoor.DoorCollider.GetContacts(_doorFilter, colliders) != 0 
                ? true
                : false;
                NeighborDoors.AddRange(colliders.ConvertAll<DoorControler>(door => door.GetComponent<DoorControler>()));
                _doors.BottomDoor.IsBossRoomDoor = IsBossRoom;
            }
            
            if(NeighborDoors.Count == 0)
            {
                Debug.Log(NeighborDoors.Count);
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
            //public GameObject Lamb;
        }
        [System.Serializable]
        public struct Doors
        {
            public DoorControler LeftDoor;
            public DoorControler RightDoor;
            public DoorControler TopDoor;
            public DoorControler BottomDoor;
        }
        [System.Serializable]
        public struct DoorData
        {
            public SpriteRenderer DoorRenderer;
            public Collider2D DoorCollider;
        }
    }
}
