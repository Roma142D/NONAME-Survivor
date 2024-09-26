using System.Collections;
using System.Collections.Generic;
using RomaDoliba.Player;
using UnityEngine;

namespace RomaDoliba
{
    [CreateAssetMenu(fileName = "GlobalWeaponData", menuName = "Data/GlobalWeaponData", order = 1)]
    public class GlobalWeaponData : ScriptableObject
    {
        [SerializeField] private CharacterData[] _charactersData;
        
        public CharacterData GetCharacterDataByName(string name)
        {
            foreach (var characterData in _charactersData)
            {
                if (characterData.name == name)
                {
                    return characterData;
                }
            }
            return null;
        }
    }
}
