using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player
{
    // ステートのインスタンス
    private static readonly StateIdoling idoling     = new StateIdoling();
    private static readonly StateNormalMoving moving = new StateNormalMoving();
    private static readonly StateDashing dashing     = new StateDashing();

    [Tooltip("現在のステート")][SerializeField] PlayerStateBase state = idoling;
    [SerializeField] string nowState = idoling.ToString();   // 現在のステートを仮表示

    // Start()より実行
    private void OnStart()
    {
        state.OnEnter(this, null);
    }

    // Update()より実行
    private void OnUpdate()
    {
        state.OnUpdate(this);
    }

    private void ChangeState(PlayerStateBase _NextState)
    {
        state.OnExit(this, _NextState);
        _NextState.OnEnter(this, state);
        state = _NextState;

        //仮表示の現在のステートを変更
        nowState = state.ToString();
    }
}
