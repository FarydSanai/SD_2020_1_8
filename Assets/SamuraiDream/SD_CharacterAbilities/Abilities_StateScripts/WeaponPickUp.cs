using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SamuraiGame
{
    [CreateAssetMenu(fileName = "New state", menuName = "SamuraiDream/AbilityData/WeaponPickUp")]
    public class WeaponPickUp : CharacterAbility
    {
        public float PickUpTiming;
        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            characterState.characterControl.animationProgress.HoldingWeapon =
                characterState.characterControl.animationProgress.GetTouchingWeapon();
        }
        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (stateInfo.normalizedTime > PickUpTiming)
            {
                if (characterState.characterControl.animationProgress.HoldingWeapon.control == null)
                {
                    MeleeWeapon w = characterState.characterControl.animationProgress.HoldingWeapon;

                    w.transform.parent = characterState.characterControl.RightHandAttack.transform;
                    w.transform.localPosition = w.CustomPosition;
                    w.transform.localRotation = Quaternion.Euler(w.CustomRotation);

                    w.control = characterState.characterControl;
                    w.triggerDetector.control = characterState.characterControl;
                    w.RemoveWeaponFromDictionary(characterState.characterControl);
                }
            }
        }
        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            
        }
    }
}