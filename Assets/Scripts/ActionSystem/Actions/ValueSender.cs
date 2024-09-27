using System;
using UnityEngine;
using RomaDoliba.Player;
using RomaDoliba.PickUps;
using RomaDoliba.UI;

namespace RomaDoliba.ActionSystem
{
    public enum PickUpType
    {
        Experience,
        Food,
        Coin
    }
    public class ValueSender : GlobalEventSender, ICollectible
    {
        [SerializeField] private PickUpType _type;
        //[SerializeField] private int _value;
        private PlayerStats _player;
        
        public void Collect()
        {
            switch (_type)
            {
                case PickUpType.Experience:
                    _player = PlayerControler.Instance.gameObject.GetComponent<PlayerStats>();
                    _player.GainExperience((int)_value);
                    break;
                case PickUpType.Food:
                    PlayerControler.Instance.Heal((int)_value);
                    break;
                case PickUpType.Coin:
                    var curCoins = PlayerPrefs.GetInt("Coins");
                    curCoins += (int)_value;
                    PlayerPrefs.SetInt("Coins", curCoins);
                    PlayerPrefs.Save();
                    break;
            }
            
        }
        public override void Execute()
        {
            Collect();
            base.Execute();
        }
    }
}
