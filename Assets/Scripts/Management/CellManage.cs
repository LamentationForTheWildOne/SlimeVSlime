using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellManage : MonoBehaviour
{
    public GameObject hover;
    public Animator myAni;
    public bool filled = false;
    public bool steamed = false;
    public bool wet = false;
    public bool grassed = false;
    public bool burned = false;
    // Start is called before the first frame update
    void Start()
    {
        myAni = GetComponent<Animator>();
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
        myAni.SetBool("steamTrail", true);
    }

    IEnumerator SteamTime()
    {
        yield return new WaitForSeconds(5);
        steamed = false;
        myAni.SetBool("steamTrail", false);
    }
    public void Wetted()
    {
        wet = true;
        StartCoroutine(WetTime());
        myAni.SetBool("waterTrail", true);

    }

    IEnumerator WetTime()
    {
        yield return new WaitForSeconds(5);
        wet = false;
        myAni.SetBool("waterTrail", false);

    }

   

    public void Burned()
    {
        burned = true;
        StartCoroutine(BurnTime());
        myAni.SetBool("burnTrail", true);
    }

    IEnumerator BurnTime()
    {
        yield return new WaitForSeconds(5);
        burned = false;
        myAni.SetBool("burnTrail", false);
    }

    public void Grassed()
    {
        grassed = true;
        StartCoroutine(GrassTime());
        myAni.SetBool("plantTrail", true);

    }

    IEnumerator GrassTime()
    {
        yield return new WaitForSeconds(5);
        grassed = false;
        myAni.SetBool("plantTrail", false);
    }
}
