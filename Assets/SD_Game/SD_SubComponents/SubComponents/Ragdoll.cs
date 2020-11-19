using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SamuraiGame
{
    public class Ragdoll : SubComponent
    {
        public RagdollData ragdollData;

        private void Start()
        {
            ragdollData = new RagdollData
            {
                RagdollTriggered = false,
                BodyParts = new List<Collider>(),
                GetBody = GetBodyPart,
                AddForceToDamagedPart = AddForceToDamagedPart,
            };

            SetupBodyParts();
            subComponentProcessor.ragdollData = ragdollData;
            subComponentProcessor.ArrSubComponents[(int)SubComponentType.RAGDOLL] = this;
            //subComponentProcessor.ComponentsDic.Add(SubComponentType.RAGDOLL, this);

        }
        public override void OnFixedUpdate()
        {
            if (ragdollData.RagdollTriggered)
            {
                ProcRagdoll();
            }
        }
        public override void OnUpdate()
        {
            
        }
        public void SetupBodyParts()
        {
            ragdollData.BodyParts.Clear();

            Collider[] colliders = control.gameObject.GetComponentsInChildren<Collider>();

            foreach (Collider c in colliders)
            {
                if (c.gameObject != control.gameObject)
                {
                    if (c.gameObject.GetComponent<LedgeChecker>() == null &&
                        c.gameObject.GetComponent<LedgeCollider>() == null)
                    {
                        c.isTrigger = true;
                        ragdollData.BodyParts.Add(c);
                        c.attachedRigidbody.interpolation = RigidbodyInterpolation.Interpolate;
                        c.attachedRigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;

                        CharacterJoint joint = c.GetComponent<CharacterJoint>();
                        if (joint != null)
                        {
                            joint.enableProjection = true;
                        }
                        if (c.GetComponent<TriggerDetector>() == null)
                        {
                            c.gameObject.AddComponent<TriggerDetector>();
                        }
                    }
                }
            }
        }
        private void ProcRagdoll()
        {
            ragdollData.RagdollTriggered = false;

            if (control.SkinnedMeshAnimator == null)
            {
                return;
            }
            //Change layers
            Transform[] arr = control.gameObject.GetComponentsInChildren<Transform>();
            foreach (Transform t in arr)
            {
                t.gameObject.layer = LayerMask.NameToLayer(SD_Layers.DEADBODY.ToString());
            }

            //save bodypart position;
            foreach (Collider c in ragdollData.BodyParts)
            {
                TriggerDetector det = c.GetComponent<TriggerDetector>();
                det.LastPosition = c.gameObject.transform.position;
                det.LastRotation = c.gameObject.transform.rotation;
            }

            //Turn off animator/avatar
            control.RIGID_BODY.useGravity = false;
            control.RIGID_BODY.velocity = Vector3.zero;
            control.gameObject.GetComponent<BoxCollider>().enabled = false;
            control.SkinnedMeshAnimator.enabled = false;
            control.SkinnedMeshAnimator.avatar = null;

            //Turn off Ledge colliders
            control.LEDGE_GRAB_DATA.LedgeCollidersOff();

            //Turn off AI
            if (control.aiController != null)
            {
                control.aiController.gameObject.SetActive(false);
                control.navMeshObstacle.enabled = false;
            }

            //Turn on Ragdoll
            foreach (Collider c in ragdollData.BodyParts)
            {
                c.isTrigger = false;

                TriggerDetector det = c.GetComponent<TriggerDetector>();
                c.attachedRigidbody.MovePosition(det.LastPosition);
                c.attachedRigidbody.MoveRotation(det.LastRotation);

                c.attachedRigidbody.velocity = Vector3.zero;
            }

            //add force
            AddForceToDamagedPart(false);
        }
        private Collider GetBodyPart(string name)
        {
            foreach (Collider c in ragdollData.BodyParts)
            {
                if (c.name.Contains(name))
                {
                    return c;
                }
            }
            return null;
        }
        private void AddForceToDamagedPart(bool zeroVelocity)
        {
            if (control.DAMAGE_DATA.DamagedTrigger != null)
            {
                if (zeroVelocity)
                {
                    foreach (Collider c in ragdollData.BodyParts)
                    {
                        c.attachedRigidbody.velocity = Vector3.zero;
                    }
                }

                control.DAMAGE_DATA.DamagedTrigger.GetComponent<Rigidbody>().
                        AddForce(control.DAMAGE_DATA.Attacker.transform.forward * control.DAMAGE_DATA.Attack.ForwardForce +
                                 control.DAMAGE_DATA.Attacker.transform.right * control.DAMAGE_DATA.Attack.RightForce +
                                 control.DAMAGE_DATA.Attacker.transform.up * control.DAMAGE_DATA.Attack.UpForce);
            }
        }
    }
}