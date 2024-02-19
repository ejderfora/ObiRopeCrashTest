using Obi;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rubbishbin : MonoBehaviour
{
    private AudioSource  rubbishAudioSource;
    public AudioClip rubbishAudio;
    public ParticleSystem rubbishPart;

    private void Start() {
        rubbishAudioSource=GetComponent<AudioSource>();
    }
    public void OnTriggerEnter(Collider other)
    { 
        if (other.tag == "Bottle")
        {
            rubbishAudioSource = GetComponent<AudioSource>();
            Destroy(other.gameObject);
            rubbishAudioSource.PlayOneShot(rubbishAudio);
            rubbishPart.Play();
        }
    }
    
}
