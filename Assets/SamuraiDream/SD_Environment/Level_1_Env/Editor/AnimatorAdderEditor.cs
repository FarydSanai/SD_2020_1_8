using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace SamuraiGame
{
    [CustomEditor(typeof(AnimatorAdder))]
    public class AnimatorAdderEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            if (GUILayout.Button("Add animators"))
            {
                AnimatorAdder an = target as AnimatorAdder;

                an.AddAnimator();
            }
            if (GUILayout.Button("Add RuntimeAnimatorController"))
            {
                AnimatorAdder an = target as AnimatorAdder;
                an.AddAnimatorController();
            }
            if (GUILayout.Button("Remove animators"))
            {
                AnimatorAdder an = target as AnimatorAdder;
                an.RemoveAnimators();
            }
        }
    }
}