using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    public float moveSpeed = 3f;      // �ړ����x
    public Grid gridManager;          // Grid�X�N���v�g
    public Animator animator;         // �A�j���[�V����
    public float dropLockTime = 1.5f; // �����Ƃ����̈ړ��֎~����
    public float dropCooldown = 3f;   // �����Ƃ��̍Ďg�p�܂ł̎���
    public Vector3 respawnPosition;   // �����ʒu
    public float respawnDelay = 2f;   // �����܂ł̎���

    private bool isMoving = false;    // �ړ�����
    private bool isDropping = false;  // �����Ƃ�����
    private bool dropOnCooldown = false; // �����Ƃ��Ďg�p�҂�����
    private bool isDead = false;      // ��������
    private Vector3 targetPosition;   // ���̈ړ���

    void Start()
    {
        respawnPosition = transform.position; // �ŏ��̈ʒu��ۑ�
    }

    void Update()
    {
        // �����`�F�b�N
        if (!isDead && transform.position.y < -2f)
        {
            StartCoroutine(RespawnPlayer());
        }

        if (isDead) return;

        // �����Ƃ�����
        if (Input.GetKeyDown(KeyCode.P) && !dropOnCooldown)
        {
            StartCoroutine(DropFloor());
        }

        // �ړ����͈ړ��p��
        if (isMoving)
        {
            MoveToTarget();
            return;
        }

        // �㉺���E1�����̂ݎ�t
        if (Input.GetKey(KeyCode.W)) StartMove(Vector3.forward);
        else if (Input.GetKey(KeyCode.S)) StartMove(Vector3.back);
        else if (Input.GetKey(KeyCode.A)) StartMove(Vector3.left);
        else if (Input.GetKey(KeyCode.D)) StartMove(Vector3.right);
    }

    void StartMove(Vector3 direction)
    {
        targetPosition = transform.position + direction;
        transform.forward = direction; // ������ς���
        isMoving = true;
    }

    void MoveToTarget()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // �ړI�n�ɂ������~
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
        GetComponent<Renderer>().enabled = false; // �����ڂ�����
        isMoving = false;
        isDropping = false;

        yield return new WaitForSeconds(respawnDelay);

        transform.position = respawnPosition;
        GetComponent<Renderer>().enabled = true; // �����ڂ�߂�
        isDead = false;
    }
}
