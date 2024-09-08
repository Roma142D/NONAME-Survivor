using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using RomaDoliba.Player;

namespace RomaDoliba.Weapon
{
    public class WeaponHolderControler : MonoBehaviour
    {
        [SerializeField] private WeaponBase _currentWeapon1;
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
            switch (_currentWeapon1.WeaponType)  //TODO перенести цей світч в метод який буде спрацьовувати коли ми підбераємо нову зброю
            {
                case WeaponType.dagger: 
                    _daggerWeapon = _currentWeapon1;
                    _currentCooldown = _daggerWeapon.Cooldown;
                    break;
                case WeaponType.aura:
                    _auraWeapon = _currentWeapon1;
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
