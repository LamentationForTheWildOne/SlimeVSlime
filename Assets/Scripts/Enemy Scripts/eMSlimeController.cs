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
    public bool canMove = false;
    public bool pastWait = false;
    public bool attacking = true;
    public RaycastHit2D hit;

    // Start is called before the first frame update
    void Start()
    {
        GameManager = GameObject.Find("GameManager");
        StartCoroutine(MoveDelay());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        
         
        hit = Physics2D.Raycast(new Vector3(transform.position.x - 0.8f, transform.position.y), Vector2.left,0.4f);
        Debug.DrawRay(new Vector3(transform.position.x - 0.8f, transform.position.y), Vector2.left * 0.4f, Color.red);

        if (pastWait) {
            canMove = true;
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
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet") {

            int Bull = 0;

            if (collision.gameObject.TryGetComponent(out fBulletController fireBull)) 
            {
                Bull = 2;
            } 
            else
            if (collision.gameObject.TryGetComponent(out wBulletController waterBull))
            {
                Bull = 1;
            } 
            else
            if (collision.gameObject.TryGetComponent(out sBulletController steamBull))
            {
                Bull = 1;
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
                var target = hit.transform;
                target.BroadcastMessage("takeDmg", dmg);
            }

        }
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
}
