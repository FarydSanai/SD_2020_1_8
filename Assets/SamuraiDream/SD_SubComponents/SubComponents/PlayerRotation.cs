using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SamuraiGame
{
    public class PlayerRotation : SubComponent
    {
        public RotationData rotationData;
        private void Start()
        {
            rotationData = new RotationData
            {
                LockEarlyTurn = false,
                LockDirectionNextState = false,
                EarlyTurnIsLocked = EarlyTurnIsLocked,
                IsFacingForward = IsFacingForward,
                FaceForward = FaceForward,
            };
            subComponentProcessor.rotationData = rotationData;
        }
        public override void OnFixedUpdate()
        {
            throw new System.NotImplementedException();
        }
        public override void OnUpdate()
        {
            throw new System.NotImplementedException();
        }
        private bool EarlyTurnIsLocked()
        {
            if (rotationData.LockEarlyTurn || rotationData.LockDirectionNextState)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private void FaceForward(bool forward)
        {
            //if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name.Equals(SDScenes.TutorialScene_CharacterSelect.ToString()))
            //{
            //    return;
            //}
            if (!control.SkinnedMeshAnimator.enabled)
            {
                return;
            }
            if (forward)
            {
                control.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            }
            else
            {
                control.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            }
        }
        private bool IsFacingForward()
        {
            if (control.transform.forward.z > 0f)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}