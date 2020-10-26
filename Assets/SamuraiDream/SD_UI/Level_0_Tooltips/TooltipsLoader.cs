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

        public Dictionary<string, bool> TooltipsDic = new Dictionary<string, bool>();
        public List<string> TooltipsList = new List<string>();
        private void Start()
        {
            //Init fields
            TooltipText = UI_Text.GetComponent<TextMeshProUGUI>();
            control = CharacterManager.Instance.GetPlayableCharacter();

            //Subscribe to events
            GameEventsManager.Instance.showTooltip += ShowTooltip;

            //Set Tooltips type
            if (tooltipsType == TooltipsType.MOVING_SYSTEM)
            {
                movingTooltips = this.gameObject.GetComponent<MovingTooltips>();

                if (CharacterCurrentStatesList.Count == 0)
                {
                    CharacterCurrentStatesList.AddRange(movingTooltips.MovingStates);
                }
            }

            for (int i = 0; i < TooltipsList.Count; i++)
            {
                TooltipsDic.Add(TooltipsList[i], false);
            }

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

                //TooltipText.text = TooltipsList[i];
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