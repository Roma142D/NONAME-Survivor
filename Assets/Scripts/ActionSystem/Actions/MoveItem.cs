using System.Collections;
using RomaDoliba.Player;
using UnityEngine;

namespace RomaDoliba.ActionSystem
{
    public class MoveItem : ActionBase
    {
        [SerializeField] private Transform _itemToMove;
        [SerializeField] private Transform _moveTo;
        [SerializeField] private bool _moveToPlayer;

        public override void Execute()
        {
            if (_moveToPlayer)
            {
                _moveTo = PlayerControler.Instance.transform;
                StartCoroutine(MoveItemCoroutine(_itemToMove));
            }
            else
            {
                StartCoroutine(MoveItemCoroutine(_itemToMove));
            }
        }
        private IEnumerator MoveItemCoroutine(Transform item)
        {
            var speed = 1f;
            while (item.position != _moveTo.position)
            {
                item.position = Vector3.MoveTowards(item.position, _moveTo.position, speed * Time.deltaTime);
                yield return new WaitForEndOfFrame();
            }
        }
    }
}
