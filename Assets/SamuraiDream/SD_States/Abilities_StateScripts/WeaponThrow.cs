using UnityEngine;

namespace SamuraiGame
{
    [CreateAssetMenu(fileName = "New state", menuName = "SamuraiDream/AbilityData/WeaponThrow")]
    public class WeaponThrow : CharacterAbility
    {
        public float ThrowTiming;
        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            
        }
        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (stateInfo.normalizedTime >= ThrowTiming)
            {
                if (characterState.characterControl.animationProgress.HoldingWeapon != null)
                {
                    characterState.characterControl.animationProgress.HoldingWeapon.ThrowWeapon();
                }
            }
        }
        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            
        }
    }
}