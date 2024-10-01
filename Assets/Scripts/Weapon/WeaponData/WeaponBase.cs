using UnityEngine;

namespace RomaDoliba.Weapon
{
    public enum WeaponType
    {
        dagger,
        aura
    }
    public class WeaponBase : ScriptableObject
    {
        [SerializeField] protected GameObject _weaponPrefab;
        [SerializeField] protected WeaponType _weaponType;
        [SerializeField] protected float _damage;
        [SerializeField] protected float _speed;
        [SerializeField] protected float _cooldown;
        [SerializeField] protected bool _passThroughEnemies;
        private float _currentDamage;
        private float _currentSpeed;
        private float _currentCooldown;
        protected WeaponHolderControler _weaponHolder;
        public WeaponType WeaponType => _weaponType;
        public float CurrentDamage {get => _currentDamage; set => _currentDamage = value;}
        public float Speed => _currentSpeed;
        public float Cooldown => _currentCooldown;
        public bool PassThroughEnemies => _passThroughEnemies;
        
        
        public GameObject Init(WeaponHolderControler weaponHolder)
        {
            _weaponHolder = weaponHolder;
            _currentDamage = _damage;
            _currentSpeed = _speed;
            _currentCooldown = _cooldown;
            return Execute();
        }
        protected virtual GameObject Execute()
        {
            return null;
        }
    }
}
