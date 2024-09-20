using System;
using UnityEngine;

namespace RomaDoliba.ActionSystem
{
    public class ExperienceSender : ActionBase
    {
        public static Action<int> OnEnemyDeath;
        [SerializeField] private int _expAmount;
        public static void FireEvent(int expAmount)
        {
            OnEnemyDeath?.Invoke(expAmount);
        }
        public override void Execute()
        {
            FireEvent(_expAmount);
            Debug.Log("Die");
        }
    }
}
