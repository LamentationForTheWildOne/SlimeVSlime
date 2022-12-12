using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject wSlime;
    public GameObject fSlime;
    public GameObject eSlime;
    public GameObject mSlime;
    public GameObject spawnTile;

    public GameObject[] slimes;

    private List<GameObject> pool = new List<GameObject>();
    
    public GameObject convey;
    public int sSec = 5;
    public int goodsSec = 4;
    private GameObject c80;
    private GameObject c81;
    private GameObject c82;
    private GameObject c83;
    private GameObject c84;

    public AudioSource myAud;
    public AudioClip waveSuc;
    public AudioClip healthLoss;

    public TextMeshProUGUI tscore;
    public TextMeshProUGUI thealth;
    public TextMeshProUGUI tsb;

    public int slimeBux = 500;
    public int score = 0;
    public int health = 5;

    public int wave = 0;
    public int leftToSpawn = 0;
    private int spawnTime = 0;
    private int xSpawn;


    public bool holding = false;
    public bool trashing = false;

    void Start()
    {
        wave = 1;
        leftToSpawn = 10;
        spawnTime = 6;
        myAud = GetComponent<AudioSource>();
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
        tscore.text = ("Wave: " + wave.ToString());
        thealth.text = ("Health: " + health.ToString());
        tsb.text = ("Slime Bucks: " + slimeBux.ToString());
        if (health <= 0) {
            SceneManager.LoadScene("Bad End");
        }
    }

    IEnumerator SpawnDelay()
    {
        yield return new WaitForSeconds(spawnTime);
        if (wave == 1)
        {
            xSpawn = Random.Range(2, 5);
        }
        else {
            xSpawn = Random.Range(1, 6);
        }
        switch (xSpawn) {
            case 1:
                spawnTile = c80;
                break;
            case 2:
                spawnTile = c81;
                break;
            case 3:
                spawnTile = c82;
                break;
            case 4:
                spawnTile = c83;
                break;
            case 5:
                spawnTile = c84;
                break;
        }

        if (leftToSpawn > 0)
        {
            leftToSpawn -= 1;
            SpawnMSlime();
            StartCoroutine(SpawnDelay());
        }
        else {
            myAud.PlayOneShot(waveSuc, 1F);
            yield return new WaitForSeconds(10);
            WaveAdvance();
        }

        
    }

    IEnumerator SlimeDelay()
    {
        
        yield return new WaitForSeconds(goodsSec);
        if (pool.Count == 0) {
            pool.AddRange(slimes); 
        }
        int index = Random.Range(0, pool.Count);
        GameObject p = pool[index];
        pool.RemoveAt(index);

        SpawnFriendSlime(p);
        StartCoroutine(SlimeDelay());
    }

    void WaveAdvance() {
        
        wave += 1;
        switch (wave) {
            case 2:
                leftToSpawn = 12;
                spawnTime = 5;
                break;
            case 3:
                leftToSpawn = 20;
                spawnTime = 4;
                break;
            case 4:
                leftToSpawn = 20;
                spawnTime = 1;
                break;
            case 5:
                leftToSpawn = 24;
                spawnTime = 5;
                break;
            case 6:
                leftToSpawn = 30;
                spawnTime = 3;
                break;
            case 7:
                leftToSpawn = 35;
                spawnTime = 1;
                break;
            case 8:
                SceneManager.LoadScene("Good End");
                break;
        }
        StartCoroutine(SpawnDelay());
    }

    void SpawnMSlime() {
        Instantiate(mSlime, spawnTile.transform.position, Quaternion.identity);
    }

    void SpawnFriendSlime(GameObject type)
    {
        Instantiate(type, convey.transform.position, Quaternion.identity);

    }
}
