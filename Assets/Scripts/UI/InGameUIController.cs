using RomaDoliba.ActionSystem;
using RomaDoliba.Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RomaDoliba.UI
{
    public class InGameUIController : UIController
    {
        [SerializeField] private PlayerStatsUI _playerStatsUI;
        [SerializeField] private GameObject _upgratesScreen;
        [SerializeField] private GameOverUI _gameOverScreen;
        private int _currentCoins;
        private int _currentLevel;

        private void Awake()
        {
            Time.timeScale = 1;
            _currentCoins = PlayerPrefs.GetInt(GlobalData.TOTAL_COINS_AMOUNT);
            _playerStatsUI.CoinsCounter.SetText(_currentCoins.ToString());
            GlobalEventSender.OnEvent += ChangeCoinValue;
            SetHPBar();
            _currentLevel = PlayerControler.Instance.PlayerStats.CurrentLevel;
            _playerStatsUI.ExpBar.maxValue = PlayerControler.Instance.PlayerStats.CurrentExpCap;
        }
        private void FixedUpdate()
        {
            SetHPBar();
            ExpChanging();
            GameOverCheck();
        }
        public void ChangeCoinValue(string eventName, float value)
        {           
            if (eventName == GlobalData.COIN_COLLECT)
            {
                _currentCoins += (int)value; 
                _playerStatsUI.CoinsCounter.SetText(_currentCoins.ToString());
            }
        }
        public void SetHPBar()
        {
            _playerStatsUI.HPBar.maxValue = PlayerControler.Instance.CurrentMaxHP;
            _playerStatsUI.HPBar.value = PlayerControler.Instance.CurrentHP;
        }
        private void ExpChanging()
        {
            _playerStatsUI.LevelCounter.SetText(PlayerControler.Instance.PlayerStats.CurrentLevel.ToString());
            _playerStatsUI.ExpBar.maxValue = PlayerControler.Instance.PlayerStats.CurrentExpCap;
            _playerStatsUI.ExpBar.value = PlayerControler.Instance.PlayerStats.CurrentExp;
            if (PlayerControler.Instance.PlayerStats.CurrentLevel > _currentLevel)
            {
                ToggleScreen(_upgratesScreen);
                _currentLevel = PlayerControler.Instance.PlayerStats.CurrentLevel;
            }
        }
        private void GameOverCheck()
        {
            if (PlayerControler.Instance.CurrentHP <= 0)
            {
                ToggleScreen(_gameOverScreen.GameOverScreen);
                _gameOverScreen.EnemiesKilled.SetText(PlayerPrefs.GetInt(GlobalData.ENEMIES_KILLED_IN_THIS_RUN).ToString());
                _gameOverScreen.CoinsCollected.SetText(PlayerPrefs.GetInt(GlobalData.COINS_COLLECTED_IN_THIS_RUN).ToString());
                PlayerPrefs.DeleteKey(GlobalData.ENEMIES_KILLED_IN_THIS_RUN);
                PlayerPrefs.DeleteKey(GlobalData.COINS_COLLECTED_IN_THIS_RUN);
            }
        }
        
        [System.Serializable]
        public struct PlayerStatsUI
        {
            public TextMeshProUGUI CoinsCounter;
            public Slider HPBar;
            public Slider ExpBar;
            public TextMeshProUGUI LevelCounter;
        }
        [System.Serializable]
        public struct GameOverUI
        {
            public GameObject GameOverScreen;
            public TextMeshProUGUI EnemiesKilled;
            public TextMeshProUGUI CoinsCollected;
        }
    }
}
