using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player : MonoBehaviour
{
    [Tooltip("情報")][SerializeField] ScriptablePlayer data;
    [Tooltip("プレイ番号")][SerializeField] short playerNumber = 1;
    [SerializeField] KeyCode dashKey = KeyCode.LeftShift;
    [SerializeField] KeyCode jumpKey = KeyCode.Space;
    [Tooltip("接地判定距離")][SerializeField] float groundJudgDist;

    //設定項目
    Rigidbody rb;           // 物理
    float     maxStamina;   // 最大体力
    float     stamina;      // 体力
    float     subStamina;   // 体力減少量
    float     dashTimeOnce; // 一回のダッシュで強制的にダッシュになる時間
    float     maxNormalVel; // 通常移動の最高速度
    float     normalACC;    // 通常移動の加速度
    float     maxDashVel;   // ダッシュの最高速度
    float     dashACC;      // ダッシュの加速度


    //処理更新項目
    Vector2   inputValue;   // XYの入力量
    float     velocity;     // 移動速度
    bool      isGround;     // 接地判定

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        stamina      = maxStamina =data.Stamina;
        subStamina   = data.StaminaSubValue;
        dashTimeOnce = data.DashTimeOnce;
        maxNormalVel = data.MaxNormalVelocity;
        normalACC    = data.NormalAcceleration;
        maxDashVel   = data.MaxDashVelocity;
        dashACC      = data.DashAcceleration;
        OnStart();
    }

    private void Update()
    {
        CheckIsGround();
        CheckXYInput();
        OnUpdate();
    }

    /// <summary>
    /// XYの入力量を取得する
    /// </summary>
    private void CheckXYInput()
    {
        inputValue.x = Input.GetAxis("Horizontal" + playerNumber);
        inputValue.y = Input.GetAxis("Vertical" + playerNumber);
    }

    /// <summary>
    /// 接地判定を取得
    /// </summary>
    private void CheckIsGround()
    {
        Vector3 rayPos = transform.position + new Vector3(0, 0, 0);
        Ray ray = new Ray(rayPos, Vector3.down);
        isGround = Physics.Raycast(ray, groundJudgDist);
    }
}