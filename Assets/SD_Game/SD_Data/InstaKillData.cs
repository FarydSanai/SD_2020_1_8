﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SamuraiGame
{
    [System.Serializable]
    public class InstaKillData
    {
        public RuntimeAnimatorController Animation_A;
        public RuntimeAnimatorController Animation_B;

        public delegate void DoSomething(CharacterController attacker);
        public DoSomething DeathByInstaKill;
    }
}