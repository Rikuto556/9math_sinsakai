using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    public float moveSpeed = 3f;      // 移動速度
    public Grid gridManager;          // Gridスクリプト
    public Animator animator;         // アニメーション
    public float dropLockTime = 1.5f; // 床落とし中の移動禁止時間
    public float dropCooldown = 3f;   // 床落としの再使用までの時間
    public Vector3 respawnPosition;   // 復活位置
    public float respawnDelay = 2f;   // 復活までの時間

    private bool isMoving = false;    // 移動中か
    private bool isDropping = false;  // 床落とし中か
    private bool dropOnCooldown = false; // 床落とし再使用待ち中か
    private bool isDead = false;      // 落下中か
    private Vector3 targetPosition;   // 次の移動先

    void Start()
    {
        respawnPosition = transform.position; // 最初の位置を保存
    }

    void Update()
    {
        // 落下チェック
        if (!isDead && transform.position.y < -2f)
        {
            StartCoroutine(RespawnPlayer());
        }

        if (isDead) return;

        // 床落とし処理
        if (Input.GetKeyDown(KeyCode.P) && !dropOnCooldown)
        {
            StartCoroutine(DropFloor());
        }

        // 移動中は移動継続
        if (isMoving)
        {
            MoveToTarget();
            return;
        }

        // 上下左右1方向のみ受付
        if (Input.GetKey(KeyCode.W)) StartMove(Vector3.forward);
        else if (Input.GetKey(KeyCode.S)) StartMove(Vector3.back);
        else if (Input.GetKey(KeyCode.A)) StartMove(Vector3.left);
        else if (Input.GetKey(KeyCode.D)) StartMove(Vector3.right);
    }

    void StartMove(Vector3 direction)
    {
        targetPosition = transform.position + direction;
        transform.forward = direction; // 向きを変える
        isMoving = true;
    }

    void MoveToTarget()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // 目的地についたら停止
        if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
        {
            transform.position = targetPosition;
            isMoving = false;
        }
    }

    IEnumerator DropFloor()
    {
        isDropping = true;
        dropOnCooldown = true;

        if (animator != null)
            animator.SetTrigger("Drop");

        int playerX = Mathf.RoundToInt(transform.position.x / gridManager.spacing);
        int playerZ = Mathf.RoundToInt(transform.position.z / gridManager.spacing);

        gridManager.DropLineFromFront(playerX, playerZ, transform.forward);

        yield return new WaitForSeconds(dropLockTime);
        isDropping = false;

        yield return new WaitForSeconds(dropCooldown - dropLockTime);
        dropOnCooldown = false;
    }


    IEnumerator RespawnPlayer()
    {
        isDead = true;
        GetComponent<Renderer>().enabled = false; // 見た目を消す
        isMoving = false;
        isDropping = false;

        yield return new WaitForSeconds(respawnDelay);

        transform.position = respawnPosition;
        GetComponent<Renderer>().enabled = true; // 見た目を戻す
        isDead = false;
    }
}
