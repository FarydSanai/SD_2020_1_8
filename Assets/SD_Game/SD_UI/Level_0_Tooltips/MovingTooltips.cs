﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SamuraiGame
{
    public class MovingTooltips : MonoBehaviour
    {
        public List<string> MovingTooltipsList = new List<string>();
        public List<TooltipsLoader.CharacterCurrentState> MovingStates = new List<TooltipsLoader.CharacterCurrentState>();

        private void Awake()
        {
            MovingStates.Add(CharacterMove);
            MovingStates.Add(CharacterRun);
            MovingStates.Add(CharacterJump);
            MovingStates.Add(CharacterHighJump);
            MovingStates.Add(CharacterLongJump);
            MovingStates.Add(CharacterGrabPlatform);
            MovingStates.Add(CharacerClimb);
            MovingStates.Add(CharacterSlide);
            MovingStates.Add(CharacterRoll);
            MovingStates.Add(CharacterWallSlide);
            MovingStates.Add(CharacterWallJump);

        }
        private bool CharacterMove(CharacterController control)
        {
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
        private bool CharacterHighJump(CharacterController control)
        {
            if (control.ANIMATION_DATA.IsRunning(typeof(Jump)) &&
                control.Jump 
                /*&& Input.GetKeyDown(KeyCode.Space)*/)
            {
                return true;
            }
            return false;
        }
        private bool CharacterLongJump(CharacterController control)
        {
            if (CharacterRun(control) && control.Jump)
            {
                return true;
            }
            return false;
        }
        private bool CharacterGrabPlatform(CharacterController control)
        {
            if (control.LEDGE_GRAB_DATA.isGrabbingledge)
            {
                return true;
            }
            return false;
        }
        private bool CharacerClimb(CharacterController control)
        {
            if (control.LEDGE_GRAB_DATA.isGrabbingledge && control.MoveUp)
            {
                return true;
            }
            return false;
        }
        private bool CharacterSlide(CharacterController control)
        {
            if (CharacterRun(control) && control.MoveDown)
            {
                if (control.GROUND_DATA.Ground != null)
                {
                    return true;
                }
            }
            return false;
        }
        private bool CharacterRoll(CharacterController control)
        {
            if (CharacterRun(control) && control.MoveUp)
            {
                return true;
            }
            return false;
        }
        private bool CharacterWallSlide(CharacterController control)
        {
            if (control.ANIMATION_DATA.IsRunning(typeof(WallSlide)))
            {
                return true;
            }
            return false;
        }
        private bool CharacterWallJump(CharacterController control)
        {
            if (control.ANIMATION_DATA.IsRunning(typeof(WallJumpPrep)))
            {
                return true;
            }
            return false;
        }
    }
}