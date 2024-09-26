using System;
using System.Collections;
using System.Collections.Generic;
using RomaDoliba.ActionSystem;
using RomaDoliba.UI;
using TMPro;
using UnityEngine;

namespace RomaDoliba.Player
{
    public class PlayerStats : MonoBehaviour
    {
        [Space]
        [Header("Leveling")]
        [SerializeField] private List<LevelRange> _levelRanges;
        [SerializeField] private TextMeshProUGUI _expText;
        private int _currentExp = 0;
        private int _currentLevel = 1;
        private int _expCap;
        [SerializeField] private CharacterData _characterData;
                        
        public void Init(Animator animator, SpriteRenderer renderer)
        {
            Debug.Log(PlayerPrefs.GetString("CharacterName"));
            
            //_characterData = CharacteeSelector.GetData();
            animator.runtimeAnimatorController = _characterData.Animator;
            renderer.sprite = _characterData.Skin;
            PlayerControler.Instance.CurrentMS = _characterData.MoveSpeed;
            PlayerControler.Instance.CurrentHP = _characterData.MaxHealth;
            PlayerControler.Instance.CurrentCollectRange = _characterData.CollectRange;
            PlayerControler.Instance.DefoltWeapon = _characterData.DefoltWeapon;
        }
        private void Awake()
        {
            _expCap = _levelRanges[0].ExpCapIncrease;
        }
        private void Update()
        {
            Recover();
        }

        public void GainExperience(int exp)
        {
            _currentExp += exp;
            LevelUpChecker();
            _expText.SetText($"Level:{_currentLevel} Exp:{_currentExp}");
        }
        
        private void LevelUpChecker()
        {
            if (_currentExp >= _expCap)
            {
                _currentLevel ++;
                _currentExp -= _expCap;

                var expCapIncrease = 0;
                foreach (var range in _levelRanges)
                {
                    if(_currentLevel >= range.StartLevel && _currentLevel <= range.EndLevel)
                    {
                        expCapIncrease = range.ExpCapIncrease;
                        break;
                    }
                }
                _expCap += expCapIncrease;
            }
        }

        public void Recover()
        {
            if (PlayerControler.Instance.CurrentHP < _characterData.MaxHealth)
            {
                PlayerControler.Instance.CurrentHP += _characterData.PasiveRecovery * Time.deltaTime;
                if (PlayerControler.Instance.CurrentHP > _characterData.MaxHealth)
                {
                    PlayerControler.Instance.CurrentHP = _characterData.MaxHealth;
                }
                PlayerControler.Instance._testHP.SetText(PlayerControler.Instance.CurrentHP.ToString());
            }
        }


        [System.Serializable]
        public struct LevelRange
        {
            public int StartLevel;
            public int EndLevel;
            public int ExpCapIncrease;
        }
    }
}
