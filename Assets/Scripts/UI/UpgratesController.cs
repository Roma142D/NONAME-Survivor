using System.Collections;
using System.Collections.Generic;
using RomaDoliba.Weapon;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace RomaDoliba.Player
{
    public class UpgratesController : MonoBehaviour
    {
        [SerializeField] private Transform _buttonContainer;
        [SerializeField] private List<Button> _buttons;
        [SerializeField] private UpgradeWeaponButtons _weaponButtons;
        [SerializeField] private AllWeaponData _weaponsData;
        private float _multiplierHP;
        private float _multiplierMS;
        private float _multiplierDMG;
        private int _daggerLvl;
        private int _auraLvl;
        private List<Button> _tempBtnList;

        private void Awake()
        {
            _multiplierHP = 1;
            _multiplierMS = 1;
            _multiplierDMG = 1;
            _daggerLvl = 0;
            _auraLvl = 0;
            CheckCurrentWeapon();
        }
        private void OnEnable()
        {
            _tempBtnList = new List<Button>();
            _tempBtnList.AddRange(_buttons);
            foreach (var btn in _buttons)
            {
                btn.gameObject.SetActive(false);
            }
            for (int i = 0; i < 3; i++)
            {
                var ranBtn = _buttons[Random.Range(0, _buttons.Count)];
                ranBtn.gameObject.SetActive(true);
                _buttons.Remove(ranBtn);
            }
            _buttons.Clear();
            _buttons.AddRange(_tempBtnList);
        }
        private void CheckCurrentWeapon()
        {
            foreach (var weapon in PlayerControler.Instance.WeaponHolder.CurrentWeapons)
            {
                switch (weapon.WeaponType)
                {
                    case WeaponType.aura:
                        AddBtnToList(_weaponButtons.AddDaggerButton);
                        AddBtnToList(_weaponButtons.UpgradeAuraButton);
                        _auraLvl += 1;
                        break;
                    case WeaponType.dagger:
                        AddBtnToList(_weaponButtons.AddAuraButton);
                        AddBtnToList(_weaponButtons.UpgradeDaggerButton);
                        _daggerLvl += 1;
                        break;
                }
            }
        }

        public void IncreaseMaxHP()
        {
            PlayerControler.Instance.CurrentMaxHP += 10 * _multiplierHP;
            _multiplierHP += 0.2f;
            Time.timeScale = 1;
        }
        public void IncreaseMoveSpeed()
        {
            PlayerControler.Instance.CurrentMS += 10 * _multiplierMS;
            _multiplierMS += 0.2f;
            Time.timeScale = 1;
        }
        public void IncreaseDamage()
        {
            PlayerControler.Instance.WeaponHolder.IncreaseDamage(1 * _multiplierDMG);
            _multiplierDMG += 0.1f;
            Time.timeScale = 1;
        }
        public void AddDagger()
        {
            var dagger = _weaponsData.GetWeaponData(WeaponType.dagger, 0);
            PlayerControler.Instance.WeaponHolder.AddWeapon(dagger);
            _daggerLvl += 1;
            Time.timeScale = 1;
        }
        public void AddAura()
        {
            var aura = _weaponsData.GetWeaponData(WeaponType.aura, 0);
            Debug.Log(aura.name);
            PlayerControler.Instance.WeaponHolder.AddWeapon(aura);
            _auraLvl += 1;
            Time.timeScale = 1;
        }
        public void UpgradeDagger(Button buttonToRemoveOnDaggerMaxLvl)
        {
            var dagger = _weaponsData.GetWeaponData(WeaponType.dagger, _daggerLvl);
            if (dagger != null)
            {
                PlayerControler.Instance.WeaponHolder.AddWeapon(dagger);
                _daggerLvl += 1;
                if (_daggerLvl > _weaponsData.MaxWeaponLevel(WeaponType.dagger))
                {
                    RemoveBtnFromList(buttonToRemoveOnDaggerMaxLvl);
                }
            }
            
            Time.timeScale = 1;
        }
        public void UpgradeAura(Button buttonToRemoveOnDaggerMaxLvl)
        {
            var aura = _weaponsData.GetWeaponData(WeaponType.aura, _auraLvl);
            if (aura != null)
            {
                PlayerControler.Instance.WeaponHolder.AddWeapon(aura);
                _auraLvl += 1;
                if (_daggerLvl > _weaponsData.MaxWeaponLevel(WeaponType.aura))
                {
                    RemoveBtnFromList(buttonToRemoveOnDaggerMaxLvl);
                }
            }
            
            Time.timeScale = 1;
        }
        
        public void RemoveBtnFromList(Button button)
        {
            _buttons.Remove(button);
            Destroy(button.gameObject);
        }
        public void AddBtnToList(GameObject button)
        {
            if (!_buttons.Contains(button.GetComponent<Button>()))
            {
                button.gameObject.transform.SetParent(_buttonContainer);
                _buttons.Add(button.GetComponent<Button>());
            }
            else
            {
                Debug.Log("AlreadyContains");
            }
        }

        [System.Serializable]
        private struct UpgradeWeaponButtons
        {
            public GameObject AddDaggerButton;
            public GameObject UpgradeDaggerButton;
            public GameObject AddAuraButton;
            public GameObject UpgradeAuraButton;
        }
    }
}
