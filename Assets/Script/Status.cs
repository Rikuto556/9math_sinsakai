using UnityEngine;

public class CharacterStatus : MonoBehaviour
{
    public int maxHP = 5; // 最大HP
    public int currentHP; // 現在のHP

    void Start()
    {
        currentHP = maxHP; // ゲーム開始時に最大HPでスタート
    }

    // HPを減らす処理
    public void TakeDamage(int damage)
    {
        currentHP -= damage; // ダメージ分減らす

        if (currentHP <= 0)
        {
            currentHP = 0; // HPがマイナスにならないようにする
            OnDeath();     // HPが0になったときの処理
        }
    }

    // HPが0になった時の処理
    void OnDeath()
    {
        // ここでゲームオーバー処理やリタイア処理を追加
        Debug.Log(gameObject.name + " はライフがなくなった！");
        // 例：動けなくする、非表示にするなど
    }
}
