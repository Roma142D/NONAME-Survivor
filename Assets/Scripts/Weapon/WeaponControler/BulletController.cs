using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RomaDoliba.Weapon
{
    public class BulletController : DaggerControler
    {
        [SerializeField] private Animator _animator;
        public Transform ShootPoint {get; set;}
        protected override void Start()
        {
            StartCoroutine(DelayToDisable(transform.gameObject));
        }
        protected override void OnEnable()
        {
            StartCoroutine(DelayToDisable(transform.gameObject));
        }

        protected override IEnumerator FireDaggerCorotine(GameObject dagger, float speed, Vector3 direction)
        {
            transform.position = ShootPoint.position;
            _animator.SetTrigger("Shoot");
            yield return new WaitForSecondsRealtime(0.4f);
            var currentTime = 0f;
            var deltaTime = 0f;
            var endTime = 1f;
            var startPosition = ShootPoint.position;
            _audioSource.Play();
            
            while (deltaTime != speed)
            {
                dagger.transform.position = Vector3.LerpUnclamped(startPosition, direction, currentTime);
                deltaTime = Mathf.Min(speed, deltaTime + Time.deltaTime);
                currentTime = Mathf.Min(endTime, (endTime * deltaTime) / speed);

                yield return new WaitForFixedUpdate();
            }
        }
        protected override IEnumerator DelayToDisable(GameObject objToDisable)
        {
            return base.DelayToDisable(objToDisable);
        }
        public void StratBulletCoroutine(Vector3 direction)
        {
            StartCoroutine(FireDaggerCorotine(gameObject, _weaponData.Speed, direction));
        }
    }
}
