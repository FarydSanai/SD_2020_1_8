using System.Collections.Generic;
using System.Net;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using UnityEngine;

namespace SamuraiGame
{
    public class DamageDetector : SubComponent
    {
        public DamageData damageData;


        [Header("Damage Setup")]
        [SerializeField]
        private List<RuntimeAnimatorController> HitReactionsList = new List<RuntimeAnimatorController>();
        [SerializeField]
        private Attack MarioStompAttack;
        [SerializeField]
        private Attack SwordThrow;

        private void Start()
        {
            damageData = new DamageData
            {
                Attacker = null,
                Attack = null,
                DamagedTrigger = null,
                AttackingPart = null,
                BlockedAttack = null,
                HP = control.CharacterHP,
                MarioStompAttack = MarioStompAttack,
                SwordThrow = SwordThrow,

                IsDead = IsDead,

                TakeDamage = TakeDamage,
            };
            subComponentProcessor.damageData = damageData;
            subComponentProcessor.ArrSubComponents[(int)SubComponentType.DAMAGE_DETECTOR] = this;
            //subComponentProcessor.ComponentsDic.Add(SubComponentType.DAMAGE_DETECTOR, this);

        }
        public override void OnFixedUpdate()
        {
            if (AttackManager.Instance.CurrentAttacks.Count > 0)
            {
                CheckAttack();
            }
        }
        public override void OnUpdate()
        {

        }
        private bool AttackIsValid(AttackCondition info)
        {
            if (info == null)
            {
                return false;
            }
            if (!info.isRegistered)
            {
                return false;
            }
            if (info.isFinished)
            {
               return false;
            }
            if (info.CurrentHits >= info.MaxHits)
            {
                return false;
            }
            if (info.Attacker == control)
            {
                return false;
            }
            if (info.MustFaceAttacker)
            {
                Vector3 vec = this.transform.position - info.Attacker.transform.position;
                if ((vec.z * info.Attacker.transform.forward.z) < 0f)
                {
                    return false;
                }
            }
            if (info.RegisteredTargets.Contains(this.control))
            {
                return false;
            }

            return true;
        }
        private void CheckAttack()
        {
            foreach (AttackCondition info in AttackManager.Instance.CurrentAttacks)
            {
                if (AttackIsValid(info))
                {
                    if (info.MustCollide)
                    {
                        if (control.animationProgress.CollidingBodyParts.Count != 0)
                        {
                            if (IsCollided(info))
                            {
                                TakeDamage(info);
                            }
                        }
                    }
                    else
                    {
                        if (IsInLethalRange(info))
                        {
                            TakeDamage(info);
                        }
                    }
                }
            }
        }
        private bool IsCollided(AttackCondition info)
        {
            foreach (KeyValuePair<TriggerDetector, List<Collider>> data in control.animationProgress.CollidingBodyParts)
            {
                foreach (Collider collider in data.Value)
                {
                    foreach (AttackPartType part in info.AttackParts)
                    {
                        if (info.Attacker.GetAttackingPart(part) == collider.gameObject)
                        {
                            damageData.SetData(info.Attacker, info.AttackAbility,
                                       data.Key, info.Attacker.GetAttackingPart(part));

                            SoundManager.PlaySound();

                            return true;
                        }
                    }
                }
            }

            return false;
        }

        private bool IsInLethalRange(AttackCondition info)
        {
            foreach (Collider c in control.RAGDOLL_DATA.BodyParts)
            {
                float dist = Vector3.SqrMagnitude(c.transform.position - info.Attacker.transform.position);

                if (dist <= info.LethalRange)
                {
                    int index = Random.Range(0, control.RAGDOLL_DATA.BodyParts.Count);
                    TriggerDetector triggerDetector = control.RAGDOLL_DATA.BodyParts[index].GetComponent<TriggerDetector>();

                    damageData.SetData(
                        info.Attacker,
                        info.AttackAbility,
                        triggerDetector,
                        null);

                    return true;
                }
            }

            return false;
        }

        private bool IsDead()
        {
            if (damageData.HP <= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool IsBLocked(AttackCondition info)
        {
            if (info == damageData.BlockedAttack && damageData.BlockedAttack != null)
            {
                return true;
            }
            if (control.ANIMATION_DATA.IsRunning(typeof(Block)))
            {
                Vector3 dir = info.Attacker.transform.position - control.transform.position;
                if (dir.z > 0f)
                {
                    if (control.ROTATION_DATA.IsFacingForward())
                    {
                        return true;
                    }
                }
                else if (dir.z < 0f)
                {
                    if (!control.ROTATION_DATA.IsFacingForward())
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        private void TakeDamage(AttackCondition info)
        {
            if (IsDead())
            {
                if (!info.RegisteredTargets.Contains(this.control))
                {
                    info.RegisteredTargets.Add(this.control);
                    control.RAGDOLL_DATA.AddForceToDamagedPart(true);
                }
                return;
            }

            if (IsBLocked(info))
            {
                damageData.BlockedAttack = info;
                return;
            }

            if (info.MustCollide)
            {
                CameraManager.Instance.ShakeCamera(0.35f);

                if (info.AttackAbility.UseDeathParticles)
                {
                    if (info.AttackAbility.ParticleType.ToString().Contains("VFX"))
                    {
                        GameObject vfx = PoolManager.Instance.GetObject(info.AttackAbility.ParticleType);
                        vfx.transform.position = damageData.AttackingPart.transform.position;

                        vfx.SetActive(true);

                        if (info.Attacker.ROTATION_DATA.IsFacingForward())
                        {
                            vfx.transform.rotation = Quaternion.Euler(0f,0f,0f);
                        } 
                        else
                        {
                            vfx.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
                        }
                    }
                }
            }
 
            Debug.Log(string.Format("{0} hits: {1}", info.Attacker.gameObject.name, this.gameObject.name));

            info.CurrentHits++;
            damageData.HP -= info.AttackAbility.Damage;

            AttackManager.Instance.ForceDeregister(control);
            control.ANIMATION_DATA.CurrentRunningAbilities.Clear();

            if (IsDead())
            {
                control.RAGDOLL_DATA.RagdollTriggered = true;

                EnemySpawnManager.Instance.AliveEnemyList.Remove(control.gameObject);
                EnemySpawnManager.Instance.DeadEnemyList.Add(control.gameObject);
            }
            else
            {
                int rand = Random.Range(0, HitReactionsList.Count);

                control.SkinnedMeshAnimator.runtimeAnimatorController = null;
                control.SkinnedMeshAnimator.runtimeAnimatorController = HitReactionsList[rand];
            }

            //register damaged target
            if (!info.RegisteredTargets.Contains(this.control))
            {
                info.RegisteredTargets.Add(this.control);
            }

        }
    }
}
