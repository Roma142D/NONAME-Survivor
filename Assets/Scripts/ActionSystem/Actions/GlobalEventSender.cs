using System;
using UnityEngine;

namespace RomaDoliba.ActionSystem
{
    public class GlobalEventSender : ActionBase
    {
        public static Action<string> OnEvent;
        [SerializeField] private string _eventName;
        public static void FireEvent(string eventName)
        {
            OnEvent?.Invoke(eventName);
        }
        public override void Execute()
        {
            FireEvent(_eventName);
        }
    }
}
