using System.Collections;
using System.Collections.Generic;
using RomaDoliba.Player;
using UnityEngine;

namespace RomaDoliba.Weapon
{
    [CreateAssetMenu(fileName = "RevolverData", menuName = "Weapon/RevolverData", order = 3)]
    public class RevolverData : WeaponBase
    {               
        [SerializeField] private int _bulletsPerQueue;
        public int BulletsPerQueue {get => _bulletsPerQueue;}
        protected override GameObject Execute()
        {
            var spawnedRevolver = Instantiate(_weaponPrefab, _weaponHolder.transform.position, Quaternion.identity, _weaponHolder.transform);
            
            return spawnedRevolver;
        }
    }  
}
