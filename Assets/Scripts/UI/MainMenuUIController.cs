using System.Collections;
using System.Collections.Generic;
using RomaDoliba.Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RomaDoliba.UI
{
    public class MainMenuUIController : UIController
    {
        [SerializeField] private GlobalCharactersData _globalCharacterData;
        [SerializeField] private CharacterScreen _characterScreen;
        private void Awake()
        {
            var lastCharacter = _globalCharacterData.GetCharacterDataByName(PlayerPrefs.GetString("CharacterName"));
            ChangeCharacterScreen(lastCharacter);
        }
        public void ChangeCharacterScreen(CharacterData characterData)
        {
            _characterScreen.CharacterImage.sprite = characterData.Skin;
            _characterScreen.HPValue.SetText(characterData.MaxHealth.ToString());
            _characterScreen.MSValue.SetText(characterData.MoveSpeed.ToString());
            _characterScreen.WeaponType.SetText(characterData.DefoltWeapon.WeaponType.ToString());
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
