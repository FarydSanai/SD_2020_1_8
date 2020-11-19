using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SamuraiGame
{
    public class MouseControl : MonoBehaviour
    {
        Ray ray;
        RaycastHit hit;

        public PlayableCharacterType selectedCharacterType;
        public CharacterSelect characterSelect;
        //CharacterHoverLight characterHoverLight;
        //CharacterSelectLight characterSelectLight;
        GameObject whiteSelection;
        Animator characterSelectCamAnimator;
        private void Awake()
        {
            characterSelect.SelectedCharacterType = PlayableCharacterType.NONE;
            //characterHoverLight = GameObject.FindObjectOfType<CharacterHoverLight>();
            //characterSelectLight = GameObject.FindObjectOfType<CharacterSelectLight>();

            whiteSelection = GameObject.Find("SelectionCircle");
            whiteSelection.SetActive(false);

            characterSelectCamAnimator = GameObject.Find("CharacterSelectCameraController").GetComponent<Animator>();
        }
        private void Update()
        {
            ray = CameraManager.Instance.MainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                CharacterController control = hit.collider.gameObject.GetComponent<CharacterController>();

                if (control != null)
                {
                    selectedCharacterType = control.playableCharacterType;
                }
                else
                {
                    selectedCharacterType = PlayableCharacterType.NONE;
                }                 
            }
            if (Input.GetMouseButtonDown(0))
            {
                if (selectedCharacterType != PlayableCharacterType.NONE)
                {
                    characterSelect.SelectedCharacterType = selectedCharacterType;
                    //characterSelectLight.transform.position = characterHoverLight.transform.position;
                    CharacterController control = CharacterManager.Instance.GetCharacter(characterSelect.SelectedCharacterType);
                    //characterSelectLight.transform.parent = control.SkinnedMeshAnimator.transform;
                    //characterSelectLight.light.enabled = true;

                    whiteSelection.SetActive(true);
                    whiteSelection.transform.parent = control.SkinnedMeshAnimator.transform;
                    whiteSelection.transform.localPosition = new Vector3(0f, -0.1f, 0f);
                }
                else
                {
                    characterSelect.SelectedCharacterType = PlayableCharacterType.NONE;
                    //characterSelectLight.light.enabled = false;
                    whiteSelection.SetActive(false);
                }
                foreach (CharacterController c in CharacterManager.Instance.Characters)
                {
                    if (c.playableCharacterType == selectedCharacterType)
                    {
                        c.SkinnedMeshAnimator.SetBool(HashManager.Instance.DicMainParams[TransitionParameter.ClickAnimation], true);
                    }
                    else
                    {
                        c.SkinnedMeshAnimator.SetBool(HashManager.Instance.DicMainParams[TransitionParameter.ClickAnimation], false);
                    }
                }
                characterSelectCamAnimator.SetBool(selectedCharacterType.ToString(), true);
            }
        }
    }
}
