using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RomaDoliba.Weapon
{
    [CreateAssetMenu(fileName = "AllWeaponData", menuName = "Weapon/AllWeponData", order = 0)]
    public class AllWeaponData : ScriptableObject
    {
        [SerializeField] private List<WeaponBase> _daggerData;
        [SerializeField] private List<WeaponBase> _auraData;

        public WeaponBase GetWeaponData(WeaponType weaponType, int weaponLvl)
        {
            WeaponBase weaponData = null;
            switch (weaponType)
            {
                case WeaponType.dagger:
                    if (weaponLvl <= MaxWeaponLevel(weaponType) && weaponLvl >= 0)
                {
                    weaponData = _daggerData[weaponLvl];
                }
                else
                {
                    Debug.Log($"DaggerMaxLvl {_daggerData.Count - 1}");
                }
                break;
                case WeaponType.aura:
                    if (weaponLvl <= MaxWeaponLevel(weaponType) && weaponLvl >= 0)
                {
                    weaponData = _auraData[weaponLvl];
                }
                else
                {
                    Debug.Log($"AuraMaxLvl {_auraData.Count - 1}");
                }
                break;
                
            }
            return weaponData;
        }
        public int MaxWeaponLevel(WeaponType weaponType)
        {
            int maxLvl = 0;
            switch (weaponType)
            {
                case WeaponType.dagger:
                    maxLvl = _daggerData.Count - 1;
                    break;
                case WeaponType.aura:
                    maxLvl = _auraData.Count - 1;
                    break;
            }
            return maxLvl;
        }
    }
}
