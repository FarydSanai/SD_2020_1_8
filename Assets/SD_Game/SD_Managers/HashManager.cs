using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SamuraiGame
{
    public enum Ledge_Trigger_States
    {
        Fall,
        RunningJump_Fall,
        Running_Jump,
        Jump_Normal,
        WallSlide,
        WallJump,

        COUNT,
    }
    public enum GatesTransitionParams
    {
        character_nearby,

        COUNT,
    }
    public class HashManager : Singleton<HashManager>
    {
        public Dictionary<TransitionParameter, int> DicMainParams = new Dictionary<TransitionParameter, int>();
        public Dictionary<CameraTrigger, int> DicCameraTriggers = new Dictionary<CameraTrigger, int>();
        public Dictionary<AI_Walk_Transitions, int> DicAIParams = new Dictionary<AI_Walk_Transitions, int>();

        public int[] ArrLedgeTriggerStates = new int[(int)Ledge_Trigger_States.COUNT];

        public int[] ArrGatesTransitionParams = new int[(int)GatesTransitionParams.COUNT];

        private void Awake()
        {
            //Animation transitions
            TransitionParameter[] arrParams = System.Enum.GetValues(typeof(TransitionParameter)) as TransitionParameter[];

            foreach (TransitionParameter t in arrParams)
            {
                DicMainParams.Add(t, Animator.StringToHash(t.ToString()));
            }

            //Camera parameters
            CameraTrigger[] arrCamTrans = System.Enum.GetValues(typeof(CameraTrigger)) as CameraTrigger[];

            foreach (CameraTrigger ct in arrCamTrans)
            {
                DicCameraTriggers.Add(ct, Animator.StringToHash(ct.ToString()));
            }

            //AI Transitions
            AI_Walk_Transitions[] arrAITrans = System.Enum.GetValues(typeof(AI_Walk_Transitions)) as AI_Walk_Transitions[];

            foreach (AI_Walk_Transitions wt in arrAITrans)
            {
                DicAIParams.Add(wt, Animator.StringToHash(wt.ToString()));
            }

            //Ledge trigger states
            for (int i = 0; i < ArrLedgeTriggerStates.Length; i++)
            {
                ArrLedgeTriggerStates[i] = Animator.StringToHash(((Ledge_Trigger_States)i).ToString());
            }

            //Gates triggerStates
            for (int i = 0; i < ArrGatesTransitionParams.Length; i++)
            {
                ArrGatesTransitionParams[i] = Animator.StringToHash(((GatesTransitionParams)i).ToString());
            }
        }
    }
}
