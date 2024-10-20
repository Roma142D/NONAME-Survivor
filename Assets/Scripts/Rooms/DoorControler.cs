using System.Collections;
using System.Collections.Generic;
using RomaDoliba.Manager;
using UnityEngine;
namespace RomaDoliba.Terrain
{
    public class DoorControler : MonoBehaviour
    {
        [SerializeField] private RoomBase _room;
        [SerializeField] private LayerMask _playerLayer;
        [SerializeField] private float _delayToCloseDoors;

        private void OnTriggerExit2D(Collider2D collider)
        {
            if(_playerLayer == (_playerLayer | (1 << collider.gameObject.layer))) 
            {
                StartCoroutine(_room.CloseAfterDelay(_delayToCloseDoors));
                GameManager.Instance.CurrentRoom = _room;
            }
        }
    }
}
