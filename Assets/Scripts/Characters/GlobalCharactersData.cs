using System.Collections;
using System.Collections.Generic;
using RomaDoliba.Player;
using UnityEngine;

namespace RomaDoliba
{
    [CreateAssetMenu(fileName = "GlobalCharactersData", menuName = "Data/GlobalCharactersData", order = 1)]
    public class GlobalCharactersData : ScriptableObject
    {
        [SerializeField] private CharacterData[] _charactersData;
        public CharacterData FirstCharacterData => _charactersData[0];
        
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
