using System.Collections;
using UnityEngine;

public class Stage : MonoBehaviour
{
    public GameObject[] stage;
    int Stagecount = 10;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        int stagecount = stage.Length;

        for (int x = 0; x < Stagecount; x++)
        {
            for (int z = 0; z < Stagecount; z++)
            {
                GameObject stagecreate = stage[(x + z) % stagecount];
                Vector3 position = new Vector3(x * 1, 0, z * 1);
                Instantiate(stagecreate, position, Quaternion.identity, transform);
            }
        }
    }
}
      
