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
    public bool canMove = false;
    public bool pastWait = false;

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
        RaycastHit2D hit = Physics2D.Raycast(new Vector3(transform.position.x - 0.6f, transform.position.y), Vector2.left,0.4f);
        Debug.DrawRay(new Vector3(transform.position.x - 0.6f, transform.position.y), Vector2.left * 0.4f, Color.red);

        if (pastWait) {
            canMove = true;
        }

        if (hit.collider != null)
        {
            if (hit.transform.CompareTag("Slime"))
            {
                canMove = false;
                Debug.Log("HIT SOMETHING");
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
        if (collision.gameObject.tag == "fBullet") {
            var fBull = collision.gameObject.GetComponent<fBulletController>();
            hp -= fBull.dmg;
            Destroy(collision.gameObject);
        }
        Debug.Log("ow");
    }

    IEnumerator MoveDelay()
    {
        yield return new WaitForSeconds(waitSec);
        canMove = true;
        pastWait = true;
    }
}
