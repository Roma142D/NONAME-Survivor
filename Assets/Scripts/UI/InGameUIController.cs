using System.Collections;
using System.Collections.Generic;
using RomaDoliba.ActionSystem;
using RomaDoliba.Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RomaDoliba.UI
{
    public class InGameUIController : UIController
    {
        [SerializeField] private TextMeshProUGUI _coinsCounter;
        [SerializeField] private Slider _HPBar;
        [SerializeField] private Slider _expBar;
        [SerializeField] private TextMeshProUGUI _levelCounter;
        private int _currentCoins;

        private void Awake()
        {
            _currentCoins = PlayerPrefs.GetInt("Coins");
            _coinsCounter.SetText(_currentCoins.ToString());
            GlobalEventSender.OnEvent += ChangeCoinValue;
            SetHPBar();
            _expBar.maxValue = PlayerControler.Instance.PlayerStats.CurrentExpCap;
        }
        private void FixedUpdate()
        {
            _HPBar.value = PlayerControler.Instance.CurrentHP;
            ExpChanging();
        }
        public void ChangeCoinValue(string eventName, float value)
        {           
            if (eventName == "Coin")
            {
                _currentCoins += (int)value; 
                _coinsCounter.SetText(_currentCoins.ToString());
            }
        }
        public void SetHPBar()
        {
            _HPBar.maxValue = PlayerControler.Instance.CurrentHP;
            _HPBar.value = PlayerControler.Instance.CurrentHP;
        }
        private void ExpChanging()
        {
            _levelCounter.SetText(PlayerControler.Instance.PlayerStats.CurrentLevel.ToString());
            _expBar.maxValue = PlayerControler.Instance.PlayerStats.CurrentExpCap;
            _expBar.value = PlayerControler.Instance.PlayerStats.CurrentExp;
        }
        
    }
}
