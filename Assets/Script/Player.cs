using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;           // �v���C���[�̈ړ����x
    public float dropCooldown = 2f;        // �����Ƃ��̃N�[���^�C��
    public float tileDropInterval = 0.3f;  // 1�u���b�N���Ƃ̗����Ԋu
    private float dropTimer = 0f;

    private Vector3 moveDirection;
    private bool isMoving = false;

    void Update()
    {
        // �ړ����́i�㉺���E1�����̂݁j
        if (!isMoving)
        {
            if (Input.GetKey(KeyCode.W)) { moveDirection = Vector3.forward; StartCoroutine(Move()); }
            else if (Input.GetKey(KeyCode.S)) { moveDirection = Vector3.back; StartCoroutine(Move()); }
            else if (Input.GetKey(KeyCode.A)) { moveDirection = Vector3.left; StartCoroutine(Move()); }
            else if (Input.GetKey(KeyCode.D)) { moveDirection = Vector3.right; StartCoroutine(Move()); }
        }

        // �����Ƃ�
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

    // �v���C���[�̐��ʕ����̃^�C����1�����Ƃ�
    private IEnumerator DropTilesInFront()
    {
        // ���ʕ�����Ray���΂��ă^�C�������ԂɎ擾
        RaycastHit[] hits = Physics.RaycastAll(transform.position, moveDirection, 10f);

        // ���������I�u�W�F�N�g�����Ԃɏ���
        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.CompareTag("Tile"))
            {
                GameObject tile = hit.collider.gameObject;
                tile.SetActive(false); // �����ɔ�\���i�����̑���j
                yield return new WaitForSeconds(tileDropInterval);
            }
        }
    }
}
