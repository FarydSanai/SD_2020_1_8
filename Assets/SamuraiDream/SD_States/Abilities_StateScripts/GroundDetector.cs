     using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SamuraiGame
{
    [CreateAssetMenu(fileName = "New state", menuName = "SamuraiDream/AbilityData/GroundDetector")]
    public class GroundDetector : StateData
    {
        public float Distance;
        private GameObject TestingSphere;
        public GameObject TESTING_SPHERE
        {
            get
            {
                if (TestingSphere == null)
                {
                    TestingSphere = GameObject.Find("TestingSphere");
                }
                return TestingSphere;
            }
        }
        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            
        }
        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (IsGrounded(characterState.characterControl))
            {
                animator.SetBool(HashManager.Instance.DicMainParams[TransitionParameter.Grounded], true);
            }
            else
            {
                animator.SetBool(HashManager.Instance.DicMainParams[TransitionParameter.Grounded], false);
            }
        }
        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            
        }
        private bool IsGrounded(CharacterController control)
        {
            if (control.contactPoints != null)
            {
                foreach (ContactPoint c in control.contactPoints)
                {
                    float collideBottom = (control.transform.position.y + control.boxCollider.center.y)
                                           - (control.boxCollider.size.y / 2f);
                    float yDifference = Mathf.Abs(c.point.y - collideBottom);

                    if (yDifference < 0.01f)
                    {
                        if (Math.Abs(control.RIGID_BODY.velocity.y) < 0.001f)
                        {
                            control.animationProgress.Ground = c.otherCollider.transform.root.gameObject;
                            control.BOX_COLLIDER_DATA.LandingPosition = new Vector3(
                                    0f,
                                    c.point.y,
                                    c.point.z);

                            return true;
                        }
                    }
                }
            }

            if (control.RIGID_BODY.velocity.y < 0f)
            {
                foreach (GameObject o in control.COLLISION_DATA.BottomSpheres)
                {
                    GameObject blockingObj = CollisionDetection.GetCollidingObject(control, o, -Vector3.up, Distance,
                                                                                   ref control.animationProgress.CollidingPoint);

                    if (blockingObj != null)
                    {
                        CharacterController c = CharacterManager.Instance.GetCharacter(blockingObj.transform.root.gameObject);
                        if (c == null)
                        {
                            control.BOX_COLLIDER_DATA.LandingPosition = new Vector3(
                                0f,
                                control.animationProgress.CollidingPoint.y,
                                control.animationProgress.CollidingPoint.z);
                            return true;
                        }
                    }
                }
            }

            control.animationProgress.Ground = null;
            return false;
        }
    }
}
