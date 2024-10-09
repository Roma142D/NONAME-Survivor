using System.Collections;
using UnityEngine;
using RomaDoliba.Player;

namespace RomaDoliba.Weapon
{
    public class MeleeWeapon : MonoBehaviour
    {
        [SerializeField] protected WeaponBase _weaponData;
        [SerializeField] protected AudioSource _audioSource;
    }
}
