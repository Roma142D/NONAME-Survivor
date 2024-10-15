using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RomaDoliba.Terrain
{
    [CreateAssetMenu (fileName = "AllRooms", menuName = "Rooms/AllRooms", order = 1)]
    public class AllRoomsData : ScriptableObject
    {
        [SerializeField] private RoomBase[] _roomsPrefabs;
        public RoomBase[] RoomsPrefabs => _roomsPrefabs;
    }
}
