using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RomaDoliba.Player;

namespace RomaDoliba.Weapon
{
    public class WeaponHolderControler : MonoBehaviour
    {
        //public WeaponBase _testSecondWeapon;
        private WeaponBase _daggerWeapon;
        private WeaponBase _auraWeapon;
        private List<GameObject> _spawnedDaggers;
        private float _currentCooldown;
        

        private void Awake()
        {
            _spawnedDaggers = new List<GameObject>();
            
        }
        private void Start()
        {
            AddWeapon(PlayerControler.Instance.DefoltWeapon);
            //AddWeapon(_testSecondWeapon);
        }
        private void AddWeapon(WeaponBase weapon)
        {
            switch (weapon.WeaponType)  
            {
                case WeaponType.dagger: 
                    _daggerWeapon = weapon;
                    _currentCooldown = _daggerWeapon.Cooldown;
                    break;
                case WeaponType.aura:
                    _auraWeapon = weapon;
                    AuraWeaponBehavior(_auraWeapon);
                    break;
                default: 
                    Debug.Log("Not a weapon");
                    break;
            }
        }

        private void Update()
        {
            _currentCooldown -= Time.deltaTime;

            if (_currentCooldown <= 0 && _daggerWeapon != null)
            {
                DaggerWeaponBehavior(_daggerWeapon);
                _currentCooldown = _daggerWeapon.Cooldown;
            }
        }

        private void DaggerWeaponBehavior(WeaponBase weapon)
        {
            if (_spawnedDaggers.Count < 5)
            {
                var spawnedWeapon = weapon.Init(this);
                _spawnedDaggers.Add(spawnedWeapon);
            }
            else
            {
                var pooledWeapon = _spawnedDaggers[0];
                _spawnedDaggers.Remove(pooledWeapon);
                pooledWeapon.transform.position = this.transform.position;
                var rotZ = Mathf.Atan2(PlayerControler.Instance.LastMoveDirection.y, PlayerControler.Instance.LastMoveDirection.x) * Mathf.Rad2Deg;
                pooledWeapon.transform.rotation = Quaternion.Euler(0f, 0f, rotZ - 45f);
                pooledWeapon.SetActive(true);
                _spawnedDaggers.Add(pooledWeapon);
            }
        }
        private void AuraWeaponBehavior(WeaponBase weapon)
        {
            weapon.Init(this);
        }
    }

}
