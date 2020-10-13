using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SamuraiGame
{
    public class AttackManager : Singleton<AttackManager>
    {
        public List<AttackCondition> CurrentAttacks = new List<AttackCondition>();
        public void ForceDeregister(CharacterController control)
        {
            foreach (AttackCondition info in CurrentAttacks)
            {
                if (info.Attacker == control)
                {
                    info.isFinished = true;
                    info.GetComponent<PoolObject>().TurnOff();
                }
            }
        }
    } 
}

