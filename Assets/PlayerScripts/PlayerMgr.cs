// プレイヤーを管理するスクリプト
// プレイヤーとは別に管理オブジェクトにアタッチして使用する。
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMgr : MonoBehaviour
{
    [Tooltip("プレイヤー情報")][SerializeField] List<GameObject> players;
    [Tooltip("タイマー")][SerializeField] float timer;

    [SerializeField]float addForceY = 1000.0f;
    [SerializeField]float addForceXZ = 1000.0f;


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