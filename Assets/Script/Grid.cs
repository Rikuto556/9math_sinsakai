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

        // チェック柄で床を作成
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

    // プレイヤーの正面方向にまっすぐ床を落とす
    public void DropLineFromFront(int playerX, int playerZ, Vector3 forward)
    {
        // 向き判定（上下左右のみ）
        if (Mathf.Abs(forward.z) > Mathf.Abs(forward.x))
        {
            // 上（z正方向）
            if (forward.z > 0)
            {
                for (int z = playerZ + 1; z < gridHeight; z++)
                    StartCoroutine(DropTile(playerX, z));
            }
            // 下（z負方向）
            else
            {
                for (int z = playerZ - 1; z >= 0; z--)
                    StartCoroutine(DropTile(playerX, z));
            }
        }
        else
        {
            // 右（x正方向）
            if (forward.x > 0)
            {
                for (int x = playerX + 1; x < gridWidth; x++)
                    StartCoroutine(DropTile(x, playerZ));
            }
            // 左（x負方向）
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
                anim.SetTrigger("Fall"); // 落下アニメーション開始

            yield return new WaitForSeconds(0.5f); // アニメーション時間

            tiles[x, z].SetActive(false); // 非表示にする
            yield return new WaitForSeconds(respawnTime);

            tiles[x, z].SetActive(true); // 復活
        }
    }
}
