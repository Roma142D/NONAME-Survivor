using UnityEngine;

namespace RomaDoliba.Enemy
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "Enemy/EnemyData", order = 0)]
    public class EnemyData : ScriptableObject
    {
        [SerializeField] protected float _moveSpeed;
        [SerializeField] protected float _health;
        [SerializeField] protected float _damage;
        public float MoveSpeed => _moveSpeed;
        public float Health => _health;
        public float Damage => _damage;
    }
}
