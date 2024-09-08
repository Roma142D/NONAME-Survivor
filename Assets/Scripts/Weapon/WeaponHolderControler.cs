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
        private List<GameObject> _spawnedWeapons1;
        private float _currentCooldown;
        //private WeaponBase _currentWeapon1;

        private void Awake()
        {
            _spawnedWeapons1 = new List<GameObject>();
            //_currentWeapon1 = WeaponBase.CreateInstance<WeaponBase>();
        }
        private void Start()
        {
            _currentCooldown = _currentWeapon1.Cooldown;
        }

        private void Update()
        {
            _currentCooldown -= Time.deltaTime;
            if (_currentCooldown <= 0)
            {
                if (_spawnedWeapons1.Count < 5)
                {
                    var spawnedWeapon = _currentWeapon1.Init(this);
                    _spawnedWeapons1.Add(spawnedWeapon);
                }
                else
                {
                    var pooledWeapon = _spawnedWeapons1[0];
                    _spawnedWeapons1.Remove(pooledWeapon);
                    pooledWeapon.transform.position = this.transform.position;
                    var rotZ = Mathf.Atan2(PlayerControler.Instance.LastMoveDirection.y, PlayerControler.Instance.LastMoveDirection.x) * Mathf.Rad2Deg;
                    pooledWeapon.transform.rotation = Quaternion.Euler(0f, 0f, rotZ - 45f);
                    pooledWeapon.SetActive(true);
                    _spawnedWeapons1.Add(pooledWeapon);
                }
                _currentCooldown = _currentWeapon1.Cooldown;
            }
        }
    }

}
