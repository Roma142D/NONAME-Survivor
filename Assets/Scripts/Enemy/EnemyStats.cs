using System.Collections;
using RomaDoliba.ActionSystem;
using RomaDoliba.PickUps;
using UnityEngine;

namespace RomaDoliba.Enemy
{
    public class EnemyStats : MonoBehaviour
    {
        [SerializeField] private EnemyData _enemyData;
        [SerializeField] private DropItem _dropItem;
        [SerializeField] private SpriteRenderer _enemyRenderer;
        [SerializeField] private Rigidbody2D _enemyRigidbody;
        private float _currentHealth;
        private float _currentSpeed;
        private float _currentDamage;
        private Coroutine _takingDamage;
        private Color _originColor;
        public float CurrentHP => _currentHealth;
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
            transform.localEulerAngles = Vector3.zero;
            _enemyRigidbody.simulated = true;
        }

        public void GetHit(float damage)
        {
            if (_takingDamage == null)
            {
                _takingDamage = StartCoroutine(TakeDamage(damage));
            }
        }

        private IEnumerator TakeDamage(float damage)
        {
            _originColor = _enemyRenderer.color;
            while (_enemyRenderer.color != Color.red)
            {
                _enemyRenderer.color = Color.Lerp(_enemyRenderer.color, Color.red, 0.5f);
                yield return new WaitForEndOfFrame();
            }
            _currentHealth -= damage;
            while(_enemyRenderer.color != _originColor)
            {
                _enemyRenderer.color = Color.Lerp(_enemyRenderer.color, _originColor, 0.5f);
                yield return new WaitForEndOfFrame();
            }

            
            if (_currentHealth <= 0)
            {
                var endRotation = new Vector3(0, 0, 90);
                _enemyRigidbody.simulated = false;
                
                while (transform.localEulerAngles.z <= 90)
                {            
                          
                    transform.Rotate(endRotation, 1f);
                                       
                    yield return new WaitForEndOfFrame();
                }
                
                _dropItem.DropRandomItem(transform.position);
                this.gameObject.SetActive(false);
            }
            _takingDamage = null;
        }
    }
}
