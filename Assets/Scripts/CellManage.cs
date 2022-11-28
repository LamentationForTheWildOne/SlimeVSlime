using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellManage : MonoBehaviour
{
    public GameObject hover;
    public bool filled = false;
    public bool steamed = false;
    public bool wet = false;
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
    }

    IEnumerator SteamTime()
    {
        yield return new WaitForSeconds(5);
        steamed = false;
    }
    public void Wetted()
    {
        wet = true;
        StartCoroutine(WetTime());
    }

    IEnumerator WetTime()
    {
        yield return new WaitForSeconds(5);
        wet = false;
    }
}
