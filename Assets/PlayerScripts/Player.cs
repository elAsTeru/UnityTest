using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player
{
    // ���C���X�e�[�g�̃C���X�^���X
    private static readonly StateIdol idol = new StateIdol();
    private static readonly StateMove move = new StateMove();
    private static readonly StateDash dash = new StateDash();
    // ���̑��X�e�[�g�̃C���X�^���X
    private static readonly StateSpin spin = new StateSpin();
    private static readonly StateJump jump = new StateJump();

    [Tooltip("�ړ��n�̃X�e�[�g")][SerializeField] PlayerStateBase state  = idol;
    
    [SerializeField] string nowState = idol.ToString();   // ���݂̃X�e�[�g�����\�� // �f�o�b�O�p
    
    // Start()�����s
    public void OnStart()
    {
        state.OnEnter(this, null);
    }

    // Update()�����s
    private void OnUpdate()
    {
        if (Input.GetKeyDown(jumpKey) && isGround && stamina > subStamina)
        {
            ChangeState(jump);
        }
        else if(Input.GetKeyDown(SpinKey))
        {
            ChangeState(spin);
        }
        else
        {
            state.OnUpdate(this);
        }
        HealStamina();
    }

    private void ChangeState(PlayerStateBase _NextState)
    {
        state.OnExit(this, _NextState);
        _NextState.OnEnter(this, state);
        state = _NextState;

        //���\���̌��݂̃X�e�[�g��ύX
        nowState = state.ToString();
    }

    private void HealStamina()
    {
        if(stamina < maxStamina && state != dash && isGround)
        {
            stamina += 0.1f;
            // ����𒴂��Ă�����Ƃɖ߂�
            if(stamina > maxStamina)
            {
                stamina = maxStamina;
            }
        }
    }

    // �ړ���������������֐�//Face...����
    private void FaceFront()
    {
        if(rb.velocity != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(rb.velocity);
            Debug.DrawRay(this.transform.position, rb.velocity / 2, Color.blue);
        }
    }
}
