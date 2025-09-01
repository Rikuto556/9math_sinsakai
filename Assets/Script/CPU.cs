using UnityEngine;
using System.Collections;

public class CPU : MonoBehaviour
{
    public Grid gridManager;
    public float moveSpeed = 3f;
    public float dropLockTime = 1.5f;
    public float dropCooldown = 3f;
    public float respawnDelay = 2f;

    private bool isMoving = false;       // �ړ����t���O
    private bool isDropping = false;     // �����Ƃ����t���O
    private bool dropOnCooldown = false; // �����Ƃ��N�[���^�C��
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
            // �����_���ɓ����������Ƃ���������
            int action = Random.Range(0, 10);
            if (action < 7) // 70%�̊m���ňړ�
                ChooseRandomDirection();
            else if (!dropOnCooldown) // 30%�̊m���ŏ����Ƃ�
                StartCoroutine(DropFloor());
        }

        // �ړ�����
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
        transform.forward = dir; // �����ύX
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

        // �v���C���[�Ɠ��������F���ʕ����ɂ܂��������Ƃ�
        gridManager.DropLineFromFront(playerX, playerZ, transform.forward);

        yield return new WaitForSeconds(dropLockTime);
        isDropping = false;

        yield return new WaitForSeconds(dropCooldown - dropLockTime);
        dropOnCooldown = false;
    }

    // ��������
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
