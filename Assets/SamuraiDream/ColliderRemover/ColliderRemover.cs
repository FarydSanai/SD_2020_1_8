using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SamuraiGame
{
    public class ColliderRemover : MonoBehaviour
    {
        public void RemoveAllColliders()
        {
            Collider[] arr = this.gameObject.GetComponentsInChildren<Collider>();

            foreach (Collider c in arr)
            {
                DestroyImmediate(c);
            }
        }

    }
}