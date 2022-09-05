using UnityEngine;

public partial class Player
{
    /// <summary>
    /// ‘Ò‹@ó‘Ô
    /// 
    /// ‚±‚Ìó‘Ô‚©‚ç‚Ì‘JˆÚæ‚Í
    /// ˆÚ“®ó‘Ô
    /// </summary>
    public class StateIdol : PlayerStateBase
    {
        public override void OnUpdate(Player owner)
        {
			// ˆÚ“®ó‘Ô‚É‘JˆÚ
            if(owner.inputValue.x != 0 || owner.inputValue.y != 0)
            {
				owner.ChangeState(move);
            }
        }
    }
}