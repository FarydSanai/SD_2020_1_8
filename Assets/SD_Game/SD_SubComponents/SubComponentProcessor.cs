﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SamuraiGame
{
    public class SubComponentProcessor : MonoBehaviour
    {
        //public Dictionary<SubComponentType, SubComponent> ComponentsDic = new Dictionary<SubComponentType, SubComponent>();
        public SubComponent[] ArrSubComponents;

        public CharacterController control;

        [Space(10)] public BlockingObjData blockingData;
        [Space(10)] public LedgeGrabData ledgeGrabData;
        [Space(10)] public RagdollData ragdollData;
        [Space(10)] public ManualInputData manualInputData;
        [Space(10)] public BoxColliderData boxColliderData;
        [Space(10)] public VerticalVelocityData verticalVelocityData;
        [Space(10)] public DamageData damageData;
        [Space(10)] public MomentumData momentumData;
        [Space(10)] public RotationData rotationData;
        [Space(10)] public JumpData jumpData;
        [Space(10)] public CollisionSphereData collisionSphereData;
        [Space(10)] public InstaKillData instaKillData;
        [Space(10)] public GroundData groundData;
        [Space(10)] public AttackData attackData;
        [Space(10)] public AnimationData animationData;
        private void Awake()
        {
            ArrSubComponents = new SubComponent[(int)SubComponentType.COUNT];
            control = GetComponentInParent<CharacterController>();
        }
        public void FixedUpdateSubComponents()
        {
            FixedUpdateSubComponent(SubComponentType.COLLISION_SPHERES);
            FixedUpdateSubComponent(SubComponentType.RAGDOLL);
            FixedUpdateSubComponent(SubComponentType.LEDGECHECKER);
            FixedUpdateSubComponent(SubComponentType.BLOCKINGOBJECTS);
            FixedUpdateSubComponent(SubComponentType.BOXCOLLIDER_UPDATER);
            FixedUpdateSubComponent(SubComponentType.VERTICAL_VELOCITY);
            FixedUpdateSubComponent(SubComponentType.INSTAKILL);
            FixedUpdateSubComponent(SubComponentType.DAMAGE_DETECTOR);
        }
        public void UpdateSubComponents()
        {
            UpdateSubComponent(SubComponentType.MANUALINPUT);
            UpdateSubComponent(SubComponentType.PLAYER_ANIMATION);
            UpdateSubComponent(SubComponentType.PLAYER_ATTACK);
        }

        private void UpdateSubComponent(SubComponentType type)
        {
            if (ArrSubComponents[(int)type] != null)
            {
                ArrSubComponents[(int)type].OnUpdate();
            }
        }
        private void FixedUpdateSubComponent(SubComponentType type)
        {
            if (ArrSubComponents[(int)type] != null)
            {
                ArrSubComponents[(int)type].OnFixedUpdate();
            }
        }
    }
}