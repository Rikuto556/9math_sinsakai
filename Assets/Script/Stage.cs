using System.Collections;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject[] stage;
    int Stagecount = 8;
    

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        StartCoroutine(createstage());
    }
    IEnumerator createstage()
    {
        int stagecount = stage.Length;

        for (int x = 0; x < Stagecount; x++)
        {
            for (int z = 0; z < Stagecount; z++)
            {
                Vector3 position = new Vector3(x * Stagecount, 0, z * Stagecount);
               // Instantiate(stage, position, Quaternion.identity, transform);

                // 10ŒÂ‚²‚Æ‚É1ƒtƒŒ[ƒ€‘Ò‚Â
                if ((x * Stagecount + z) % 10 == 0)
                    yield return null;
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
