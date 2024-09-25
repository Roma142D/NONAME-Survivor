using RomaDoliba.Player;
using UnityEngine;
//using UnityEngine.UI;
using UnityEngine.UIElements;

namespace RomaDoliba.UI
{
    public class CharacteeSelector : MonoBehaviour
    {
        public static CharacteeSelector Instance{get; private set;}
        private CharacterData _characterData;
                
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
            _characterData = characterData;
            Debug.Log(_characterData);
        }
        

        public static CharacterData GetData()
        {
            return Instance._characterData;
        }

    }
}
