using RomaDoliba.Player;
using UnityEngine;
using UnityEngine.UIElements;

namespace RomaDoliba.UI
{
    public enum CharacterName
    {
        Sceleton,
        Detective
    }
    public class CharacteeSelector : MonoBehaviour
    {
        public static CharacteeSelector Instance{get; private set;}
        [SerializeField] private GlobalWeaponData _globalCharacterData;
        private CharacterData _characterData = null;
                
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
            
        }

        public void SelectCharacter(CharacterData characterData)
        {
            var characterName = characterData.name;
            _characterData = characterData;
            PlayerPrefs.SetString("CharacterName", characterName);
            Debug.Log(characterName);
            PlayerPrefs.Save();
        }
        

        public static CharacterData GetData()
        {
            if (Instance._characterData == null)
            {
                Instance._characterData = Instance._globalCharacterData.GetCharacterDataByName(PlayerPrefs.GetString("CharacterName"));
            }

            return Instance._characterData;
        }

    }
}
