using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace SamuraiGame 
{
    [CustomEditor(typeof(ColliderRemover))]
    public class ColliderRemoverEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            if (GUILayout.Button("Remove all colliders"))
            {
                ColliderRemover rem = target as ColliderRemover;
                rem.RemoveAllColliders();
            }
        }
    }
}