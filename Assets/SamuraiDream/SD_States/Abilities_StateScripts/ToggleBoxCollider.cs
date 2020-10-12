using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SamuraiGame
{
    [CreateAssetMenu(fileName = "New state", menuName = "SamuraiDream/AbilityData/ToggleBoxCollider")]
    public class ToggleBoxCollider : StateData
    {
        public bool OnStart;
        public bool On;
        public bool OnEnd;
        [Space(10)]
        public bool RepositionSpheres;
        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (OnStart)
            {
                ToggleBoxCol(characterState.characterControl);
            }
        }
        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }
        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (OnEnd)
            {
                ToggleBoxCol(characterState.characterControl);
            }
        }
        private void ToggleBoxCol(CharacterController control)
        {
            control.RIGID_BODY.velocity = Vector3.zero;
            control.GetComponent<BoxCollider>().enabled = On;

            if (RepositionSpheres)
            {
                control.COLLISION_DATA.Reposition_BottomSpheres();
                control.COLLISION_DATA.Reposition_FrontSpheres();
                control.COLLISION_DATA.Reposition_BackSpheres();
                control.COLLISION_DATA.Reposition_TopSpheres();
            }
        }
    }
}

