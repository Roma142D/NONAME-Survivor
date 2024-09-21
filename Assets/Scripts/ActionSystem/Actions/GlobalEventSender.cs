using System;
using UnityEngine;

namespace RomaDoliba.ActionSystem
{
    public class GlobalEventSender : ActionBase
    {
        public static Action<string, float> OnEvent;
        [SerializeField] protected string _eventName;
        [SerializeField] protected float _value;
        public static void FireEvent(string eventName, float value)
        {
            OnEvent?.Invoke(eventName, value);
        }
        public override void Execute()
        {
            FireEvent(_eventName, _value);
        }
    }
}
