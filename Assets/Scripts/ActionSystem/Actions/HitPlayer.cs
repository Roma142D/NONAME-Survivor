using System;
using RomaDoliba.Enemy;
using UnityEngine;

namespace RomaDoliba.ActionSystem
{
    public class HitPlayer : GlobalEventSender
    {
        [SerializeField] private EnemyData _enemyData;
                
        public override void Execute()
        {
            _value = _enemyData.Damage;
            FireEvent(_eventName, _value);
        }
    }
}
