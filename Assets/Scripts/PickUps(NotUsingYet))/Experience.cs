using RomaDoliba.Player;
using UnityEngine;

namespace RomaDoliba.PickUps
{
    public class Experience : MonoBehaviour, ICollectible
    {
        [SerializeField] private int _expAmount;
        private PlayerStats _player;
        public void Collect()
        {
            _player = PlayerControler.Instance.gameObject.GetComponent<PlayerStats>();
            _player.GainExperience(_expAmount);
        }
    }
}
