using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SamuraiGame
{
    public class PlayGame : MonoBehaviour
    {
        public CharacterSelect characterSelect;
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                if (characterSelect.SelectedCharacterType != PlayableCharacterType.NONE)
                {
                    UnityEngine.SceneManagement.SceneManager.LoadScene(SDScenes.SamuraiScene_1.ToString());
                }
                else
                {
                    Debug.Log("Must select character first!");
                }                
            } 
        }
    }
}
