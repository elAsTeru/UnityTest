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
    [Tooltip("�^�C�}�[")][SerializeField] float timer;

    [SerializeField] float disableTime = 0.1f;  // �U���������ł��鎞��
    [SerializeField] float stopTime = 0.1f;
    public enum AtkState
    {
        Dash,
        Spin
    }
    public AtkState atkState;

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
        // �v���C���[�̍X�V
        for (int i = 0; i < player_num; ++i)
        {
            playerCSs[i].PlayerUpdate();
        }
    }

    // �U������
    public void AttackProc(GameObject _Attacker, GameObject _Hitter, AtkState atkState)
    {
        // -----�U�����󂯂��I�u�W�F�N�g�̌�����ς��鏈��-----
        Vector3 dir = _Hitter.transform.position - _Attacker.transform.position;
        _Hitter.transform.rotation = Quaternion.Euler(dir);

        /// -----�U�����󂯂��I�u�W�F�N�g����ԏ���-----
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
        //// �U�����󂯂��I�u�W�F�N�g�ɍ쐬�����͂�������
        _Hitter.GetComponent<Rigidbody>().AddForce(force);
    }
    
    // �Q�[���I�u�W�F�N�g�̃I�C���[�p���擾����֐�
    public Vector3 GetEuler(GameObject _Objct)
    {
        Quaternion qua = _Objct.transform.rotation;
        return qua.eulerAngles;
    }

    //�N���N�ɉ��������̂�
    //public void TellAttack(GameObject _Attacker,GameObject _Hitter, PlayerState.attack_state _AtkState)
    //{
    //    Debug.Log(_Attacker + "��" + _Hitter + "��" + _AtkState + "�������B");
    //    //_Attacker�̍U�����̌������擾��(�N�I�[�^�j�I��)
    //    Quaternion qur = _Attacker.transform.rotation;
    //    //�I�C���[�ɕϊ�
    //    Vector3 eul = qur.eulerAngles;
    //    //_Hitter�̌�����Y�ȊO_Attacker�̔��Ό����ɂ���
    //    eul = -1 * eul;
    //    _Hitter.transform.rotation = Quaternion.Euler(eul);
    //    ////xz�̗͂𐳋K��
    //    eul.y = 0;
    //    eul = eul.normalized;
    //    //��΂��͂𐳋K�������x�N�g���ɏ�Z
    //    eul *= addForceXZ;
    //    //�c�����̗͂�ݒ�l�ɂ���
    //    eul.y = addForceY;
    //    //_Hitter���쐬�����x�N�g���ɕ�����AddForce + ������̗͂�������
    //    _Hitter.GetComponent<Rigidbody>().AddRelativeForce(eul);
    //    //�����I��� 
    //}
    ////�`����ꂽ��A�ݒ肳�ꂽ�b��ɏ������s�����ƂŁA�����ɍU�������ꍇ�̏����Ȃǂ��s����
    

    // �U���𖳌����ł��鎞�Ԃ�0.1�b�ȂǂŐݒ肵��
    // �ŏ��̍U����J�E���g�J�n���āA���̍U�������̊ԂɂȂ����
    // �܂��U���������Ė��������������Ƃ�
    // ��x�v���C���[�̓������~���Ă���
    // ���̂ӂ��Ƃт◼�����������鏈���Ȃǂ��ꂼ�ꍇ�����������s��
}