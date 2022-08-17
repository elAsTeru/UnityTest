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
                //スタミナ減少
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
                        //一回の強制ダッシュ時間を引いておく
                        dashTimer -= owner.dashTimeOnce;
                        //ダッシュフラグを一度折る
                        isDash = false;
                        //もしこの時点でダッシュキーが入力されてなければ通常移動に遷移
                        if(!Input.GetKey(owner.dashKey))
                        {
                            owner.ChangeState(moving);
                        }
                    }
                }

                // ダッシュ処理
                owner.MoveProc(owner.inputValue, owner.velocity, owner.maxDashVel, owner.dashACC);
            }
            //通常の移動状態に遷移
            else
            {
                owner.ChangeState(moving);
            }
        }
    }
}