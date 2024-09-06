using System;
using System.Collections;
using System.Collections.Generic;
using RomaDoliba.Player;
using UnityEngine;
using UnityEngine.InputSystem;
using static RomaDoliba.Player.MyPlayerInput;

namespace RomaDoliba.Weapon
{
    public class DaggerSpawner : WeaponBase
    {
        private List<GameObject> _spawnedDaggers;
        
        protected override void Start()
        {
            base.Start();
            _spawnedDaggers = new List<GameObject>();
        }
        protected override void Update()
        {
            base.Update();
            if (CurrentCooldown <= 0)
            {
                Execute();
            }
            
        }
        protected override void Execute()
        {
            //var dif = (transform.position + new Vector3(PlayerControler.Instance.MoveDirection.x, PlayerControler.Instance.MoveDirection.y) * 22f) - transform.position;
            var rotZ = Mathf.Atan2(PlayerControler.Instance.MoveDirection.y, PlayerControler.Instance.MoveDirection.x) * Mathf.Rad2Deg;
            var daggerRotation = Quaternion.Euler(0f, 0f, rotZ - 45f);
            var spawnedDagger = Instantiate(_weaponPrefab, transform.position, daggerRotation);
            spawnedDagger.transform.position = transform.position;
            _spawnedDaggers.Add(spawnedDagger);
                        
            base.Execute();
        }
        
    }
}
