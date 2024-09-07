using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RomaDoliba.Weapon
{
    [CreateAssetMenu]
    public class WeaponBase : ScriptableObject
    {
        [SerializeField] protected GameObject _weaponPrefab;
        [SerializeField] protected float _damage;
        [SerializeField] protected float _speed;
        [SerializeField] protected float _cooldown;
        protected WeaponHolderControler _weaponHolder;
        public float Speed => _speed;
        public float Cooldown => _cooldown;
        
        
        public GameObject Init(WeaponHolderControler weaponHolder)
        {
            _weaponHolder = weaponHolder;
            return Execute();
        }
        protected virtual GameObject Execute()
        {
            //_currentCooldown = _cooldown;
            return null;
        }
    }
}
