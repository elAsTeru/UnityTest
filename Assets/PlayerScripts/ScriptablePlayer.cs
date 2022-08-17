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
    
    [Header("�_�b�V���ړ�")]
    [Tooltip("1��̃_�b�V������")]  public float DashTimeOnce;
    [Tooltip("�_�b�V���̍ō����x")] public float MaxDashVelocity;
    [Tooltip("�_�b�V���̉����x")]   public float DashAcceleration;

    [Header("�W�����v")]
    [Tooltip("�W�����v��")]         public float JumpPower;

    [Header("�U��")]
    [Tooltip("�_�b�V���U����")]     public float DashAttackPower;
    [Tooltip("��]�U����")]         public float SpinAttackPower;
}