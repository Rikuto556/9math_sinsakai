using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemymove : MonoBehaviour
{
    public Transform player;      // �v���C���[�̈ʒu
    public float speed = 3f;      // �ړ����x
    public float attackRange = 1.5f; // �U������
    public float detectionRange = 10f; // �T�m�͈�

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance < detectionRange)
        {
            if (distance > attackRange)
            {
                MoveTowardsPlayer();
            }
            else
            {
                AttackPlayer();
            }
        }
    }

    void MoveTowardsPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        Vector3 move = new Vector3(direction.x, 0, direction.z); // �������������ړ�
        rb.MovePosition(transform.position + move * speed * Time.deltaTime);
    }

    void AttackPlayer()
    {
        Debug.Log("�U������I");
        // �����ōU����������Ŋg��
    }
}
    
