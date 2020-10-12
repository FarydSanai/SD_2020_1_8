using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SamuraiGame
{
    public class AttackManager : Singleton<AttackManager>
    {
        public List<AttackInfo> CurrentAttacks = new List<AttackInfo>();
        public void ForceDeregister(CharacterController control)
        {
            foreach (AttackInfo info in CurrentAttacks)
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

