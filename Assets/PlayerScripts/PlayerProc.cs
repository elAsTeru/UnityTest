// ��Ԃɍ������������s���X�N���v�g
// �v���C���[���ꂼ��ɃA�^�b�`���Ďg�p����B
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���\�ł���΂���������
// �E�L�[�̐ݒ���X�N���v�g�ōs��
// InputManager�Őݒ肵�Ȃ��Ă��悭���邱�ƂŁA
// ����ݒ肷���Ԃ��Ȃ���A������Ȃ��l���ݒ�Ɏ��Ԃ������Ȃ��Ă悭�Ȃ�

public class PlayerProc : MonoBehaviour
{
    [Header("�A�^�b�`�ݒ�")]
    [Tooltip("PlayerMgrC#")][SerializeField] PlayerMgr playerMgrCS;
    [Tooltip("PlayerStateC#")][SerializeField]PlayerState playerStateCS;
    [Tooltip("����")][SerializeField] private Rigidbody ownRB;
    [Tooltip("�_�b�V�������蔻��")][SerializeField] private Collider dashColl;
    [Tooltip("�����ۓ����蔻��")][SerializeField] private Collider spinColl;

    [Header("��ԏ�� : PlayerState���狤�L")]
    [Tooltip("�ړ����")][SerializeField]PlayerState.move_state moveState;
    [Tooltip("�U�����")][SerializeField]PlayerState.attack_state atkState;
    [Tooltip("���̑����")][SerializeField] PlayerState.other_state otherState;

    [Header("�ړ��������")]
    [Tooltip("�ړ���")][SerializeField] Vector2 inputValue;
    [Tooltip("�Ō�̈ړ���")][SerializeField] Vector2 lastInputValue;

    [Header("�ړ��ݒ�")]
    [Tooltip("�ړ����x(�ǂݐ�p)")][SerializeField] private float velocity = 0.0f;
    [Tooltip("�ړ����̍ő呬�x")][SerializeField] private float maxMoveVel = 5.0f;
    [Tooltip("�ړ����̉����x")][SerializeField] private float moveACC = 5.0f;

    [Header("�W�����v�ݒ�")]
    [Tooltip("�W�����v��")][SerializeField] private float jumpPower = 400.0f;

    [Header("�_�b�V���U���ݒ�")]
    [Tooltip("�_�b�V�����̉����x")][SerializeField] private float DashACC = 10.0f;
    [Tooltip("�_�b�V�����̍ő呬�x")][SerializeField] private float maxDashVel = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        bool exitFlag = false;

        if (!(playerMgrCS = GameObject.Find("PlayerMgr").GetComponent<PlayerMgr>()))
        {
            Debug.LogError("�v���C���[���Ǘ�����X�N���v�g�����ݒ�");
            exitFlag = true;
        }

        if (!(playerStateCS = GetComponent<PlayerState>()))
        {
            Debug.LogError("�v���C���[�̏�Ԃ����߂�X�N���v�g�����ݒ�");
            exitFlag = true;
        }

        if(!(ownRB = GetComponent<Rigidbody>()))
        {
            Debug.LogError("�����R���|�[�l���g�����ݒ�");
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

    // ���삪�ǂ��Ȃ��Ă�̂��͖��ň͂܂�Ă镔�������������̂킩��Ǝv���܂��B
    // ������������������������������������������������������������������������������������������������ //
    // Update is called once per frame
    void Update()
    {
        //��ԍX�V
        playerStateCS.StateUpdate();
        moveState = playerStateCS.GetMoveState();
        atkState = playerStateCS.GetAtkState();
        otherState = playerStateCS.GetOtherState();

        // ��Ԃɂ�鏈��
        if (moveState == PlayerState.move_state.Idol)
        {
        }
        // �ʏ�ړ����̏���
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
        // �_�b�V�����̏���
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
            // �����蔻���L����
            dashColl.enabled = true;
        }

        if(atkState == PlayerState.attack_state.None)
        {
            dashColl.enabled = false;
            spinColl.enabled = false;
        }
        //�ːi�U������
        else if(atkState == PlayerState.attack_state.Dash)
        {
            dashColl.enabled = true;
        }
        //��]�U������
        else if(atkState == PlayerState.attack_state.Spin)
        {
            spinColl.enabled = true;
        }

        // �W�����v���̏���
        if (otherState == PlayerState.other_state.Jump && playerStateCS.IsGround())
        {
            // �W�����v����(������̗͂�^����)
            ownRB.AddForce(new Vector3(0, jumpPower, 0));
        }

        // ���̑������E����s������
        {
            FaceFront();
        }
    }
    // ������������������������������������������������������������������������������������������������ //

    /// <summary>
    /// �ړ�����
    /// </summary>
    /// <param name="_MoveX">        ���E�̓����̊���   </param>
    /// <param name="_MoveZ">        �O��̓����̊���   </param>
    /// <param name="_Velocity">     ���݂̈ړ����x     </param>
    /// <param name="_MaxVelocity">  �ړ����x�̍ő�l   </param>
    /// <param name="_Acceleration"> �����x             </param>
    private void MoveProc(Vector2 _InputValue, float _MaxVelocity, float _Acceleration)
    {
        //�㏑�������O�ɗ����̗͂��L�^
        float forceY = ownRB.velocity.y;
        //���x�𐳋K�������ړ��ʂɂ���
        ownRB.velocity = new Vector3(_InputValue.x, 0, _InputValue.y).normalized;

        //��������Ɖ����x���瑬�x�����߂�
        velocity = Accelerate(velocity, _MaxVelocity, _Acceleration);

        //���x���ړ��ʂɏ�Z����
        ownRB.velocity *= velocity;
        //�����̗͂𕜋A������
        ownRB.velocity = new Vector3(ownRB.velocity.x, forceY, ownRB.velocity.z);
    }

    /// <summary>
    /// ����������
    /// </summary>
    /// <param name="_Velocity">���݂̑��x</param>
    /// <param name="_MaxVelocity">�����̖ڕW���x</param>
    /// <param name="_Acceleration">�����x</param>
    /// <returns></returns>
    private float Accelerate(float _Velocity, float _MaxVelocity, float _Acceleration)
    {
        //�ړ����x�̏���𒴂��ĂȂ���Ώ���܂ŉ���
        if (_Velocity < _MaxVelocity)
        {
            //���Ԑϕ����瑬�x�����߂�
            _Velocity += _Acceleration * Time.deltaTime;
            //������ɏ���𒴂����狸������
            if(_Velocity > _MaxVelocity)
            {
                _Velocity = _MaxVelocity;
            }
        }
        //�����łȂ���Ώ���܂Ō���
        else if(_Velocity > _MaxVelocity)
        {
            _Velocity -= _Acceleration * Time.deltaTime;
            //������ɏ������������狸������
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
            //�N���N�ɂǂ�ȍU���������̂���`����
            playerMgrCS.TellAttack(this.gameObject, other.gameObject, atkState);
        }
    }

    /// <summary>
    /// �ړ���������������
    /// </summary>
    void FaceFront()
    {
        if (ownRB.velocity != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(ownRB.velocity);
            Debug.DrawRay(this.transform.position, ownRB.velocity / 2, Color.blue);    // ���������Ȃ�̂Ŕ����̒�����
        }
    }
}