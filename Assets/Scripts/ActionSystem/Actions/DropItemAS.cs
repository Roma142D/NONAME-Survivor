using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RomaDoliba.ActionSystem
{
    public class DropItemAS : ActionBase
    {
        [SerializeField] private List<Drop> _drops;

        public override void Execute()
        {
            foreach (var drop in _drops)
            {
                var rand = Random.Range(0, 100);
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
