using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellManage : MonoBehaviour
{
    public GameObject hover;
    public bool filled = false;
    public bool steamed = false;
    public bool wet = false;
    public bool grassed = false;
    public bool burned = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {

    }

    private void OnMouseEnter()
    {
        hover.SetActive(true);
    }

    private void OnMouseExit()
    {
        hover.SetActive(false);
    }

    public void Steamed() {
        steamed = true;
        StartCoroutine(SteamTime());
        this.GetComponent<SpriteRenderer>().color = Color.grey;
    }

    IEnumerator SteamTime()
    {
        yield return new WaitForSeconds(5);
        steamed = false;
        this.GetComponent<SpriteRenderer>().color = Color.clear;
    }
    public void Wetted()
    {
        wet = true;
        StartCoroutine(WetTime());
        this.GetComponent<SpriteRenderer>().color = Color.blue;
    }

    IEnumerator WetTime()
    {
        yield return new WaitForSeconds(5);
        wet = false;
        this.GetComponent<SpriteRenderer>().color = Color.clear;
    }

    public void Grassed()
    {
        grassed = true;
        StartCoroutine(GrassTime());
        this.GetComponent<SpriteRenderer>().color = Color.green;
    }

    IEnumerator GrassTime()
    {
        yield return new WaitForSeconds(5);
        grassed = false;
        this.GetComponent<SpriteRenderer>().color = Color.clear;
    }

    public void Burned()
    {
        burned = true;
        StartCoroutine(BurnTime());
        this.GetComponent<SpriteRenderer>().color = Color.red;
    }

    IEnumerator BurnTime()
    {
        yield return new WaitForSeconds(5);
        burned = false;
        this.GetComponent<SpriteRenderer>().color = Color.clear;
    }
}
