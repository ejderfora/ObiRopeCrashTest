/*-----------------------------------------------------

PEILABS LLC.
info@peilabs.com

Creation Date: 01.01.2023
Author: Halil Doğan
Description: This script handles the activation process of crane control process. Which is an Easter Egg feature.

-----------------------------------------------------*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Autohand;

public class EasterEggScript : MonoBehaviour
{

    public GameObject CaseLid;
    public GameObject Keycard;
    public Grabbable KeycardGrabbable;

    [Header("Sound Variables")]
    public AudioClip pickUpSFX;
    public AudioClip openLidSFX;
    public AudioSource AudioSFXSource;
    public float SFXVolume;
    [Space(10)]

    [Header("Crane Variables")]
    public GameObject CraneMotor;
    [Space(5)]
    public float lowSpeed;
    public float midSpeed;
    public float highSpeed;


    private bool isCaseOpen;

    [HideInInspector]
    public bool isKeycardPickedUp;

    private Vector3 currentKeycardPos;
    

    // Start is called before the first frame update

    void Start()
    {
        isCaseOpen = false;
        isKeycardPickedUp = false;

        KeycardGrabbable.isGrabbable = false;


        currentKeycardPos = Keycard.transform.position;
        currentKeycardPos.z = currentKeycardPos.z - 0.15f;


        // isCaseOpen = true;
        // KeycardGrabbable.isGrabbable = true;
        // Keycard.transform.position = currentKeycardPos;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void openLid(){

        if(isCaseOpen == false){

            CaseLid.SetActive(false);
            isCaseOpen = true;

            AudioSFXSource.PlayOneShot(openLidSFX, SFXVolume);

            Keycard.transform.position = currentKeycardPos;

            KeycardGrabbable.isGrabbable = true; 

            isCaseOpen = true;
        }
    }

    public void pickUpKeycard(){

        if(isKeycardPickedUp == false && isCaseOpen == true){

            Destroy(Keycard);

            AudioSFXSource.PlayOneShot(pickUpSFX, SFXVolume);

            isKeycardPickedUp = true;

        }
    }

    public void LeverMin(){
        Debug.Log("MINIMUM");
    }
    public void LeverMid(){
        Debug.Log("MEDIUM");
    }
    public void LeverHigh(){
        Debug.Log("HIGH");
    }
}
