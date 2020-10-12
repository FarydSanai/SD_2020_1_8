using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.AI;

namespace SamuraiGame
{
    public enum AI_Walk_Transitions
    {
        start_walking,
        jump_platform,
        fall_platform,

    }
    [CreateAssetMenu(fileName = "New state", menuName = "SamuraiDream/AI/SendPathFindingAgent")]
    public class SendPathFindingAgent : StateData
    {
        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (characterState.characterControl.aiProgress.pathFindingAgent == null)
            {
                GameObject p = Instantiate(Resources.Load("PathFindingAgent", typeof(GameObject)) as GameObject);
                characterState.characterControl.aiProgress.pathFindingAgent = p.GetComponent<PathFindingAgent>();
            }

            characterState.characterControl.aiProgress.pathFindingAgent.owner = characterState.characterControl;

            characterState.characterControl.aiProgress.pathFindingAgent.GetComponent<NavMeshAgent>().enabled = false;

            characterState.characterControl.aiProgress.pathFindingAgent.transform.position = 
                characterState.characterControl.transform.position + (Vector3.up * 0.25f);

            characterState.characterControl.navMeshObstacle.carving = false;

            characterState.characterControl.aiProgress.pathFindingAgent.GoToTarget();
        }
        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (characterState.characterControl.aiProgress.pathFindingAgent.StartWalk)
            {
                animator.SetBool(HashManager.Instance.DicAIParams[AI_Walk_Transitions.start_walking], true);
            }
        }
        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            animator.SetBool(HashManager.Instance.DicAIParams[AI_Walk_Transitions.start_walking], false);
        }
    }
}
