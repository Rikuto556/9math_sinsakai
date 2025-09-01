using UnityEngine;

public class Enemymove : MonoBehaviour
{
    public Transform player;
    public float speed = 3f;
    public float attackRange = 1.5f;
    public float detectionRange = 10f;

    private Rigidbody rb;
    private BlockAttacker attacker;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        attacker = GetComponent<BlockAttacker>();
    }
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
                attacker.AttackForward();
            }
        }
    }

    void MoveTowardsPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        Vector3 move = new Vector3(direction.x, 0, direction.z);
        rb.MovePosition(transform.position + move * speed * Time.deltaTime);
    }
}