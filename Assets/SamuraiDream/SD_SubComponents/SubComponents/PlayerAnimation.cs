using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SamuraiGame
{
    public class PlayerAnimation : SubComponent
    {
        public AnimationData animationData;
        private void Start()
        {
            animationData = new AnimationData
            {
                CurrentRunningAbilities = new Dictionary<CharacterAbility, int>(),
                IsRunning = IsRunning,
            };

            subComponentProcessor.animationData = animationData;
            subComponentProcessor.ComponentsDic.Add(SubComponentType.PLAYER_ANIMATION, this);
        }
        public override void OnFixedUpdate()
        {
            throw new System.NotImplementedException();
        }
        public override void OnUpdate()
        {
            if (IsRunning(typeof(LockTransition)))
            {
                if (control.animationProgress.LockTransition)
                {
                    control.SkinnedMeshAnimator.
                        SetBool(HashManager.Instance.DicMainParams[TransitionParameter.LockTransition],
                                true);
                }
                else
                {
                    control.SkinnedMeshAnimator.
                        SetBool(HashManager.Instance.DicMainParams[TransitionParameter.LockTransition],
                                false);
                }
            }
            else
            {
                if (control.SkinnedMeshAnimator.parameters.Length != 0)
                {
                    control.SkinnedMeshAnimator.
                    SetBool(HashManager.Instance.DicMainParams[TransitionParameter.LockTransition],
                            false);
                }
            }
        }
        private bool IsRunning(System.Type type)
        {
            foreach (KeyValuePair<CharacterAbility, int> data in animationData.CurrentRunningAbilities)
            {
                if (data.Key.GetType() == type)
                {
                    return true;
                }
            }
            return false;
        }
    }
}