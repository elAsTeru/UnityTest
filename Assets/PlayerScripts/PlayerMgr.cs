// �v���C���[���Ǘ�����X�N���v�g
// �v���C���[�Ƃ͕ʂɊǗ��I�u�W�F�N�g�ɃA�^�b�`���Ďg�p����B
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMgr : MonoBehaviour
{
    [Tooltip("�v���C���[���")][SerializeField] List<GameObject> players;
    [Tooltip("�^�C�}�[")][SerializeField] float timer;

    [SerializeField]float addForceY = 1000.0f;
    [SerializeField]float addForceXZ = 1000.0f;


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