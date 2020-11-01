using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SamuraiGame
{
    public enum AttackPartType
    {
        LEFT_HAND,
        RIGHT_HAND,

        LEFT_FOOT,
        RIGHT_FOOT,

        MELEE_WEAPON,
    }

    [CreateAssetMenu(fileName = "New state", menuName = "SamuraiDream/AbilityData/Attack")]
    public class Attack : CharacterAbility
    {
        [Header("Base settings")]
        public bool IsDebug;
        public float StartAttackTime;
        public float EndAttackTime;
        public List<AttackPartType> AttackParts = new List<AttackPartType>();
        public bool MustCollide;
        public bool MustFaceAttacker;
        public float LethalRange;
        public int MaxHits;
        public float Damage;

        [Space(5)]

        [Header("Ragdoll Death")]
        public float ForwardForce;
        public float RightForce;
        public float UpForce;

        [Space(5)]

        [Header("Combo timing")]
        [Range(0f, 1f)]
        public float ComboStartTime;
        [Range(0f, 1f)]
        public float ComboEndTime;

        public bool UseEmissionLight;

        private List<AttackCondition> FinishedAttacks = new List<AttackCondition>();

        [Space(5)]

        [Header("Death particles")]
        public bool UseDeathParticles;
        public PoolObjectType ParticleType;

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            characterState.ATTACK_DATA.AttackTriggered = false;
            animator.SetBool(HashManager.Instance.DicMainParams[TransitionParameter.Attack], false);

            GameObject obj = PoolManager.Instance.GetObject(PoolObjectType.ATTACK_CONDITION);
            AttackCondition info = obj.GetComponent<AttackCondition>();

            obj.SetActive(true);

            info.ResetInfo(this, characterState.characterControl);

            if (!AttackManager.Instance.CurrentAttacks.Contains(info))
            { 
                AttackManager.Instance.CurrentAttacks.Add(info);
            }
        }
        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (UseEmissionLight)
            {
                RaiseEmissionForAttack(characterState.characterControl.jointsEmission, stateInfo);
            }

            RegisterAttack(characterState, animator, stateInfo);
            DeregisterAttack(characterState, animator, stateInfo);
            CheckCombo(characterState, animator, stateInfo);
        }
        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            animator.SetBool(HashManager.Instance.DicMainParams[TransitionParameter.Attack], false);
            ClearAttack();
        }

        public void RegisterAttack(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (StartAttackTime <= stateInfo.normalizedTime && EndAttackTime > stateInfo.normalizedTime)
            {
                foreach (AttackCondition info in AttackManager.Instance.CurrentAttacks)
                {
                    if (info == null)
                    {
                        continue;
                    }
                    if (!info.isRegistered && info.AttackAbility == this)
                    {
                        if (IsDebug)
                        {
                            Debug.Log(string.Format("{0} registered: {1}", this.name, stateInfo.normalizedTime));
                            //Debug.Break();
                        }
                        info.Register(this);
                    }
                } 
            }
        }
        public void DeregisterAttack(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (stateInfo.normalizedTime >= EndAttackTime)
            {
                foreach (AttackCondition info in AttackManager.Instance.CurrentAttacks)
                {
                    if (info == null)
                    {
                        continue;
                    }
                    if (info.AttackAbility == this && !info.isFinished)
                    {
                        info.isFinished = true;
                        info.GetComponent<PoolObject>().TurnOff();

                        foreach (CharacterController c in CharacterManager.Instance.Characters)
                        {
                            if (c.DAMAGE_DATA.BlockedAttack == info)
                            {
                                c.DAMAGE_DATA.BlockedAttack = null;
                            }
                        }

                        if (IsDebug)
                        {
                            Debug.Log(string.Format("{0} de-registered: {1}", this.name, stateInfo.normalizedTime));
                        }
                    } 
                }
            }
        }
        public void CheckCombo(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (stateInfo.normalizedTime >= ComboStartTime)
            {
                if (stateInfo.normalizedTime <= ComboEndTime)
                {
                    if (characterState.ATTACK_DATA.AttackTriggered)
                    {
                        animator.SetBool(HashManager.Instance.DicMainParams[TransitionParameter.Attack], true);
                    }
                }
            }
        }

        public void ClearAttack()
        {
            FinishedAttacks.Clear();

            foreach (AttackCondition info in AttackManager.Instance.CurrentAttacks)
            {
                if (info == null || info.AttackAbility == this)
                {
                    FinishedAttacks.Add(info);
                }
            }

            foreach (AttackCondition info in FinishedAttacks)
            {
                if (AttackManager.Instance.CurrentAttacks.Contains(info))
                {
                    AttackManager.Instance.CurrentAttacks.Remove(info);
                }
            }
        }
        private void RaiseEmissionForAttack(RaiseEmission emission, AnimatorStateInfo stateInfo)
        {
            if (emission != null)
            {
                if (stateInfo.normalizedTime <= 0.5f)
                {
                    Vector3 val = Vector3.Lerp(new Vector3(0f, 0f, 0f), new Vector3(0.1f, 3f, 6f), stateInfo.normalizedTime);
                    emission.RaiseEmissionLight(val);
                }
                else
                {
                    Vector3 val = Vector3.Lerp(new Vector3(0.1f, 3f, 6f), new Vector3(0f, 0f, 0f), stateInfo.normalizedTime);
                    emission.RaiseEmissionLight(val);
                }
            }
        } 
    }
}

