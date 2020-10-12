using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using UnityEngine;

namespace SamuraiGame
{
    public class MeleeWeapon : MonoBehaviour
    {
        public CharacterController control;

        public Vector3 CustomPosition = new Vector3();
        public Vector3 CustomRotation = new Vector3();

        public BoxCollider PickUpCollider;
        public BoxCollider AttackCollider;

        public TriggerDetector triggerDetector;

        [Header("Throw Offset")]
        public Vector3 ThrowOffset = new Vector3();
        public bool IsThrown;
        public bool FlyingForward;
        public float FlightSpeed;
        public float RotationSpeed;
        public CharacterController Thrower;
        public GameObject SwordTip;

        private void Start()
        {
            IsThrown = false;
        }

        private void Update()
        {
            if (control != null)
            {
                PickUpCollider.enabled = false;
                AttackCollider.enabled = true;
            }
            else
            {
                PickUpCollider.enabled = true;
                AttackCollider.enabled = false;
            }
        }
        private void FixedUpdate()
        {
            if (IsThrown)
            {
                if (FlyingForward)
                {
                    this.transform.position += (Vector3.forward * FlightSpeed * Time.deltaTime);
                }
                else
                {
                    this.transform.position -= (Vector3.forward * FlightSpeed * Time.deltaTime);
                }
                this.transform.Rotate(Vector3.forward, RotationSpeed * Time.deltaTime);
            }
        }
        public static bool IsWeapon(GameObject obj)
        {
            if (obj.transform.root.GetComponent<MeleeWeapon>() != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void DropWeapon()
        {
            MeleeWeapon w = control.animationProgress.HoldingWeapon;

            if (w != null)
            {
                w.transform.parent = null;
                if (control.ROTATION_DATA.IsFacingForward())
                {
                    w.transform.position = control.transform.position + (Vector3.forward * 0.25f);
                    w.transform.rotation = Quaternion.Euler(90f, -55f, 0f);
                } 
                else
                {
                    w.transform.position = control.transform.position + (Vector3.forward * -0.25f);
                    w.transform.rotation = Quaternion.Euler(-90f, -55f, 0f);
                }

                RemoveWeaponFromDictionary(control);

                control.animationProgress.HoldingWeapon = null;
                control = null;
                w.triggerDetector.control = null;
            }
        }
        public void ThrowWeapon()
        {
            MeleeWeapon w = control.animationProgress.HoldingWeapon;

            if (w != null)
            {
                w.transform.parent = null;
                if (control.ROTATION_DATA.IsFacingForward())
                {
                    w.transform.rotation = Quaternion.Euler(0f, 90f, 0f);
                }
                else
                {
                    w.transform.rotation = Quaternion.Euler(0f, -90f, 0f);
                }

                FlyingForward = control.ROTATION_DATA.IsFacingForward();

                w.transform.position = control.transform.position + (Vector3.up * ThrowOffset.y);
                w.transform.position += (control.transform.forward * ThrowOffset.z);

                RemoveWeaponFromDictionary(control);

                Thrower = control;
                control.animationProgress.HoldingWeapon = null;
                control = null;
                w.triggerDetector.control = null;
                IsThrown = true;
            }
        }
        public void RemoveWeaponFromDictionary(CharacterController control)
        {
            foreach (Collider col in control.RAGDOLL_DATA.BodyParts)
            {
                TriggerDetector t = col.GetComponent<TriggerDetector>();

                if (t != null)
                {
                    ProcRemove(control.animationProgress.CollidingWeapons, t);
                    ProcRemove(control.animationProgress.CollidingBodyParts, t);
                }
            }
        }
        private void ProcRemove(Dictionary<TriggerDetector, List<Collider>> d, TriggerDetector t)
        {
            if (d.ContainsKey(t))
            {
                if (d[t].Contains(PickUpCollider))
                {
                    d[t].Remove(PickUpCollider);
                }
                if (d[t].Contains(AttackCollider))
                {
                    d[t].Remove(AttackCollider);
                }
                if (d[t].Count == 0)
                {
                    d.Remove(t);
                }
            }
        }
    }
}