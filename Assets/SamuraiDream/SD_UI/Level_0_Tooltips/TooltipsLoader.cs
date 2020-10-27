using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using UnityEngine;

namespace SamuraiGame
{
    public enum TooltipsType
    {
        NONE,
        MOVING_SYSTEM,
    }
    public class TooltipsLoader : MonoBehaviour
    {
        public TooltipsType tooltipsType;
        public GameObject UI_Text;

        TextMeshProUGUI TooltipText;
        CharacterController control;
        Coroutine TooltipRoutine;

        MovingTooltips movingTooltips;

        public delegate bool CharacterCurrentState(CharacterController control);
        public List<CharacterCurrentState> CharacterCurrentStatesList = new List<CharacterCurrentState>();

        public List<string> TooltipsList = new List<string>();
        private void Start()
        {
            //Init fields
            TooltipText = UI_Text.GetComponent<TextMeshProUGUI>();
            control = CharacterManager.Instance.GetPlayableCharacter();

            //Subscribe to events
            GameEventsManager.Instance.showTooltip += ShowTooltip;

            //Clear current tooltips list
            TooltipsList.Clear();
            CharacterCurrentStatesList.Clear();

            //Set Tooltips type
            if (tooltipsType == TooltipsType.MOVING_SYSTEM)
            {
                movingTooltips = this.GetComponentInChildren<MovingTooltips>();

                TooltipsList.AddRange(movingTooltips.MovingTooltipsList);
                CharacterCurrentStatesList.AddRange(movingTooltips.MovingStates);

            }

            //Start tooltips coroutine
            if (TooltipRoutine == null)
            {
                TooltipRoutine = StartCoroutine(_NextTooltip());
            }
        }
        private void Update()
        {
            
        }
        IEnumerator _NextTooltip()
        {
            for (int i = 0; i < TooltipsList.Count; i++)
            {
                yield return new WaitForSeconds(0.5f);

                GameEventsManager.Instance.ShowTooltipEvent(i);

                yield return new WaitUntil(() => CharacterCurrentStatesList[i](control));
            }
        }
        public void ShowTooltip(int index)
        {
            TooltipText.text = TooltipsList[index];
        }
        public void HideTooltip()
        {

        }
    }
}