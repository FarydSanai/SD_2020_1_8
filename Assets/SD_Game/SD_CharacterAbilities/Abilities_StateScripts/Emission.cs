﻿using UnityEngine;

namespace SamuraiGame
{
    [CreateAssetMenu(fileName = "New state", menuName = "SamuraiDream/VFX/Emission")]
    public class Emission : CharacterAbility
    {
        public bool FirstHalfEmission;
        public bool SecondHalfEmission;
        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }
        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (characterState.characterControl.jointsEmission != null)
            {
                if (FirstHalfEmission)
                {
                    Vector3 val = Vector3.Lerp(new Vector3(0f, 0f, 0f), new Vector3(0.1f, 3f, 6f), stateInfo.normalizedTime);
                    characterState.characterControl.jointsEmission.RaiseEmissionLight(val);
                }
                else if (SecondHalfEmission)
                {
                    Vector3 val = Vector3.Lerp(new Vector3(0.1f, 3f, 6f), new Vector3(0f, 0f, 0f), stateInfo.normalizedTime);
                    characterState.characterControl.jointsEmission.RaiseEmissionLight(val);
                }
                else
                {
                    if (stateInfo.normalizedTime <= 0.5f)
                    {
                        Vector3 val = Vector3.Lerp(new Vector3(0f, 0f, 0f), new Vector3(0.1f, 3f, 6f), stateInfo.normalizedTime);
                        characterState.characterControl.jointsEmission.RaiseEmissionLight(val);
                    }
                    else
                    {
                        Vector3 val = Vector3.Lerp(new Vector3(0.1f, 3f, 6f), new Vector3(0f, 0f, 0f), stateInfo.normalizedTime);
                        characterState.characterControl.jointsEmission.RaiseEmissionLight(val);
                    }
                }
            }
            
        }
        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            
        }
    }
}
