using UnityEngine;
using System.Collections;

public class Grid : MonoBehaviour
{
    public GameObject tilePrefab1;
    public GameObject tilePrefab2;
    public int gridWidth = 10;
    public int gridHeight = 10;
    public float spacing = 1.05f;
    public float respawnTime = 3f;

    private GameObject[,] tiles;

    void Start()
    {
        tiles = new GameObject[gridWidth, gridHeight];

        // �`�F�b�N���ŏ����쐬
        for (int x = 0; x < gridWidth; x++)
        {
            for (int z = 0; z < gridHeight; z++)
            {
                GameObject prefabToUse;
                if ((x + z) % 2 == 0)
                    prefabToUse = tilePrefab1;
                else
                    prefabToUse = tilePrefab2;

                GameObject tile = Instantiate(prefabToUse, new Vector3(x * spacing, 0, z * spacing), Quaternion.identity);
                tile.transform.parent = transform;
                tiles[x, z] = tile;
            }
        }
    }

    // �v���C���[�̐��ʕ����ɂ܂��������𗎂Ƃ�
    public void DropLineFromFront(int playerX, int playerZ, Vector3 forward)
    {
        // ��������i�㉺���E�̂݁j
        if (Mathf.Abs(forward.z) > Mathf.Abs(forward.x))
        {
            // ��iz�������j
            if (forward.z > 0)
            {
                for (int z = playerZ + 1; z < gridHeight; z++)
                    StartCoroutine(DropTile(playerX, z));
            }
            // ���iz�������j
            else
            {
                for (int z = playerZ - 1; z >= 0; z--)
                    StartCoroutine(DropTile(playerX, z));
            }
        }
        else
        {
            // �E�ix�������j
            if (forward.x > 0)
            {
                for (int x = playerX + 1; x < gridWidth; x++)
                    StartCoroutine(DropTile(x, playerZ));
            }
            // ���ix�������j
            else
            {
                for (int x = playerX - 1; x >= 0; x--)
                    StartCoroutine(DropTile(x, playerZ));
            }
        }
    }

    IEnumerator DropTile(int x, int z)
    {
        if (tiles[x, z] != null)
        {
            Animator anim = tiles[x, z].GetComponent<Animator>();
            if (anim != null)
                anim.SetTrigger("Fall"); // �����A�j���[�V�����J�n

            yield return new WaitForSeconds(0.5f); // �A�j���[�V��������

            tiles[x, z].SetActive(false); // ��\���ɂ���
            yield return new WaitForSeconds(respawnTime);

            tiles[x, z].SetActive(true); // ����
        }
    }
}
