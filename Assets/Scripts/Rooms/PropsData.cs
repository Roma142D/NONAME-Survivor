using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace RomaDoliba.Terrain
{
    [CreateAssetMenu (fileName = "PropsData", menuName = "Rooms/PropsData", order = 1)]
    public class PropsData : ScriptableObject
    {
        [SerializeField] private List<GameObject> _propsPrefabs;

        public void SpawnProps(Transform[] propsSpawnPosints, int numberOfPropsToSpawn)
        {
            for (int i = 0; i < numberOfPropsToSpawn; i++)
            {
                var ranProp = _propsPrefabs[Random.Range(0, _propsPrefabs.Count)];
                var ranPos = propsSpawnPosints[Random.Range(0, propsSpawnPosints.Length)];
                Instantiate(ranProp, ranPos.position, Quaternion.identity, ranPos);
                propsSpawnPosints.ToList().Remove(ranPos);
            }
        }
    }
}
