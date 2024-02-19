using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class EmergencyButton : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip audioClip;
    private bool canPres = false;

    void Start()
    {
        audioSource=GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartButton()
    {
        if (LOTOSceneManagement.stage == 13) 
        {
            audioSource.PlayOneShot(audioClip);
            Debug.Log("Start ButonaBas�ldi");
            LOTOSceneManagement.stage = 14;
            
        }
    }
    public void EmergencyPress()
    {
        if (LOTOSceneManagement.stage==5)
        {
            audioSource.PlayOneShot(audioClip);
            Debug.Log("Emergency ButonaBas�ldi");
            LOTOSceneManagement.stage = 6;
            
        }
        if (LOTOSceneManagement.stage==23)
        {
            audioSource.PlayOneShot(audioClip);
            Debug.Log("acil stop butonu kald�r�ld�");
            LOTOSceneManagement.stage = 24;
        }
            
    }
    public void InformSuperVisor()
    {
        if (LOTOSceneManagement.stage == 1)
        {
            audioSource.PlayOneShot(audioClip);
            Debug.Log("super visor bilgilendirildi ");//
            LOTOSceneManagement.stage = 2;

        }
        if (LOTOSceneManagement.stage == 25)
        {
            audioSource.PlayOneShot(audioClip);
            Debug.Log("super visor bilgilendirildi ");//
            LOTOSceneManagement.stage = 26;
        }

    }
}
