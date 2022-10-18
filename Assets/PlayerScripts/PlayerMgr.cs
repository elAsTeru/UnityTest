// �v���C���[���Ǘ�����X�N���v�g
// �v���C���[�Ƃ͕ʂɊǗ��I�u�W�F�N�g�ɃA�^�b�`���Ďg�p����B
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMgr : MonoBehaviour
{
    [Tooltip("�v���C���[��")][SerializeField] const int player_num = 2;
    [Tooltip("�v���C���[���")][SerializeField] List<GameObject> players;
    [Tooltip("�v���C���[��C#")][SerializeField] List<Player> playerCSs;

    [SerializeField] float stopTime = 0.1f;     // �U���������������ɒ�~���鎞��
    [Tooltip("�^�C�}�[")] float stopTimer;      // �U���������������Ɏg�p����^�C�}�[
    GameObject hitter;

    public enum AtkState
    {
        Dash,
        Spin
    }
    public AtkState atkState;
    Vector3 atkForce;                           // �֐����Ă΂ꂽ�Ƃ��ɔ�΂��͂�����

    // �_�ŏ���
    float blinkTime = 1.0f;         // �_�Ŏ���
    float blinkInterval = 0.1f;     // �_�ŊԊu
    float blinkNextTime = 0;        // ���ɓ_�ł��鎞��
    float blinkTimer = 0;           // �_�Ŏ��Ɏg�p����^�C�}�[
    [SerializeField] float disableTime = 0.1f;  // �U���������ł��鎞��

    private void Awake()
    {
        //���ݒ肪�����Awake�Ō�Ƀf�o�b�O�I��
        bool exitFlag = false;

        for (int i = 0; i < player_num; ++i)
        {
            if (!players[i] || players[i].tag != "Player")
            {
                Debug.LogError("�v���C���[��z��ɃZ�b�g�ł��ĂȂ����A�v���C���[�Ƀ^�O�����Ă��܂���B");
                exitFlag = true;
            }
            if(!playerCSs[i])
            {
                Debug.LogError("�v���C���[��C#���Z�b�g�ł��Ă܂���B");
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
        // �v���C���[�̊J�n
        for (int i = 0; i < player_num; ++i)
        {
            playerCSs[i].PlayerStart();
        }
    }
    private void Update()
    {
        if (stopTimer == 0)
        {
            // �v���C���[�̍X�V
            for (int i = 0; i < player_num; ++i)
            {
                playerCSs[i].PlayerUpdate();
            }

            // �_�ŏ���
            if (blinkTimer != 0)
            {
                // �_�ł��邩���肵��
                if (blinkTimer <= blinkNextTime)
                {
                    // ���ɓ_�ł��鎞�Ԃ�ݒ肵��
                    blinkNextTime -= blinkInterval;
                    if (hitter.GetComponent<MeshRenderer>().enabled)
                    {
                        hitter.GetComponent<MeshRenderer>().enabled = false;
                    }
                    else
                    {
                        hitter.GetComponent<MeshRenderer>().enabled = true;
                    }
                }
                // ���Ԃ��o�߂����Ă���
                blinkTimer -= Time.deltaTime;
                // ���Ԃ��z������
                if (blinkTimer <= 0)
                {
                    // �_�ł��I��
                    blinkTimer = 0;
                    hitter.GetComponent<MeshRenderer>().enabled = true;
                }
            }
        }
        else
        {
            stopTimer -= Time.deltaTime;
            // �v���C���[�̓������~����
            for (int i = 0; i < player_num; ++i)
            {
                players[i].GetComponent<Rigidbody>().isKinematic = true;
            }
            // ��~���Ԃ��߂�����0�ɂ��Ă����邱�ƂŁA�v���C���[�̍X�V�ɖ߂�
            if (stopTimer <= 0.0f)
            {
                stopTimer = 0;
                // �v���C���[�̓������ĊJ����
                for (int i = 0; i < player_num; ++i)
                {
                    players[i].GetComponent<Rigidbody>().isKinematic = false;
                }
                // �U�����󂯂��I�u�W�F�N�g�ɍ쐬�����͂�������
                hitter.GetComponent<Rigidbody>().AddForce(atkForce);
            }
        }
    }

    // �U������
    // �U������
    public void AttackProc(GameObject _Attacker, GameObject _Hitter, AtkState _AtkState)
    {
        // ��΂��Ƃ��̏����̂��߂ɋL�^���Ă���
        hitter = _Hitter;
        atkState = _AtkState;

        // -----�U�����󂯂��I�u�W�F�N�g�̌�����ς��鏈��-----
        Vector3 dir = _Hitter.transform.position - _Attacker.transform.position;
        _Hitter.transform.rotation = Quaternion.Euler(dir);

        /// -----�U�����󂯂��I�u�W�F�N�g����ԗ͂����߂鏈��-----
        Vector3 force;
        // xz�𐳋K��
        force.y = 0;
        force = dir.normalized;
        // ��΂��͂𐳋K�������x�N�g���ɏ�Z
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
        // �͂��L�^���Ă���
        atkForce = force;
        // ��~�^�C�}�[���Z�b�g����
        stopTimer = stopTime;
        // �_�Ń^�C�}�[���Z�b�g
        blinkTimer = blinkTime;
        // ���̓_�Ŏ��Ԃ��Z�b�g
        blinkNextTime = blinkTime;

        ////�U�������������瓖���������̃X�^�~�i�����炷
        //GameObject StaminaDown = GameObject.Find("PlayerStamina");
        //StaminaDown.GetComponent<playerStamina>().StaminaDown(10.0f, hitter.GetComponent<Player>().GetPlayerNumber());
        ////�U��������������w�C�g���グ��
        //GameObject HateUp = GameObject.Find("player01hate");
        //HateUp.GetComponent<player01hate>().HateUp(10, hitter.GetComponent<Player>().GetPlayerNumber());
    }

    // �Q�[���I�u�W�F�N�g�̃I�C���[�p���擾����֐�
    public Vector3 GetEuler(GameObject _Objct)
    {
        Quaternion qua = _Objct.transform.rotation;
        return qua.eulerAngles;
    }

    // �U���𖳌����ł��鎞�Ԃ�0.1�b�ȂǂŐݒ肵��
    // �ŏ��̍U����J�E���g�J�n���āA���̍U�������̊ԂɂȂ����
    // �܂��U���������Ė��������������Ƃ�
    // ��x�v���C���[�̓������~���Ă���
    // ���̂ӂ��Ƃт◼�����������鏈���Ȃǂ��ꂼ�ꍇ�����������s��
}