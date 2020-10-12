using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SamuraiGame
{
    public class InstaKill : SubComponent
    {
        public InstaKillData instaKillData;

        [Header("InstaKillAnimators")]
        [SerializeField]
        private RuntimeAnimatorController Assasination_A;
        [SerializeField]
        private RuntimeAnimatorController Assasination_B;
        private void Start()
        {
            instaKillData = new InstaKillData
            {
                Animation_A = Assasination_A,
                Animation_B = Assasination_B,
                DeathByInstaKill = DeathByInstaKill,
            };
            subComponentProcessor.instaKillData = instaKillData;
            subComponentProcessor.ComponentsDic.Add(SubComponentType.INSTAKILL, this);
        }
        public override void OnFixedUpdate()
        {
            if (control.subComponentProcessor.ComponentsDic.ContainsKey(SubComponentType.MANUALINPUT))
            {
                return;
            }
            if (!control.SkinnedMeshAnimator.GetBool(
                        HashManager.Instance.DicMainParams[TransitionParameter.Grounded]))
            {
                return;
            }
            foreach (KeyValuePair<TriggerDetector, List<Collider>> data in control.animationProgress.CollidingBodyParts)
            {
                foreach (Collider col in data.Value)
                {
                    CharacterController c = CharacterManager.Instance.GetCharacter(
                                col.transform.root.gameObject);

                    if (c == control)
                    {
                        continue;
                    }
                    if (!c.subComponentProcessor.ComponentsDic.ContainsKey(SubComponentType.MANUALINPUT))
                    {
                        continue;
                    }
                    if (!control.SkinnedMeshAnimator.GetBool(
                                HashManager.Instance.DicMainParams[TransitionParameter.Grounded]))
                    {
                        continue;
                    }
                    if (c.animationProgress.IsRunning(typeof(Attack)))
                    {
                        continue;
                    }
                    if (control.animationProgress.IsRunning(typeof(Attack)))
                    {
                        continue;
                    }
                    if (c.animationProgress.StateNameContains("RunningSlide"))
                    {
                        continue;
                    }
                    if (c.DAMAGE_DATA.IsDead())
                    {
                        continue;
                    }
                    if (control.DAMAGE_DATA.IsDead())
                    {
                        continue;
                    }

                    c.INSTAKILL_DATA.DeathByInstaKill(control);

                    return;
                }
            }
        }
        public override void OnUpdate()
        {
            throw new System.NotImplementedException();
        }
        private void DeathByInstaKill(CharacterController attacker)
        {
            control.animationProgress.CurrentRunningAbilities.Clear();
            attacker.animationProgress.CurrentRunningAbilities.Clear();

            Vector3 dir = control.transform.position - attacker.transform.position;

            if (!control.SkinnedMeshAnimator.GetBool(HashManager.Instance.DicMainParams[TransitionParameter.Grounded]))
            {
                return;
            }

            control.RIGID_BODY.useGravity = false;
            control.boxCollider.enabled = false;
            control.SkinnedMeshAnimator.runtimeAnimatorController = control.INSTAKILL_DATA.Animation_B;

            attacker.RIGID_BODY.useGravity = false;
            attacker.boxCollider.enabled = false;
            attacker.SkinnedMeshAnimator.runtimeAnimatorController = control.INSTAKILL_DATA.Animation_A;

            if (dir.z < 0f)
            {
                attacker.ROTATION_DATA.FaceForward(false);
            }
            else if (dir.z > 0f)
            {
                attacker.ROTATION_DATA.FaceForward(true);
            }

            control.transform.LookAt(control.transform.position + (attacker.transform.forward * 5f), Vector3.up);
            control.transform.forward = attacker.transform.forward;
            control.transform.position = control.transform.position +
                                        (attacker.transform.forward * -0.2f);

            control.DAMAGE_DATA.HP = 0f;
        }
    } 
}
