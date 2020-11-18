using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SamuraiGame
{
    public class GameSounds : MonoBehaviour
    {
        private static GameSounds _inst;

        public static GameSounds Inst
        {
            get
            {
                if (_inst == null)
                {
                    _inst = Instantiate(Resources.Load("GameSounds", typeof(GameObject)) as GameObject)
                            .GetComponent<GameSounds>();
                }
                return _inst;
            }
        }

        public SoundAudioClip[] soundAudioClipArr;

        [System.Serializable]
        public class SoundAudioClip
        {
            public SoundManager.Sound sound;
            public AudioClip audioClip;
        }
    }
}