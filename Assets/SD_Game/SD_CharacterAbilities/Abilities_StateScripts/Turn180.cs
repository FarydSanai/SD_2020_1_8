using SamuraiGame;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace SamuraiGame
{
    [CreateAssetMenu(fileName = "New state", menuName = "SamuraiDream/AbilityData/Turn180")]
    public class Turn180 : CharacterAbility
    {
        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (characterState.ROTATION_DATA.IsFacingForward())
            {
                characterState.ROTATION_DATA.FaceForward(false);
            } else
            {
                characterState.ROTATION_DATA.FaceForward(true);
            }
        }
        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            
        }
        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            
        }
    }
}
