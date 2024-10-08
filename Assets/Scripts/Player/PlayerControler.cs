//using System;
using System.Collections;
using System.Collections.Generic;
using RomaDoliba.ActionSystem;
using RomaDoliba.Weapon;
using UnityEngine;

namespace RomaDoliba.Player
{
    public class PlayerControler : MonoBehaviour
    {
        public static PlayerControler Instance{get; private set;}
        [SerializeField] private PlayerStats _playerStats;
        [SerializeField] private WeaponHolderControler _weaponHolder;
        [SerializeField] private Rigidbody2D _player;
        [SerializeField] private SpriteRenderer _playerRenderer;
        [SerializeField] private Animator _playerAnimator;
        [SerializeField] private Transform _camera;
        [SerializeField] private float _cameraSpeed;
        [SerializeField] private CircleCollider2D _collector;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private List<AudioClip> _takeDamageClips;
        
        private float _currentMoveSpeed;
        private float _currentHP;
        private float _maxHP;
        private MyPlayerInput _playerInput;
        private Vector2 _moveDirection;
        private Vector2 _lastMoveDirection;
        private Coroutine _takingDamage;
        private Coroutine _cameraFollow;
        private float _currentCollectRange;
        private WeaponBase _defoltWeapon;
        
        public WeaponBase DefoltWeapon {get => _defoltWeapon; set => _defoltWeapon = value;}
        public float CurrentMS {get => _currentMoveSpeed; set => _currentMoveSpeed = value;}
        public float CurrentHP {get => _currentHP; set => _currentHP = value;}
        public float CurrentMaxHP {get => _maxHP; set => _maxHP = value;}
        public float CurrentCollectRange {get => _currentCollectRange; set => _currentCollectRange = value;}
        public Vector2 LastMoveDirection => _lastMoveDirection;
        public Vector2 MoveDirection => _moveDirection;
        public PlayerStats PlayerStats => _playerStats;
        public WeaponHolderControler WeaponHolder => _weaponHolder;
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
            _playerStats.Init(_playerAnimator, _playerRenderer);
            _maxHP = _currentHP;
            _collector.radius = _currentCollectRange;
            _playerInput = new MyPlayerInput();
            GlobalEventSender.OnEvent += TakeDamage;
        }

        
        private void Update()
        {
            _moveDirection = _playerInput.Player.Move.ReadValue<Vector2>();
        }
        private void FixedUpdate()
        {
            Move();
        }
                       
        private void Move()
        {
            var velocityByInput = new Vector2(_moveDirection.x, _moveDirection.y).normalized;
            var modifiedVelocity = velocityByInput * _currentMoveSpeed * Time.fixedDeltaTime;

            _player.velocity = new Vector2(modifiedVelocity.x, modifiedVelocity.y);
            if (_moveDirection != Vector2.zero)
            {
                _lastMoveDirection = _moveDirection;
                _playerAnimator.SetTrigger("Walk");
                if (_moveDirection.x > 0)
                {
                    _playerRenderer.flipX = false;
                }
                else if (_moveDirection.x < 0)
                {
                    _playerRenderer.flipX = true;
                }
            }
            if (_cameraFollow == null)
            {
                _cameraFollow = StartCoroutine(CameraFolow(_cameraSpeed));
            }
        }
        private void TakeDamage(string eventName, float damage)
        {
            if (eventName == "TakeDamage" && _takingDamage == null)
            {
                _takingDamage = StartCoroutine(TakeDamageCoroutine(damage));
            }
        }
        public void Heal(int value)
        {
            _currentHP += value;
            if (_currentHP > _maxHP)
            {
                _currentHP = _maxHP;
            }
        }

        private IEnumerator TakeDamageCoroutine(float damage)
        {
            var originColor = _playerRenderer.color;
            _currentHP -= damage;
            _audioSource.clip = _takeDamageClips[Random.Range(0, _takeDamageClips.Count)];
            _audioSource.Play();
            while (_playerRenderer.color != Color.red)
            {
                _playerRenderer.color = Color.Lerp(_playerRenderer.color, Color.red, 0.25f);
                yield return new WaitForEndOfFrame();
            }
            while(_playerRenderer.color != originColor)
            {
                _playerRenderer.color = Color.Lerp(_playerRenderer.color, originColor, 0.25f);
                yield return new WaitForEndOfFrame();
            }
            _takingDamage = null;
        }
        
        private IEnumerator CameraFolow(float delay)
        {
            var currentTime = 0f;
            var deltaTime = 0f;
            var endTime = 1f;
            while (_camera.position != transform.position)
            {
                _camera.position = Vector3.Lerp(_camera.position, transform.position, currentTime);
                deltaTime = Mathf.Min(delay, deltaTime + Time.deltaTime);
                currentTime = Mathf.Min(endTime, (endTime * deltaTime) / delay);

                yield return new WaitForEndOfFrame();
            }
            _cameraFollow = null;
        }
        
        
        private void OnEnable()
        {
            _playerInput.Enable();
        }
        private void OnDisable()
        {
            GlobalEventSender.OnEvent -= TakeDamage;
            _playerInput.Disable();
        }

        [System.Serializable]
        public struct LevelData
        {
            public int Level;
            public int Experience;
        }
    }
}
