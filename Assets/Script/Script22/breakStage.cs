using System.Collections;
using UnityEngine;

public class breakStage : MonoBehaviour
{
    private float _breaktime = 2;
    private float _revaivaltime = 4;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(BreakStage());
        }
    }
    IEnumerator BreakStage()
    {
        yield return new WaitForSeconds(_breaktime);
        GetComponent<Renderer>().enabled = false;
        GetComponent<Collider>().enabled = false;
        yield return new WaitForSeconds(_revaivaltime);
        GetComponent<Renderer>().enabled = true;
        GetComponent<Collider>().enabled = true;
    }
}
