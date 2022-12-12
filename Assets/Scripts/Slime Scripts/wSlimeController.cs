using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wSlimeController : MonoBehaviour
{
    public GameObject GameManager;
    public bool held = false;
    public bool active = false;
    public GameObject hoverCell;
    public float aspd = 2;
    public float saspd = 10;
    public bool canshoot = true;
    public GameObject Bullet;
    public GameObject sSlime;
    public GameObject pSlime;
    public int hp = 10;
    public GameObject[] gos;
    public bool canspecial = true;
    public bool steaming = false;
    public bool soaking = false;
    public bool grassing = false;
    public int heal = 0;
    public GameObject mergeSlime;
    public GameObject[] slimes;
    public int drift = 2;
    public Animator myAni;
    public AudioSource myAud;

    public AudioClip place;
    public AudioClip shoot;
    public AudioClip hurt;
    public AudioClip merge;

    public AudioClip trash;
    public AudioClip die;
    public AudioClip buy;
    public AudioClip buyfail;

    // Start is called before the first frame update

    private void Awake()
    {
        StartCoroutine(Regen());
    }
    void Start()
    {
        GameManager = GameObject.Find("GameManager");
        myAni = this.gameObject.transform.GetChild(0).GetComponent<Animator>();
        myAud = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hp < 5)
        {
            myAni.SetBool("Hurt", true);
        }
        else
        {
            myAni.SetBool("Hurt", false);
        }
    }

    void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(new Vector3(transform.position.x + 0.6f, transform.position.y), -Vector2.left, 20, 1 << 7);
        Debug.DrawRay(new Vector3(transform.position.x + 0.6f, transform.position.y), -Vector2.left * 20, Color.green);
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (held)
        {
            transform.position = new Vector3(mousePos.x, mousePos.y);
            //FindClosestCell();
            hoverCell = FindClosestCell();
        }

        if (hit.collider != null)
        {
            Debug.Log("hit");
        }

        if (active)
        {
            if (hit.collider != null)
            {
                if (hit.transform.CompareTag("Enemy"))
                {
                    if (canshoot)
                    {
                        Fire();
                        canshoot = false;
                        StartCoroutine(ShootDelay());
                    }
                    Debug.Log("hitE");
                }
            }

            if (canspecial)
            {
                Special();
                canspecial = false;

                StartCoroutine(SpecialDelay());
            }

            if (hoverCell.GetComponent<CellManage>().steamed)
            {
                steaming = true;
            }
            else
            {
                steaming = false;
            }

            if (hoverCell.GetComponent<CellManage>().wet)
            {
                soaking = true;
            }
            else
            {
                soaking = false;
            }

            if (hoverCell.GetComponent<CellManage>().grassed)
            {
                grassing = true;
            }
            else
            {
                grassing = false;
            }

            if (steaming)
            {
                aspd = 1;
            }
            else
            {
                aspd = 2;
            }

            if (soaking)
            {
                heal = 2;
            }
            else
            {
                heal = 0;
            }

            if (grassing)
            {
                heal = 4;
            }
            else
            {
                heal = 0;
            }
        }
        else if (!held)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - drift * Time.deltaTime);
        }
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {

            if (!GameManager.GetComponent<GameManager>().holding)
            {

                if (!active)
                {

                    if (GameManager.GetComponent<GameManager>().slimeBux >= 100)
                    {

                        GameManager.GetComponent<GameManager>().slimeBux -= 100;
                        held = true;
                        gameObject.layer = 1;
                        GameManager.GetComponent<GameManager>().holding = true;
                        myAud.PlayOneShot(buy, 0.5F);
                    }
                    else
                    {
                        myAud.PlayOneShot(buyfail, 1F);
                    }


                }

            }
            else
            {

                if (held)
                {
                    if (Vector2.Distance(transform.position, hoverCell.transform.position) < 1)
                    {
                        if (hoverCell.GetComponent<CellManage>().filled == false)
                        {
                            gameObject.layer = 0;
                            held = false;
                            GameManager.GetComponent<GameManager>().holding = false;
                            active = true;
                            transform.position = hoverCell.transform.position;
                            hoverCell.GetComponent<CellManage>().filled = true;
                            myAud.PlayOneShot(place, 1F);
                        }
                        else 
                        {
                            Merge();
                        
                        }
                    }


                }
                if (GameManager.GetComponent<GameManager>().trashing && active)
                {
                    GameManager.GetComponent<GameManager>().slimeBux += 50;
                    AudioSource.PlayClipAtPoint(trash, this.gameObject.transform.position);
                    hoverCell.GetComponent<CellManage>().filled = false;
                    Destroy(gameObject);
                }
            }

        }

        if (Input.GetMouseButtonDown(1)) 
        {
            if (held)
            {
                held = false;
                GameManager.GetComponent<GameManager>().holding = false;
                GameManager.GetComponent<GameManager>().slimeBux += 100;
                transform.position = GameManager.GetComponent<GameManager>().convey.transform.position;

            }

        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Cell")
        {
            //hoverCell = col.gameObject;
        }
    }

    public GameObject FindClosestCell()
    {

        gos = GameObject.FindGameObjectsWithTag("Cell");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }

    public GameObject CloseSlime()
    {
        slimes = GameObject.FindGameObjectsWithTag("Slime");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in slimes)
        {
            if (go.Equals(this.gameObject))
                continue;
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;

    }

    private void Fire()
    {
        myAni.SetTrigger("Shoot");
        myAud.PlayOneShot(shoot, 1F);
        var spawnedBullet = Instantiate(Bullet, transform.position, Quaternion.identity);
    }

    private void Merge() {
        var slime = CloseSlime();

        if (slime.TryGetComponent(out fSlimeController fslimeCheck)) {

            var spawnedsSlime = Instantiate(sSlime, slime.transform.position, Quaternion.identity);
            spawnedsSlime.GetComponent<sSlimeController>().active = true;
            spawnedsSlime.GetComponent<sSlimeController>().hoverCell = fslimeCheck.hoverCell;
            GameManager.GetComponent<GameManager>().holding = false;
            AudioSource.PlayClipAtPoint(merge, this.gameObject.transform.position);

            Destroy(gameObject);
            Destroy(slime);
        }

        if (slime.TryGetComponent(out eSlimeController eslimeCheck))
        {

            var spawnedpSlime = Instantiate(pSlime, slime.transform.position, Quaternion.identity);
            spawnedpSlime.GetComponent<pSlimeController>().active = true;
            spawnedpSlime.GetComponent<pSlimeController>().hoverCell = eslimeCheck.hoverCell;
            GameManager.GetComponent<GameManager>().holding = false;
            AudioSource.PlayClipAtPoint(merge, this.gameObject.transform.position);

            Destroy(gameObject);
            Destroy(slime);
        }



    }

    private void Special()
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(new Vector3(transform.position.x, transform.position.y), -Vector2.left, 20, 1 << 2);

        for (int i = 0; i < hits.Length; i++)
        {
            RaycastHit2D hit = hits[i];
            if (hit.transform.CompareTag("Cell"))
            {
                var Cell = hit.transform.GetComponent<CellManage>();

                if (Cell)
                {
                    Cell.BroadcastMessage("Wetted");
                }
            }
        }
    }

    IEnumerator ShootDelay()
    {
        yield return new WaitForSeconds(aspd);
        canshoot = true;
    }

    IEnumerator SpecialDelay()
    {
        yield return new WaitForSeconds(saspd);
        canspecial = true;
    }

    IEnumerator Regen()
    {
        while (true)
        {
            yield return new WaitForSeconds(2);
            if ((hp += heal) >= 10)
            {
                hp = 10;
            }
            else
            {
                hp += heal;
            }
        }
    }

    public void takeDmg(int damage)
    {
        hp -= damage;
        myAud.PlayOneShot(hurt, 1F);
        if (hp <= 0)
        {
            AudioSource.PlayClipAtPoint(die, this.gameObject.transform.position);
            hoverCell.GetComponent<CellManage>().filled = false;
            Destroy(gameObject);
        }
    }
}
