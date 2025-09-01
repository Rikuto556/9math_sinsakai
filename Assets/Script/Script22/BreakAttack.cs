using UnityEngine;

public class BlockAttacker : MonoBehaviour
{
    [SerializeField] int AttackRange = 3;
    [SerializeField] float CheckSpacing = 1.0f;
    [SerializeField] LayerMask BlockLayer;
    [SerializeField] float HeightOffset = -0.4f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))//プレイヤー用テスト
        {
            AttackForward();
        }
    }
    public void AttackForward()
    {
        Vector3 direction = transform.forward;

        for (int i = 1; i <= AttackRange; i++)
        {
            Vector3 checkPos = transform.position + direction * (i * CheckSpacing);
            checkPos.y += HeightOffset;

            Collider[] hits = Physics.OverlapBox(checkPos, Vector3.one * 0.4f, Quaternion.identity, BlockLayer);

            foreach (var hit in hits)
            {
                BlockController block = hit.GetComponent<BlockController>();
                if (block != null)
                {
                    block.TriggerBreak();
                }
            }
        }
    }
}
