using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public string sceneToLoad;
    public AudioClip click;
    public AudioSource myAud;
    private void Start()
    {
        myAud = GetComponent<AudioSource>();
    }
    public void ChangeScene()
    {
        myAud.PlayOneShot(click, 1f);
        StartCoroutine(SceneDelay());
        

    }

    IEnumerator SceneDelay()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(sceneToLoad);
    }

}
