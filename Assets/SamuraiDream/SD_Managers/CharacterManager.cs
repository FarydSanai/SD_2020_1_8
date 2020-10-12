using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SamuraiGame
{
    public class CharacterManager : Singleton<CharacterManager>
    {
        public List<CharacterController> Characters = new List<CharacterController>();

        public CharacterController GetCharacter(PlayableCharacterType playableCharacterType)
        {
            foreach (CharacterController control in Characters)
            {
                if (control.playableCharacterType == playableCharacterType)
                {
                    return control;
                }
            }
            return null;
        }

        public CharacterController GetCharacter(Animator animator)
        {
            foreach (CharacterController control in Characters)
            {
                if (control.SkinnedMeshAnimator == animator)
                {
                    return control;
                }
            }
            return null;
        }
        public CharacterController GetCharacter(GameObject obj)
        {
            foreach (CharacterController control in Characters)
            {
                if (control.gameObject == obj)
                {
                    return control;
                }
            }
            return null;
        }
        public CharacterController GetPlayableCharacter()
        {
            foreach (CharacterController control in Characters)
            {
                if (control.subComponentProcessor.ComponentsDic.ContainsKey(SubComponentType.MANUALINPUT))
                {
                    return control;
                }
            }
            return null;
        }
    }
}
