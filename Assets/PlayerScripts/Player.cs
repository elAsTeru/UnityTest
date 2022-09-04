using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player
{
    // 移動ステートのインスタンス
    private static readonly StateIdol idol = new StateIdol();
    private static readonly StateMove move = new StateMove();
    private static readonly StateDash dash = new StateDash();
    private static readonly StateJump jump = new StateJump();
    // 攻撃ステートのインスタンス
    private static readonly StateIdolAtk idolAtk = new StateIdolAtk();
    private static readonly StateDashAtk dashAtk = new StateDashAtk();
    private static readonly StateSpinAtk spinAtk = new StateSpinAtk();

    [Tooltip("移動系のステート")][SerializeField] PlayerStateBase moveState = idol;
    [Tooltip("攻撃系のステート")][SerializeField] PlayerStateBase atkState = idolAtk;
    
    [SerializeField] string nowMoveState = idol.ToString();   // 現在のステートを仮表示 // デバッグ用
    [SerializeField] string nowAtkState = idol.ToString();   // 現在のステートを仮表示 // デバッグ用

    // Start()より実行
    private void OnStart()
    {
        moveState.OnEnter(this, null);
        atkState.OnEnter(this, null);
    }

    // Update()より実行
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

        //仮表示の現在のステートを変更
        nowMoveState = moveState.ToString();
    }

    private void ChangeAtkState(PlayerStateBase _NextState)
    {
        atkState.OnExit(this, _NextState);
        _NextState.OnEnter(this, atkState);
        atkState = _NextState;

        //仮表示の現在のステートを変更
        nowAtkState = atkState.ToString();
    }
}
