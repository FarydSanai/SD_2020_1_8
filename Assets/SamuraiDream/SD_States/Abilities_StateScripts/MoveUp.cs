using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace SamuraiGame
{
    [CreateAssetMenu(fileName = "New state", menuName = "SamuraiDream/AbilityData/MoveUp")]
    public class MoveUp : CharacterAbility
    {
        public float Speed;
        public AnimationCurve SpeedGraph;

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            characterState.characterControl.animationProgress.LatestMoveUp = this;
        }
        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

            if (!characterState.characterControl.RIGID_BODY.useGravity)
            {
                if (characterState.BLOCKING_DATA.UpBlockingDicCount == 0)
                {
                    characterState.characterControl.transform.
                        Translate(Vector3.up * Speed *
                                  SpeedGraph.Evaluate(stateInfo.normalizedTime) *
                                  Time.deltaTime);
                }

            }

        }
        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }
    }
}
