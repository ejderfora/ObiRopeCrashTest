
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeightUIManager : MonoBehaviour
{
    //[Header("Script References")]
    //public InteractionSFXManager SFXManager;

    [Space(5)]

    [Header("Checklist Canvas Variables")]
    public GameObject WristCanvas;
    public GameObject ChecklistCanvas;
    public GameObject HelmetTick;
    public GameObject GlovesTick;
    public GameObject ShoesTick;
    public GameObject HookTick;
    public GameObject EarTick;

    [Header("Wrist Canvas Variables")]

    public GameObject CongratsTick;
    public GameObject CongratsText;
    [Space(5)]

    public GameObject RestartText;
    public GameObject RestartButton;
    [Space(5)]

    public GameObject ToDoText;
    [Space(5)]

    public GameObject PPECheckText;
    public GameObject PPECheckTick;
    [Space(5)]

    public GameObject ManliftCheckText;
    public GameObject ManliftCheckTick;
    [Space(5)]

    public GameObject RebootCraneText;
    public GameObject RebootCraneTick;
    [Space(5)]

    [Header("Prologue Variables")]

    public GameObject autoHandPlayer;
    public GameObject tutorialText11;
    public GameObject tutorialText12;
    public GameObject nextButton1;
    public GameObject tutorialText2;
    public GameObject nextButton2;
    public GameObject tutorialText31;
    public GameObject tutorialText32;
    public GameObject nextButton3;
    public GameObject tutorialText41;
    public GameObject tutorialText42;
    public GameObject nextButton4;
    public GameObject tutorialText5;
    public GameObject nextButton5;

    // Start is called before the first frame update
    void Start()
    {

        autoHandPlayer.GetComponent<Autohand.AutoHandPlayer>().maxMoveSpeed = 0;

        tutorialText11.SetActive(true);
        tutorialText12.SetActive(true);
        nextButton1.SetActive(true);

        tutorialText2.SetActive(false);
        nextButton2.SetActive(false);
        tutorialText31.SetActive(false);
        tutorialText32.SetActive(false);
        nextButton3.SetActive(false);
        tutorialText41.SetActive(false);
        tutorialText42.SetActive(false);
        nextButton4.SetActive(false);
        tutorialText5.SetActive(false);
        nextButton5.SetActive(false);


        HelmetTick.SetActive(false);
        GlovesTick.SetActive(false);
        ShoesTick.SetActive(false);
        HookTick.SetActive(false);
        EarTick.SetActive(false);


        WristCanvas.SetActive(false);
        
        RestartButton.SetActive(true);
        RestartText.SetActive(true);

        CongratsText.SetActive(false);
        CongratsTick.SetActive(false);

        ToDoText.SetActive(true);

        PPECheckText.SetActive(true);
        PPECheckTick.SetActive(false);

        ManliftCheckText.SetActive(true);
        ManliftCheckTick.SetActive(false);

        RebootCraneText.SetActive(true);
        RebootCraneTick.SetActive(false);
 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HelmetCheck(){

        HelmetTick.SetActive(true);
    }
    public void GlovesCheck(){

        GlovesTick.SetActive(true);
    }
    public void ShoesCheck(){
        
        ShoesTick.SetActive(true);
    }
    public void HookCheck(){

        HookTick.SetActive(true);
    }
    public void EarCheck(){
        
        EarTick.SetActive(true);
    }

    public void EnableWristCanvas(){
        //SFXManager.PlayWristCanvasOnSFX();
        WristCanvas.SetActive(true);
    }

    public void DisableWristCanvas(){
        //SFXManager.PlayWristCanvasOffSFX();
        WristCanvas.SetActive(false);
    }

    public void WearedPPE(){

        PPECheckTick.SetActive(true);
    }

    public void EnteredManlift(){

        ManliftCheckTick.SetActive(true);
    }

    public void RebootedCrane(){

        RebootCraneTick.SetActive(true);

        Debug.Log("AŞAĞI İNME GÖREVİNİ YAP");
    }

    public void FinishedMissions(){

        //SFXManager.PlayLevelFinishedSFX();

        RestartButton.SetActive(true);
        RestartText.SetActive(true);

        CongratsText.SetActive(true);
        CongratsTick.SetActive(true);

        ToDoText.SetActive(false);

        PPECheckText.SetActive(false);
        PPECheckTick.SetActive(false);

        ManliftCheckText.SetActive(false);
        ManliftCheckTick.SetActive(false);

        RebootCraneText.SetActive(false);
        RebootCraneTick.SetActive(false);

    }


    public void NextButton1Pressed(){

        //SFXManager.PlayUIButtonClickSFX();

        tutorialText11.SetActive(false);
        tutorialText12.SetActive(false);
        nextButton1.SetActive(false);

        tutorialText2.SetActive(true);
        nextButton2.SetActive(true);

    }
    public void NextButton2Pressed(){

        //SFXManager.PlayUIButtonClickSFX();

        tutorialText2.SetActive(false);
        nextButton2.SetActive(false);
        tutorialText31.SetActive(true);
        tutorialText32.SetActive(true);
        nextButton3.SetActive(true);
    }
    public void NextButton3Pressed(){

        //SFXManager.PlayUIButtonClickSFX();

        tutorialText31.SetActive(false);
        tutorialText32.SetActive(false);
        nextButton3.SetActive(false);
        tutorialText41.SetActive(true);
        tutorialText42.SetActive(true);
        nextButton4.SetActive(true);

    }
    public void NextButton4Pressed(){

        //SFXManager.PlayUIButtonClickSFX();

        tutorialText41.SetActive(false);
        tutorialText42.SetActive(false);
        nextButton4.SetActive(false);
        tutorialText5.SetActive(true);
        nextButton5.SetActive(true);

    }
    public void NextButton5Pressed(){

        //SFXManager.PlayUIButtonClickSFX();
        autoHandPlayer.GetComponent<Autohand.AutoHandPlayer>().maxMoveSpeed = 2;

        tutorialText5.SetActive(false);
        nextButton5.SetActive(false);
    }
}