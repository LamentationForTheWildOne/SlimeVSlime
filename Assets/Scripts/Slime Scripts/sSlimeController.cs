using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sSlimeController : MonoBehaviour
{
    public GameObject GameManager;
    public bool held = false;
    public bool active = false;
    public GameObject hoverCell;
    public float aspd = 2;
    public float saspd = 10;
    public bool canshoot = true;
    public GameObject Bullet;
    public int hp = 10;
    public GameObject[] gos;
    public bool canspecial = true;
    public bool steaming = false;
    public bool soaking = false;
    public int heal = 0;

    private void Awake()
    {
        StartCoroutine(Regen());
    }
    // Start is called before the first frame update
    void Start()
    {
        GameManager = GameObject.Find("GameManager");
        StartCoroutine(Regen());
    }

    // Update is called once per frame
    void Update()
    {
        
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

                    held = true;
                    gameObject.layer = 1;
                    GameManager.GetComponent<GameManager>().holding = true;
                    Debug.Log("click");

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
                        }
                    }


                }
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

    private void Fire()
    {
        var spawnedBullet = Instantiate(Bullet, transform.position, Quaternion.identity);
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
                    Cell.BroadcastMessage("Steamed");
                }
            }
        }
    }

    IEnumerator ShootDelay()
    {
        yield return new WaitForSeconds(aspd);
        canshoot = true;
    }

    IEnumerator SpecialDelay() {
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
        if (hp <= 0)
        {
            hoverCell.GetComponent<CellManage>().filled = false;
            Destroy(gameObject);
        }
    }
}
