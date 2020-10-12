using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace SamuraiGame
{
    public class PlayerSpawn : MonoBehaviour
    {
        public CharacterSelect characterSelect;
        private string objName;

        IEnumerator Start()
        {
            switch (characterSelect.SelectedCharacterType)
            {
                case PlayableCharacterType.YELLOW:
                    {
                        objName = "yBot_Yellow";
                    }
                    break;
                case PlayableCharacterType.BLUE:
                    {
                        objName = "yBot_Blue";
                    }
                    break;
                case PlayableCharacterType.GREEN:
                    {
                        objName = "yBot_Green";
                    }
                    break;
            }

            GameObject obj = Instantiate(Resources.Load(objName, typeof(GameObject))) as GameObject;

            obj.transform.position = this.transform.position;            
            GetComponent<MeshRenderer>().enabled = false;

            CharacterController control = CharacterManager.Instance.GetCharacter(characterSelect.SelectedCharacterType);
            Collider target = control.RAGDOLL_DATA.GetBody("Spine1");

            yield return new WaitForEndOfFrame();

            CinemachineVirtualCamera[] arr = GameObject.FindObjectsOfType<CinemachineVirtualCamera>();

            foreach (CinemachineVirtualCamera v in arr)
            {                
                v.LookAt = target.transform;
                v.Follow = target.transform;
            }
        }   
    } 
}   
