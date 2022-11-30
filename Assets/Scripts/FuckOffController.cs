using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuckOffController : MonoBehaviour
{
    public GameManager GameManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy")){
            GameManager.GetComponent<GameManager>().health -= 1;
        } 
        Destroy(col.gameObject);
    }
}
