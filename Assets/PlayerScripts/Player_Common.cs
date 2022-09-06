using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player : MonoBehaviour
{
    [Tooltip("�v���C���[�Ǘ�")][SerializeField] PlayerMgr mgr;
    [Tooltip("���")][SerializeField] ScriptablePlayer data;
    [Tooltip("�v���C�ԍ�")][SerializeField] short playerNumber = 1;
    [SerializeField] KeyCode dashKey = KeyCode.LeftShift;
    [SerializeField] KeyCode jumpKey = KeyCode.Space;
    [SerializeField] KeyCode SpinKey = KeyCode.Return;
    [Tooltip("�ڒn���苗��")][SerializeField] float groundJudgDist;

    //�ݒ荀��
    Rigidbody rb;           // ����
    float     maxStamina;   // �ő�̗�
    [SerializeField]float     stamina;      // �̗�
    float     subStamina;   // �̗͌�����
    float     dashTimeOnce; // ���̃_�b�V���ŋ����I�Ƀ_�b�V���ɂȂ鎞��
    float     maxNormalVel; // �ʏ�ړ��̍ō����x
    float     normalACC;    // �ʏ�ړ��̉����x
    float     maxDashVel;   // �_�b�V���̍ō����x
    float     dashACC;      // �_�b�V���̉����x
    float     jumpPower;    // �W�����v��
    float     spinTimeOnce; // 1��̉�]�U���ɂ����鎞��
    public ScriptablePlayer GetPlayerData() { return data; }


    //�����X�V����
    Vector2   inputValue;   // XY�̓��͗�
    float     velocity;     // �ړ����x
    bool      isGround;     // �ڒn����
    [SerializeField]float     timeCounter;  // ���Ԍv���p�^�C�}�[

    private void Awake()
    {
        //���ݒ肪�����Awake�Ō�Ƀf�o�b�O�I��
        bool exitFlag = false;

        if (!(rb = GetComponent<Rigidbody>()))
        {
            Debug.LogError("���������R���|�[�l���g�����ݒ�");
            exitFlag = true;
        }

        //�^�O�ݒ�m�F
        //�^�O�͐ݒ肳��Ă��邩�H
        if (tag == "Untagged")
        {
            Debug.LogError(this.gameObject.name + "�I�u�W�F�N�g�̃^�O���ݒ肳��Ă��܂���B");
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

    public void PlayerStart()
    {
        mgr = GameObject.Find("PlayerMgr").GetComponent<PlayerMgr>();
        rb = GetComponent<Rigidbody>();
        stamina      = maxStamina =data.Stamina;
        subStamina   = data.StaminaSubValue;
        dashTimeOnce = data.DashTimeOnce;
        maxNormalVel = data.MaxNormalVelocity;
        normalACC    = data.NormalAcceleration;
        maxDashVel   = data.MaxDashVelocity;
        dashACC      = data.DashAcceleration;
        jumpPower    = data.JumpPower;
        spinTimeOnce = data.SpinTimeOnce;
        // �����蔻��𖳌���
        dashColl.enabled = false;
        spinColl.enabled = false;
        OnStart();      // �v���C���[�̊J�n
    }

    public void PlayerUpdate()
    {
        CheckIsGround();// �ڒn�Ǘ�
        CheckXYInput(); // �X�e�B�b�N��ړ��L�[�̓��͍X�V
        OnUpdate();     // �v���C���[�̍X�V
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