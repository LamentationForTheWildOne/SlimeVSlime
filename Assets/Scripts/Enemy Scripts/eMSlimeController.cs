using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eMSlimeController : MonoBehaviour
{
    public GameObject GameManager;
    public int spd = 2;
    public int hp = 10;
    public int dmg = 2;
    public int waitSec = 3;
    public int attSec = 2;
    public int burn = 0;
    public bool canMove = false;
    public bool pastWait = false;
    public bool attacking = true;
    public bool rooted = false;
    public RaycastHit2D hit;
    public AudioSource myAud;

    public AudioClip hurt;
    public AudioClip walk;

    public GameObject hoverCell;
    public GameObject[] gos;

    public GameObject vine;

    // Start is called before the first frame update
    void Start()
    {
        GameManager = GameObject.Find("GameManager");
        StartCoroutine(MoveDelay());
        StartCoroutine(DoT());
        myAud = GetComponent<AudioSource>();
        vine = this.gameObject.transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        hoverCell = FindClosestCell();

        if (hoverCell.GetComponent<CellManage>().burned)
        {
            burn = 3;
            spd = 1;
        }
        else {
            burn = 0;
            spd = 2;
        }

        

        hit = Physics2D.Raycast(new Vector3(transform.position.x - 0.8f, transform.position.y), Vector2.left,0.4f);
        Debug.DrawRay(new Vector3(transform.position.x - 0.8f, transform.position.y), Vector2.left * 0.4f, Color.red);

        if (pastWait) {
            canMove = true;
        }

        if (rooted)
        {
            canMove = false;
        }

        if (hit.collider != null)
        {
            if (hit.transform.CompareTag("Slime"))
            {
                if (hit.transform.gameObject.layer == 0)
                {
                    canMove = false;
                    Debug.Log("HIT SOMETHING");
                    if (attacking)
                    {
                        Attack();
                        attacking = false;
                        StartCoroutine(AttackDelay());
                    }
                }
                }
                Debug.Log("SEE SOMETHING");
        }

        if (canMove) 
        {
            Debug.Log("LETS GO");
            transform.position = new Vector3(transform.position.x - (spd * Time.deltaTime), transform.position.y);
        }

        if (hp <= 0) {
            GameManager.GetComponent<GameManager>().score += 10;
            GameManager.GetComponent<GameManager>().slimeBux += 50;
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet") {

            int Bull = 0;
            

            if (collision.gameObject.TryGetComponent(out fBulletController fireBull)) 
            {
                Bull = 3;
            } 
            
            if (collision.gameObject.TryGetComponent(out wBulletController waterBull))
            {
                Bull = 1;
            } 
            
            if (collision.gameObject.TryGetComponent(out sBulletController steamBull))
            {
                Bull = 2;
                Debug.Log("steam");
            }

            
            hp -= Bull;
            Destroy(collision.gameObject);
        }
        Debug.Log("ow");
    }

    void Attack()
    {

        if (hit.collider != null)
        {
            if (hit.transform.CompareTag("Slime"))
            {
                myAud.PlayOneShot(hurt, 1F);
                var target = hit.transform;
                target.BroadcastMessage("takeDmg", dmg);
            }

        }
    }

    public void Meleed(int damage) {
        hp -= damage;
    }

    public void Root(int roottime) {
        
        canMove = false;
        vine.SetActive(true);
        StartCoroutine(RootDelay(roottime));
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

    IEnumerator RootDelay(int roottime)
    {
        yield return new WaitForSeconds(roottime);
        rooted = false;
        vine.SetActive(false);

    }

    IEnumerator MoveDelay()
    {
        yield return new WaitForSeconds(waitSec);
        canMove = true;
        pastWait = true;
    }

    IEnumerator AttackDelay()
    {
        yield return new WaitForSeconds(attSec);
        attacking = true;
    }

    IEnumerator DoT()
    {
        yield return new WaitForSeconds(1);
        hp -= burn;
        StartCoroutine(DoT());
    }
}
