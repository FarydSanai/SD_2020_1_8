using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SamuraiGame
{
    [CreateAssetMenu(fileName = "New state", menuName = "SamuraiDream/AbilityData/Landing")]
    public class Landing : CharacterAbility
    {
        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            //animator.SetBool(HashManager.Instance.DicMainParams[TransitionParameter.Jump], false);
            animator.SetBool(HashManager.Instance.DicMainParams[TransitionParameter.Move], false);

            SoundManager.PlaySound(SoundManager.Sound.PlayerLanding);
        }
        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            
        }
        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }
    }
}