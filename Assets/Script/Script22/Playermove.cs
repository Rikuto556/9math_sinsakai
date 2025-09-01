using UnityEngine;

public class Playermove : MonoBehaviour
{
    public float speed = 3.0f;

    void Update()
    {
        Vector3 direction = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
            direction = Vector3.forward;
        else if (Input.GetKey(KeyCode.S))
            direction = Vector3.back;
        else if (Input.GetKey(KeyCode.D))
            direction = Vector3.right;
        else if (Input.GetKey(KeyCode.A))
            direction = Vector3.left;

        if (direction != Vector3.zero)
        {
            // ������ύX
            transform.rotation = Quaternion.LookRotation(direction);

            // �O�ɐi��
            transform.position += transform.forward * speed * Time.deltaTime;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("EnemyAttack"))
        {
            BlockController block = GetComponent<BlockController>();
            if (block != null)
            {
                block.TriggerBreak();
            }
        }
    }


}
