using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

namespace SamuraiGame
{
    [CreateAssetMenu(fileName = "New state", menuName = "SamuraiDream/AbilityData/ShakeCamera")]
    public class ShakeCamera : CharacterAbility
    {
        [Range(0f, 0.99f)]
        public float ShakeTiming;
        public float ShakeLenght;

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (ShakeTiming == 0f)
            {
                CameraManager.Instance.ShakeCamera(ShakeLenght);
                characterState.characterControl.animationProgress.CameraShaken = true;
            }
        }
        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (!characterState.characterControl.animationProgress.CameraShaken)
            {
                if (stateInfo.normalizedTime >= ShakeTiming)
                {
                    characterState.characterControl.animationProgress.CameraShaken = true;

                    CameraManager.Instance.ShakeCamera(ShakeLenght);
                }
            }
        }
        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            characterState.characterControl.animationProgress.CameraShaken = false;
        }
    }
}
