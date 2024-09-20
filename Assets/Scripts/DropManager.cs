using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RomaDoliba.Manager
{
    public class DropManager : MonoBehaviour
    {
        [SerializeField] private List<Drop> _drops;
        private void OnDisable()
        {
            var rand = Random.Range(0, 100);
            foreach (var drop in _drops)
            {
                if (rand <= drop.DropChance)
                {
                    Instantiate(drop.DropPrefab, transform.position, Quaternion.identity);
                    break;
                }
            }
        }
        [System.Serializable]
        public struct Drop
        {
            public GameObject DropPrefab;
            public int DropChance;
        }
    }
}
