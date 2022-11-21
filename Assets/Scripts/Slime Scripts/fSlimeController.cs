using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fSlimeController : MonoBehaviour
{
    public GameObject GameManager;
    public bool held = false;
    public bool active = false;
    public GameObject hoverCell;
    public float aspd = 2;
    public bool canshoot = true;
    public GameObject Bullet;
    public int hp = 10;
    // Start is called before the first frame update
    void Start()
    {
        GameManager = GameObject.Find("GameManager");
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(new Vector3(transform.position.x + 0.6f, transform.position.y), -Vector2.left, 20);
        Debug.DrawRay(new Vector3(transform.position.x + 0.6f, transform.position.y), -Vector2.left * 20, Color.green);
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (held) {
            transform.position = new Vector3(mousePos.x, mousePos.y);
            //FindClosestCell();
            hoverCell = FindClosestCell();
        }

        if (hit.collider != null) {
            Debug.Log("hit");
        }

        if (active) {
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
        }
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0)) {

            if (!GameManager.GetComponent<GameManager>().holding)
            {

                if (!active)
                {

                    held = true;
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
                        held = false;
                        GameManager.GetComponent<GameManager>().holding = false;
                        active = true;
                        transform.position = hoverCell.transform.position;
                    }


                }
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Cell") {
            //hoverCell = col.gameObject;
        }
    }

    public GameObject FindClosestCell()
    {
        GameObject[] gos;
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

    private void Fire() {
        var spawnedBullet = Instantiate(Bullet, transform.position, Quaternion.identity);
    }

    IEnumerator ShootDelay()
    {
        yield return new WaitForSeconds(aspd);
        canshoot = true;
    }

    public void takeDmg(int damage)
    {
        hp -= damage;
        if (hp <= 0) 
        {
            Destroy(gameObject);
        }
    }



}
