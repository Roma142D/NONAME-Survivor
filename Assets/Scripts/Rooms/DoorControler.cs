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
        [SerializeField] private SpriteRenderer _doorRenderer;
        [SerializeField] private Collider2D _doorCollider;
        [SerializeField] private ContactFilter2D _doorFilter;

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
