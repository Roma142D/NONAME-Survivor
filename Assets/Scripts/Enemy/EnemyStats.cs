using System.Collections;
using System.Collections.Generic;
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
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private List<AudioClip> _takeDamageClips;
        [SerializeField] private Animator _animator;

        private float _currentHealth;
        private float _currentSpeed;
        private Coroutine _takingDamage;
        private Color _originColor;
        public Animator EnemyAnimator => _animator;
        public float CurrentDamage {get; set;}
        public float CurrentHP => _currentHealth;
        public float MoveSpeed => _currentSpeed;

        private void Awake()
        {
            _currentHealth = _enemyData.Health;
            _currentSpeed = _enemyData.MoveSpeed;
            CurrentDamage = _enemyData.Damage;
        }
        private void OnEnable()
        {
            _currentHealth = _enemyData.Health;
            _currentSpeed = _enemyData.MoveSpeed;
            CurrentDamage = _enemyData.Damage;
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
            _audioSource.clip = _takeDamageClips[Random.Range(0, _takeDamageClips.Count)];
            _audioSource.Play();
            while (_enemyRenderer.color != Color.red)
            {
                _enemyRenderer.color = Color.Lerp(_enemyRenderer.color, Color.red, 1f);
                yield return new WaitForSeconds(0.2f);
            }
            _currentHealth -= damage;
            while(_enemyRenderer.color != _originColor)
            {
                _enemyRenderer.color = Color.Lerp(_enemyRenderer.color, _originColor, 1f);
                yield return new WaitForSeconds(0.2f);
            }

            
            if (_currentHealth <= 0)
            {
                _animator.SetTrigger("Death");
                var killedEnemies = PlayerPrefs.GetInt(GlobalData.ENEMIES_KILLED_IN_THIS_RUN, 0);
                killedEnemies += 1;
                PlayerPrefs.SetInt(GlobalData.ENEMIES_KILLED_IN_THIS_RUN, killedEnemies);
                PlayerPrefs.Save();
                _dropItem.DropRandomItem(transform.position);
            }
            _takingDamage = null;
        }
        private void EnableEnemy()
        {
            gameObject.SetActive(false);
        }
    }
}
