using UnityEngine;

public partial class Player
{
    /// <summary>
    /// ’Êíó‘Ô
    /// </summary>
    public class StateIdol : PlayerStateBase
    {
        public override void OnUpdate(Player owner)
        {
			//’Êí‚ÌˆÚ“®ó‘Ô‚É‘JˆÚ
            if(owner.inputValue.x != 0 || owner.inputValue.y !=0)
            {
				owner.ChangeMoveState(move);
            }
        }
    }
}