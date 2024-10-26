using System.Collections;
using System.Collections.Generic;
using RomaDoliba.Player;
using UnityEngine;

namespace RomaDoliba.Weapon
{
    public class RevolverControler : MonoBehaviour
    {
        private Joystick _weaponJoystick;
        public void Init(Joystick joystick)
        {
            _weaponJoystick = joystick;
        }
        private void Update()
        {
            var rotZ = PlayerControler.Instance.ControlerType == ControlerType.Android 
                ? Mathf.Atan2(_weaponJoystick.Vertical, _weaponJoystick.Horizontal) * Mathf.Rad2Deg
                : Mathf.Atan2(PlayerControler.Instance.LastMoveDirection.y, PlayerControler.Instance.LastMoveDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rotZ); 
        }
    }
}
