using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;           // プレイヤーの移動速度
    public float dropCooldown = 2f;        // 床落としのクールタイム
    public float tileDropInterval = 0.3f;  // 1ブロックごとの落下間隔
    private float dropTimer = 0f;

    private Vector3 moveDirection;
    private bool isMoving = false;

    void Update()
    {
        // 移動入力（上下左右1方向のみ）
        if (!isMoving)
        {
            if (Input.GetKey(KeyCode.W)) { moveDirection = Vector3.forward; StartCoroutine(Move()); }
            else if (Input.GetKey(KeyCode.S)) { moveDirection = Vector3.back; StartCoroutine(Move()); }
            else if (Input.GetKey(KeyCode.A)) { moveDirection = Vector3.left; StartCoroutine(Move()); }
            else if (Input.GetKey(KeyCode.D)) { moveDirection = Vector3.right; StartCoroutine(Move()); }
        }

        // 床落とし
        dropTimer -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.P) && dropTimer <= 0f)
        {
            StartCoroutine(DropTilesInFront());
            dropTimer = dropCooldown;
        }
    }

    private IEnumerator Move()
    {
        isMoving = true;
        Vector3 startPos = transform.position;
        Vector3 targetPos = startPos + moveDirection;

        float elapsed = 0f;
        while (elapsed < 1f)
        {
            transform.position = Vector3.Lerp(startPos, targetPos, elapsed);
            elapsed += Time.deltaTime * moveSpeed;
            yield return null;
        }
        transform.position = targetPos;
        isMoving = false;
    }

    // プレイヤーの正面方向のタイルを1つずつ落とす
    private IEnumerator DropTilesInFront()
    {
        // 正面方向にRayを飛ばしてタイルを順番に取得
        RaycastHit[] hits = Physics.RaycastAll(transform.position, moveDirection, 10f);

        // 当たったオブジェクトを順番に処理
        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.CompareTag("Tile"))
            {
                GameObject tile = hit.collider.gameObject;
                tile.SetActive(false); // 即座に非表示（落下の代わり）
                yield return new WaitForSeconds(tileDropInterval);
            }
        }
    }
}
