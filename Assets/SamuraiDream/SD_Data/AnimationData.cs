using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SamuraiGame
{
    [System.Serializable]
    public class AnimationData
    {
        public Dictionary<CharacterAbility, int> CurrentRunningAbilities;

        public delegate bool bool_type(System.Type type);
        public bool_type IsRunning;
    }
}