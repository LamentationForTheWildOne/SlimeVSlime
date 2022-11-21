using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject mSlime;
    public GameObject spawnTile;
    public int sSec = 5;
    private GameObject c80;
    private GameObject c81;
    private GameObject c82;
    private GameObject c83;
    private GameObject c84;

    // Start is called before the first frame update
    public bool holding = false;
    void Start()
    {
        c80 = GameObject.Find("Cell 8:0");
        c81 = GameObject.Find("Cell 8:1");
        c82 = GameObject.Find("Cell 8:2");
        c83 = GameObject.Find("Cell 8:3");
        c84 = GameObject.Find("Cell 8:4");
        StartCoroutine(SpawnDelay());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnDelay()
    {
        yield return new WaitForSeconds(sSec);
        int x = Random.Range(1, 6);
        if (x == 1) {
            spawnTile = c80;
        }
        if (x == 2)
        {
            spawnTile = c81;
        }
        if (x == 3)
        {
            spawnTile = c82;
        }
        if (x == 4)
        {
            spawnTile = c83;
        }
        if (x == 5)
        {
            spawnTile = c84;
        }

        SpawnMSlime();
        StartCoroutine(SpawnDelay());
    }

    void SpawnMSlime() {
        Instantiate(mSlime, spawnTile.transform.position, Quaternion.identity);
    }
}
