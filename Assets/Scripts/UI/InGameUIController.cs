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
        [SerializeField] private GameObject _upgratesScreen;
        private int _currentCoins;
        private int _currentLevel;

        private void Awake()
        {
            _currentCoins = PlayerPrefs.GetInt("Coins");
            _coinsCounter.SetText(_currentCoins.ToString());
            GlobalEventSender.OnEvent += ChangeCoinValue;
            SetHPBar();
            _currentLevel = PlayerControler.Instance.PlayerStats.CurrentLevel;
            _expBar.maxValue = PlayerControler.Instance.PlayerStats.CurrentExpCap;
        }
        private void FixedUpdate()
        {
            SetHPBar();
            //_HPBar.value = PlayerControler.Instance.CurrentHP;
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
            _HPBar.maxValue = PlayerControler.Instance.CurrentMaxHP;
            _HPBar.value = PlayerControler.Instance.CurrentHP;
        }
        private void ExpChanging()
        {
            _levelCounter.SetText(PlayerControler.Instance.PlayerStats.CurrentLevel.ToString());
            _expBar.maxValue = PlayerControler.Instance.PlayerStats.CurrentExpCap;
            _expBar.value = PlayerControler.Instance.PlayerStats.CurrentExp;
            if (PlayerControler.Instance.PlayerStats.CurrentLevel > _currentLevel)
            {
                Time.timeScale = 0;
                ToggleScreen(_upgratesScreen);
                _currentLevel = PlayerControler.Instance.PlayerStats.CurrentLevel;
            }
        }
        
    }
}
