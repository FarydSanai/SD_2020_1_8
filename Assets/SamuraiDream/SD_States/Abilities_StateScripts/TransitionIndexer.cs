using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace SamuraiGame
{
    public enum TransitionConditionType
    {
        UP,
        DOWN,
        RIGHT,
        LEFT,
        ATTACK,
        JUMP,
        GRABBING_LEDGE,
        LEFT_OR_RIGHT,
        GROUNDED,
        MOVE_FORWARD,
        AIR,
        BLOCKED_BY_WALL,
        CAN_WALLJUMP,
        NOT_GRABBING_LEDGE,
        NOT_BLOCKED_BY_WALL,
        MOVING_TO_BLOCKING_OBJ,
        DOUBLE_TAP_UP,

        DOUBLE_TAP_DOWN,
        DOUBLE_TAP_LEFT,
        DOUBLE_TAP_RIGHT,

        TOUCHING_WEAPON,
        HOLDING_SWORD,
        NOT_MOVING,
        RUN,
        NOT_RUN,

        BLOCKING,
        NOT_BLOCKING,
        ATTACK_IS_BLOCKED,
    }
    [CreateAssetMenu(fileName = "New state", menuName = "SamuraiDream/AbilityData/TransitionIndexer")]
    public class TransitionIndexer : CharacterAbility
    {
        public int Index;
        public List<TransitionConditionType> transitionConditions = new List<TransitionConditionType>();
        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (MakeTransition(characterState.characterControl))
            {
                animator.SetInteger(HashManager.Instance.DicMainParams[TransitionParameter.TransitionIndex], Index);
            }
        }
        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            characterState.JUMP_DATA.CheckWallBlock = StartCheckingWallBlock();

            if (animator.GetInteger(HashManager.Instance.DicMainParams[TransitionParameter.TransitionIndex]) == 0)
            {
                if (MakeTransition(characterState.characterControl))
                {
                    animator.SetInteger(HashManager.Instance.DicMainParams[TransitionParameter.TransitionIndex], Index);
                }
            }
        }
        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            animator.SetInteger(HashManager.Instance.DicMainParams[TransitionParameter.TransitionIndex], 0);
        }

        private bool StartCheckingWallBlock()
        {
            foreach (TransitionConditionType t in transitionConditions)
            {
                if (t == TransitionConditionType.BLOCKED_BY_WALL || 
                    t == TransitionConditionType.NOT_BLOCKED_BY_WALL)
                {
                    return true;
                }
            }
            return false;
        } 

        private bool MakeTransition(CharacterController control)
        {
            foreach (TransitionConditionType c in transitionConditions)
            {
                switch (c)
                {
                    case TransitionConditionType.UP:
                        {
                            if (!control.MoveUp)
                            {
                                return false;
                            }
                        }
                        break;
                    case TransitionConditionType.DOWN:
                        {
                            if (!control.MoveDown)
                            {
                                return false;
                            }
                        }
                        break;
                    case TransitionConditionType.RIGHT:
                        {
                            if (!control.MoveRight)
                            {
                                return false;
                            }
                        }
                        break;
                    case TransitionConditionType.LEFT:
                        {
                            if (!control.MoveLeft)
                            {
                                return false;
                            }
                        }
                        break;
                    case TransitionConditionType.ATTACK:
                        {
                            if (!control.ATTACK_DATA.AttackTriggered)
                            {
                                return false;
                            }
                        }
                        break;
                    case TransitionConditionType.JUMP:
                        {
                            if (!control.Jump)
                            {
                                return false;
                            }
                        }
                        break;
                    case TransitionConditionType.GRABBING_LEDGE:
                        {
                            if (!control.LEDGE_GRAB_DATA.isGrabbingledge)
                            {
                                return false;
                            }
                        }
                        break;
                    case TransitionConditionType.NOT_GRABBING_LEDGE:
                        {
                            if (control.LEDGE_GRAB_DATA.isGrabbingledge)
                            {
                                return false;
                            }
                        }
                        break;
                    case TransitionConditionType.LEFT_OR_RIGHT:
                        {
                            if (!control.MoveRight && !control.MoveLeft)
                            {
                                return false;
                            }
                            if (control.MoveLeft && control.MoveRight)
                            {
                                return false;
                            }
                        }
                        break;
                    case TransitionConditionType.GROUNDED:
                        {
                            if (!control.SkinnedMeshAnimator.GetBool(HashManager.Instance.DicMainParams[TransitionParameter.Grounded]))
                            {
                                return false;
                            }
                        }
                        break;
                    case TransitionConditionType.MOVE_FORWARD:
                        {
                            if (control.ROTATION_DATA.IsFacingForward())
                            {
                                if (!control.MoveRight)
                                {
                                    return false;
                                }
                            }
                            else
                            {
                                if (!control.MoveLeft)
                                {
                                    return false;
                                }
                            }
                        }
                        break;
                    case TransitionConditionType.AIR:
                        {
                            if (control.SkinnedMeshAnimator.GetBool(HashManager.Instance.DicMainParams[TransitionParameter.Grounded]))
                            {
                                return false;
                            }
                        }
                        break;
                    case TransitionConditionType.BLOCKED_BY_WALL:
                        {
                            foreach (OverlapChecker oc in control.COLLISION_SPHERE_DATA.FrontOverlapCheckers)
                            {
                                if (!oc.ObjIsOverlapping)
                                {
                                    return false;
                                }
                            }
                        }
                        break;
                    case TransitionConditionType.NOT_BLOCKED_BY_WALL:
                        {
                            bool AllIsOverlapping = true;
                            foreach (OverlapChecker oc in control.COLLISION_SPHERE_DATA.FrontOverlapCheckers)
                            {
                                if (!oc.ObjIsOverlapping)
                                {
                                    AllIsOverlapping = false;
                                }
                            }
                            if (AllIsOverlapping)
                            {
                                return false;
                            }
                        }
                        break;
                    case TransitionConditionType.CAN_WALLJUMP:
                        {
                            if (!control.JUMP_DATA.CanWallJump)
                            {
                                return false;
                            }
                        }
                        break;
                    case TransitionConditionType.MOVING_TO_BLOCKING_OBJ:
                        {
                            List<GameObject> objs = control.BLOCKING_DATA.FrontBlockingObjectsList();

                            foreach (GameObject o in objs)
                            {
                                Vector3 dir = o.transform.position - control.transform.position;

                                if (dir.z > 0f && !control.MoveRight)
                                {
                                    return false;
                                }
                                if (dir.z < 0f && !control.MoveLeft)
                                {
                                    return false;
                                }
                            }
                        }
                        break;
                    case TransitionConditionType.DOUBLE_TAP_UP:
                        {
                            if (!control.subComponentProcessor.ComponentsDic.ContainsKey(SubComponentType.MANUALINPUT))
                            {
                                return false;
                            }
                            if(!control.MANUAL_INPUT_DATA.DoubleTapUp())
                            {
                                return false;
                            }
                        }
                        break;
                    case TransitionConditionType.DOUBLE_TAP_DOWN:
                        {
                            if (!control.subComponentProcessor.ComponentsDic.ContainsKey(SubComponentType.MANUALINPUT))
                            {
                                return false;
                            }
                            if (!control.MANUAL_INPUT_DATA.DoubleTapDown())
                            {
                                return false;
                            }
                        }
                        break;
                    //case TransitionConditionType.DOUBLE_TAP_LEFT:
                    //    {
                    //        return false;
                    //    }
                    //    break;
                    //case TransitionConditionType.DOUBLE_TAP_RIGHT:
                    //    {
                    //        return false;
                    //    }
                    //    break;
                    case TransitionConditionType.TOUCHING_WEAPON:
                        {
                            if (control.animationProgress.CollidingWeapons.Count == 0)
                            {
                                if (control.animationProgress.HoldingWeapon == null)
                                {
                                    return false;
                                }
                            }
                        }
                        break;
                    case TransitionConditionType.HOLDING_SWORD:
                        {
                            if (control.animationProgress.HoldingWeapon == null)
                            {
                                return false;
                            }
                            if (!control.animationProgress.HoldingWeapon.name.Contains("Emissive_Katana"))
                            {
                                return false;
                            }
                        }
                        break;
                    case TransitionConditionType.NOT_MOVING:
                        {
                            if (control.MoveRight || control.MoveLeft)
                            {
                                if (!(control.MoveRight && control.MoveLeft))
                                {
                                    return false;
                                }
                            }
                        }
                        break;
                    case TransitionConditionType.RUN:
                        {
                            if (!control.Turbo)
                            {
                                return false;
                            }
                            if (control.MoveLeft && control.MoveRight)
                            {
                                return false;
                            }
                            if (!control.MoveLeft && !control.MoveRight)
                            {
                                return false;
                            }
                        }
                        break;
                    case TransitionConditionType.NOT_RUN:
                        {
                            if (control.Turbo)
                            {
                                if (control.MoveRight || control.MoveLeft)
                                {
                                    if (!(control.MoveRight && control.MoveLeft))
                                    {
                                        return false;
                                    }
                                }
                            }
                        }
                        break;
                    case TransitionConditionType.BLOCKING:
                        {
                            if (!control.Block)
                            {
                                return false;
                            }
                        }
                        break;
                    case TransitionConditionType.NOT_BLOCKING:
                        {
                            if (control.Block)
                            {
                                return false;
                            }
                        }
                        break;
                    case TransitionConditionType.ATTACK_IS_BLOCKED:
                        {
                            if (control.DAMAGE_DATA.BlockedAttack == null)
                            {
                                return false;
                            }
                        }
                        break;
                }
            }
            return true;
        }
    }
}
