using System.Collections.Generic;
using UnityEngine;

namespace RomaDoliba.Sound
{
    public class BackGroundMusic : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private float _mixRate;
        [SerializeField] private List<AudioClip> _audioClips;
        private int _currentClipIndex = 0;
        private bool _isEnoughTimeToPlayNext => _audioSource.clip.length - _mixRate <= _audioSource.time;

        private void Update()
        {
            if (!_audioSource.isPlaying || (_audioSource.isPlaying && _isEnoughTimeToPlayNext))
            {
                _audioSource.clip = _audioClips[_currentClipIndex];
                _audioSource.Play();
                _currentClipIndex = GetNextClipIndex(_currentClipIndex, _audioClips.Count);
            }
        }
        private int GetNextClipIndex(int current, int length)
        {
            var next = current + 1;
            if (next < length)
            {
                return next;
            }
            else
            {
                return 0;
            }
        }
    }
}
