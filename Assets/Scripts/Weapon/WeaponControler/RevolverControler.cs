using System.Collections;
using System.Collections.Generic;
using RomaDoliba.Player;
using UnityEngine;

namespace RomaDoliba.Weapon
{
    public class RevolverControler : MonoBehaviour
    {
        [SerializeField] private RevolverData _revolverData;
        [SerializeField] private Transform _shootPoint;
        [SerializeField] private BulletController _bulletPrefab;
        [SerializeField] private Animator _animator;
        private Joystick _weaponJoystick;
        private float _currentCooldown;
        private List<GameObject> _spawnedBullets;
        private Coroutine _shootingCoroutine;
        public int BulletsPerQueue {get; set;}
        private void Start()
        {
            _spawnedBullets = new List<GameObject>();
            _currentCooldown = _bulletPrefab.WeaponData.Cooldown;
            _bulletPrefab.WeaponData.WeaponJoystick = _weaponJoystick;
            BulletsPerQueue = _revolverData.BulletsPerQueue;
        }
        public void Init(Joystick joystick)
        {
            _weaponJoystick = joystick;
        }
        private void Update()
        {
            RotateGun();
            _currentCooldown -= Time.deltaTime;
            if (_currentCooldown <= 0 && _shootingCoroutine == null)
            {
                _shootingCoroutine = StartCoroutine(Shoot(BulletsPerQueue));
            }
        }
        private void RotateGun()
        {
            var rotZ = PlayerControler.Instance.ControlerType == ControlerType.Android 
                ? Mathf.Atan2(_weaponJoystick.Vertical, _weaponJoystick.Horizontal) * Mathf.Rad2Deg
                : Mathf.Atan2(PlayerControler.Instance.LastMoveDirection.y, PlayerControler.Instance.LastMoveDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rotZ);
            transform.localScale = Mathf.Abs(rotZ) > 90 ? new Vector3(1, -1, 1) : new Vector3(1, 1, 1);
        }
        private IEnumerator Shoot(int bulletsPerQueue)
        {
            var bulletsToShoot = bulletsPerQueue;
            while (bulletsToShoot != 0)
            {
                if (_spawnedBullets.Count < 12)
                {
                    var bullet = _bulletPrefab.WeaponData.Init(PlayerControler.Instance.WeaponHolder);
                    _spawnedBullets.Add(bullet);
                    bullet.GetComponent<BulletController>().ShootPoint = _shootPoint;
                    _animator.SetTrigger("Shoot");
                    bullet.GetComponent<BulletController>().StratBulletCoroutine(CalculateDirection());
                }
                else
                {
                    var pooledBullet = _spawnedBullets[0];
                    _spawnedBullets.Remove(pooledBullet);
                    pooledBullet.transform.position = _shootPoint.position;
                    var rotZ = PlayerControler.Instance.ControlerType == ControlerType.Android 
                    ? Mathf.Atan2(_weaponJoystick.Vertical, _weaponJoystick.Horizontal) * Mathf.Rad2Deg
                    : Mathf.Atan2(PlayerControler.Instance.LastMoveDirection.y, PlayerControler.Instance.LastMoveDirection.x) * Mathf.Rad2Deg;
                    pooledBullet.transform.rotation = Quaternion.Euler(0f, 0f, rotZ);
                    pooledBullet.SetActive(true);
                    _spawnedBullets.Add(pooledBullet);
                    pooledBullet.GetComponent<BulletController>().ShootPoint = _shootPoint;
                    _animator.SetTrigger("Shoot");
                    pooledBullet.GetComponent<BulletController>().StratBulletCoroutine(CalculateDirection());
                }
                bulletsToShoot -= 1;
                yield return new WaitForSeconds(0.5f);
            }
            _currentCooldown = _bulletPrefab.WeaponData.Cooldown;
            _shootingCoroutine = null;
        }
        private Vector3 CalculateDirection()
        {
            var fireDirection = _shootPoint.position;
            fireDirection += PlayerControler.Instance.ControlerType == ControlerType.Android
            ? new Vector3(_weaponJoystick.Horizontal, _weaponJoystick.Vertical, 0f).normalized * 44f
            : new Vector3(PlayerControler.Instance.MoveDirection.x, PlayerControler.Instance.MoveDirection.y, 0f) * 44f;

            if (fireDirection == _shootPoint.position)
            {
                fireDirection += Vector3.right * 44f;
            }
            return fireDirection;
        }
    }
}
