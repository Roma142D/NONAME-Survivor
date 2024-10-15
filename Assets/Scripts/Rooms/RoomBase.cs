using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RomaDoliba.Terrain
{
    public class RoomBase : TileBase
    {
        [SerializeField] private Doors _doors;
        [SerializeField] private List<Direction> _roomDirection;
        public Doors RoomDoors => _doors;
        public List<Direction> RoomDirection => _roomDirection;
        
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
