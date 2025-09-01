using UnityEngine;

public class CharacterStatus : MonoBehaviour
{
    public int maxHP = 5; // �ő�HP
    public int currentHP; // ���݂�HP

    void Start()
    {
        currentHP = maxHP; // �Q�[���J�n���ɍő�HP�ŃX�^�[�g
    }

    // HP�����炷����
    public void TakeDamage(int damage)
    {
        currentHP -= damage; // �_���[�W�����炷

        if (currentHP <= 0)
        {
            currentHP = 0; // HP���}�C�i�X�ɂȂ�Ȃ��悤�ɂ���
            OnDeath();     // HP��0�ɂȂ����Ƃ��̏���
        }
    }

    // HP��0�ɂȂ������̏���
    void OnDeath()
    {
        // �����ŃQ�[���I�[�o�[�����⃊�^�C�A������ǉ�
        Debug.Log(gameObject.name + " �̓��C�t���Ȃ��Ȃ����I");
        // ��F�����Ȃ�����A��\���ɂ���Ȃ�
    }
}
