using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RomaDoliba.Player
{
    public class PlayerCollector : MonoBehaviour
    {
        [SerializeField] private Collider2D _collerctor;
        [SerializeField] private LayerMask _pickUpsLayer;
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (_pickUpsLayer == (_pickUpsLayer | (1 << col.gameObject.layer)))
            {
                StartCoroutine(MoveItem(col.transform));
            }
        }
        private IEnumerator MoveItem(Transform item)
        {
            var speed = 5f;
            while (item.position != PlayerControler.Instance.transform.position)
            {
                item.position = Vector3.MoveTowards(item.position, PlayerControler.Instance.transform.position, speed * Time.deltaTime);
                yield return new WaitForEndOfFrame();
            }
        }
    }
}
