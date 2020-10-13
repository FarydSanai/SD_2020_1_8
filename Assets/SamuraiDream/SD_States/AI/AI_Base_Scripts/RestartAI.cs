using System.Collections.Generic;
using UnityEngine;

namespace SamuraiGame
{
    [CreateAssetMenu(fileName = "New AI state", menuName = "SamuraiDream/AI/RestartAI")]
    public class RestartAI : StateData
    {
        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            
        }
        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

            if (!AIIsOnGround(characterState.characterControl))
            {
                return;
            }

            //Going starigh
            if (characterState.characterControl.aiProgress.AIDistanceToEndSphere() < 1f)
            {
                if (characterState.characterControl.aiProgress.TargetDistanceToEndSphere() > 0.5f)
                {
                    if (characterState.characterControl.aiProgress.TargetIsGrounded())
                    {
                        characterState.characterControl.aiController.InitializeAI();
                    }
                }
            }

            //Landing
            if (characterState.ANIMATION_DATA.IsRunning(typeof(Landing)))
            {
                characterState.characterControl.Turbo = false;
                characterState.characterControl.Jump = false;
                characterState.characterControl.MoveUp = false;
                characterState.characterControl.aiController.InitializeAI();
            }

            //path is blocked
            if (characterState.BLOCKING_DATA.FrontBlockingDicCount == 0)
            {
                characterState.characterControl.aiProgress.BlockCharacter = null;
            }
            else
            {
                List<GameObject> objs = characterState.characterControl.BLOCKING_DATA.FrontBlockingCharacterList();

                foreach (GameObject o in objs)
                {
                    CharacterController blockChar = CharacterManager.Instance.GetCharacter(o);
                    if (blockChar != null)
                    {
                        characterState.characterControl.aiProgress.BlockCharacter = blockChar;
                        break;
                    }
                    else
                    {
                        characterState.characterControl.aiProgress.BlockCharacter = null;
                    }
                }
            }

            if (characterState.characterControl.aiProgress.BlockCharacter != null)
            {
                if (characterState.GROUND_DATA.Ground != null)
                {
                    if (!characterState.ANIMATION_DATA.IsRunning(typeof(Jump)) &&
                        !characterState.ANIMATION_DATA.IsRunning(typeof(JumpPrep)))
                    {
                        characterState.characterControl.Turbo = false;
                        characterState.characterControl.Jump = false;
                        characterState.characterControl.MoveUp = false;
                        characterState.characterControl.MoveDown = false;
                        characterState.characterControl.MoveLeft = false;
                        characterState.characterControl.MoveRight = false;
                        characterState.characterControl.aiController.InitializeAI();
                    }
                }
            }

            //StartSphere height
            if (characterState.GROUND_DATA.Ground != null && 
                !characterState.ANIMATION_DATA.IsRunning(typeof(Jump)) &&
                !characterState.ANIMATION_DATA.IsRunning(typeof(WallJumpPrep)))
            {
                if (characterState.characterControl.aiProgress.GetStartSphereHeight() > 0.3f)
                {
                    //Debug.Log("GetStartSphereHeight() is " + characterState.characterControl.aiProgress.GetStartSphereHeight());
                    characterState.characterControl.Turbo = false;
                    characterState.characterControl.Jump = false;
                    characterState.characterControl.MoveUp = false;
                    characterState.characterControl.MoveDown = false;
                    characterState.characterControl.MoveLeft = false;
                    characterState.characterControl.MoveRight = false;
                    characterState.characterControl.aiController.InitializeAI();
                }
            }
        }
        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }

        private bool AIIsOnGround(CharacterController control)
        {
            if (!control.ANIMATION_DATA.IsRunning(typeof(MoveUp)))
            {
                if (control.RIGID_BODY.useGravity)
                {
                    if (control.SkinnedMeshAnimator.GetBool(HashManager.Instance.DicMainParams[TransitionParameter.Grounded]))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}

