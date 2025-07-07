using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class breakStage : MonoBehaviour
{
    public float breaktime = 2;
    private bool breaking = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && !breaking)
        {
            breaking = true;
            Invoke("Stage1", breaktime);
            Invoke("Stage2", breaktime);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
