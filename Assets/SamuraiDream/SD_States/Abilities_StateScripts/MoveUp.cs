using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace SamuraiGame
{
    [CreateAssetMenu(fileName = "New state", menuName = "SamuraiDream/AbilityData/MoveUp")]
    public class MoveUp : StateData
    {
        public float Speed;
        public AnimationCurve SpeedGraph;

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            characterState.characterControl.animationProgress.LatestMoveUp = this;
        }
        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

            if (!characterState.characterControl.RIGID_BODY.useGravity)
            {
                if (characterState.BLOCKING_DATA.UpBlockingDicCount == 0)
                {
                    characterState.characterControl.transform.
                        Translate(Vector3.up * Speed *
                                  SpeedGraph.Evaluate(stateInfo.normalizedTime)*
                                  Time.deltaTime);
                }

            }

        }
        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }

        //private bool UpIsBlocked(CharacterController control)
        //{
        //    foreach (GameObject o in control.collisionSpheres.TopSpheres)
        //    {
        //        Debug.DrawRay(o.transform.position, control.transform.up * 0.4f, Color.yellow);
        //        RaycastHit hit;

        //        if (Physics.Raycast(o.transform.position, control.transform.up, out hit, 0.125f))
        //        {
        //            if (hit.collider.transform.root.gameObject != control.gameObject && 
        //                !Ledge.IsLedge(hit.collider.gameObject))
        //            {
        //                return true;
        //            }
        //        }
        //    }
        //    return false;
        //}
    }
}
