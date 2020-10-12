using System.Collections.Generic;
using UnityEngine;

namespace SamuraiGame
{
    [System.Serializable]
    public class RagdollData
    {
        public bool RagdollTriggered;
        public List<Collider> BodyParts;

        public delegate Collider GetCollider(string name);
        public GetCollider GetBody;

        public delegate void DoSomething(bool zeroVelocity);
        public DoSomething AddForceToDamagedPart;
    }
}