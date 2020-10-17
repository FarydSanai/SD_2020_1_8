using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SamuraiGame
{
    public class CharacterState : StateMachineBehaviour
    {
        public CharacterController characterControl;
        [Space(3)]
        public List<CharacterAbility> ListAbilityData = new List<CharacterAbility>();

        public BlockingObjData BLOCKING_DATA => characterControl.subComponentProcessor.blockingData;
        public RagdollData RAGDOLL_DATA => characterControl.subComponentProcessor.ragdollData;
        public BoxColliderData BOX_COLLIDER_DATA => characterControl.subComponentProcessor.boxColliderData;
        public VerticalVelocityData VERTICAL_VELOCITY_DATA => characterControl.subComponentProcessor.verticalVelocityData;
        public MomentumData MOMENTUM_DATA => characterControl.subComponentProcessor.momentumData;
        public RotationData ROTATION_DATA => characterControl.subComponentProcessor.rotationData;
        public JumpData JUMP_DATA => characterControl.subComponentProcessor.jumpData;
        public CollisionSphereData COLLISION_SPHERE_DATA => characterControl.subComponentProcessor.collisionSphereData;
        public GroundData GROUND_DATA => characterControl.subComponentProcessor.groundData;
        public AttackData ATTACK_DATA => characterControl.subComponentProcessor.attackData;
        public AnimationData ANIMATION_DATA => characterControl.subComponentProcessor.animationData;
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (characterControl == null)
            {
                CharacterController control = animator.transform.root.GetComponent<CharacterController>();
                control.InitCharacterStates(animator);
            }
            foreach (CharacterAbility d in ListAbilityData)
            {
                d.OnEnter(this, animator, stateInfo);

                if (ANIMATION_DATA.CurrentRunningAbilities.ContainsKey(d))
                {
                    ANIMATION_DATA.CurrentRunningAbilities[d] += 1;
                } 
                else
                {
                    ANIMATION_DATA.CurrentRunningAbilities.Add(d, 1);
                }
            }
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            foreach (CharacterAbility d in ListAbilityData)
            {
                d.UpdateAbility(this, animator, stateInfo);
            }
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            foreach (CharacterAbility d in ListAbilityData)
            {
                d.OnExit(this, animator, stateInfo);

                if (ANIMATION_DATA.CurrentRunningAbilities.ContainsKey(d))
                {
                    ANIMATION_DATA.CurrentRunningAbilities[d] -= 1;

                    if (ANIMATION_DATA.CurrentRunningAbilities[d] <= 0)
                    {
                        ANIMATION_DATA.CurrentRunningAbilities.Remove(d);
                    }
                }
            }
        }
    }
}

