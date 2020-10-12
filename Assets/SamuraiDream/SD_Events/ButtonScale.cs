﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SamuraiGame
{
    public enum UIParameters
    {
        ScaleUp,

    }
    public class ButtonScale : MonoBehaviour
    {
        GameEventListener listener;
        Dictionary<GameObject, Animator> DicButtons = new Dictionary<GameObject, Animator>();
        private void Awake()
        {
            listener = GetComponent<GameEventListener>();
        }
        public void ScaleUpButton()
        {
            GetButtonAnimator(listener.gameEvent.EventObj).SetBool(UIParameters.ScaleUp.ToString(), true);
        }
        public void ResetScaleButton()
        {
            GetButtonAnimator(listener.gameEvent.EventObj).SetBool(UIParameters.ScaleUp.ToString(), false);
        }
        Animator GetButtonAnimator(GameObject obj)
        {
            if (!DicButtons.ContainsKey(obj))
            {
                Animator animator = obj.GetComponent<Animator>();
                DicButtons.Add(obj, animator);
                return animator;
            } else
            {
                return DicButtons[obj];
            }
        }
    }
}
