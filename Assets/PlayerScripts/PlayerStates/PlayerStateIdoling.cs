using UnityEngine;

public partial class Player
{
    /// <summary>
    /// 通常状態
    /// </summary>
    public class StateIdoling : PlayerStateBase
    {
        public override void OnUpdate(Player owner)
        {
			//通常の移動状態に遷移
            if(owner.inputValue.x != 0 || owner.inputValue.y !=0)
            {
				owner.ChangeState(moving);
            }
        }
    }
}