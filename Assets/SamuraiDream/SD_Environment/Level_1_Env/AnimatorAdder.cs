using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace SamuraiGame
{
    public class AnimatorAdder : MonoBehaviour
    {
        public RuntimeAnimatorController colController;
        public void AddAnimator()
        {
            Transform[] objs = this.gameObject.GetComponentsInChildren<Transform>();

            foreach (Transform obj in objs)
            {
                if (obj.GetComponent<Animator>() == null)
                {
                    obj.gameObject.AddComponent<Animator>();
                }
            }
        }
        public void AddAnimatorController()
        {
            Animator[] animators = this.gameObject.GetComponentsInChildren<Animator>();
            foreach (Animator a in animators)
            {
                if (a.runtimeAnimatorController == null)
                {
                    a.runtimeAnimatorController = colController;
                }
            }
        }
        public void RemoveAnimators()
        {
            Animator[] animators = this.gameObject.GetComponentsInChildren<Animator>();
            foreach (Animator a in animators)
            {
                DestroyImmediate(a);
            }
        }
    }
}