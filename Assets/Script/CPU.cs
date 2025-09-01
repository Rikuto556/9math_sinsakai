using UnityEngine;
using System.Collections;

public class CPU : MonoBehaviour
{
    public Grid gridManager;
    public float moveSpeed = 3f;
    public float dropLockTime = 1.5f;
    public float dropCooldown = 3f;
    public float respawnDelay = 2f;

    private bool isMoving = false;       // 移動中フラグ
    private bool isDropping = false;     // 床落とし中フラグ
    private bool dropOnCooldown = false; // 床落としクールタイム
    private Vector3 targetPosition;
    private Animator animator;
    private Vector3 spawnPosition;

    void Start()
    {
        animator = GetComponent<Animator>();
        spawnPosition = transform.position;
        targetPosition = transform.position;
    }

    void Update()
    {
        if (!isMoving && !isDropping)
        {
            // ランダムに動くか床落とすかを決定
            int action = Random.Range(0, 10);
            if (action < 7) // 70%の確率で移動
                ChooseRandomDirection();
            else if (!dropOnCooldown) // 30%の確率で床落とし
                StartCoroutine(DropFloor());
        }

        // 移動処理
        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
            {
                isMoving = false;
            }
        }
    }

    void ChooseRandomDirection()
    {
        Vector3[] directions = { Vector3.forward, Vector3.back, Vector3.left, Vector3.right };
        Vector3 dir = directions[Random.Range(0, directions.Length)];
        targetPosition = transform.position + dir;
        transform.forward = dir; // 向き変更
        isMoving = true;
    }

    IEnumerator DropFloor()
    {
        isDropping = true;
        dropOnCooldown = true;

        if (animator != null)
            animator.SetTrigger("Drop");

        int playerX = Mathf.RoundToInt(transform.position.x / gridManager.spacing);
        int playerZ = Mathf.RoundToInt(transform.position.z / gridManager.spacing);

        // プレイヤーと同じ処理：正面方向にまっすぐ落とす
        gridManager.DropLineFromFront(playerX, playerZ, transform.forward);

        yield return new WaitForSeconds(dropLockTime);
        isDropping = false;

        yield return new WaitForSeconds(dropCooldown - dropLockTime);
        dropOnCooldown = false;
    }

    // 落下処理
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Void"))
        {
            StartCoroutine(Respawn());
        }
    }

    IEnumerator Respawn()
    {
        gameObject.SetActive(false);
        yield return new WaitForSeconds(respawnDelay);
        transform.position = spawnPosition;
        gameObject.SetActive(true);
    }
}
