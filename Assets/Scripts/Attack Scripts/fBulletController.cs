using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fBulletController : MonoBehaviour
{
    public int dmg = 2;
    public int spd = 4;
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
