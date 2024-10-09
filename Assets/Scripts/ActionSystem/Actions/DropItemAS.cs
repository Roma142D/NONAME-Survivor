using RomaDoliba.PickUps;
using UnityEngine;

namespace RomaDoliba.ActionSystem
{
    public class DropItemAS : ActionBase
    {
        [SerializeField] private DropItem _drops;

        public override void Execute()
        {
            _drops.DropRandomItem(transform.position);
        }
    }
}
