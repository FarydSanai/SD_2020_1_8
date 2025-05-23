﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SamuraiGame
{
    [CreateAssetMenu(fileName = "New state", menuName = "SamuraiDream/AI/JumpPlatform")]
    public class JumpPlatform : CharacterAbility
    {
        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            characterState.characterControl.Jump = true;
            characterState.characterControl.MoveUp = true;
        }
        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (characterState.characterControl.Attack)
            {
                return;
            }

            float platformDist = characterState.characterControl.aiProgress.pathFindingAgent.EndSphere.transform.position.y
                               - characterState.COLLISION_SPHERE_DATA.FrontSpheres[0].transform.position.y;

            if (platformDist > 0.1f)
            {
                if (characterState.characterControl.aiProgress.pathFindingAgent.StartSphere.transform.position.z <
                    characterState.characterControl.aiProgress.pathFindingAgent.EndSphere.transform.position.z)
                {
                    characterState.characterControl.MoveUp = true;
                    characterState.characterControl.MoveRight = true;
                    characterState.characterControl.MoveLeft = false;
                }
                else
                {
                    characterState.characterControl.MoveUp = true;
                    characterState.characterControl.MoveRight = false;
                    characterState.characterControl.MoveLeft = true;
                }
            }
            if (platformDist < 0.1f)
            {
                characterState.characterControl.MoveRight = false;
                characterState.characterControl.MoveLeft = false;
                characterState.characterControl.MoveUp = false;
                characterState.characterControl.Jump = false;
            }
        }
        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            
        }
    }
}
