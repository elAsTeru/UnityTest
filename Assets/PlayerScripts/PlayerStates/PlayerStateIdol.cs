using UnityEngine;

public partial class Player
{
    /// <summary>
    /// �ʏ���
    /// </summary>
    public class StateIdol : PlayerStateBase
    {
        public override void OnUpdate(Player owner)
        {
			//�ʏ�̈ړ���ԂɑJ��
            if(owner.inputValue.x != 0 || owner.inputValue.y !=0)
            {
				owner.ChangeMoveState(move);
            }
        }
    }
}