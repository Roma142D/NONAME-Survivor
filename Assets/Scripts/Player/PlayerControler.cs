using System;
using System.Collections;
using System.Collections.Generic;
using RomaDoliba.ActionSystem;
using RomaDoliba.Weapon;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using static RomaDoliba.Player.MyPlayerInput;

namespace RomaDoliba.Player
{
    public class PlayerControler : MonoBehaviour
    {
        public static PlayerControler Instance{get; private set;}
        [SerializeField] private PlayerStats _playerStats;
        [SerializeField] private Rigidbody2D _player;
        [SerializeField] private SpriteRenderer _playerRenderer;
        [SerializeField] private Animator _playerAnimator;
        [SerializeField] private Transform _camera;
        [SerializeField] private float _cameraSpeed;
        [SerializeField] private TextMeshProUGUI _testHP;
        private float _currentMoveSpeed;
        private float _currentHP;
        private float _maxHP;
        private MyPlayerInput _playerInput;
        private Vector2 _moveDirection;
        private Vector2 _lastMoveDirection;
        private Coroutine _takingDamage;

        public float CurrentMS {get => _currentMoveSpeed; set => _currentMoveSpeed = value;}
        public float CurrentHP {get => _currentHP; set => _currentHP = value;}
        public Vector2 LastMoveDirection => _lastMoveDirection;
        public Vector2 MoveDirection => _moveDirection;
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
            _playerInput = new MyPlayerInput();
            GlobalEventSender.OnEvent += TakeDamage;
            _testHP.SetText(_currentHP.ToString());
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
            StartCoroutine(CameraFolow(_player.transform.position, _cameraSpeed));
        }
        private void TakeDamage(string eventName, float damage)
        {
            if (eventName == "TakeDamage" && _takingDamage == null)
            {
                _takingDamage = StartCoroutine(TakeDamageCoroutine(damage));
                _testHP.SetText(_currentHP.ToString());
                if (_currentHP <= 0)
                {
                    Die();
                }
            }
        }
        private void Die()
        {
            Time.timeScale = 0;
        }
        public void Heal(int value)
        {
            _currentHP += value;
            if (_currentHP > _maxHP)
            {
                _currentHP = _maxHP;
            }
            _testHP.SetText(_currentHP.ToString());
        }
        private IEnumerator TakeDamageCoroutine(float damage)
        {
            _currentHP -= damage;
            yield return new WaitForSeconds(1f);
            _takingDamage = null;
        }
        
        private IEnumerator CameraFolow(Vector2 playerPosition, float delay)
        {
            var currentTime = 0f;
            var deltaTime = 0f;
            var endTime = 1f;
            var cameraStartPosition = _camera.position;
            while (deltaTime != delay)
            {
                _camera.position = Vector3.Lerp(cameraStartPosition, playerPosition, currentTime);
                deltaTime = Mathf.Min(delay, deltaTime + Time.deltaTime);
                currentTime = Mathf.Min(endTime, (endTime * deltaTime) / delay);

                yield return new WaitForEndOfFrame();
            }
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
    }
}
