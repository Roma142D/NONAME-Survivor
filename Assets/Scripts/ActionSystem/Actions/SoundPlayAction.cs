using System.Collections;
using RomaDoliba.Manager;
using UnityEngine;

namespace RomaDoliba.ActionSystem
{
    public class SoundPlayAction : ActionBase
    {
        [SerializeField] private AudioClip _clipToPlay;
        public override void Execute()
        {
            GameManager.Instance.ActionAudioSource.PlayOneShot(_clipToPlay);
        }
    }
}
