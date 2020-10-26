using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SamuraiGame
{
    public class MovingTooltips : MonoBehaviour
    {
        public List<string> MovingTooltipsList = new List<string>();
        public List<TooltipsLoader.CharacterCurrentState> MovingStates = new List<TooltipsLoader.CharacterCurrentState>();
        private void Start()
        {
            MovingStates.Add(CharacterMove);
            MovingStates.Add(CharacterRun);
            MovingStates.Add(CharacterJump);
        }
        private bool CharacterMove(CharacterController control)
        {
            if (!control.MoveLeft && !control.MoveRight)
            {
                return false;
            }
            if (control.MoveLeft && control.MoveRight)
            {
                return false;
            }
            if (control.MoveLeft || control.MoveRight)
            {
                return true;
            }
            return false;
        }
        private bool CharacterRun(CharacterController control)
        {
            if (CharacterMove(control) && control.Turbo)
            {
                return true;
            }
            return false;
        }
        private bool CharacterJump(CharacterController control)
        {
            if (control.Jump)
            {
                return true;
            }
            return false;
        }

    }
}