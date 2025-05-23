﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SamuraiGame
{
    public class LedgeChecker : SubComponent
    {
        public LedgeGrabData ledgeGrabData;

        [Header("Ledge Setup")]
        [SerializeField] private Vector3 LedgeCalibration = new Vector3();
        public LedgeCollider Collider1;
        public LedgeCollider Collider2;

        private void Start()
        {
            ledgeGrabData = new LedgeGrabData
            {
                isGrabbingledge = false,
                LedgeCollidersOff = LedgeCollidersOff,
            };

            subComponentProcessor.ledgeGrabData = ledgeGrabData;
            subComponentProcessor.ArrSubComponents[(int)SubComponentType.LEDGECHECKER] = this;
            //subComponentProcessor.ComponentsDic.Add(SubComponentType.LEDGECHECKER, this);
        }
        public override void OnUpdate()
        {
            
        }
        public override void OnFixedUpdate()
        {
            if (control.SkinnedMeshAnimator.GetBool(HashManager.Instance.DicMainParams[TransitionParameter.Grounded]))
            {
                if (control.RIGID_BODY.useGravity)
                {
                    ledgeGrabData.isGrabbingledge = false;
                }
            }
            if (IsLedgeGrabCondition())
            {
                ProcessLedgeGrab();

                //if (ledgeGrabData.isGrabbingledge &&
                //    !control.RIGID_BODY.useGravity &&
                //    !control.SkinnedMeshAnimator.GetBool(HashManager.Instance.DicMainParams[TransitionParameter.Grounded])
                //    )
                //{
                //    if (!control.ANIMATION_DATA.IsRunning(typeof(LedgeGrabState)))
                //    {
                //        Debug.Break();
                //    }
                //}
            }
        }
        private bool IsLedgeGrabCondition()
        {
            if (!control.Jump)
            {
                return false;
            }

            for (int i = 0; i < HashManager.Instance.ArrLedgeTriggerStates.Length; i++)
            {
                AnimatorStateInfo info = control.SkinnedMeshAnimator.GetCurrentAnimatorStateInfo(0);
                if (info.shortNameHash == HashManager.Instance.ArrLedgeTriggerStates[i])
                {
                    return true;
                }
            }

            return false;
        }
        private void ProcessLedgeGrab()
        {
            if (!control.SkinnedMeshAnimator.
                            GetBool(HashManager.Instance.DicMainParams[TransitionParameter.Grounded]))
            {
                foreach (GameObject obj in Collider1.CollidedObjects)
                {
                    if (!Collider2.CollidedObjects.Contains(obj))
                    {
                        if (OffsetPosition(obj))
                        {
                            break;
                        }
                    }
                    else
                    {
                        ledgeGrabData.isGrabbingledge = false;
                    }
                }
            }
            else
            {
                ledgeGrabData.isGrabbingledge = false;
            }
        }
        private bool OffsetPosition(GameObject platform)
        {
            BoxCollider platformCol = platform.GetComponent<BoxCollider>();
            if (platformCol == null)
            {
                return false;
            }
            if (ledgeGrabData.isGrabbingledge)
            {
                return false;
            }

            ledgeGrabData.isGrabbingledge = true;
            control.RIGID_BODY.useGravity = false;
            control.RIGID_BODY.velocity = Vector3.zero;

            //Debug.Log(ledgeGrabData.isGrabbingledge);

            float y, z;

            y = platformCol.transform.position.y + platformCol.bounds.extents.y;

            if (control.ROTATION_DATA.IsFacingForward())
            {
                z = platform.transform.position.z - platformCol.bounds.extents.z;
                //z = platformCol.bounds.center.z - platformCol.bounds.extents.z;
            }
            else
            {
                z = platform.transform.position.z + platformCol.bounds.extents.z;
                //z = platformCol.bounds.center.z + platformCol.bounds.extents.z;
            }

            Vector3 platformEdge = new Vector3(0f, y, z);

            if (control.BLOCKING_DATA.RightSideBlocked())
            {
                control.RIGID_BODY.MovePosition(platformEdge + LedgeCalibration);
            }
            else if(control.BLOCKING_DATA.LeftSideBlocked())
            {
                control.RIGID_BODY.MovePosition(platformEdge + new Vector3(0f, LedgeCalibration.y, -LedgeCalibration.z));
            }
            else
            {
                ledgeGrabData.isGrabbingledge = false;
                control.RIGID_BODY.useGravity = true;
                return false;
            }

            return true;
        }
        public void LedgeCollidersOff()
        {
            Collider1.GetComponent<BoxCollider>().enabled = false;
            Collider2.GetComponent<BoxCollider>().enabled = false;
        }
    }
}
