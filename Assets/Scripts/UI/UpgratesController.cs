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
        [SerializeField] private List<WeaponBase> _daggersData;
        private float _multiplierHP;
        private float _multiplierMS;
        private float _multiplierDMG;
        private int _daggerLvl;
        private List<Button> _tempBtnList;

        private void Awake()
        {
            _multiplierHP = 1;
            _multiplierMS = 1;
            _multiplierDMG = 1;
            _daggerLvl = 0;
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
            PlayerControler.Instance.WeaponHolder.AddWeapon(_daggersData[_daggerLvl]);
            _daggerLvl += 1;
            Debug.Log(_daggerLvl);
            Time.timeScale = 1;
        }
        public void UpgradeDagger(Button buttonToRemoveOnDaggerMaxLvl)
        {
            Debug.Log(_daggerLvl);
            PlayerControler.Instance.WeaponHolder.AddWeapon(_daggersData[_daggerLvl]);
            _daggerLvl += 1;
            if (_daggerLvl >= _daggersData.Count)
            {
                RemoveBtnFromList(buttonToRemoveOnDaggerMaxLvl);
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
            //var btn = Instantiate(button, _buttonContainer.position, Quaternion.identity, _buttonContainer);
            button.gameObject.transform.SetParent(_buttonContainer);
            _buttons.Add(button.GetComponent<Button>());
        }
    }
}
