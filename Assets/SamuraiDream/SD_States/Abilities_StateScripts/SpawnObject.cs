using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SamuraiGame
{
    [CreateAssetMenu(fileName = "New state", menuName = "SamuraiDream/ObjectData/SpawnObject")]
    public class SpawnObject : StateData
    {
        public bool StickToParent;
        public PoolObjectType ObjectType;
        [Range(0f, 1f)]
        public float SpawnTiming;
        public string ParentObjName = string.Empty;

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (SpawnTiming == 0)
            {
                //CharacterController control = characterState.GetCharacterController(animator);
                SpawnObj(characterState.characterControl);
            }
        }
        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (!characterState.characterControl.animationProgress.PoolObjectTypes.Contains(ObjectType))
            {
                if (stateInfo.normalizedTime >= SpawnTiming)
                {
                    SpawnObj(characterState.characterControl);
                }
            }
        }
        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (characterState.characterControl.animationProgress.PoolObjectTypes.Contains(ObjectType))
            {
                characterState.characterControl.animationProgress.PoolObjectTypes.Remove(ObjectType);
            }
        }

        private void SpawnObj(CharacterController control)
        {
            if (control.animationProgress.PoolObjectTypes.Contains(ObjectType))
            {
                return;
            }

            GameObject obj = PoolManager.Instance.GetObject(ObjectType);

            Debug.Log("spawning: " + ObjectType.ToString() + " | loking for: " + ParentObjName);

            if (!string.IsNullOrEmpty(ParentObjName))
            {
                GameObject p = control.GetChildObj(ParentObjName);
                obj.transform.parent = p.transform;
                obj.transform.localPosition = Vector3.zero;
                obj.transform.localRotation = Quaternion.identity;
            }

            if (!StickToParent)
            {
                obj.transform.parent = null;
            }

            obj.SetActive(true);

            control.animationProgress.PoolObjectTypes.Add(ObjectType);
        }
    }
}
