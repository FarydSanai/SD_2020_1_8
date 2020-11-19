using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SamuraiGame
{
    public class AttackTooltips : MonoBehaviour
    {
        public List<string> AttackTooltipsList = new List<string>();
        public List<TooltipsLoader.CharacterCurrentState> AttackStates = new List<TooltipsLoader.CharacterCurrentState>();
        
        private void Awake()
        {
            AttackStates.Add(CharacterAttack_One);
            AttackStates.Add(CharacterAttack_Two_Three);
            AttackStates.Add(CharacterAttack_JumpingPunch);
            AttackStates.Add(CharacterAttack_FlyingKick);
            AttackStates.Add(CharacterAttack_RoundKick);
            AttackStates.Add(CharacterAttack_Uppercut);
            AttackStates.Add(CharacterAttack_Shoruyken);
            //AttackStates.Add(CharacterAttack_DownSmash);
            AttackStates.Add(CharacterAttack_BadKick);
        }
        private bool CharacterAttack_One(CharacterController control)
        {
            if (control.Attack)
            {
                return true;
            }
            return false;
        }
        private bool CharacterAttack_Two_Three(CharacterController control)
        {
            if (control.ANIMATION_DATA.IsRunning(typeof(AttackStateCombo)))
            {
                return true;
            }
            return false;
        }
        private bool CharacterAttack_JumpingPunch(CharacterController control)
        {
            if (control.Attack &&
               (control.MoveLeft || control.MoveRight) &&
                control.ANIMATION_DATA.IsRunning(typeof(Jump)))
            {
                return true;
            }
            return false;
        }
        private bool CharacterAttack_FlyingKick(CharacterController control)
        {
            if ((control.MoveLeft || control.MoveRight) &&
                 control.GROUND_DATA.Ground != null &&
                 control.Turbo &&
                 control.Attack)
            {
                return true;
            }
            return false;
        }
        private bool CharacterAttack_RoundKick(CharacterController control)
        {
            if ((control.MoveLeft || control.MoveRight) &&
                 control.Attack)
            {
                return true;
            }
            return false;
        }
        private bool CharacterAttack_Uppercut(CharacterController control)
        {
            if (control.ANIMATION_DATA.IsRunning(typeof(AttackStateCombo)))
            {
                return true;
            }
            return false;
        }
        private bool CharacterAttack_Shoruyken(CharacterController control)
        {
            if (control.MoveUp && control.Attack)
            {
                return true;
            }
            return false;
        }
        //private bool CharacterAttack_DownSmash(CharacterController control)
        //{
        //    if (control.GROUND_DATA.Ground == null &&
        //        control.Attack && 
        //       !control.ANIMATION_DATA.IsRunning(typeof(Jump)))
        //    {
        //        return true;
        //    }
        //    return false;
        //}
        private bool CharacterAttack_BadKick(CharacterController control)
        {
            if ((control.MoveLeft || control.MoveRight) &&
                control.GROUND_DATA.Ground != null &&
                control.MoveDown &&
                control.Attack)
            {
                return true;
            }
            return false;
        }
    }

}