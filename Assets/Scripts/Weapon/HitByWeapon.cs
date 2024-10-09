using RomaDoliba.Enemy;
using RomaDoliba.Weapon;
using UnityEngine;

namespace RomaDoliba.ActionSystem
{
    public class HitByWeapon : MonoBehaviour
    {
        [SerializeField] private WeaponBase _weaponData;
        private EnemyStats _enemy;
        private void OnTriggerEnter2D(Collider2D enemy)
        {
            if (enemy.CompareTag("Enemy"))
            {
                if (enemy.TryGetComponent<EnemyStats>(out EnemyStats enemyStats))
                {
                    _enemy = enemyStats;
                    Execute();
                }
            }
        }
        
        public void Execute()
        {
            if (_enemy != null)
            {
                _enemy.GetHit(_weaponData.CurrentDamage);
                if (!_weaponData.PassThroughEnemies)
                {
                    this.gameObject.SetActive(false);
                }
            }
        }
    }
}
