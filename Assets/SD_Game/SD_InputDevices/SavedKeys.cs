using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SamuraiGame
{
    [CreateAssetMenu(fileName = "Settings", menuName = "SamuraiDream/Settings/SavedKeys")]
    public class SavedKeys : ScriptableObject
    {
        public List<KeyCode> keyCodesList = new List<KeyCode>();
    }
}
