using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SamuraiGame
{
    public class AttackInfo : MonoBehaviour
    {
        public CharacterController Attacker = null;
        public Attack AttackAbility;
        public List<AttackPartType> AttackParts = new List<AttackPartType>();

        public bool MustCollide; 
        public bool MustFaceAttacker;
        public float LethalRange;
        public int MaxHits;

        public int CurrentHits;
        public bool isRegistered;
        public bool isFinished;

        public DeathType DeathType;

        public bool UseRagdollDeath;
        public List<CharacterController> RegisteredTargets = new List<CharacterController>();

        public void ResetInfo(Attack attack, CharacterController attacker)
        {
            isRegistered = false;
            isFinished = false;
            AttackAbility = attack;
            Attacker = attacker;
            RegisteredTargets.Clear();
        }

        public void Register(Attack attack)
        {
            isRegistered = true;

            AttackAbility = attack;
            AttackParts = attack.AttackParts;
            MustCollide = attack.MustCollide;
            MustFaceAttacker = attack.MustFaceAttacker;
            LethalRange = attack.LethalRange;
            MaxHits = attack.MaxHits;
            CurrentHits = 0;
        }
        public void CopyInfo(Attack attack, CharacterController control)
        {
            Attacker = control;
            AttackAbility = attack;
            AttackParts = attack.AttackParts;
            MustCollide = attack.MustCollide;
            MustFaceAttacker = attack.MustFaceAttacker;
            LethalRange = attack.LethalRange;
            MaxHits = attack.MaxHits;
            CurrentHits = 0;
        }
        private void OnDisable()
        {
            isFinished = true;
        }
    }
}
