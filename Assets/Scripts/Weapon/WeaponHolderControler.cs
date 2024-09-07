using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RomaDoliba.Weapon
{
    public class WeaponHolderControler : MonoBehaviour
    {
        [SerializeField] private WeaponBase _currentWeapon;
        private float _currentCooldown;
        void Start()
        {
            _currentCooldown = _currentWeapon.Cooldown;
        }

        void Update()
        {
            _currentCooldown -= Time.deltaTime;
            if (_currentCooldown <= 0)
            {
                _currentWeapon.Init(this);
                _currentCooldown = _currentWeapon.Cooldown;
            }
        }
    }

}
