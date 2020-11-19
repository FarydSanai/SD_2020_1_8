using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace SamuraiGame
{
    public class ChangeScene : MonoBehaviour
    {
        public string NextScene;

        private void OnTriggerEnter()
        {
            Debug.Log("??");
            ChangeSceneTo(NextScene);
        }
        public void ChangeSceneTo(string sceneName)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
        }
        public static bool IsNextScenePoint(GameObject point)
        {
            if (point.GetComponent<ChangeScene>() == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
