using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SamuraiGame
{
    [CreateAssetMenu(fileName = "New tooltips", menuName = "SamuraiDream/Tooltips/DummyTooltips")]
    public class DummyTooltips : ScriptableObject
    {
        public List<string> DummyTooltipsList = new List<string>();
    }
}