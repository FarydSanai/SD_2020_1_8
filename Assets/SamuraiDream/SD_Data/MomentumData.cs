using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SamuraiGame
{
    [System.Serializable]
    public class MomentumData
    {
        public float Momentum;

        public delegate void DoSomething(float speed, float maxMomentum);
        public DoSomething CalculateMomentum;
    }
}