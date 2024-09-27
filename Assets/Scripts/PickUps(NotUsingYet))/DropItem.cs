using System.Collections;
using System.Collections.Generic;
using RomaDoliba.Manager;
using Unity.VisualScripting;
using UnityEngine;

namespace RomaDoliba.PickUps
{
    [CreateAssetMenu(fileName = "ItemEjector", menuName = "Items/ItemEjector", order = 1)]
    public class DropItem : ScriptableObject
    {
        [SerializeField] private List<Drop> _drops;
        private GameManager _gameManager;
        
        private void Awake()
        {
            _gameManager = FindObjectOfType<GameManager>();
        }
        public GameObject DropRandomItem(Vector3 dropPosition)
        {
            foreach (var drop in _drops)
            {
                var rand = Random.Range(0, 100);
                GameObject dropedItem = null;
                if (rand <= drop.DropChance)
                {
                    if (_gameManager.DropedItems.Count <= 5)
                    {
                        dropedItem = Instantiate(drop.DropPrefab, dropPosition, Quaternion.identity, _gameManager.DropedItemsCollector);
                        _gameManager.AddItemToPool(dropedItem);
                        return dropedItem;
                    }
                    else
                    {
                        foreach (var item in _gameManager.DropedItems)
                        {
                            var dropItemName = $"{drop.DropPrefab.name}(Clone)";
                            
                            if (item.activeSelf == false && item.name == dropItemName)
                            {
                                item.transform.position = dropPosition;
                                item.SetActive(true);
                                _gameManager.DropedItems.Remove(item);
                                dropedItem = item;
                                _gameManager.AddItemToPool(dropedItem);
                                return dropedItem;
                            }
                        }
                        if (dropedItem == null)
                        {
                            dropedItem = Instantiate(drop.DropPrefab, dropPosition, Quaternion.identity, _gameManager.DropedItemsCollector);
                            _gameManager.AddItemToPool(dropedItem);
                            return dropedItem;
                        }
                    }
                }
            }
            return null;
        }

        [System.Serializable]
        public struct Drop
        {
            public GameObject DropPrefab;
            public int DropChance;
        }
    }
}
