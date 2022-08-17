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

    // Start is called before the first frame update
    void Start()
    {
        bool exitFlag = false;

        if (!(playerMgrCS = GameObject.Find("PlayerMgr").GetComponent<PlayerMgr>()))
        {
            Debug.LogError("プレイヤーを管理するスクリプトが未設定");
            exitFlag = true;
        }

        if (!(playerStateCS = GetComponent<PlayerState>()))
        {
            Debug.LogError("プレイヤーの状態を決めるスクリプトが未設定");
            exitFlag = true;
        }

        if(!(ownRB = GetComponent<Rigidbody>()))
        {
            Debug.LogError("物理コンポーネントが未設定");
            exitFlag = true;
        }

        if (exitFlag)
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }

    // 動作がどうなってるのかは矢印で囲まれてる部分だけ見たら大体わかると思います。
    // ↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓ //
    // Update is called once per frame
    void Update()
    {
        //状態更新
        playerStateCS.StateUpdate();
        moveState = playerStateCS.GetMoveState();
        atkState = playerStateCS.GetAtkState();
        otherState = playerStateCS.GetOtherState();

        // 状態による処理
        if (moveState == PlayerState.move_state.Idol)
        {
        }
        // 通常移動中の処理
        else if (moveState == PlayerState.move_state.Move)
        {
            inputValue = playerStateCS.GetInputValue();
            if(inputValue.x == 0 && inputValue.y == 0)
            {
                inputValue = lastInputValue;
            }
            else
            {
                lastInputValue = inputValue;
            }

            MoveProc(inputValue, maxMoveVel, moveACC);
        }
        // ダッシュ中の処理
        else if (moveState == PlayerState.move_state.Dash)
        {
            inputValue = playerStateCS.GetInputValue();
            if (inputValue.x == 0 && inputValue.y == 0)
            {
                inputValue = lastInputValue;
            }
            else
            {
                lastInputValue = inputValue;
            }

            MoveProc(inputValue, maxDashVel, DashACC);
            // 当たり判定を有効化
            dashColl.enabled = true;
        }

        if(atkState == PlayerState.attack_state.None)
        {
            dashColl.enabled = false;
            spinColl.enabled = false;
        }
        //突進攻撃処理
        else if(atkState == PlayerState.attack_state.Dash)
        {
            dashColl.enabled = true;
        }
        //回転攻撃処理
        else if(atkState == PlayerState.attack_state.Spin)
        {
            spinColl.enabled = true;
        }

        // ジャンプ中の処理
        if (otherState == PlayerState.other_state.Jump && playerStateCS.IsGround())
        {
            // ジャンプ処理(上方向の力を与える)
            ownRB.AddForce(new Vector3(0, jumpPower, 0));
        }

        // その他処理・毎回行うもの
        {
            FaceFront();
        }
    }
    // ↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑ //

    /// <summary>
    /// 移動処理
    /// </summary>
    /// <param name="_MoveX">        左右の動きの割合   </param>
    /// <param name="_MoveZ">        前後の動きの割合   </param>
    /// <param name="_Velocity">     現在の移動速度     </param>
    /// <param name="_MaxVelocity">  移動速度の最大値   </param>
    /// <param name="_Acceleration"> 加速度             </param>
    private void MoveProc(Vector2 _InputValue, float _MaxVelocity, float _Acceleration)
    {
        //上書きされる前に落下の力を記録
        float forceY = ownRB.velocity.y;
        //速度を正規化した移動量にする
        ownRB.velocity = new Vector3(_InputValue.x, 0, _InputValue.y).normalized;

        //加速上限と加速度から速度を求める
        velocity = Accelerate(velocity, _MaxVelocity, _Acceleration);

        //速度を移動量に乗算する
        ownRB.velocity *= velocity;
        //落下の力を復帰させる
        ownRB.velocity = new Vector3(ownRB.velocity.x, forceY, ownRB.velocity.z);
    }

    /// <summary>
    /// 加減速処理
    /// </summary>
    /// <param name="_Velocity">現在の速度</param>
    /// <param name="_MaxVelocity">加速の目標速度</param>
    /// <param name="_Acceleration">加速度</param>
    /// <returns></returns>
    private float Accelerate(float _Velocity, float _MaxVelocity, float _Acceleration)
    {
        //移動速度の上限を超えてなければ上限まで加速
        if (_Velocity < _MaxVelocity)
        {
            //時間積分から速度を求める
            _Velocity += _Acceleration * Time.deltaTime;
            //加速後に上限を超えたら矯正する
            if(_Velocity > _MaxVelocity)
            {
                _Velocity = _MaxVelocity;
            }
        }
        //そうでなければ上限まで減速
        else if(_Velocity > _MaxVelocity)
        {
            _Velocity -= _Acceleration * Time.deltaTime;
            //減速後に上限を下回ったら矯正する
            if(_Velocity < _MaxVelocity)
            {
                _Velocity = _MaxVelocity;
            }
        }
        return _Velocity;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject != this.gameObject && other.gameObject.tag == "Player")
        {
            //誰が誰にどんな攻撃をしたのかを伝える
            playerMgrCS.TellAttack(this.gameObject, other.gameObject, atkState);
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