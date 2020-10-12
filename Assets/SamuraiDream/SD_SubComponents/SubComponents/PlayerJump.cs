﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SamuraiGame
{
    public class PlayerJump : SubComponent
    {
        public JumpData jumpData;
        private void Start()
        {
            jumpData = new JumpData
            {
                Jumped = false,
                CanWallJump = false,
                CheckWallBlock = false,
                
            };
            subComponentProcessor.jumpData = jumpData;
        }
        public override void OnFixedUpdate()
        {
            throw new System.NotImplementedException();
        }
        public override void OnUpdate()
        {
            throw new System.NotImplementedException();
        }
    }
}