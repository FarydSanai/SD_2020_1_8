using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SamuraiGame
{
    [System.Serializable]
    public class CollisionSphereData
    {
        public GameObject[] FrontSpheres;
        public GameObject[] BottomSpheres;
        public GameObject[] BackSpheres;
        public GameObject[] UpSpheres;

        public OverlapChecker[] FrontOverlapCheckers;
        public OverlapChecker[] AllOverlapCheckers;

        public delegate bool ReturnBool(OverlapChecker checker);
        public ReturnBool FrontOverlapCheckerContains;

        public delegate void DoSomething();
        public DoSomething Reposition_BottomSpheres;
        public DoSomething Reposition_FrontSpheres;
        public DoSomething Reposition_BackSpheres;
        public DoSomething Reposition_TopSpheres;
    }
}