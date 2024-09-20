using System;
using System.Collections;
using System.Collections.Generic;
using RomaDoliba.Weapon;
using UnityEngine;
using UnityEngine.InputSystem;
using static RomaDoliba.Player.MyPlayerInput;

namespace RomaDoliba.Player
{
    public class PlayerControler : MonoBehaviour
    {
        public static PlayerControler Instance{get; private set;}
        [SerializeField] private CharacterData _characterData;
        [SerializeField] private Rigidbody2D _player;
        [SerializeField] private SpriteRenderer _playerRenderer;
        [SerializeField] private Animator _playerAnimator;
        [SerializeField] private Transform _camera;
        [SerializeField] private float _cameraSpeed;
        private float _currentMoveSpeed;
        private float _currentHP;
        private MyPlayerInput _playerInput;
        private Vector2 _moveDirection;
        private Vector2 _lastMoveDirection;

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
            _playerAnimator.runtimeAnimatorController = _characterData.Animator;
            _playerRenderer.sprite = _characterData.Skin;
            _currentMoveSpeed = _characterData.MoveSpeed;
            _currentHP = _characterData.MaxHealth;
            _playerInput = new MyPlayerInput();
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
            _playerInput.Disable();
        }
    }
}
