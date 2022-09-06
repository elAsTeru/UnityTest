// プレイヤーを管理するスクリプト
// プレイヤーとは別に管理オブジェクトにアタッチして使用する。
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMgr : MonoBehaviour
{
    [Tooltip("プレイヤー数")][SerializeField] const int player_num = 2;
    [Tooltip("プレイヤー情報")][SerializeField] List<GameObject> players;
    [Tooltip("プレイヤーのC#")][SerializeField] List<Player> playerCSs;
    [Tooltip("タイマー")][SerializeField] float timer;

    [SerializeField] float disableTime = 0.1f;  // 攻撃無効化できる時間
    [SerializeField] float stopTime = 0.1f;
    public enum AtkState
    {
        Dash,
        Spin
    }
    public AtkState atkState;

    private void Awake()
    {
        //未設定があればAwake最後にデバッグ終了
        bool exitFlag = false;

        for (int i = 0; i < player_num; ++i)
        {
            if (!players[i] || players[i].tag != "Player")
            {
                Debug.LogError("プレイヤーを配列にセットできてないか、プレイヤーにタグがついていません。");
                exitFlag = true;
            }
            if(!playerCSs[i])
            {
                Debug.LogError("プレイヤーのC#がセットできてません。");
                exitFlag = true;
            }
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
    private void Start()
    {
        // プレイヤーの開始
        for (int i = 0; i < player_num; ++i)
        {
            playerCSs[i].PlayerStart();
        }
    }
    private void Update()
    {
        // プレイヤーの更新
        for (int i = 0; i < player_num; ++i)
        {
            playerCSs[i].PlayerUpdate();
        }
    }

    // 攻撃処理
    public void AttackProc(GameObject _Attacker, GameObject _Hitter, AtkState atkState)
    {
        // -----攻撃を受けたオブジェクトの向きを変える処理-----
        Vector3 dir = _Hitter.transform.position - _Attacker.transform.position;
        _Hitter.transform.rotation = Quaternion.Euler(dir);

        /// -----攻撃を受けたオブジェクトが飛ぶ処理-----
        Vector3 force;
        // xzを正規化
        force.y = 0;
        force = dir.normalized;
        // 飛ばす力を正規化したベクトルに乗算
        if (atkState == AtkState.Dash)
        {
            force *= _Attacker.GetComponent<Player>().GetPlayerData().DashAtkForce.x;
            force.y = _Attacker.GetComponent<Player>().GetPlayerData().DashAtkForce.y;
        }
        else
        {
            force *= _Attacker.GetComponent<Player>().GetPlayerData().SpinAtkForce.x;
            force.y = _Attacker.GetComponent<Player>().GetPlayerData().SpinAtkForce.y;
        }
        //// 攻撃を受けたオブジェクトに作成した力を加える
        _Hitter.GetComponent<Rigidbody>().AddForce(force);
    }
    
    // ゲームオブジェクトのオイラー角を取得する関数
    public Vector3 GetEuler(GameObject _Objct)
    {
        Quaternion qua = _Objct.transform.rotation;
        return qua.eulerAngles;
    }

    //誰が誰に何をしたのか
    //public void TellAttack(GameObject _Attacker,GameObject _Hitter, PlayerState.attack_state _AtkState)
    //{
    //    Debug.Log(_Attacker + "が" + _Hitter + "に" + _AtkState + "をした。");
    //    //_Attackerの攻撃時の向きを取得し(クオータニオン)
    //    Quaternion qur = _Attacker.transform.rotation;
    //    //オイラーに変換
    //    Vector3 eul = qur.eulerAngles;
    //    //_Hitterの向きをY以外_Attackerの反対向きにする
    //    eul = -1 * eul;
    //    _Hitter.transform.rotation = Quaternion.Euler(eul);
    //    ////xzの力を正規化
    //    eul.y = 0;
    //    eul = eul.normalized;
    //    //飛ばす力を正規化したベクトルに乗算
    //    eul *= addForceXZ;
    //    //縦方向の力を設定値にして
    //    eul.y = addForceY;
    //    //_Hitterを作成したベクトルに方向にAddForce + 上向きの力も加えて
    //    _Hitter.GetComponent<Rigidbody>().AddRelativeForce(eul);
    //    //多分終わり 
    //}
    ////伝えられた後、設定された秒後に処理を行うことで、同時に攻撃した場合の処理などを行える
    

    // 攻撃を無効化できる時間を0.1秒などで設定して
    // 最初の攻撃後カウント開始して、次の攻撃がその間になければ
    // また攻撃があって無効化が入ったとき
    // 一度プレイヤーの動きを停止してから
    // 次のふっとびや両方少し下がる処理などそれぞれ合った処理を行う
}