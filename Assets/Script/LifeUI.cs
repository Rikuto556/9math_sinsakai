using UnityEngine;
using UnityEngine.UI;

public class LifeUI : MonoBehaviour
{
    public CharacterStatus playerStatus; // プレイヤーのHPスクリプト
    public Image[] hearts;               // ハートImageを配列で登録
    public Sprite fullHeart;             // 満タンのハート画像
    public Sprite emptyHeart;            // 空のハート画像

    void Update()
    {
        // HPに合わせてハートの表示を更新
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < playerStatus.currentHP)
            {
                hearts[i].sprite = fullHeart; // 残りライフ分は赤ハート
            }
            else
            {
                hearts[i].sprite = emptyHeart; // それ以外は空ハート
            }
        }
    }
}
