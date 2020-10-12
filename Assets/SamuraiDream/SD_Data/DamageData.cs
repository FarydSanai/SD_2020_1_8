using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SamuraiGame
{
    [System.Serializable]
    public class DamageData
    {
        public CharacterController Attacker;
        public Attack Attack;
        public TriggerDetector DamagedTrigger;
        public GameObject AttackingPart;
        public AttackInfo BlockedAttack;
        public float HP;
        public Attack MarioStompAttack;
        public Attack SwordThrow;

        public delegate bool ReturnBool();
        public ReturnBool IsDead;

        public delegate void DoSomething(AttackInfo info);
        public DoSomething TakeDamage;

        public void SetData(CharacterController attacker, Attack attack,
                            TriggerDetector damagedTrigger, GameObject attackingPart)
        {
            Attacker = attacker;
            Attack = attack;
            DamagedTrigger = damagedTrigger;
            AttackingPart = attackingPart;
        }
    }
}