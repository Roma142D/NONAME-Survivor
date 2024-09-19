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
        protected WeaponHolderControler _weaponHolder;
        public WeaponType WeaponType => _weaponType;
        public float Damage => _damage;
        public float Speed => _speed;
        public float Cooldown => _cooldown;
        public bool PassThroughEnemies => _passThroughEnemies;
        
        
        public GameObject Init(WeaponHolderControler weaponHolder)
        {
            _weaponHolder = weaponHolder;
            return Execute();
        }
        protected virtual GameObject Execute()
        {
            return null;
        }
    }
}
