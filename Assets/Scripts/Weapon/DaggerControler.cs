using System.Collections;
using System.Collections.Generic;
using RomaDoliba.Player;
using UnityEngine;

namespace RomaDoliba.Weapon
{
    public class DaggerControler : ProjectileWeapon
    {
        private DaggerSpawner _daggerData;
        
        protected override void Start()
        {
            base.Start();
            _daggerData = FindAnyObjectByType<DaggerSpawner>();
            _fireDirection = PlayerControler.Instance.transform.position;
            _fireDirection += new Vector3(PlayerControler.Instance.MoveDirection.x, PlayerControler.Instance.MoveDirection.y, 0f) * 44f;
            
            if (_fireDirection == PlayerControler.Instance.transform.position)
            {
                
                _fireDirection += Vector3.right * 44f;
            
            }
            StartCoroutine(FireDaggerCorotine(this.gameObject, _daggerData.Speed, _fireDirection));
        }

        private IEnumerator FireDaggerCorotine(GameObject dagger, float speed, Vector3 direction)
        {
            var currentTime = 0f;
            var deltaTime = 0f;
            var endTime = 1f;
            var startPosition = dagger.transform.position;
            
            while (deltaTime != speed)
            {
                dagger.transform.position = Vector3.Lerp(startPosition, direction, currentTime);
                //dagger.transform.Rotate(new Vector3(0f, 0f, _daggerData.RotationSpeed * Time.deltaTime));
                deltaTime = Mathf.Min(speed, deltaTime + Time.deltaTime);
                currentTime = Mathf.Min(endTime, (endTime * deltaTime) / speed);

                yield return new WaitForEndOfFrame();
            }
        }
    }
}
