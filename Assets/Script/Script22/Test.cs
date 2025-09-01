using UnityEngine;

public class Test : MonoBehaviour
{

    int e;
    Vector3 y = new Vector3(0, 1.5f, 0);

    public LayerMask TargetLayer;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(transform.position - y, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 10f, TargetLayer))
        {
            Debug.Log("hit" + hit.collider.gameObject.name);
        }
        Debug.DrawRay(transform.position-y, transform.forward * 10f, Color.red);

        RaycastHit[] hits = Physics.RaycastAll(transform.position-y, transform.forward, 10f);
    }
}
