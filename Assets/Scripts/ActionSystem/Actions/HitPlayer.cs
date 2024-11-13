using System;
using RomaDoliba.Enemy;
using UnityEngine;

namespace RomaDoliba.ActionSystem
{
    public class HitPlayer : GlobalEventSender
    {
        [SerializeField] private EnemyStats _enemyStats;
                
        public override void Execute()
        {
            _value = _enemyStats.CurrentDamage;
            FireEvent(_eventName, _value);
        }
    }
}
