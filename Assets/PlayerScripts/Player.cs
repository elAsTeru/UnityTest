using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player
{
    // �ړ��X�e�[�g�̃C���X�^���X
    private static readonly StateIdol idol = new StateIdol();
    private static readonly StateMove move = new StateMove();
    private static readonly StateDash dash = new StateDash();
    private static readonly StateJump jump = new StateJump();
    // �U���X�e�[�g�̃C���X�^���X
    private static readonly StateIdolAtk idolAtk = new StateIdolAtk();
    private static readonly StateDashAtk dashAtk = new StateDashAtk();
    private static readonly StateSpinAtk spinAtk = new StateSpinAtk();

    [Tooltip("�ړ��n�̃X�e�[�g")][SerializeField] PlayerStateBase moveState = idol;
    [Tooltip("�U���n�̃X�e�[�g")][SerializeField] PlayerStateBase atkState = idolAtk;
    
    [SerializeField] string nowMoveState = idol.ToString();   // ���݂̃X�e�[�g�����\�� // �f�o�b�O�p
    [SerializeField] string nowAtkState = idol.ToString();   // ���݂̃X�e�[�g�����\�� // �f�o�b�O�p

    // Start()�����s
    private void OnStart()
    {
        moveState.OnEnter(this, null);
        atkState.OnEnter(this, null);
    }

    // Update()�����s
    private void OnUpdate()
    {
        moveState.OnUpdate(this);
        atkState.OnUpdate(this);
    }

    private void ChangeMoveState(PlayerStateBase _NextState)
    {
        moveState.OnExit(this, _NextState);
        _NextState.OnEnter(this, moveState);
        moveState = _NextState;

        //���\���̌��݂̃X�e�[�g��ύX
        nowMoveState = moveState.ToString();
    }

    private void ChangeAtkState(PlayerStateBase _NextState)
    {
        atkState.OnExit(this, _NextState);
        _NextState.OnEnter(this, atkState);
        atkState = _NextState;

        //���\���̌��݂̃X�e�[�g��ύX
        nowAtkState = atkState.ToString();
    }
}
