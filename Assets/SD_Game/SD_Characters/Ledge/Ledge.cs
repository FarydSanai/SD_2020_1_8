using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

namespace SamuraiGame
{
    public class Ledge : MonoBehaviour
    {
        public Vector3 Offset;

        //public static bool IsLedge(GameObject obj)
        //{
        //    if (obj.GetComponent<Ledge>() == null)
        //    {
        //        return false;
        //    }
        //    return true;
        //}
        public static bool IsLedgeChecker(GameObject obj)
        {
            if (obj.GetComponent<LedgeCollider>() == null)
            {
                return false;
            }
            return true;
        }
        public static bool IsCharacter(GameObject obj)
        {
            if (obj.transform.root.GetComponent<CharacterController>() != null)
            {
                return true;
            }
            return false;
        }
    }
}