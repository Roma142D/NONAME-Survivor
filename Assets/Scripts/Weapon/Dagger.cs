using System;
using System.Collections;
using System.Collections.Generic;
using RomaDoliba.Player;
using UnityEngine;
using UnityEngine.InputSystem;


namespace RomaDoliba.Weapon
{
    [CreateAssetMenu(fileName = "DaggerData", menuName = "Weapon/DaggerData", order = 0)]
    public class DaggerSpawner : WeaponBase
    {
        protected override GameObject Execute()
        {
            var rotZ = Mathf.Atan2(PlayerControler.Instance.LastMoveDirection.y, PlayerControler.Instance.LastMoveDirection.x) * Mathf.Rad2Deg;
            var daggerRotation = Quaternion.Euler(0f, 0f, rotZ - 45f);
            var spawnedDagger = Instantiate(_weaponPrefab, _weaponHolder.transform.position, daggerRotation);
            
            return spawnedDagger;
        }
        
    }
}
