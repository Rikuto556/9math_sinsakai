using System.Collections;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    [Header("ブロックが消えるまでの落下距離")]
    public float FallThreshold = 2f;

    [Header("復活までの時間")]
    public float reviveTime = 4f;

    private Vector3 originalPosition;
    private Quaternion originalRotation;

    private Rigidbody rb;
    private Collider col;
    private Renderer rend;

    private bool isBreaking = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        rend = GetComponent<Renderer>();

        originalPosition = transform.position;
        originalRotation = transform.rotation;

        rb.isKinematic = true; // 初期は固定
    }

    /// <summary>
    /// 踏んだ or 攻撃された時に呼ぶ
    /// </summary>
    public void TriggerBreak()
    {
        if (!isBreaking)
        {
            StartCoroutine(BreakRoutine());
        }
    }

    IEnumerator BreakRoutine()
    {
        isBreaking = true;

        // 落下開始（物理ON）
        rb.isKinematic = false;

        // 一定距離落ちるまで待つ
        while (transform.position.y > originalPosition.y - FallThreshold)
        {
            yield return null;
        }

        // 見た目と当たり判定をOFF
        rend.enabled = false;
        col.enabled = false;

        // Rigidbodyを止めて非表示
        rb.isKinematic = true;

        // transformだけ直接戻す（velocityは使わない）
        transform.position = originalPosition;
        transform.rotation = originalRotation;

        // 復活まで待つ
        yield return new WaitForSeconds(reviveTime);

        // 再表示
        rend.enabled = true;
        col.enabled = true;

        isBreaking = false;
    }
}
