using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SamuraiGame
{
    [CreateAssetMenu(fileName = "New state", menuName = "SamuraiDream/VFX/GlowEyes")]
    public class GlowEyes : StateData
    {
        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            
        }
        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            //if (characterState.characterControl.EyesEmission != null)
            //{
            //    if (stateInfo.normalizedTime < 0.5f)
            //    {
            //        Vector3 val = Vector3.Lerp(new Vector3(-1f, -1f, -1f), new Vector3(1f, 1f, 1f), stateInfo.normalizedTime);
            //        characterState.characterControl.EyesEmission.TurnOnWhiteEmission(val);
            //    }
            //    else
            //    {
            //        Vector3 val = Vector3.Lerp(new Vector3(1f, 1f, 1f), new Vector3(-1f, -1f, -1f), stateInfo.normalizedTime);
            //        characterState.characterControl.EyesEmission.TurnOnWhiteEmission(val);
            //    }
            //}
        }
        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            
        }
    }
}