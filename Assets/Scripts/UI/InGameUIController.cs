using System.Collections;
using System.Collections.Generic;
using RomaDoliba.ActionSystem;
using TMPro;
using UnityEngine;

namespace RomaDoliba.UI
{
    public class InGameUIController : UIController
    {
        [SerializeField] private TextMeshProUGUI _coinsCounter;
        private int _currentCoins;

        private void Awake()
        {
            _currentCoins = PlayerPrefs.GetInt("Coins");
            _coinsCounter.SetText(_currentCoins.ToString());
            GlobalEventSender.OnEvent += AddCoins;
        }
        public void AddCoins(string eventName, float value)
        {
            if (eventName == "Coin")
            {
                _currentCoins += (int)value; 
                _coinsCounter.SetText(_currentCoins.ToString());
            }
        }
    }
}
