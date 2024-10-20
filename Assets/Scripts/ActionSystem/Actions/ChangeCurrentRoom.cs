using RomaDoliba.Manager;
using RomaDoliba.Terrain;
using UnityEngine;

namespace RomaDoliba.ActionSystem
{
    public class ChangeCurrentRoom : ActionBase
    {
        [SerializeField] private RoomBase _roomToSet;
        public override void Execute()
        {
            GameManager.Instance.CurrentRoom = _roomToSet;
        }
    }
}
