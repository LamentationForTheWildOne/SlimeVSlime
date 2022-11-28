using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject wSlime;
    public GameObject fSlime;
    public GameObject mSlime;
    public GameObject spawnTile;
    
    public GameObject convey;
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
        convey = GameObject.Find("conveyor");
        c80 = GameObject.Find("Cell 8:0");
        c81 = GameObject.Find("Cell 8:1");
        c82 = GameObject.Find("Cell 8:2");
        c83 = GameObject.Find("Cell 8:3");
        c84 = GameObject.Find("Cell 8:4");
        StartCoroutine(SpawnDelay());
        StartCoroutine(SlimeDelay());
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

    IEnumerator SlimeDelay()
    {
        GameObject slimeType = null;
        yield return new WaitForSeconds(4);
        int x = Random.Range(1, 3);
        
        if (x == 1)
        {
            slimeType = wSlime;
        }
        if (x == 2)
        {
            slimeType = fSlime;
        }
        

        SpawnFriendSlime(slimeType);
        StartCoroutine(SlimeDelay());
    }

    void SpawnMSlime() {
        Instantiate(mSlime, spawnTile.transform.position, Quaternion.identity);
    }

    void SpawnFriendSlime(GameObject type)
    {
        Instantiate(type, convey.transform.position, Quaternion.identity);

    }
}
