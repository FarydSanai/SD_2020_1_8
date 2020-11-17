using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SamuraiGame
{
    public static class SoundManager
    {
        public static void PlaySound()
        {
            GameObject soundGameObj = new GameObject("Sound");
            AudioSource audioSource = soundGameObj.AddComponent<AudioSource>();

            GameSounds clip = GameObject.Find("GameSounds").GetComponent<GameSounds>();
            //audioSource.clip = clip;
            audioSource.PlayOneShot(clip.playerAttack);
        }

    }
}