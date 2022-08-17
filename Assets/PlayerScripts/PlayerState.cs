// ��ԂɊւ�������������Ă���X�N���v�g
// �v���C���[���ꂼ��ɃA�^�b�`���Ďg�p����B
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public enum move_state
    {
        Idol,             //�A�C�h��
        Move,             //�ړ�
        Dash,             //�_�b�V��
    }
    public enum attack_state
    {
        None,
        Dash,            //�_�b�V���U��
        Spin,            //��]�U��
    }
    public enum other_state
    {
        None,
        Jump            //�W�����v
    }

    [SerializeField]ScriptablePlayer playerData;

    [Header("��ԏ��")]
    [Tooltip("�ړ����")][SerializeField] move_state moveState;
    [Tooltip("�U�����")][SerializeField] attack_state atkState;
    [Tooltip("���̑����")][SerializeField] other_state otherState;

    [Header("�X�e�[�^�X�ݒ�")]
    [Tooltip("�v���C���[�ԍ�")][SerializeField] short playerNumber = 1;

    float maxStamina;

    [Tooltip("�X�^�~�i������")][SerializeField] float subStamina = 0.5f;
    [Tooltip("�X�^�~�i�񕜗�")][SerializeField] float recovStamina = 2.0f;
    [Header("�X�e�[�^�X���")]
    [Tooltip("�X�^�~�i")][SerializeField] float stamina;

    [Header("���")]
    [Tooltip("�^�C�}�[")][SerializeField] float timer;
    [Tooltip("����")][SerializeField] Rigidbody rb;

    [Header("�ړ��ݒ�")]
    [Tooltip("�_�b�V���L�[")][SerializeField] KeyCode dashKey = KeyCode.LeftShift;

    float dashTimeOnce = 0.5f;

    [Header("�ړ����")]
    [Tooltip("�_�b�V���t���O")][SerializeField] bool isDash;
    [Tooltip("�ړ���")][SerializeField] Vector2 inputValue;

    [Header("�U���ݒ�")]
    [Tooltip("�U���L�[")][SerializeField] KeyCode atkButton = KeyCode.Mouse0;
    [Tooltip("��]�U������")][SerializeField] float spinTime = 0.5f;
    [Header("�U�����")]
    [Tooltip("��]�t���O")][SerializeField] bool isSpin;

    [Header("�W�����v�֘A�ݒ�")]
    [Tooltip("�W�����v�L�[")][SerializeField] KeyCode jumpKey = KeyCode.Space;
    [Tooltip("�ڒn����")][SerializeField] float distance = 1.0f;
    [Header("�W�����v�֘A���")]
    [Tooltip("�ڒn���")][SerializeField] bool isGround;
    [Tooltip("�W�����v�t���O")][SerializeField] bool isJump;

    // Getter /////////////////////////////////////////////////
    public move_state GetMoveState() { return moveState; }
    public attack_state GetAtkState() { return atkState; }
    public other_state GetOtherState() { return otherState; }
    public Vector2 GetInputValue() { return inputValue; }
    public bool IsGround() { return isGround; }
    ///////////////////////////////////////////////////////////

    private void Awake()
    {
        //���ݒ肪�����Awake�Ō�Ƀf�o�b�O�I��
        bool exitFlag = false;

        if(!(rb = GetComponent<Rigidbody>()))
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
        //CheckMoveState();
        CheckAtkState();
        CheckOtherState();
    }

    void CheckMoveState()
    {
        //���񏈗�
        {
            //�X�^�~�i��
            {
                if (stamina < maxStamina && moveState!=move_state.Dash && isGround && !Input.GetKey(dashKey))
                {
                    stamina += recovStamina;
                    //����������狸��
                    if(stamina > maxStamina)
                    {
                        stamina = maxStamina;
                    }
                }
            }
        }
    }

    void CheckAtkState()
    {
        //None
        {
            atkState = attack_state.None;
        }
        //Dash
        {
            if(moveState == move_state.Dash)
            {
                atkState = attack_state.Dash;
            }
        }
        //Spin
        {
            if(Input.GetKey(atkButton) || isSpin)
            {
                atkState = attack_state.Spin;
                if(!isSpin)
                {
                    isSpin = true;
                    timer = 0;
                }
                else
                {
                    timer += Time.deltaTime;
                    if(timer > spinTime)
                    {
                        isSpin = false;
                    }
                }

            }
        }
    }

    void CheckOtherState()
    {
        //None
        {
            otherState = other_state.None;
        }
        //Jump
        {
            Vector3 rayPos = transform.position + new Vector3(0, 0, 0);
            Ray ray = new Ray(rayPos, Vector3.down);
            isGround = Physics.Raycast(ray, distance);

            Debug.DrawRay(rayPos, Vector3.down * distance, Color.red);
            //�ڒn���Ă���/�W�����v���͂��Ă���/�W�����v���Ă��Ȃ� || �W�����v��

            if (isGround && Input.GetKeyDown(jumpKey) && !isJump)
            {
                otherState = other_state.Jump;
                if(!isJump)
                {
                    isJump = true;
                }
            }
            else if(isGround && !Input.GetKeyDown(jumpKey) && isJump)
            {
                isJump = false;
            }
        }
    }
}