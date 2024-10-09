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
                
        public void Collect()
        {
            switch (_type)
            {
                case PickUpType.Experience:
                    PlayerControler.Instance.PlayerStats.GainExperience((int)_value);
                    break;
                case PickUpType.Food:
                    PlayerControler.Instance.Heal((int)_value);
                    break;
                case PickUpType.Coin:
                    var collectedCoins = PlayerPrefs.GetInt(GlobalData.COINS_COLLECTED_IN_THIS_RUN, 0);
                    collectedCoins += (int)_value;
                    PlayerPrefs.SetInt(GlobalData.COINS_COLLECTED_IN_THIS_RUN, collectedCoins);
                    
                    var curCoins = PlayerPrefs.GetInt(GlobalData.TOTAL_COINS_AMOUNT);
                    curCoins += (int)_value;
                    PlayerPrefs.SetInt(GlobalData.TOTAL_COINS_AMOUNT, curCoins);
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
