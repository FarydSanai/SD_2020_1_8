using System.Collections;
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
            PlayerJump,
            PlayerFall,
            DeathFall,
        }

        private static Dictionary<Sound, float> soundTimingDic;
        private static GameObject singleSoundObj;
        private static AudioSource singleSoundSource;

        public static void Initialize()
        {
            soundTimingDic = new Dictionary<Sound, float>();
            soundTimingDic[Sound.PlayerWalk] = 0f;
            soundTimingDic[Sound.PlayerRun] = 0f;
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
                        if (soundTimingDic.ContainsKey(sound))
                        {
                            float lastTimePlayed = soundTimingDic[sound];
                            float maxTiming = 0.5f;

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
                case Sound.PlayerRun:
                    {
                        if (soundTimingDic.ContainsKey(sound))
                        {
                            float lastTimePlayed = soundTimingDic[sound];
                            float maxTiming = 0.25f;

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
                    //break;
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