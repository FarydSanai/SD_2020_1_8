using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SamuraiGame
{
    [CreateAssetMenu(fileName = "Settings", menuName = "SamuraiDream/Settings/PhysicsSettings")]
    public class PhysicsSettings : ScriptableObject
    {
        public int DefaultSolverIterations;
        public int DefaultSolverVelocityIterations;

    }
}
