using UnityEngine;

// Assets/Create/ScriptableObjects/CreateScriptablePlayer�̍��ڂ�I�����邱�Ƃ�
// Data�Ƃ������O�ŃA�Z�b�g�������assets�t�H���_�ɓ���B
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/CreateScriptablePlayer")]

public class ScriptablePlayer : ScriptableObject
{
    // Variables
    [Tooltip("���O")]               public string Name = "Anonymous";
    [Tooltip("�̗�")]               public float Stamina;
    [Tooltip("�̗͌�����")]         public float StaminaSubValue;

    [Header("�ʏ�ړ�")]
    [Tooltip("�ʏ�ړ��̍ō����x")] public float MaxNormalVelocity;
    [Tooltip("�ʏ�ړ��̉����x")]   public float NormalAcceleration;
    
    [Header("�_�b�V��")]
    [Tooltip("1��̃_�b�V������")]  public float DashTimeOnce;
    [Tooltip("�_�b�V���̍ō����x")] public float MaxDashVelocity;
    [Tooltip("�_�b�V���̉����x")]   public float DashAcceleration;
    [Tooltip("�_�b�V���U����X:��ԕ����̗�/Y:������̗�")]     public Vector2 DashAtkForce;

    [Header("�W�����v")]
    [Tooltip("�W�����v��")]         public float JumpPower;

    [Header("��]")]
    [Tooltip("��]����")]           public float SpinTimeOnce;
    [Tooltip("��]�U����X:��ԕ����̗�/Y:������̗�")]         public Vector2 SpinAtkForce;
}