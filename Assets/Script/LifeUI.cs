using UnityEngine;
using UnityEngine.UI;

public class LifeUI : MonoBehaviour
{
    public CharacterStatus playerStatus; // �v���C���[��HP�X�N���v�g
    public Image[] hearts;               // �n�[�gImage��z��œo�^
    public Sprite fullHeart;             // ���^���̃n�[�g�摜
    public Sprite emptyHeart;            // ��̃n�[�g�摜

    void Update()
    {
        // HP�ɍ��킹�ăn�[�g�̕\�����X�V
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < playerStatus.currentHP)
            {
                hearts[i].sprite = fullHeart; // �c�胉�C�t���͐ԃn�[�g
            }
            else
            {
                hearts[i].sprite = emptyHeart; // ����ȊO�͋�n�[�g
            }
        }
    }
}
