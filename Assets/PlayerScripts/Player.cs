using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player
{
    // �X�e�[�g�̃C���X�^���X
    private static readonly StateIdoling idoling     = new StateIdoling();
    private static readonly StateNormalMoving moving = new StateNormalMoving();
    private static readonly StateDashing dashing     = new StateDashing();

    [Tooltip("���݂̃X�e�[�g")][SerializeField] PlayerStateBase state = idoling;
    [SerializeField] string nowState = idoling.ToString();   // ���݂̃X�e�[�g�����\��

    // Start()�����s
    private void OnStart()
    {
        state.OnEnter(this, null);
    }

    // Update()�����s
    private void OnUpdate()
    {
        state.OnUpdate(this);
    }

    private void ChangeState(PlayerStateBase _NextState)
    {
        state.OnExit(this, _NextState);
        _NextState.OnEnter(this, state);
        state = _NextState;

        //���\���̌��݂̃X�e�[�g��ύX
        nowState = state.ToString();
    }
}
