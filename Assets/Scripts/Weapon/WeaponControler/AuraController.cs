using System.Collections;
using UnityEngine;

namespace RomaDoliba.Weapon
{
    public class AuraController : MeleeWeapon
    {
        [SerializeField] private Vector3 _minScale;
        [SerializeField] private Vector3 _maxScale;
        private float _currentCooldown;
        private void Start()
        {
            StartAuraCorotine();
            _currentCooldown = _weaponData.Cooldown;
            //_auraCoroutine = StartCoroutine(AuraAnimationCorotine(this.gameObject, _weaponData.Speed));
        }
        private void Update()
        {
            _currentCooldown -= Time.deltaTime;
            if (_currentCooldown <= 0)
            {
                StartAuraCorotine();
                _currentCooldown = _weaponData.Cooldown;
            }
        }
        
        public void StartAuraCorotine()
        {
            StartCoroutine(AuraAnimationCorotine(this.gameObject, _weaponData.Speed));
        }

        private IEnumerator AuraAnimationCorotine(GameObject aura, float animSpeed)
        {
            var currentTime = 0f;
            var deltaTime = 0f;
            var endTime = 1f;
            var currentScale = aura.transform.localScale;
            while (deltaTime != animSpeed)
            {
                aura.transform.localScale = Vector3.Lerp(currentScale, _maxScale, currentTime);
                deltaTime = Mathf.Min(animSpeed, deltaTime + Time.deltaTime);
                currentTime = Mathf.Min(endTime, (endTime * deltaTime) / animSpeed);
                currentScale = aura.transform.localScale;
                yield return new WaitForEndOfFrame();
            }
            currentTime = 0f;
            deltaTime = 0f;
            while (deltaTime != animSpeed)
            {
                aura.transform.localScale = Vector3.Lerp(currentScale, _minScale, currentTime);
                deltaTime = Mathf.Min(animSpeed, deltaTime + Time.deltaTime);
                currentTime = Mathf.Min(endTime, (endTime * deltaTime) / animSpeed);
                currentScale = aura.transform.localScale;
                yield return new WaitForEndOfFrame();
            }
        }
    }
}
