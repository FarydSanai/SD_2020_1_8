using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SamuraiGame
{
    public class TriggerDetector : MonoBehaviour
    {
        //public List<Collider> CollidingParts = new List<Collider>();
        public Vector3 LastPosition;
        public Quaternion LastRotation;

        public CharacterController control;

        private void Awake()
        {
            control = this.GetComponentInParent<CharacterController>();
        } 
        private void OnTriggerEnter(Collider col)
        {
            CheckCollidingBodyParts(col);
            CheckCollidingWeapons(col);
        }

        private void OnTriggerExit(Collider col)
        {
            CheckExitingBodyParts(col);
            CheckExitingWeapons(col);
        }
        private void CheckCollidingWeapons(Collider col)
        {
            MeleeWeapon weapon = col.transform.root.gameObject.GetComponent<MeleeWeapon>();

            if (weapon == null)
            {
                return;
            }
            if (weapon.IsThrown)
            {
                if (weapon.Thrower != control)
                {
                    AttackInfo info = gameObject.AddComponent<AttackInfo>();
                    info.CopyInfo(control.DAMAGE_DATA.SwordThrow, control);

                    control.DAMAGE_DATA.SetData(
                        weapon.Thrower,
                        control.DAMAGE_DATA.SwordThrow,
                        this,
                        null
                        );

                    control.DAMAGE_DATA.TakeDamage(info);

                    if (weapon.FlyingForward)
                    {
                        weapon.transform.rotation = Quaternion.Euler(0f, 90f, 75f);
                    }
                    else
                    {
                        weapon.transform.rotation = Quaternion.Euler(0f, -90f, 75f);
                    }

                    weapon.transform.parent = this.transform;

                    Vector3 offset = this.transform.position - weapon.SwordTip.transform.position;
                    weapon.transform.position += offset;

                    weapon.IsThrown = false;
                    return;
                }
            }

            if (!control.animationProgress.CollidingWeapons.ContainsKey(this))
            {
                control.animationProgress.CollidingWeapons.Add(this, new List<Collider>());
            }
            if (!control.animationProgress.CollidingWeapons[this].Contains(col))
            {
                control.animationProgress.CollidingWeapons[this].Add(col);
            }
        }
        private void CheckExitingWeapons(Collider col)
        {
            if (control == null)
            {
                return;
            }
            if (control.animationProgress.CollidingWeapons.ContainsKey(this))
            {
                if (control.animationProgress.CollidingWeapons[this].Contains(col))
                {
                    control.animationProgress.CollidingWeapons[this].Remove(col);
                }
                if (control.animationProgress.CollidingWeapons[this].Count == 0)
                {
                    control.animationProgress.CollidingWeapons.Remove(this);
                }
            }
        }
        private void CheckExitingBodyParts(Collider col)
        {
            if (control == null)
            {
                return;
            }
            if (control.animationProgress.CollidingBodyParts.ContainsKey(this))
            {
                if (control.animationProgress.CollidingBodyParts[this].Contains(col))
                {
                    control.animationProgress.CollidingBodyParts[this].Remove(col);
                }
                if (control.animationProgress.CollidingBodyParts[this].Count == 0)
                {
                    control.animationProgress.CollidingBodyParts.Remove(this);
                }
            }
        }
        private void CheckCollidingBodyParts(Collider col)
        {
            if (control == null)
            {
                return;
            }
            if (control.RAGDOLL_DATA.BodyParts.Contains(col))
            {
                return;
            }

            CharacterController attacker = col.transform.root.GetComponent<CharacterController>();
            if (attacker == null)
            {
                return;
            }

            if (col.gameObject == attacker.gameObject)
            {
                return;
            }

            if (!control.animationProgress.CollidingBodyParts.ContainsKey(this))
            {
                control.animationProgress.CollidingBodyParts.Add(this, new List<Collider>());
            }

            if (!control.animationProgress.CollidingBodyParts[this].Contains(col))
            {
                control.animationProgress.CollidingBodyParts[this].Add(col);
            }
        }
    }
}