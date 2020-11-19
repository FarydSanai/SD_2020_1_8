using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SamuraiGame
{
    public class ManualInputData
    {
        public delegate bool GetBool();
        public GetBool DoubleTapUp;
        public GetBool DoubleTapDown;
    }
}