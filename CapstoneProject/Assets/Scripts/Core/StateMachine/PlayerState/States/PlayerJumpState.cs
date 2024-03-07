using System.Collections;
using System.Collections.Generic;
using Core.Entity;
using UnityEngine;

namespace Core.StateMachine
{
    public class PlayerJumpState : PlayerAirState
    {
        public PlayerJumpState(Player inputPlayer, string inputAnimName) : base(inputPlayer, inputAnimName)
        {
        }
        public override void StateBegin()
        {
            base.StateBegin();
            Player.rb.SetVelocity(Player.rb.velocity.x, Player.GetJumpForce());
           // Object.FindObjectOfType<SoundManager> ().Play("PlayerJump"); Example of using the sound manager to play a jump sound when the player jumps.
           // Uncomment above line if the sound manager is in the scene, and has a sound named "Player Jump" Attached.
        }

        public override void StateUpdate()
        {
            base.StateUpdate();

            if (Player.rb.velocity.y < 0)
                Player.States.stateMachine.ChangeState(Player.States.airState);
        }

        public override void StateEnd()
        {
            base.StateEnd();
        }
    }
}

