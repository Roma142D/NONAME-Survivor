using System;
using UnityEngine;
using RomaDoliba.Player;
using RomaDoliba.PickUps;

namespace RomaDoliba.ActionSystem
{
    public enum PickUpType
    {
        Experience,
        Food
    }
    public class ValueSender : ActionBase, ICollectible
    {
        [SerializeField] private PickUpType _type;
        [SerializeField] private int _value;
        private PlayerStats _player;
        public void Collect()
        {
            switch (_type)
            {
                case PickUpType.Experience:
                    _player = PlayerControler.Instance.gameObject.GetComponent<PlayerStats>();
                    _player.GainExperience(_value);
                    break;
                case PickUpType.Food:
                    PlayerControler.Instance.Heal(_value);
                    break;
            }
            
        }
        public override void Execute()
        {
            Collect();
        }
    }
}
