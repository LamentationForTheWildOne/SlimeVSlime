using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashController : MonoBehaviour
{
    public GameObject GameManager;
    public SpriteRenderer myRend;
    public AudioSource myAud;

    public AudioClip trash;
    public Sprite open;
    public Sprite close;
    // Start is called before the first frame update
    void Start()
    {
        GameManager = GameObject.Find("GameManager");
        myAud = GetComponent<AudioSource>();
        myRend = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {

            if (!GameManager.GetComponent<GameManager>().holding && !GameManager.GetComponent<GameManager>().trashing)
            {
                GameManager.GetComponent<GameManager>().trashing = true;
                GameManager.GetComponent<GameManager>().holding = true;

                myRend.sprite = open;

            }
            else {
                GameManager.GetComponent<GameManager>().trashing = false;
                GameManager.GetComponent<GameManager>().holding = false;

                myRend.sprite = close;
            }
        }
    }



            }
