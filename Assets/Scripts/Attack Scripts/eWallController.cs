using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eWallController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject maker;
    public GameObject hoverCell;
    public GameObject[] gos;
    public int hp = 20;
    void Start()
    {
        hoverCell = FindClosestCell();
        hoverCell.GetComponent<CellManage>().filled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        
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

    public void takeDmg(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            hoverCell.GetComponent<CellManage>().filled = false;
            maker.GetComponent<eSlimeController>().canspecial = true;
            Destroy(gameObject);
        }
    }
}
