using UnityEngine;
using System;

namespace RomaDoliba.ActionSystem
{
    public class OnPhysikExecutor : ExecutorBase
    {
        [SerializeField] private bool _onCollision;
        [SerializeField] private bool _onTrigger;
        [SerializeField] private State _state;

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (_onTrigger && _state._enter)
            {
                Execute(collider);
            }
        }
        private void OnTriggerExit2D(Collider2D collider)
        {
            if (_onTrigger && _state._exit)
            {
                Execute(collider);
            }
        }
        private void OnTriggerStay2D(Collider2D collider)
        {
            if (_onTrigger && _state._stay)
            {
                Execute(collider);
            }
        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (_onCollision && _state._enter)
            {
                Execute(collision);
            }
        }
        private void OnCollisionExit2D(Collision2D collision)
        {
            if (_onCollision && _state._exit)
            {
                Execute(collision);
            }
        }
        private void OnCollisionStay2D(Collision2D collision)
        {
            if (_onCollision && _state._stay)
            {
                Execute(collision);
            }
        }
        
        [Serializable]
        public struct State
        {
            public bool _enter;
            public bool _stay;
            public bool _exit;
        }
    }
}
