using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player
{
    public class StateDash : PlayerStateBase
    {
        bool isDash;
        float dashTimer;

        public override void OnEnter(Player owner, PlayerStateBase prevState)
        {
            isDash = false;
            dashTimer = 0;
        }

        public override void OnUpdate(Player owner)
        {
            // �_�b�V���t���O���܂�Ă�����
            if (!isDash)
            {
                // �_�b�V�������𖞂����Ă���
                if (Input.GetKey(owner.dashKey) && owner.isGround && owner.stamina > owner.subStamina)
                {
                    // �R�X�g���x��������
                    owner.stamina -= owner.subStamina;
                    // �t���O������
                    isDash = true;
                    // �����蔻���L����
                    owner.dashColl.enabled = true;
                }
                // �������ĂȂ�
                else if(!Input.GetKey(owner.dashKey) || !owner.isGround || owner.stamina <= owner.subStamina)
                {
                    // �_�b�V���U���������I������
                    owner.dashColl.enabled = false;
                    owner.ChangeState(move);
                }
            }
            // �_�b�V���������s��
            owner.MoveProc(owner.inputValue, owner.velocity, owner.maxDashVel, owner.dashACC);
            // �ړ���������������
            owner.FaceFront();
            // ���Ԃ��v������
            dashTimer += Time.deltaTime;
            // ���Ԃ����̃_�b�V���̎��Ԃ𒴂��Ă�����
            if(dashTimer > owner.dashTimeOnce)
            {
                // �t���O��܂�
                isDash = false;
            }
        }
    }
}