using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RomaDoliba.Player;
using System;

namespace RomaDoliba.Weapon
{
    public class ProjectileWeapon : MonoBehaviour
    {
        [SerializeField] protected WeaponBase _weaponData;
        [SerializeField] protected float _delayToDestroy;
        private Vector3 _fireDirection;
        public WeaponBase WeaponData => _weaponData;

        protected virtual void Start()
        {
            //Destroy(this.gameObject, _delayToDestroy);
            StartCoroutine(DelayToEnable());
        }
        protected virtual void OnEnable()
        {
            StartCoroutine(DelayToEnable());
        }
        public Vector3 CalculateDirection()
        {
            _fireDirection = PlayerControler.Instance.transform.position;
            _fireDirection += new Vector3(PlayerControler.Instance.MoveDirection.x, PlayerControler.Instance.MoveDirection.y, 0f) * 44f;
            var lastMoveDirection = PlayerControler.Instance.LastMoveDirection;
            
            if (_fireDirection == PlayerControler.Instance.transform.position)
            {
                if (lastMoveDirection != Vector2.zero)
                {
                    _fireDirection += new Vector3(lastMoveDirection.x, lastMoveDirection.y, 0f) * 44f;
                }
                else
                {
                    _fireDirection += Vector3.right * 44f;
                }
            }
            return _fireDirection;
        }

        protected virtual IEnumerator DelayToEnable()
        {           
            yield return new WaitForSeconds(_delayToDestroy);
            ToggleWeapon();
        }
        protected virtual void ToggleWeapon()
        {
            if (this.gameObject.activeSelf == true)
            {
                this.gameObject.SetActive(false);
            }
            else
            {
                this.gameObject.SetActive(true);
            }
        }
    }
}
