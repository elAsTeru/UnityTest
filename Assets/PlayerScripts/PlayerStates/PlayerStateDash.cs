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
            // ダッシュフラグが折れていたら
            if (!isDash)
            {
                // ダッシュ条件を満たしている
                if (owner.IsDash())
                {
                    // コストを支払ったら
                    owner.stamina -= owner.subStamina;
                    // フラグが立つ
                    isDash = true;
                }
                // 満たしてない
                else
                {
                    owner.ChangeState(move);
                }
            }
            // ダッシュ処理を行う
            owner.MoveProc(owner.inputValue, owner.velocity, owner.maxDashVel, owner.dashACC);
            // ダッシュ攻撃処理を行う
            owner.dashColl.enabled = true;
            // 時間を計測する
            dashTimer += Time.deltaTime;
            // 時間が一回のダッシュの時間を超えていたら
            if(dashTimer > owner.dashTimeOnce)
            {
                // ダッシュ攻撃処理を終了する
                owner.dashColl.enabled = false;
                // フラグを折る
                isDash = false;
            }
        }
    }
}