using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleTrigger : MonoBehaviour
{
    public ParticleSystem ps;
    // Start is called before the first frame update
    void Start()
    {
        //ps.playbackSpeed = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            TriggerPar();
        }
        
    }

    void TriggerPar()
    {
        //Debug.Log("func triggered");
        ps.Play();
    }
}
