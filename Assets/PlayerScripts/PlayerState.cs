// 状態に関する条件を書いているスクリプト
// プレイヤーそれぞれにアタッチして使用する。
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public enum move_state
    {
        Idol,             //アイドル
        Move,             //移動
        Dash,             //ダッシュ
    }
    public enum attack_state
    {
        None,
        Dash,            //ダッシュ攻撃
        Spin,            //回転攻撃
    }
    public enum other_state
    {
        None,
        Jump            //ジャンプ
    }

    [SerializeField]ScriptablePlayer playerData;

    [Header("状態情報")]
    [Tooltip("移動状態")][SerializeField] move_state moveState;
    [Tooltip("攻撃状態")][SerializeField] attack_state atkState;
    [Tooltip("その他状態")][SerializeField] other_state otherState;

    [Header("ステータス設定")]
    [Tooltip("プレイヤー番号")][SerializeField] short playerNumber = 1;

    float maxStamina;

    [Tooltip("スタミナ減少量")][SerializeField] float subStamina = 0.5f;
    [Tooltip("スタミナ回復量")][SerializeField] float recovStamina = 2.0f;
    [Header("ステータス情報")]
    [Tooltip("スタミナ")][SerializeField] float stamina;

    [Header("情報")]
    [Tooltip("タイマー")][SerializeField] float timer;
    [Tooltip("物理")][SerializeField] Rigidbody rb;

    [Header("移動設定")]
    [Tooltip("ダッシュキー")][SerializeField] KeyCode dashKey = KeyCode.LeftShift;

    float dashTimeOnce = 0.5f;

    [Header("移動情報")]
    [Tooltip("ダッシュフラグ")][SerializeField] bool isDash;
    [Tooltip("移動量")][SerializeField] Vector2 inputValue;

    [Header("攻撃設定")]
    [Tooltip("攻撃キー")][SerializeField] KeyCode atkButton = KeyCode.Mouse0;
    [Tooltip("回転攻撃時間")][SerializeField] float spinTime = 0.5f;
    [Header("攻撃情報")]
    [Tooltip("回転フラグ")][SerializeField] bool isSpin;

    [Header("ジャンプ関連設定")]
    [Tooltip("ジャンプキー")][SerializeField] KeyCode jumpKey = KeyCode.Space;
    [Tooltip("接地距離")][SerializeField] float distance = 1.0f;
    [Header("ジャンプ関連情報")]
    [Tooltip("接地情報")][SerializeField] bool isGround;
    [Tooltip("ジャンプフラグ")][SerializeField] bool isJump;

    // Getter /////////////////////////////////////////////////
    public move_state GetMoveState() { return moveState; }
    public attack_state GetAtkState() { return atkState; }
    public other_state GetOtherState() { return otherState; }
    public Vector2 GetInputValue() { return inputValue; }
    public bool IsGround() { return isGround; }
    ///////////////////////////////////////////////////////////

    private void Awake()
    {
        //未設定があればAwake最後にデバッグ終了
        bool exitFlag = false;

        if(!(rb = GetComponent<Rigidbody>()))
        {
            Debug.LogError("物理挙動コンポーネントが未設定");
            exitFlag = true;
        }

        //タグ設定確認
        //タグは設定されているか？
        if (tag == "Untagged")
        {
            Debug.LogError(this.gameObject.name + "オブジェクトのタグが設定されていません。");
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

    private void Start()
    {
        maxStamina = playerData.Stamina;
        dashTimeOnce = playerData.DashTimeOnce;
    }

    private void Init()
    {
        stamina = maxStamina;
    }

    public void StateUpdate()
    { 
        //CheckOtherState();
    }

    void CheckMoveState()
    {
        //毎回処理
        {
            //スタミナ回復
            {
                if (stamina < maxStamina && moveState!=move_state.Dash && isGround && !Input.GetKey(dashKey))
                {
                    stamina += recovStamina;
                    //上限超えたら矯正
                    if(stamina > maxStamina)
                    {
                        stamina = maxStamina;
                    }
                }
            }
        }
    }
}