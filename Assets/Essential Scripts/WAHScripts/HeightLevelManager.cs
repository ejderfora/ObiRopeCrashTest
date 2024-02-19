

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Autohand;
using Obi;

public class HeightLevelManager : MonoBehaviour
{

    public ManliftAnimation ManliftAnimation;
    [Space(10)]

    [Header("Objects")]
    public GameObject XRPlayer;
    public AutoHandPlayer myPlayer;
    [Space(10)]

    [Header("Teleport Locations")]
    public GameObject groundToLiftPos;
    public GameObject liftToGroundPos;
    public GameObject liftToCatwalkPos;
    public GameObject catwalkToLiftPos;
    public GameObject catwalkToCatwalkPos;
    [Space(10)]
    [Header("ObiSolver Refrences")]

    public GameObject beltClynder;
    public GameObject obiParent;
    public ObiSolver solver;
    public ObiFixedUpdater fixedUpdater;

    public GameObject leftHook;
    public GameObject rightHook;

  
    
    [Space(5)]

    [Header("Hook Mechanics")]
    public HookMechanics hookMechanicsRight;
    public HookMechanics hookMechanicsLeft;
    [Space(10)]

    [Header("Death Variables")]
    public AudioSource playerSFXSource;
    public AudioSource ambianceSFXSource;
    public GameObject blackScreenObject;
    public AudioClip deathSFX;
    [Range(0f, 1f)]
    public float SFXVolume;
    private bool isDead;
    private bool isFinished = false;

    public bool canExit;
    public bool isGameComplete;
    private int entryCounter;
    private int isGroundToLiftActive;
    private int isLiftToGroundActive;
    private int isLiftToCatwalkActive;
    private int isCatwalkToLiftActive;
    private int isCatWalkToCatwalkActive;

    [Header("Power Cut Variables")]
    public AudioClip PowerCutSFX;
    private bool isPowerCut;

    [Header("UI Variables")]
    public HeightUIManager UIManagementScript;
    private bool isFirstEnter;

    [Header("LOTO Variables")]
    public GameObject LOTOObject;

    // Start is called before the first frame update
    private void Awake()
    {
        
    }
    void Start()
    {
        //Debug.Log(LobbyLevelManager.playerHeight);
        //myPlayer.GetComponent<AutoHandPlayer>().heightOffset = LobbyLevelManager.playerHeight;
        entryCounter = 0;
        canExit = true;
        isGameComplete=false;
        isGroundToLiftActive = 0;
        isLiftToGroundActive = 0;
        isLiftToCatwalkActive = 0;
        isCatwalkToLiftActive = 0;
        isCatWalkToCatwalkActive=0;

        isPowerCut = false;

        blackScreenObject.SetActive(false);
        isDead = false;

        isFirstEnter = true;

    }

    // Update is called once per frame
    void Update()
    {
        if (isDead == true && playerSFXSource.isPlaying == false)
        {
            SceneManager.LoadScene(0, LoadSceneMode.Single);
        }
       /* if (Input.GetKeyDown(KeyCode.K))
        {
            RestartCurrentLevel();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            ActivateHook();
        }*/
    }

    public void ActivateHook()
    {
        beltClynder.SetActive(true);
    }

    public void enterLiftFromGround()
    {

        //SFXManager.PlayPhysicalButtonSFX();
        //Debug.Log("Ground ---> Lift Button Pressed");
        // Debug.Log(" before - Işınlanmak istenen yer: " + GroundToLiftPos.transform.position);
        // Debug.Log(" before - Olunan yer: " + XRPlayer.transform.position);

        if (isGroundToLiftActive == 1)
        {
            //XRPlayer.transform.position = GroundToLiftPos.transform.position;

            myPlayer.SetPosition(groundToLiftPos.transform.position);
            Debug.Log("IŞINLANDI");

            if (isFirstEnter == true)
            {
                entryCounter++;
                canExit = false;
                isFirstEnter = false;
            }
            else
            {
                canExit = true;
            }

            // Debug.Log(" after - Işınlanmak istenen yer: " + GroundToLiftPos.transform.position);
            // Debug.Log(" after - Işınlanılan yer: " + XRPlayer.transform.position);

        }
        else
        {
            // Debug.Log("NOT IN RIGHT PLACE!");
        }

    }

