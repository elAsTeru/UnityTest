using UnityEngine;

public partial class Player
{
    /// <summary>
    /// �ړ����
    /// 
    /// ���̏�Ԃ���̑J�ڐ��
    /// �ҋ@���
    /// ������
    /// </summary>
    public class StateMove : PlayerStateBase
    {
        public override void OnUpdate(Player owner)
        {
            // �ړ�����
            owner.MoveProc(owner.inputValue, owner.velocity, owner.maxNormalVel, owner.normalACC);
            // �ړ���������������
            owner.FaceFront();

            // �ҋ@��ԂɑJ��
            if (owner.inputValue.x == 0 && owner.inputValue.y == 0)
            {
                owner.ChangeState(idol);
            }
            // �_�b�V����ԂɑJ��
            else if (Input.GetKeyDown(owner.dashKey) && owner.isGround && owner.stamina > owner.subStamina)
            {
                owner.ChangeState(dash);
            }
        }
    }

    /// <summary>
    /// �ړ�����
    /// </summary>
    /// <param name="_MoveX">        ���E�̓����̊���   </param>
    /// <param name="_MoveZ">        �O��̓����̊���   </param>
    /// <param name="_Velocity">     ���݂̈ړ����x     </param>
    /// <param name="_MaxVelocity">  �ړ����x�̍ő�l   </param>
    /// <param name="_Acceleration"> �����x             </param>
    private void MoveProc(Vector2 _InputValue, float _Velocity, float _MaxVelocity, float _Acceleration)
    {
        //�㏑�������O�ɗ����̗͂��L�^
        float forceY = rb.velocity.y;
        //���x�𐳋K�������ړ��ʂɂ���
        rb.velocity = new Vector3(_InputValue.x, 0, _InputValue.y).normalized;

        //��������Ɖ����x���瑬�x�����߂�
        velocity = Accelerate(_Velocity, _MaxVelocity, _Acceleration);
        //����velocity�͈������g���ƈړ����S�R�ł��Ȃ��Ȃ�B�𖾂ł����璼��

        //���x���ړ��ʂɏ�Z����
        rb.velocity *= _Velocity;
        //�����̗͂𕜋A������
        rb.velocity = new Vector3(rb.velocity.x, forceY, rb.velocity.z);
    }

    /// <summary>
    /// ����������
    /// </summary>
    /// <param name="_Velocity">���݂̑��x</param>
    /// <param name="_MaxVelocity">�����̖ڕW���x</param>
    /// <param name="_Acceleration">�����x</param>
    /// <returns></returns>
    private float Accelerate(float _Velocity, float _MaxVelocity, float _Acceleration)
    {
        //�ړ����x�̏���𒴂��ĂȂ���Ώ���܂ŉ���
        if (_Velocity < _MaxVelocity)
        {
            //���Ԑϕ����瑬�x�����߂�
            _Velocity += _Acceleration * Time.deltaTime;
            //������ɏ���𒴂����狸������
            if (_Velocity > _MaxVelocity)
            {
                _Velocity = _MaxVelocity;
            }
        }
        //�����łȂ���Ώ���܂Ō���
        else if (_Velocity > _MaxVelocity)
        {
            _Velocity -= _Acceleration * Time.deltaTime;
            //������ɏ������������狸������
            if (_Velocity < _MaxVelocity)
            {
                _Velocity = _MaxVelocity;
            }
        }
        return _Velocity;
    }
}