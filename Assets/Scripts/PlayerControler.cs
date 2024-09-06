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
        [SerializeField] private Rigidbody2D _player;
        [SerializeField] private SpriteRenderer _playerRenderer;
        [SerializeField] private float _moveSpeed;
        [SerializeField] private Animator _playerAnimator;
        [SerializeField] private Transform _camera;
        [SerializeField] private float _cameraSpeed;
        private MyPlayerInput _playerInput;
        private Vector2 _moveDirection;

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
            var modifiedVelocity = velocityByInput * _moveSpeed * Time.fixedDeltaTime;

            _player.velocity = new Vector2(modifiedVelocity.x, modifiedVelocity.y);
            if (_moveDirection != Vector2.zero)
            {
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
