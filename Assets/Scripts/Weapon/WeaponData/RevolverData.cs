using System.Collections;
using System.Collections.Generic;
using RomaDoliba.Player;
using UnityEngine;

namespace RomaDoliba.Weapon
{
    [CreateAssetMenu(fileName = "RevolverData", menuName = "Weapon/RevolverData", order = 3)]
    public class RevolverData : DaggerSpawner
    {
        [SerializeField] private RevolverControler _gunPrefab;
                
        protected override GameObject Execute()
        {
            if (_weaponHolder.RevolverWeapon == null) SpawnGun();
            return base.Execute();
        }
        private void SpawnGun()
        {
            var rotZ = PlayerControler.Instance.ControlerType == ControlerType.Android 
            ? Mathf.Atan2(WeaponJoystick.Vertical, WeaponJoystick.Horizontal) * Mathf.Rad2Deg
            : Mathf.Atan2(PlayerControler.Instance.LastMoveDirection.y, PlayerControler.Instance.LastMoveDirection.x) * Mathf.Rad2Deg;
            var gunRotation = Quaternion.Euler(0f, 0f, rotZ);
            var gun = Instantiate(_gunPrefab,  _weaponHolder.transform.position, gunRotation, _weaponHolder.transform);
            _weaponHolder.RevolverWeapon = gun;
            gun.Init(_weaponHolder.WeaponJoystick);
        }
    }  
}
