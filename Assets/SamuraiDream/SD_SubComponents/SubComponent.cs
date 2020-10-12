using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SamuraiGame
{
    public enum SubComponentType
    {
        NONE,
        MANUALINPUT,
        LEDGECHECKER,
        RAGDOLL,
        BLOCKINGOBJECTS,
        BOXCOLLIDER_UPDATER,
        VERTICAL_VELOCITY,
        DAMAGE_DETECTOR,
        MOMENTUM_CALCULATOR,
        COLLISION_SPHERES,
        INSTAKILL,
    }

    public abstract class SubComponent : MonoBehaviour
    {
        protected SubComponentProcessor subComponentProcessor;
        public CharacterController control
        {
            get
            {
                return subComponentProcessor.control;
            }
        } 
        private void Awake()
        {
            subComponentProcessor = GetComponentInParent<SubComponentProcessor>();
        }
        public abstract void OnUpdate();
        public abstract void OnFixedUpdate();
    }
}
