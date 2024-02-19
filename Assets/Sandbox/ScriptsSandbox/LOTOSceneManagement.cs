using Autohand;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LOTOSceneManagement : MonoBehaviour
{
    public static int stage = 0;
    public int stageChecker = 0;

    public GameObject warningSignPLaceField;
    //stage 3 things
    public Grabbable warningSignGrabbAble;
    public BoxCollider dubaCollider1;
    public BoxCollider dubaCollider2;
    public BoxCollider dubaCollider3;
    public BoxCollider dubaCollider4;
    public Grabbable tapeGrabbAble;
    //stage 4 things

    //stage 5 things


    //stage 6 things
    public GameObject matGmj;
    public GameObject matPlaceHolderGmj;
    //stage 7 things
    public Grabbable electricalSwitchGrabbAble;
    //stage 8 things
    public GameObject multipleLockGameobject;
    //stage 9 things
    public Grabbable redValveGrabbAble;

    //stage 10 things
    public GameObject valveLockRed;
    public GameObject valveLockBlue;
    //stage 11 things
    public Grabbable blueValveGrabbAble;
    //stage 9 things

    //stage 13 things

    public Grabbable brokenGear;
    public GameObject gear;
    //stage 14 things
    public GameObject gearPlacefield;
    //stage 15 things
    public GameObject blueValveAnimArea;
    //stage 17 things
    public GameObject redValveAnimArea;
    //stage 19 things
    public GameObject electricalSwitchAnimArea;
    public void Start()
    {
        redValveGrabbAble.enabled = false; 
        blueValveGrabbAble.enabled = false;
        electricalSwitchGrabbAble.enabled = false;
        //
        matGmj.GetComponent<Grabbable>().enabled = false;
        multipleLockGameobject.GetComponent<Grabbable>().enabled = false;
        gear.GetComponent<Grabbable>().enabled = false;
        brokenGear.GetComponent<Grabbable>().enabled = false;
        valveLockBlue.GetComponent<Grabbable>().enabled = false;
        valveLockRed.GetComponent<Grabbable>().enabled = false;
        stage = 1;
    }
    public void Update()
    {
        Checker();
        if (Input.GetKeyDown(KeyCode.K))
        {
            stage++;
        }
        stageChecker = stage;
    }
    public void Checker()
    {
        if (stage==1)
        {
            // Debug.Log("Supervis�r� bilgilendir");
        }
        if (stage==2)
        {
            warningSignPLaceField.SetActive(true);
        }
        if (stage==3)
        {
            warningSignGrabbAble.enabled = false;
            dubaCollider1.enabled=true;
            dubaCollider2.enabled=true;
            dubaCollider3.enabled=true;
            dubaCollider4.enabled = true;
            tapeGrabbAble.enabled = true;
            // Debug.Log("Serit Cek");
        }
        if (stage == 4)
        {
            stage = 5;
            
        }
        if (stage == 5)
        {
            // Debug.Log("k�rm�z� Butona Bas");

        }
        if (stage==6)
        {
            // Debug.Log("Yal�tkan paspas Koy");
            matPlaceHolderGmj.SetActive(true);
            matGmj.GetComponent<Grabbable>().enabled = true;
        }
        if (stage==7)
        {
            electricalSwitchGrabbAble.enabled = true;
            // Debug.Log("salteri kapat");
        }
        if (stage==8)
        {
            multipleLockGameobject.GetComponent<Grabbable>().enabled = true;
            // Debug.Log("multiple LOTO tak");
        }
        if (stage==9)
        {
            multipleLockGameobject.GetComponent<Grabbable>().enabled = false;
            // Debug.Log("K�rm�z�Vany� Kapat");
            redValveGrabbAble.enabled = true;
        }
        if (stage==10)
        {
            redValveGrabbAble.enabled = false;
            // Debug.Log("K�rmm�z�vanayaLoto Tak");
            valveLockRed.GetComponent<Grabbable>().enabled = true;
        }
        if (stage==11)
        {
            valveLockRed.GetComponent<Grabbable>().enabled = false;
            // Debug.Log("mavi vanay� kapat");
            blueValveGrabbAble.enabled = true;
            
        }
        if (stage==12)
        {
            // Debug.Log("Mavi vanaya loto tak");
            blueValveGrabbAble.enabled = false;

            valveLockBlue.SetActive(true);
            valveLockBlue.GetComponent<BoxCollider>().enabled = true;
            valveLockBlue.GetComponent<Grabbable>().enabled = true;
        }
        if (stage==13)
        {
            valveLockBlue.GetComponent<Grabbable>().enabled = false;
            // Debug.Log("Makineyi �al��t�rmay� dene");
        }
        if (stage==14)
        {
            // Debug.Log("Bozuk parcayi cikar");
            brokenGear.enabled = true;
            //gearPlacefield.GetComponent<MeshRenderer>().enabled = true;
        }
        if (stage==15)
        {
            // Debug.Log("saglam parcayi  tak");
            gear.GetComponent<Grabbable>().enabled = true;
        }
        if (stage==16)
        {
            gear.GetComponent<Grabbable>().enabled = false;
            // Debug.Log("Mavi Vanan�n Lotosunu ��kar");
            valveLockBlue.GetComponent<Grabbable>().enabled = true;
            blueValveAnimArea.GetComponent<MeshRenderer>().enabled = true;
        }
        if (stage==17)
        {
            blueValveAnimArea.GetComponent<MeshRenderer>().enabled = false;
            valveLockBlue.GetComponent<Grabbable>().enabled = false;
            // Debug.Log("Mavi Vanay� Ac");
            blueValveGrabbAble.enabled= true;
        }
        if (stage==18)
        {
            blueValveGrabbAble.enabled = false;
            // Debug.Log("K�rmz� vanan�n lotosunu cikar");
            valveLockRed.GetComponent<Grabbable>().enabled = true;
            redValveAnimArea.GetComponent <MeshRenderer>().enabled = true;
        }
        if (stage==19)
        {
            // Debug.Log("K�rm�z� vanay� a�");
            redValveGrabbAble.enabled = true;
        }
        if (stage==20)
        {
            redValveGrabbAble.enabled = false;
            // Debug.Log("electricswitchten multiple loto yu c�kar");
            multipleLockGameobject.GetComponent<Grabbable>().enabled= true;
            electricalSwitchAnimArea.GetComponent<MeshRenderer>().enabled= true;


        }
        if (stage==21)
        {
            multipleLockGameobject.GetComponent<Grabbable>().enabled = false;
            electricalSwitchAnimArea.GetComponent<MeshRenderer>().enabled = false;
            // Debug.Log("electrical swtichi a�");
            electricalSwitchGrabbAble.enabled = true;
        }
        if (stage==22)
        {
            electricalSwitchGrabbAble.enabled = false;
            // Debug.Log("plastic mat� Kaldir");
            matGmj.GetComponent<Grabbable>().enabled = true;
        }
        if (stage == 23)
        {
            // Debug.Log("ACil stop butonunu kald�r");
        }
        if (stage==24)
        {
            // Debug.Log("warning signI kald�r");
            warningSignGrabbAble.enabled = true;
        }
        if (stage==25)
        {
            // Debug.Log("supervizoru bilgilendir");
        }
        if(stage==26)
        {
            // Debug.Log("form doldur");
            stage++;
        }
        if(stage==27)
        {
            // Debug.Log("serit kaldir");
        }
        if(stage==28)
        {
            // Debug.Log("bitirdin");

        }
    }
}
