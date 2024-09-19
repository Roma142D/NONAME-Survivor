using System.Collections;
using System.Collections.Generic;
using RomaDoliba.Player;
using UnityEngine;

namespace RomaDoliba.Weapon
{
    public class DaggerControler : ProjectileWeapon
    {
        //[SerializeField] private WeaponBase _weaponData;
        
        protected override void Start()
        {
            StartCoroutine(FireDaggerCorotine(this.gameObject, _weaponData.Speed, CalculateDirection()));
            base.Start();
        }
        protected override void OnEnable()
        {
            StartCoroutine(FireDaggerCorotine(this.gameObject, _weaponData.Speed, CalculateDirection()));
            base.OnEnable();
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
                deltaTime = Mathf.Min(speed, deltaTime + Time.deltaTime);
                currentTime = Mathf.Min(endTime, (endTime * deltaTime) / speed);

                yield return new WaitForEndOfFrame();
            }
        }
        protected override IEnumerator DelayToDisable()
        {
            return base.DelayToDisable();
        }
    }
}
