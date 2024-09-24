using UnityEngine;

namespace RomaDoliba.Enemy
{
    public class EnemyStats : MonoBehaviour
    {
        [SerializeField] private EnemyData _enemyData;
        private float _currentHealth;
        private float _currentSpeed;
        private float _currentDamage;
        public float MoveSpeed => _currentSpeed;

        private void Awake()
        {
            _currentHealth = _enemyData.Health;
            _currentSpeed = _enemyData.MoveSpeed;
            _currentDamage = _enemyData.Damage;
        }
        private void OnEnable()
        {
            _currentHealth = _enemyData.Health;
            _currentSpeed = _enemyData.MoveSpeed;
            _currentDamage = _enemyData.Damage;
        }

        public void GetHit(float damage)
        {
            _currentHealth -= damage;
            if (_currentHealth <= 0)
            {
                this.gameObject.SetActive(false);
            }
        }
    }
}
