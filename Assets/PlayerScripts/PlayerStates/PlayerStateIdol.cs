using UnityEngine;

public partial class Player
{
    /// <summary>
    /// �ҋ@���
    /// 
    /// ���̏�Ԃ���̑J�ڐ��
    /// �ړ����
    /// </summary>
    public class StateIdol : PlayerStateBase
    {
        public override void OnUpdate(Player owner)
        {
			// �ړ���ԂɑJ��
            if(owner.inputValue.x != 0 || owner.inputValue.y != 0)
            {
				owner.ChangeState(move);
            }
        }
    }
}