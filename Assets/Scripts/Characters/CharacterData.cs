using System.Collections;
using System.Collections.Generic;
using RomaDoliba.Weapon;
using UnityEditor.Animations;
using UnityEngine;

namespace RomaDoliba.Player
{
    [CreateAssetMenu (fileName = "CharacterData", menuName = "CharacterData", order = 1)]
    public class CharacterData : ScriptableObject
    {
        [SerializeField] private WeaponBase _defoltWeapon;
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _maxHealth;
        [SerializeField] private float _pasiveRecovery;
        [SerializeField] private float _collectRange;
        [SerializeField] private Sprite _characterSkin;
        [SerializeField] private AnimatorController _animationControler;
        public WeaponBase DefoltWeapon => _defoltWeapon;
        public float MoveSpeed => _moveSpeed;
        public float MaxHealth => _maxHealth;
        public float PasiveRecovery => _pasiveRecovery;
        public float CollectRange => _collectRange;
        public Sprite Skin => _characterSkin;
        public AnimatorController Animator => _animationControler;

    }
}
