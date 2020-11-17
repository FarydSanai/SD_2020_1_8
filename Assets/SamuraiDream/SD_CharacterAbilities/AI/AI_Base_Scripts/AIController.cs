using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SamuraiGame
{
    public enum AI_TYPE
    {
        NONE,
        WALK_AND_JUMP,
    }
    public class AIController : MonoBehaviour
    {
        public AI_TYPE InitialAI;

        private List<AISubSet> AIList = new List<AISubSet>();
        private Coroutine AIRoutine;
        private Vector3 TargetDir = new Vector3();
        private CharacterController control;

        private void Awake()
        {
            control = this.GetComponentInParent<CharacterController>();
        }
        private void Start()
        {
            InitializeAI();
        }
        public void InitializeAI()
        {
            if (AIList.Count == 0)
            {
                AISubSet[] arr = this.gameObject.GetComponentsInChildren<AISubSet>();

                foreach (AISubSet s in arr)
                {
                    if (!AIList.Contains(s))
                    {
                        AIList.Add(s);
                        s.gameObject.SetActive(false);
                    }
                }
            }
            AIRoutine = StartCoroutine(_Init());
        }
        private void OnEnable()
        {
            if (AIRoutine != null)
            {
                StopCoroutine(AIRoutine);
            }
        }
        private IEnumerator _Init()
        {
            yield return new WaitForEndOfFrame();
            TriggerAI(InitialAI);
        }

        public void TriggerAI(AI_TYPE aiType)
        {
            AISubSet next = null;

            foreach (AISubSet s in AIList)
            {
                s.gameObject.SetActive(false);
                if (s.AIType == aiType)
                {
                    next = s;
                }
            }

            if (next != null)
            {
                next.gameObject.SetActive(true);
            }
        }
        public void WalkStraightToStartSphere()
        {
            TargetDir = control.aiProgress.pathFindingAgent.StartSphere.transform.position -
                        control.transform.position;
            if (TargetDir.z > 0f)
            {
                control.MoveRight = true;
                control.MoveLeft = false;
            }
            else
            {
                control.MoveRight = false;
                control.MoveLeft = true;
            }
        }
        public void WalkStraightToEndSphere()
        {
            TargetDir = control.aiProgress.pathFindingAgent.EndSphere.transform.position -
                        control.transform.position;
            if (TargetDir.z > 0f)
            {
                control.MoveRight = true;
                control.MoveLeft = false;
            }
            else
            {
                control.MoveRight = false;
                control.MoveLeft = true;
            }
        }
    }
}
