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
        [SerializeField] private List<Button> _buttons;
        private float _multiplierHP;
        private float _multiplierMS;
        private float _multiplierDMG;
        private int _weaponLvl;
        private List<Button> _tempBtnList;

        private void Awake()
        {
            _multiplierHP = 1;
            _multiplierMS = 1;
            _multiplierDMG = 1;
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

        public void IncreaseMaxHP()
        {
            PlayerControler.Instance.CurrentMaxHP += 10 * _multiplierHP;
            _multiplierHP += 0.5f;
            Time.timeScale = 1;
        }
        public void IncreaseMoveSpeed()
        {
            PlayerControler.Instance.CurrentMS += 10 * _multiplierMS;
            _multiplierMS += 0.5f;
            Time.timeScale = 1;
        }
        public void IncreaseDamage()
        {
            PlayerControler.Instance.WeaponHolder.IncreaseDamage(1 * _multiplierDMG);
            _multiplierDMG += 0.1f;
            Time.timeScale = 1;
        }
        public void AddWeapon(WeaponBase weapon)
        {
            PlayerControler.Instance.WeaponHolder.AddWeapon(weapon);
            Time.timeScale = 1;
        }

        public void RemoveBtnFromList(Button button)
        {
            _buttons.Remove(button);
            button.gameObject.transform.SetParent(null);
        }
    }
}
