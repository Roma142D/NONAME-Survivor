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
        [SerializeField] private List<Direction> _roomDirection;
        [SerializeField] private ContactFilter2D _doorFilter;
        public Doors RoomDoors => _doors;
        public List<Direction> RoomDirection => _roomDirection;
        public List<Collider2D> NeighborDoors{get; set;}
        
        private IEnumerator Start()
        {
            //GlobalEventSender.OnEvent += CloseDoors;
            NeighborDoors = new List<Collider2D>();
            yield return new WaitForSecondsRealtime(2.1f);
            CheckDoors();
        }

        private void Update()
        {
            if (GameManager.Instance.IsWaveDefeated) OpenDoors();
        }
        private void CloseDoors(string eventName, float delay)
        {
            if (eventName == "CloseDoors")
            {
                
                StartCoroutine(CloseAfterDelay(delay));
            }
        }

        public IEnumerator CloseAfterDelay(float delay)
        {
            GameManager.Instance.IsWaveDefeated = false;
            yield return new WaitForSecondsRealtime(delay);

            if (_doors.LeftDoor.Collider != null)
            {
                _doors.LeftDoor.Collider.isTrigger = false;
                _doors.LeftDoor.Collider.GetContacts(_doorFilter, NeighborDoors);
                _doors.LeftDoor.SpriteRenderer.color = Color.white;
                
            }
            if (_doors.RightDoor.Collider != null)
            {
                _doors.RightDoor.Collider.isTrigger = false;
                _doors.RightDoor.Collider.GetContacts(_doorFilter, NeighborDoors);
                _doors.RightDoor.SpriteRenderer.color =Color.white;
                
            }
            if (_doors.TopDoor.Collider != null)
            {
                _doors.TopDoor.Collider.isTrigger = false;
                _doors.TopDoor.Collider.GetContacts(_doorFilter, NeighborDoors);
                _doors.TopDoor.SpriteRenderer.color = Color.white;
                
            }
            if (_doors.BottomDoor.Collider != null)
            {
                _doors.BottomDoor.Collider.isTrigger = false;
                _doors.BottomDoor.Collider.GetContacts(_doorFilter, NeighborDoors);
                _doors.BottomDoor.SpriteRenderer.color = Color.white;
                
            }
        }

        private void OpenDoors()                                    //Test
        {
            if (_doors.LeftDoor.Collider != null)
            {
                _doors.LeftDoor.Collider.isTrigger = true;
                _doors.LeftDoor.Collider.GetContacts(_doorFilter, NeighborDoors);
                _doors.LeftDoor.SpriteRenderer.color = Color.black;
                
            }
            if (_doors.RightDoor.Collider != null)
            {
                _doors.RightDoor.Collider.isTrigger = true;
                _doors.RightDoor.Collider.GetContacts(_doorFilter, NeighborDoors);
                _doors.RightDoor.SpriteRenderer.color =Color.black;
                
            }
            if (_doors.TopDoor.Collider != null)
            {
                _doors.TopDoor.Collider.isTrigger = true;
                _doors.TopDoor.Collider.GetContacts(_doorFilter, NeighborDoors);
                _doors.TopDoor.SpriteRenderer.color = Color.black;
                
            }
            if (_doors.BottomDoor.Collider != null)
            {
                _doors.BottomDoor.Collider.isTrigger = true;
                _doors.BottomDoor.Collider.GetContacts(_doorFilter, NeighborDoors);
                _doors.BottomDoor.SpriteRenderer.color = Color.black;
                
            }
        }
        
        private void CheckDoors()
        {
            if (_doors.LeftDoor.Collider != null)
            {
                //List<Collider2D> colliders = new List<Collider2D>();
                _doors.LeftDoor.SpriteRenderer.enabled = _doors.LeftDoor.Collider.GetContacts(_doorFilter, NeighborDoors) != 0 
                ? true
                : false;
            }
            if (_doors.RightDoor.Collider != null)
            {
                //List<Collider2D> colliders = new List<Collider2D>();
                _doors.RightDoor.SpriteRenderer.enabled = _doors.RightDoor.Collider.GetContacts(_doorFilter, NeighborDoors) != 0 
                ? true
                : false;
            }
            if (_doors.TopDoor.Collider != null)
            {
                //List<Collider2D> colliders = new List<Collider2D>();
                _doors.TopDoor.SpriteRenderer.enabled = _doors.TopDoor.Collider.GetContacts(_doorFilter, NeighborDoors) != 0 
                ? true
                : false;
            }
            if (_doors.BottomDoor.Collider != null)
            {
                //List<Collider2D> colliders = new List<Collider2D>();
                _doors.BottomDoor.SpriteRenderer.enabled = _doors.BottomDoor.Collider.GetContacts(_doorFilter, NeighborDoors) != 0 
                ? true
                : false;
            }
            //Debug.Log(NeighborDoors.Count);
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
