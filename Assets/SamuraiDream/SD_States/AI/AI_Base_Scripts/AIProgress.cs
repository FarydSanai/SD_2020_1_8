using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Remoting.Messaging;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SamuraiGame
{
    public class AIProgress : MonoBehaviour
    {
        public PathFindingAgent pathFindingAgent;
        public CharacterController BlockCharacter;

        [Header("AiAttacks")]
        public bool DoFlyingKick;

        private CharacterController control;

        private void Awake()
        {
            control = this.gameObject.GetComponentInParent<CharacterController>();
        }
        public float AIDistanceToStartSphere()
        {
            return Vector3.SqrMagnitude(
                control.aiProgress.pathFindingAgent.StartSphere.transform.position -
                control.transform.position);
        }
        public float AIDistanceToEndSphere()
        {
            return Vector3.SqrMagnitude(
                control.aiProgress.pathFindingAgent.EndSphere.transform.position -
                control.transform.position);
        }
        public float AIDistanceToTarget()
        {
            return Vector3.SqrMagnitude(
                control.aiProgress.pathFindingAgent.Target.transform.position -
                control.transform.position);
        }
        public float TargetDistanceToEndSphere()
        {
            return Vector3.SqrMagnitude(
                control.aiProgress.pathFindingAgent.EndSphere.transform.position-
                control.aiProgress.pathFindingAgent.Target.transform.position);
        }
        public bool TargetIsDead()
        {
            if (CharacterManager.Instance.GetCharacter(control.aiProgress.pathFindingAgent.Target)
                                                       .DAMAGE_DATA.IsDead())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool TargetIsGrounded()
        {
            CharacterController target = CharacterManager.Instance.GetCharacter(control.aiProgress.pathFindingAgent.Target);

            if (target.GROUND_DATA.Ground == null)
            {
                return false;
            } 
            else
            {
                return true;
            }
        }
        public bool TargetIsOnTheSamePlatform()
        {
            CharacterController target = CharacterManager.Instance.GetCharacter(control.aiProgress.pathFindingAgent.Target);

            if (target.GROUND_DATA.Ground == control.GROUND_DATA.Ground)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool EndSphereIsHigher()
        {
            if (EndSphereIsStraight())
            {
                return false;
            }
            if ((pathFindingAgent.EndSphere.transform.position.y - pathFindingAgent.StartSphere.transform.position.y) > 0f)
            {
                return true;
            } 
            else
            {
                return false;
            }
        }
        public bool EndSphereIsLower()
        {
            if (EndSphereIsStraight())
            {
                return false;
            }
            if ((pathFindingAgent.EndSphere.transform.position.y - pathFindingAgent.StartSphere.transform.position.y) > 0f)
            {
                return false;
            } 
            else
            {
                return true;
            }
        }
        public bool EndSphereIsStraight()
        {
            if (Math.Abs(pathFindingAgent.EndSphere.transform.position.y - pathFindingAgent.StartSphere.transform.position.y) > 0.01f)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public bool TargetIsOnRightSide()
        {
            if ((control.aiProgress.pathFindingAgent.Target.transform.position - control.transform.position).z > 0f)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool IsFacingTarget()
        {
            if ((control.aiProgress.pathFindingAgent.Target.transform.position - control.transform.position).z > 0f)
            {
                if (control.ROTATION_DATA.IsFacingForward())
                {
                    return true;
                }
            }
            else
            {
                if (!control.ROTATION_DATA.IsFacingForward())
                {
                    return true;
                }
            }
            return false;
        }

        public void RepositionDestination()
        {
            pathFindingAgent.StartSphere.transform.position = pathFindingAgent.Target.transform.position;
            pathFindingAgent.EndSphere.transform.position = pathFindingAgent.Target.transform.position;
        }
        public void SetRandomFlyingKick()
        {
            float range = Random.Range(0, 7);
            if (range <= 1)
            {
                //Debug.Log(range);
                DoFlyingKick = true;
            }
            else
            {
                //Debug.Log(range);
                DoFlyingKick = false;
            }
        }

        public float GetStartSphereHeight()
        {
            Vector3 vec = control.transform.position - pathFindingAgent.StartSphere.transform.position;

            return Math.Abs(vec.y);
        }
    }
}
