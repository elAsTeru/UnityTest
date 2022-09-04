// 状態に合った処理を行うスクリプト
// プレイヤーそれぞれにアタッチして使用する。
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ◆可能であればしたいこと
// ・キーの設定をスクリプトで行う
// InputManagerで設定しなくてもよくすることで、
// 毎回設定する手間を省ける、分からない人が設定に時間をかけなくてよくなる

public class PlayerProc : MonoBehaviour
{
    [Header("アタッチ設定")]
    [Tooltip("PlayerMgrC#")][SerializeField] PlayerMgr playerMgrCS;
    [Tooltip("PlayerStateC#")][SerializeField]PlayerState playerStateCS;
    [Tooltip("物理")][SerializeField] private Rigidbody ownRB;
    [Tooltip("ダッシュ当たり判定")][SerializeField] private Collider dashColl;
    [Tooltip("しっぽ当たり判定")][SerializeField] private Collider spinColl;

    [Header("状態情報 : PlayerStateから共有")]
    [Tooltip("移動状態")][SerializeField]PlayerState.move_state moveState;
    [Tooltip("攻撃状態")][SerializeField]PlayerState.attack_state atkState;
    [Tooltip("その他状態")][SerializeField] PlayerState.other_state otherState;

    [Header("移動処理情報")]
    [Tooltip("移動量")][SerializeField] Vector2 inputValue;
    [Tooltip("最後の移動量")][SerializeField] Vector2 lastInputValue;

    [Header("移動設定")]
    [Tooltip("移動速度(読み専用)")][SerializeField] private float velocity = 0.0f;
    [Tooltip("移動時の最大速度")][SerializeField] private float maxMoveVel = 5.0f;
    [Tooltip("移動時の加速度")][SerializeField] private float moveACC = 5.0f;

    [Header("ジャンプ設定")]
    [Tooltip("ジャンプ力")][SerializeField] private float jumpPower = 400.0f;

    [Header("ダッシュ攻撃設定")]
    [Tooltip("ダッシュ時の加速度")][SerializeField] private float DashACC = 10.0f;
    [Tooltip("ダッシュ時の最大速度")][SerializeField] private float maxDashVel = 10.0f;

    void Update()
    {

        // その他処理・毎回行うもの
        {
            FaceFront();
        }
    }


    /// <summary>
    /// 移動方向を向く処理
    /// </summary>
    void FaceFront()
    {
        if (ownRB.velocity != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(ownRB.velocity);
            Debug.DrawRay(this.transform.position, ownRB.velocity / 2, Color.blue);    // 線が長くなるので半分の長さで
        }
    }
}