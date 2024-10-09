using RomaDoliba.Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RomaDoliba.UI
{
    public class MainMenuUIController : UIController
    {
        [SerializeField] private CharacterScreen _characterScreen;
        [SerializeField] private GlobalCharactersData _charactersData;
        private void Awake()
        {
            var lastCharacter = _charactersData.GetCharacterDataByName(PlayerPrefs.GetString(GlobalData.SELECTED_CHARACTER));
            ChangeCharacterScreen(lastCharacter);
        }
        public void ChangeCharacterScreen(CharacterData characterData)
        {
            _characterScreen.CharacterImage.sprite = characterData.Skin;
            _characterScreen.HPValue.SetText(characterData.MaxHealth.ToString());
            _characterScreen.MSValue.SetText(characterData.MoveSpeed.ToString());
            _characterScreen.WeaponType.SetText(characterData.DefoltWeapon.WeaponType.ToString());
        }
        public void SelectCharacter(CharacterData characterData)
        {
            var characterName = characterData.name;
            PlayerPrefs.SetString(GlobalData.SELECTED_CHARACTER, characterName);
            Debug.Log(characterName);
            PlayerPrefs.Save();
        }

        [System.Serializable]
        public struct CharacterScreen
        {
            public Image CharacterImage;
            public TextMeshProUGUI HPValue;
            public TextMeshProUGUI MSValue;
            public TextMeshProUGUI WeaponType;
        }
    }
}
