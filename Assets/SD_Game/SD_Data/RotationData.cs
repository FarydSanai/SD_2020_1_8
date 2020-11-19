using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEngine;

namespace SamuraiGame
{
    [System.Serializable]
    public class RotationData
    {
        public bool LockEarlyTurn;
        public bool LockDirectionNextState;

        public delegate bool ReturnBool();
        public ReturnBool EarlyTurnIsLocked;
        public ReturnBool IsFacingForward;

        public delegate void DoSomething(bool faceForward);
        public DoSomething FaceForward;
    }
}