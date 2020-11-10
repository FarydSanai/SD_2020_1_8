using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEditorInternal;
using UnityEngine;

namespace SamuraiGame
{
    public class MoveColumns : MonoBehaviour
    {
        private Transform[] columns;
        private void Start()
        {
            columns = this.GetComponentsInChildren<Transform>();
        }
    }
}