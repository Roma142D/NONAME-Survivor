using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RomaDoliba.Terrain
{
    public class TileBase : MonoBehaviour
    {
        [SerializeField] private Transform[] _propsSpawnPoints;

        public Transform[] PropsSpawnPoints => _propsSpawnPoints;
        
    }
}
