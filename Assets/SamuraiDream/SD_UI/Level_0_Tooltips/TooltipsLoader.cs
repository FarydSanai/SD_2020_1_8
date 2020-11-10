using JetBrains.Annotations;
using System.CodeDom;
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
        ATTACK_SYSTEM,
    }
    public class TooltipsLoader : MonoBehaviour
    {
        public TooltipsType tooltipsType;
        public GameObject UI_Text;

        public delegate bool CharacterCurrentState(CharacterController control);
        public List<CharacterCurrentState> CharacterCurrentStatesList = new List<CharacterCurrentState>();
        public List<string> TooltipsList = new List<string>();

        private TextMeshProUGUI TooltipText;
        private CharacterController control;
        private ManualInput controlManualInput;
        private Coroutine TooltipRoutine;

        private MovingTooltips movingTooltips;
        private AttackTooltips attackTooltips;
        public DummyTooltips DummyTooltips;

        private void Start()
        {
            //Init fields
            TooltipText = UI_Text.GetComponent<TextMeshProUGUI>();
            control = CharacterManager.Instance.GetPlayableCharacter();
            controlManualInput = control.GetComponentInChildren<ManualInput>();

            //Subscribe to events
            GameEventsManager.Instance.showTooltip += ShowTooltip;
            GameEventsManager.Instance.showDummyTooltip += ShowDummyTooltip;

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
            else if (tooltipsType == TooltipsType.ATTACK_SYSTEM)
            {
                attackTooltips = this.GetComponentInChildren<AttackTooltips>();

                TooltipsList.AddRange(attackTooltips.AttackTooltipsList);
                CharacterCurrentStatesList.AddRange(attackTooltips.AttackStates);
            }

            //Start tooltips coroutine
            if (TooltipRoutine != null)
            {
                StopCoroutine(TooltipRoutine);
            }
            if (TooltipRoutine == null)
            {
                TooltipRoutine = StartCoroutine(_NextTooltip());
            }
        }
        IEnumerator _NextTooltip()
        {
            for (int i = 0; i < TooltipsList.Count; i++)
            {
                GameEventsManager.Instance.ShowTooltipEvent(i);

                yield return new WaitUntil(() => CharacterCurrentStatesList[i](control));

                yield return new WaitForSeconds(0.2f);

                TogglePlayerInput(control, false);

                GameEventsManager.Instance.ShowDummyTooltipEvent();

                yield return new WaitForSeconds(1.5f);

                TogglePlayerInput(control, true);
            }
            //TogglePlayerInput(control, true);
        }
        public void ShowTooltip(int index)
        {
            TooltipText.text = TooltipsList[index];
        }
        public void ShowDummyTooltip()
        {
            int rand = Random.Range(0, DummyTooltips.DummyTooltipsList.Count);
            if (DummyTooltips.DummyTooltipsList.Count > 0)
            {
                TooltipText.text = DummyTooltips.DummyTooltipsList[rand];
            }
        }
        public void HideTooltip()
        {
            TooltipText.text = string.Empty;
        }
        private void TogglePlayerInput(CharacterController control, bool inputEnable)
        {
            if (!inputEnable)
            {
                VirtualInputManager.Instance.playerInput.enabled = false;
                control.MoveLeft = false;
                control.MoveRight = false;
                control.Turbo = false;
                control.Jump = false;
            } else
            {
                VirtualInputManager.Instance.playerInput.enabled = true;
            }

        }
    }
}