using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SamuraiGame
{
    [System.Serializable]
    public class LedgeGrabData
    {
        public bool isGrabbingledge;

        public delegate void DoSomething();

        public DoSomething LedgeCollidersOff;
    }
}