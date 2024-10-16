using System.Collections;
using System.Collections.Generic;
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
        
        private IEnumerator Start()
        {
            yield return new WaitForSecondsRealtime(5f);
            CheckDoors();
        }
        private void CheckDoors()
        {
            if (_doors.LeftDoor.Collider != null)
            {
                List<Collider2D> colliders = new List<Collider2D>();
                _doors.LeftDoor.SpriteRenderer.enabled = _doors.LeftDoor.Collider.GetContacts(_doorFilter, colliders) != 0 
                ? true
                : false;
            }
            if (_doors.RightDoor.Collider != null)
            {
                List<Collider2D> colliders = new List<Collider2D>();
                _doors.RightDoor.SpriteRenderer.enabled = _doors.RightDoor.Collider.GetContacts(_doorFilter, colliders) != 0 
                ? true
                : false;
            }
            if (_doors.TopDoor.Collider != null)
            {
                List<Collider2D> colliders = new List<Collider2D>();
                _doors.TopDoor.SpriteRenderer.enabled = _doors.TopDoor.Collider.GetContacts(_doorFilter, colliders) != 0 
                ? true
                : false;
            }
            if (_doors.BottomDoor.Collider != null)
            {
                List<Collider2D> colliders = new List<Collider2D>();
                _doors.BottomDoor.SpriteRenderer.enabled = _doors.BottomDoor.Collider.GetContacts(_doorFilter, colliders) != 0 
                ? true
                : false;
            }
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
