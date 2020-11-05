using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SamuraiGame
{
    public class GatesDetector : StateMachineBehaviour
    {
        private CharacterController control;
        private GameObject door;
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            control = CharacterManager.Instance.GetPlayableCharacter();
            door = animator.transform.root.gameObject;
        }

        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (Vector3.SqrMagnitude(door.transform.position - control.transform.position) < 8f)
            {
                animator.SetBool(
                    HashManager.Instance.ArrGatesTransitionParams[(int)GatesTransitionParams.character_nearby],
                    true);
            }
            else
            {
                animator.SetBool(
                    HashManager.Instance.ArrGatesTransitionParams[(int)GatesTransitionParams.character_nearby],
                    false);
            }
        }

        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {

        }
    }
}