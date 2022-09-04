using UnityEngine;

public partial class Player
{
    public class StateIdolAtk : PlayerStateBase
    {
        public override void OnUpdate(Player owner)
        {
            if(Input.GetKeyDown(owner.SpinKey))
            {
                owner.ChangeAtkState(spinAtk);
            }
            else if(owner.moveState == dash)
            {
                owner.ChangeAtkState(dashAtk);
            }
        }
    }
}
