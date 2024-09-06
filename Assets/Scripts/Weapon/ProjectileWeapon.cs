using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RomaDoliba.Player;

namespace RomaDoliba.Weapon
{
    public class ProjectileWeapon : MonoBehaviour
    {
        [SerializeField] protected float _delayToDestroy;
        protected Vector3 _fireDirection;


        protected virtual void Start()
        {
            Destroy(this.gameObject, _delayToDestroy);
        }
        public Vector3 CalculateDirection(Vector3 lastDirection)
        {
             _fireDirection = PlayerControler.Instance.transform.position;
            _fireDirection += new Vector3(PlayerControler.Instance.MoveDirection.x, PlayerControler.Instance.MoveDirection.y, 0f) * 44f;
            
            if (_fireDirection == PlayerControler.Instance.transform.position)
            {
                /*
                if (_lastDirection != Vector3.zero)
                {
                    _fireDirection = _lastDirection;
                }
                else
                {
                    _fireDirection += Vector3.right * 44f;
                }
                */
            }
            return _fireDirection;

        }
    }
}
