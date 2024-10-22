using UnityEngine;
namespace RomaDoliba.ActionSystem
{
    public class DestroyAction : ActionBase
    {
        [SerializeField] private GameObject _objectToDestroy;
        [SerializeField] private float _delay;
        public override void Execute()
        {
            Destroy(_objectToDestroy, _delay);
        }
    }
}
