using UnityEngine;

public partial class Player
{
    public class StateJump : PlayerStateBase
    {
        public override void OnEnter(Player owner, PlayerStateBase prevState)
        {
            if(owner.isGround)
            {
                owner.rb.AddForce(new Vector3(0, owner.jumpPower, 0));
            }
            else
            {
                owner.ChangeMoveState(move);
            }
        }
        public override void OnUpdate(Player owner)
        {
            if(owner.isGround)
            {
                owner.ChangeMoveState(idol);
            }
        }
    }
}