    public void exitLift()
    {
        //SFXManager.PlayPhysicalButtonSFX();

        
        if (isLiftToGroundActive == 1 && hookMechanicsRight.isHooked==false && hookMechanicsLeft.isHooked==false && canExit==true && entryCounter == 1)
        {
            //XRPlayer.transform.position = LiftToGroundPos.transform.position;
            PlayLiftAudio(false);
            myPlayer.SetPosition(liftToGroundPos.transform.position);
        }
        if (isLiftToCatwalkActive == 1 && hookMechanicsRight.isHooked==false && hookMechanicsLeft.isHooked==false && canExit == true)
        {
            //XRPlayer.transform.position = LiftToCatwalkPos.transform.position;
            PlayLiftAudio(false);

            myPlayer.SetPosition(liftToCatwalkPos.transform.position);

            UIManagementScript.EnteredManlift();
        }
        else
        {
            //Debug.Log("Not in Right Place or Hooked");
        }
    }

    private void PlayLiftAudio(bool state)
    {
        ManliftAnimation.PlayManliftAudio(state);
        
    }
        

    public void enterLiftFromCatwalk()
    {
       // SFXManager.PlayPhysicalButtonSFX();

        if (isCatwalkToLiftActive == 1 && isGameComplete==true)
        {

            myPlayer.SetPosition(catwalkToLiftPos.transform.position);

        }
        else
        {
            //Debug.Log("Not in Right Place or Game is Not Finished!");
        }
    }

    public void ExitLadder()
    {
       // SFXManager.PlayPhysicalButtonSFX();
        if (isCatWalkToCatwalkActive == 1 && isGameComplete==true && (!hookMechanicsRight.isHooked || !hookMechanicsLeft.isHooked))
        {

            myPlayer.SetPosition(catwalkToCatwalkPos.transform.position);

        }

    }
    /*
    public void CutThePower()
    {
        SFXManager.PlayPhysicalButtonSFX();

        if(isPowerCut==false)
        {
            isPowerCut = true;
            SFXManager.PlayLevelFinishedSFX();

            LOTOObject.SetActive(true); //LOTO ANIMATION

            Debug.Log("Cut The Power");

            UIManagementScript.CutTheElectric();
        }
       

    }
    */

    public void RebootCrane()
    {
        Debug.Log("Oyun zaten bitti sal");
        if (!isFinished)
        {
            //SFXManager.PlayPhysicalButtonSFX();
            isPowerCut = true;
            isGameComplete = true;
            if (isPowerCut == true)
            {
                UIManagementScript.RebootedCrane();

                //SFXManager.PlayLevelFinishedSFX();
            }
            else
            {
                Debug.Log("You didn't cut the power");
            }
            isFinished = true;
        }
        
    }

    public void RestartCurrentLevel()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);

    }

    public void BackToMainMenu()
    {

        SceneManager.LoadScene(1, LoadSceneMode.Single);

    }
    public void PosReset()
    {
        isGroundToLiftActive = 0;
        isLiftToGroundActive = 0;
        isLiftToCatwalkActive = 0;
        isCatwalkToLiftActive = 0;
        isCatWalkToCatwalkActive=0;
    }

    private void OnTriggerEnter(Collider ColliderForTrigger)
    {
        if (ColliderForTrigger.tag == "ManliftEnterExitTrigger")
        {
            if (ColliderForTrigger.name == "GroundEnterTrigger")
            {
                PosReset();
                isGroundToLiftActive = 1;

                // Debug.Log("Ground To Lift Button ACTIVE");
            }
            if (ColliderForTrigger.name == "GroundExitTrigger")
            {
                PosReset();
                isLiftToGroundActive = 1;

                // Debug.Log("Lift To Ground Button ACTIVE");
            }
            if (ColliderForTrigger.name == "CatwalkEnterTrigger")
            {
                PosReset();
                isLiftToCatwalkActive = 1;

                //Debug.Log("Lift To Catwalk Button ACTIVE");
            }
            if (ColliderForTrigger.name == "CatwalkExitTrigger")
            {
                PosReset();
                isCatwalkToLiftActive = 1;

                //Debug.Log("Catwalk to Lift Button ACTIVE");
            }if (ColliderForTrigger.name == "CatwalkToCatwalkTrigger")
            {
                PosReset();
                isCatWalkToCatwalkActive = 1;

                //Debug.Log("Catwalk to Lift Button ACTIVE");
            }
        }

        if (ColliderForTrigger.tag == "DeathTrigger")
        {
            playerSFXSource.PlayOneShot(deathSFX, SFXVolume);
            blackScreenObject.SetActive(true);

            isDead = true;

            //Debug.Log("Catwalk to Lift Button ACTIVE");
        }

    }

}
