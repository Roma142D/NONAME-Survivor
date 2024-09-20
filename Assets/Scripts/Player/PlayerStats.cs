using System;
using System.Collections;
using System.Collections.Generic;
using RomaDoliba.ActionSystem;
using TMPro;
using UnityEngine;

namespace RomaDoliba.Player
{
    public class PlayerStats : MonoBehaviour
    {
        [SerializeField] private List<LevelRange> _levelRanges;
        [SerializeField] private TextMeshProUGUI _expText;
        private int _currentExp = 0;
        private int _currentLevel = 1;
        private int _expCap;

        private void Awake()
        {
            //_levelRanges = new List<LevelRange>();
            _expCap = _levelRanges[0].ExpCapIncrease;
            ExperienceSender.OnEnemyDeath += GainExperience;
        }

        private void GainExperience(int exp)
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

        [System.Serializable]
        public struct LevelRange
        {
            public int StartLevel;
            public int EndLevel;
            public int ExpCapIncrease;
        }
    }
}
