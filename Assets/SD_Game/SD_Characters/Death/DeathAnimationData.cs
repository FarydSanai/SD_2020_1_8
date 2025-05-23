﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SamuraiGame
{
    public enum DeathType
    {
        NONE,
        LAUNCH_INTO_AIR,
        GROUND_SHOCK,

    }

    [CreateAssetMenu(fileName = "New ScriptableObject", menuName = "SamuraiDream/Death/DeathAnimationData")]
    public class DeathAnimationData : ScriptableObject
    {
        public RuntimeAnimatorController Animator;
        public bool IsFacingAttacker;
        //public bool LaunchIntoAir;
        public DeathType DeathType;

    }
}
