using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player : MonoBehaviour
{
    [Tooltip("���")][SerializeField] ScriptablePlayer data;
    [Tooltip("�v���C�ԍ�")][SerializeField] short playerNumber = 1;
    [SerializeField] KeyCode dashKey = KeyCode.LeftShift;
    [SerializeField] KeyCode jumpKey = KeyCode.Space;
    [Tooltip("�ڒn���苗��")][SerializeField] float groundJudgDist;

    //�ݒ荀��
    Rigidbody rb;           // ����
    float     maxStamina;   // �ő�̗�
    float     stamina;      // �̗�
    float     subStamina;   // �̗͌�����
    float     dashTimeOnce; // ���̃_�b�V���ŋ����I�Ƀ_�b�V���ɂȂ鎞��
    float     maxNormalVel; // �ʏ�ړ��̍ō����x
    float     normalACC;    // �ʏ�ړ��̉����x
    float     maxDashVel;   // �_�b�V���̍ō����x
    float     dashACC;      // �_�b�V���̉����x


    //�����X�V����
    Vector2   inputValue;   // XY�̓��͗�
    float     velocity;     // �ړ����x
    bool      isGround;     // �ڒn����

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
    /// XY�̓��͗ʂ��擾����
    /// </summary>
    private void CheckXYInput()
    {
        inputValue.x = Input.GetAxis("Horizontal" + playerNumber);
        inputValue.y = Input.GetAxis("Vertical" + playerNumber);
    }

    /// <summary>
    /// �ڒn������擾
    /// </summary>
    private void CheckIsGround()
    {
        Vector3 rayPos = transform.position + new Vector3(0, 0, 0);
        Ray ray = new Ray(rayPos, Vector3.down);
        isGround = Physics.Raycast(ray, groundJudgDist);
    }
}