using System;
using UnityEngine;
using RomaDoliba.Player;

namespace RomaDoliba.ActionSystem
{
    public class ExperienceSender : ActionBase, ICollectible
    {
        [SerializeField] private int _expAmount;
        private PlayerStats _player;
        /*
        public static Action<int> OnEnemyDeath;
        public static void FireEvent(int expAmount)
        {
            OnEnemyDeath?.Invoke(expAmount);
        }
        */
        public void Collect()
        {
            _player = PlayerControler.Instance.gameObject.GetComponent<PlayerStats>();
            _player.GainExperience(_expAmount);
        }
        public override void Execute()
        {
            Collect();
            //FireEvent(_expAmount);
        }
    }
}
