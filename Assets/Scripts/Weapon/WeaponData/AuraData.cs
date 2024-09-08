using UnityEngine;

namespace RomaDoliba.Weapon
{
    [CreateAssetMenu(fileName = "AuraData", menuName = "Weapon/AuraData", order = 1)]
    public class AuraData : WeaponBase
    {
        protected override GameObject Execute()
        {
            var spawnedAura = Instantiate(_weaponPrefab, _weaponHolder.transform.position, Quaternion.identity, _weaponHolder.transform);
            return spawnedAura;
        }
    }
}
