using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SamuraiGame
{
    public class PlayerGround : SubComponent
    {
        public GroundData groundData;
        void Start()
        {
            groundData = new GroundData
            {
                Ground = null,
            };

            subComponentProcessor.groundData = groundData;
        }
        public override void OnFixedUpdate()
        {

        }
        public override void OnUpdate()
        {

        }
    }
}