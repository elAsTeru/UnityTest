using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player
{
    public class StateDashing : PlayerStateBase
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
            if (owner.stamina > 0)
            {
                //�X�^�~�i����
                owner.stamina -= owner.subStamina;

                if (!isDash)
                {
                    isDash = true;
                    dashTimer = 0;
                }
                else
                {
                    dashTimer += Time.deltaTime;
                    if(dashTimer > owner.dashTimeOnce || owner.stamina <= 0)
                    {
                        //���̋����_�b�V�����Ԃ������Ă���
                        dashTimer -= owner.dashTimeOnce;
                        //�_�b�V���t���O����x�܂�
                        isDash = false;
                        //�������̎��_�Ń_�b�V���L�[�����͂���ĂȂ���Βʏ�ړ��ɑJ��
                        if(!Input.GetKey(owner.dashKey))
                        {
                            owner.ChangeState(moving);
                        }
                    }
                }

                // �_�b�V������
                owner.MoveProc(owner.inputValue, owner.velocity, owner.maxDashVel, owner.dashACC);
            }
            //�ʏ�̈ړ���ԂɑJ��
            else
            {
                owner.ChangeState(moving);
            }
        }
    }
}