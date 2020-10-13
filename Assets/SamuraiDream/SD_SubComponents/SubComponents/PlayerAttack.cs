using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace SamuraiGame
{
    public class PlayerAttack : SubComponent
    {
        public AttackData attackData;
        private void Start()
        {
            attackData = new AttackData
            {
                AttackTriggered = false,
                AttackButtonIsReset = false,
            };
            subComponentProcessor.attackData = attackData;
            subComponentProcessor.ComponentsDic.Add(SubComponentType.PLAYER_ATTACK, this);
        }
        public override void OnFixedUpdate()
        {
            throw new System.NotImplementedException();
        }
        public override void OnUpdate()
        {
            if (control.Attack)
            {
                if (attackData.AttackButtonIsReset)
                {
                    attackData.AttackTriggered = true;
                    attackData.AttackButtonIsReset = false;
                }
            }
            else
            {
                attackData.AttackTriggered = false;
                attackData.AttackButtonIsReset = true;
            }
        }
    }
}