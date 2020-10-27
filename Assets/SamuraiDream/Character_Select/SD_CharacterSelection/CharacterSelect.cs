using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SamuraiGame
{
    public enum PlayableCharacterType
    {
        NONE,
        YELLOW,
        BLUE,
        DEFAULT,
    }

    [CreateAssetMenu(fileName = "characterSelect", menuName = "SamuraiDream/CharacterSelection/CharacterSelect")]
    public class CharacterSelect : ScriptableObject
    {
        public PlayableCharacterType SelectedCharacterType;
    }
}

