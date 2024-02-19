
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WearPPEScript : MonoBehaviour
{

    [Header("Script References")]
    public HeightUIManager UIManagerScript;

    [Header("PPE On Display")]
    public GameObject helmetPPE;
    public GameObject glovesPPE;
    public GameObject bootsPPE;
    public GameObject hookPPE;
    public GameObject earPPE;
    public GameObject ppeWarningDoor;

    [Header("Player Textures")]
    public SkinnedMeshRenderer leftHandRenderer;
    public SkinnedMeshRenderer rightHandRenderer;
    public Material handNakedMat;
    public Material handWithGlovesMat;

    [Space(5)]

    [Header("PPE On Player")]

    public GameObject hookOnPlayer;

    [Header("Audio Variables")]
    public AudioSource playerSFXSource;
    public float sfxVolume;
    public AudioSource ambianceSFXSource;
    [Space(5)]
    public AudioClip wearHelmetSFX;
    public AudioClip wearGlovesSFX;
    public AudioClip wearShoesSFX;
    public AudioClip wearHookSFX;

    [Header("UI Variables")]

    private bool isHelmetWorn = false;
    private bool isGlovesWorn = false;
    private bool isShoesWorn = false;
    private bool isHookWorn = false;
    private bool isEarProtectionWorn = false;
    

    // Start is called before the first frame update
    void Start()
    {
        
        hookPPE.SetActive(true);
        //hookOnPlayer.SetActive(true);
        //hookOnPlayer.SetActive(false);
        
        ambianceSFXSource.volume = 0.2f;
 
    }
    private void Update()
    {
        
    }

    public void WoreHelmet(){

        Debug.Log("Helmet Worn");
        helmetPPE.SetActive(false);
        // HelmetOnPlayer.SetActive(true);
        playerSFXSource.PlayOneShot(wearHelmetSFX);
        isHelmetWorn = true;       
        UIManagerScript.HelmetCheck();
        CheckEquipment();
    }

    public void WoreGloves(){

        glovesPPE.SetActive(false);
        Debug.Log("Gloves Worn");
        leftHandRenderer.material = handWithGlovesMat;
        rightHandRenderer.material = handWithGlovesMat;
        glovesPPE.SetActive(false);

        playerSFXSource.PlayOneShot(wearGlovesSFX);
        isGlovesWorn = true;

        UIManagerScript.GlovesCheck();
        CheckEquipment();         
    }

    public void WoreShoes(){

        Debug.Log("Shoes Worn");    
        bootsPPE.SetActive(false);

        playerSFXSource.PlayOneShot(wearShoesSFX);
        isShoesWorn = true;

        UIManagerScript.ShoesCheck();
        CheckEquipment();
    }

    public void WoreHook(){

        Debug.Log("Wore hook çalıştı");   

        //Destroy(hookPPE);
        hookPPE.SetActive(false);
        Debug.Log("Yerdeki hook kapandı");
        //HookPPE.SetActive(false);
        hookOnPlayer.SetActive(true);
        Debug.Log("Hook Worn");
        playerSFXSource.PlayOneShot(wearHookSFX);
        Debug.Log("Ses çalındı");
        isHookWorn = true;
        Debug.Log("ishookworn true");

        UIManagerScript.HookCheck();
        Debug.Log("hookchecked");
        CheckEquipment();
        Debug.Log("checkequipmented");
    }

    public void WoreEarProtection(){

        Debug.Log("Ear Protection Worn");
        earPPE.SetActive(false);
        ambianceSFXSource.volume = 0.05f;
        isEarProtectionWorn = true;

        UIManagerScript.EarCheck();
        CheckEquipment();
    }

    public void CheckEquipment(){

        if(isHelmetWorn == true && isGlovesWorn == true && isShoesWorn == true && isHookWorn == true && isEarProtectionWorn == true){

            Debug.Log("Protection Equipments are OK");
            ppeWarningDoor.SetActive(false);
            UIManagerScript.WearedPPE();
        }    
    }

}
