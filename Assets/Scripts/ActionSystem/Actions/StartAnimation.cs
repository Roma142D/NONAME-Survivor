using UnityEngine;

namespace RomaDoliba.ActionSystem
{
    public class StartAnimation : ActionBase
    {
        [SerializeField] private string _stateName;
        [SerializeField] private Animator _animator;
        public override void Execute()
        {
            _animator.Play(_stateName);
        }
    }
}
