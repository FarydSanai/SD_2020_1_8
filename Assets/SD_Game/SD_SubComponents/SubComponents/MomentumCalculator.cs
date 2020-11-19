using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SamuraiGame
{
    public class MomentumCalculator : SubComponent
    {
        public MomentumData momentumData;
        private void Start()
        {
            momentumData = new MomentumData
            {
                Momentum = 0f,
                CalculateMomentum = CalculateMomentum,
            };

            subComponentProcessor.momentumData = momentumData; 
        }
        public override void OnFixedUpdate()
        {

        }
        public override void OnUpdate()
        {

        }
        private void CalculateMomentum(float speed, float maxMomentum)
        {
            //current air momentum
            if (!control.BLOCKING_DATA.RightSideBlocked())
            {
                if (control.MoveRight)
                {
                    momentumData.Momentum += speed;
                }
            }

            if (!control.BLOCKING_DATA.LeftSideBlocked())
            {
                if (control.MoveLeft)
                {
                    momentumData.Momentum -= speed;
                }
            }

            if (control.BLOCKING_DATA.RightSideBlocked() || control.BLOCKING_DATA.LeftSideBlocked())
            {
                float lerped = Mathf.Lerp(momentumData.Momentum, 0f, Time.deltaTime * 1.5f);
                momentumData.Momentum = lerped;
            }

            if (Mathf.Abs(momentumData.Momentum) >= maxMomentum)
            {
                if (momentumData.Momentum > 0f)
                {
                    momentumData.Momentum = maxMomentum;
                }
                else if (momentumData.Momentum < 0f)
                {
                    momentumData.Momentum = -maxMomentum;
                }
            }
        }
    }
}