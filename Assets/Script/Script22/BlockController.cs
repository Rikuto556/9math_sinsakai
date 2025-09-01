using System.Collections;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    [Header("�u���b�N��������܂ł̗�������")]
    public float FallThreshold = 2f;

    [Header("�����܂ł̎���")]
    public float reviveTime = 4f;

    private Vector3 originalPosition;
    private Quaternion originalRotation;

    private Rigidbody rb;
    private Collider col;
    private Renderer rend;

    private bool isBreaking = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        rend = GetComponent<Renderer>();

        originalPosition = transform.position;
        originalRotation = transform.rotation;

        rb.isKinematic = true; // �����͌Œ�
    }

    /// <summary>
    /// ���� or �U�����ꂽ���ɌĂ�
    /// </summary>
    public void TriggerBreak()
    {
        if (!isBreaking)
        {
            StartCoroutine(BreakRoutine());
        }
    }

    IEnumerator BreakRoutine()
    {
        isBreaking = true;

        // �����J�n�i����ON�j
        rb.isKinematic = false;

        // ��苗��������܂ő҂�
        while (transform.position.y > originalPosition.y - FallThreshold)
        {
            yield return null;
        }

        // �����ڂƓ����蔻���OFF
        rend.enabled = false;
        col.enabled = false;

        // Rigidbody���~�߂Ĕ�\��
        rb.isKinematic = true;

        // transform�������ږ߂��ivelocity�͎g��Ȃ��j
        transform.position = originalPosition;
        transform.rotation = originalRotation;

        // �����܂ő҂�
        yield return new WaitForSeconds(reviveTime);

        // �ĕ\��
        rend.enabled = true;
        col.enabled = true;

        isBreaking = false;
    }
}
