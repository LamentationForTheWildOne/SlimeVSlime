using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wBulletController : MonoBehaviour
{
    // Start is called before the first frame update
    public int dmg = 1;
    public int spd = 2;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x + (spd * Time.deltaTime), transform.position.y);
    }
}
