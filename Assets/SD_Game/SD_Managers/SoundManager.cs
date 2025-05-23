﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SamuraiGame
{
    public static class SoundManager
    {
        public enum Sound
        {
            NONE,
            PlayerAttack,
            PlayerWalk,
            PlayerRun,
            PlayerRunToStop,
            PlayerJump,
            PlayerLanding,
            DeathFall,
        }

        private static Dictionary<Sound, float> soundTimingDic;
        private static GameObject singleSoundObj;
        private static AudioSource singleSoundSource;

        public static void Initialize()
        {
            Sound[] soundArr = System.Enum.GetValues(typeof(Sound)) as Sound[];
            soundTimingDic = new Dictionary<Sound, float>();

            for (int i = 0; i < soundArr.Length; i++)
            {
                soundTimingDic[soundArr[i]] = 0f;
            }
        }
        public static void PlaySound(Sound sound, Vector3 position)
        {
            if (IntervalSound(sound))
            {
                GameObject soundGameObj = new GameObject("Sound");
                soundGameObj.transform.position = position;

                AudioSource audioSource = soundGameObj.AddComponent<AudioSource>();
                audioSource.clip = GetAudioClip(sound);
                audioSource.volume = 0.25f;
                audioSource.maxDistance = 100f;
                audioSource.spatialBlend = 1f;
                audioSource.rolloffMode = AudioRolloffMode.Linear;
                audioSource.dopplerLevel = 0f;

                audioSource.Play();

                Object.Destroy(soundGameObj, audioSource.clip.length);
            }
        }
        public static void PlaySound(Sound sound)
        {
            if (IntervalSound(sound))
            {
                if (singleSoundObj == null)
                {
                    singleSoundObj = new GameObject("Sound");
                    singleSoundSource = singleSoundObj.AddComponent<AudioSource>();
                }
                singleSoundSource.PlayOneShot(GetAudioClip(sound));
            }
        }

        public static bool IntervalSound(Sound sound)
        {
            switch (sound)
            {
                default:
                    {
                        return true;
                    }
                case Sound.PlayerWalk:
                    {
                        return MakeInterval(sound, 0.5f);
                    }
                case Sound.PlayerRun:
                    {
                        return MakeInterval(sound, 0.25f);
                    }
                case Sound.PlayerRunToStop:
                    {
                        return MakeInterval(sound, 1f);
                    }
                case Sound.PlayerLanding:
                    {
                        return MakeInterval(sound, 1f);
                    }
                    //break;
            }
        }

        private static bool MakeInterval(Sound sound, float timing)
        {
            if (soundTimingDic.ContainsKey(sound))
            {
                float lastTimePlayed = soundTimingDic[sound];
                float maxTiming = timing;

                if ((lastTimePlayed + maxTiming) < Time.time)
                {
                    soundTimingDic[sound] = Time.time;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return true;
            }
        }

        private static AudioClip GetAudioClip(Sound sound)
        {
            foreach (GameSounds.SoundAudioClip s in GameSounds.Inst.soundAudioClipArr)
            {
                if (s.sound == sound)
                {
                    return s.audioClip;
                }
            }
            Debug.LogError("Sound " + sound + " not found!");
            return null;
        }
    }
}