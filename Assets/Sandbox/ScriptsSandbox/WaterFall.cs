using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class WaterFall : MonoBehaviour
{
    private ParticleSystem particle;
    private float rotate = 0;
    private float pausedTime = 0;
    private bool finished = false;
    public GameObject waterInside;

    void Start()
    {
        particle = GetComponentInChildren<ParticleSystem>();
    }

    void Update()
    {
        waterFall();
    }
    public void waterFall()
    {
        rotate = (Mathf.Round(transform.eulerAngles.x)%360);

        
        if (rotate >=0 && rotate <= 180&& finished == false)
        {
            Debug.Log("Particle.time: " + particle.time);
            Debug.Log("pausedTÝme: " + pausedTime);

            if (particle.isEmitting==false && finished==false)
            {
                particle.Play();
                particle.time = particle.main.duration - pausedTime;
            }
            
            if (particle.time >= particle.main.duration-0.1f)
            {
                Debug.Log("Finished=true");
                waterInside.SetActive(false);
                particle.Stop();
                finished = true;
                particle.gameObject.SetActive(false);
            }
        }
        else if (finished == false)
        {  
            if (particle.isEmitting == true)
            {
                pausedTime = particle.main.duration - particle.time;
            }
            particle.time = pausedTime;
            particle.Stop();
        }
    }
}