using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RomaDoliba.Weapon
{
    public class WeaponBase : MonoBehaviour
    {
        [SerializeField] protected GameObject _weaponPrefab;
        [SerializeField] protected float _damage;
        [SerializeField] protected float _speed;
        [SerializeField] protected float _rotationSpeed;
        [SerializeField] protected float _cooldown;
        private float _currentCooldown;
        public float CurrentCooldown => _currentCooldown;
        public float Speed => _speed;
        public float RotationSpeed => _rotationSpeed;
        
        protected virtual void Start()
        {
            _currentCooldown = _cooldown;
        }
        protected virtual void Update()
        {
            _currentCooldown -= Time.deltaTime;
        }
        protected virtual void Execute()
        {
            _currentCooldown = _cooldown;
        }
    }
}
