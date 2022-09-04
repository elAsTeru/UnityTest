// ��Ԃɍ������������s���X�N���v�g
// �v���C���[���ꂼ��ɃA�^�b�`���Ďg�p����B
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���\�ł���΂���������
// �E�L�[�̐ݒ���X�N���v�g�ōs��
// InputManager�Őݒ肵�Ȃ��Ă��悭���邱�ƂŁA
// ����ݒ肷���Ԃ��Ȃ���A������Ȃ��l���ݒ�Ɏ��Ԃ������Ȃ��Ă悭�Ȃ�

public class PlayerProc : MonoBehaviour
{
    [Header("�A�^�b�`�ݒ�")]
    [Tooltip("PlayerMgrC#")][SerializeField] PlayerMgr playerMgrCS;
    [Tooltip("PlayerStateC#")][SerializeField]PlayerState playerStateCS;
    [Tooltip("����")][SerializeField] private Rigidbody ownRB;
    [Tooltip("�_�b�V�������蔻��")][SerializeField] private Collider dashColl;
    [Tooltip("�����ۓ����蔻��")][SerializeField] private Collider spinColl;

    [Header("��ԏ�� : PlayerState���狤�L")]
    [Tooltip("�ړ����")][SerializeField]PlayerState.move_state moveState;
    [Tooltip("�U�����")][SerializeField]PlayerState.attack_state atkState;
    [Tooltip("���̑����")][SerializeField] PlayerState.other_state otherState;

    [Header("�ړ��������")]
    [Tooltip("�ړ���")][SerializeField] Vector2 inputValue;
    [Tooltip("�Ō�̈ړ���")][SerializeField] Vector2 lastInputValue;

    [Header("�ړ��ݒ�")]
    [Tooltip("�ړ����x(�ǂݐ�p)")][SerializeField] private float velocity = 0.0f;
    [Tooltip("�ړ����̍ő呬�x")][SerializeField] private float maxMoveVel = 5.0f;
    [Tooltip("�ړ����̉����x")][SerializeField] private float moveACC = 5.0f;

    [Header("�W�����v�ݒ�")]
    [Tooltip("�W�����v��")][SerializeField] private float jumpPower = 400.0f;

    [Header("�_�b�V���U���ݒ�")]
    [Tooltip("�_�b�V�����̉����x")][SerializeField] private float DashACC = 10.0f;
    [Tooltip("�_�b�V�����̍ő呬�x")][SerializeField] private float maxDashVel = 10.0f;

    void Update()
    {

        // ���̑������E����s������
        {
            FaceFront();
        }
    }


    /// <summary>
    /// �ړ���������������
    /// </summary>
    void FaceFront()
    {
        if (ownRB.velocity != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(ownRB.velocity);
            Debug.DrawRay(this.transform.position, ownRB.velocity / 2, Color.blue);    // ���������Ȃ�̂Ŕ����̒�����
        }
    }
}