/*-----------------------------------------------------

PEILABS LLC.
info@peilabs.com

Creation Date: 01.01.2023
Author: Halil Doğan
Description: This script handles the warnings of Hooks. 

-----------------------------------------------------*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Autohand;

public class HookWarningScript : MonoBehaviour
{

    public GameObject XRPlayerCollider;
    public HookMechanics LeftHookMechanicsScript;
    public HookMechanics RightHookMechanicsScript;

    public Hand PlayerLeftHand;
    public Hand PlayerRightHand;

    [Space(10)]
    [Header("Warning Signs")]

    public GameObject HookWarningScreen;

    private bool warningActive;
    private bool isVibrationActive;

    public bool isClimbing;

    // Start is called before the first frame update
    void Start()
    {
        warningActive = false;
        HookWarningScreen.SetActive(false);
        isVibrationActive = false;

        isClimbing = false;
        

    }
    public void Awake()
    {
        //XRPlayerCollider.GetComponent<AutoHandPlayer>().heightOffset = 1;
        XRPlayerCollider.GetComponent<AutoHandPlayer>().heightOffset = 1.5f;
    }

    // Update is called once per frame
    void Update()
    {

        if(warningActive == true){

            HookWarningScreen.SetActive(true);

        }if(warningActive == false){

            HookWarningScreen.SetActive(false);

        }

        if(isVibrationActive == true){
            PlayerLeftHand.PlayHapticVibration(0.2f, 0.3f);
            PlayerRightHand.PlayHapticVibration(0.2f, 0.3f);
        }
    }


    private void OnTriggerEnter(Collider ColliderForWarning){

        isClimbing = true;

        //Debug.Log("Checking if isHooked is true, if false then enable the Warning..");

        if(RightHookMechanicsScript.isHooked == true && ColliderForWarning.tag == "HookWarningTrigger"){

            warningActive = false;
            //Debug.Log("Hooked, no problem");

        }
        if(RightHookMechanicsScript.isHooked == false && ColliderForWarning.tag == "HookWarningTrigger"){

            warningActive = true; 
            //Debug.Log("Hook Warning. Sign Activated");

        }

        if(LeftHookMechanicsScript.isHooked == true && RightHookMechanicsScript.isHooked == true && ColliderForWarning.tag == "LadderHookWarningTrigger"){

            warningActive = false;
            isVibrationActive = false;
            //Debug.Log("Hooked, no problem");
            
        }
        
        if(LeftHookMechanicsScript.isHooked == true && RightHookMechanicsScript.isHooked == false && ColliderForWarning.tag == "LadderHookWarningTrigger"){

            warningActive = false;
            isVibrationActive = false;
            //Debug.Log("Hooked, no problem");
            
        }

        if(LeftHookMechanicsScript.isHooked == false && RightHookMechanicsScript.isHooked == true && ColliderForWarning.tag == "LadderHookWarningTrigger"){

            warningActive = false;
            isVibrationActive = false;
            //Debug.Log("Hooked, no problem");
            
        }

        if(LeftHookMechanicsScript.isHooked == false && RightHookMechanicsScript.isHooked == false && ColliderForWarning.tag == "LadderHookWarningTrigger"){

            warningActive = true; 
            isVibrationActive = true;
            //Debug.Log("Ladder Hook Warning, Vibrating...");

        }
    }

    private void OnTriggerStay(Collider ColliderForWarning){

        //Debug.Log("Checking if isHooked is true, if false then enable the Warning..");

        if(RightHookMechanicsScript.isHooked == true && ColliderForWarning.tag == "HookWarningTrigger"){
            warningActive = false;
            
        }

        if(RightHookMechanicsScript.isHooked == false && ColliderForWarning.tag == "HookWarningTrigger"){
            if(LeftHookMechanicsScript.isHooked == true){
                warningActive = false;
            }else{
                warningActive = true;
            }
        }

        if(LeftHookMechanicsScript.isHooked == true && RightHookMechanicsScript.isHooked == true && ColliderForWarning.tag == "LadderHookWarningTrigger"){

            warningActive = false;
            isVibrationActive = false;
        }
        
        if(LeftHookMechanicsScript.isHooked == false && RightHookMechanicsScript.isHooked == false && ColliderForWarning.tag == "LadderHookWarningTrigger"){

            warningActive = true; 
            isVibrationActive = true;
        }

        if(LeftHookMechanicsScript.isHooked == true && RightHookMechanicsScript.isHooked == false && ColliderForWarning.tag == "LadderHookWarningTrigger"){

            warningActive = false; 
            isVibrationActive = false;
        }

        if(LeftHookMechanicsScript.isHooked == false && RightHookMechanicsScript.isHooked == true && ColliderForWarning.tag == "LadderHookWarningTrigger"){

            warningActive = false; 
            isVibrationActive = false;
        }
    }

    private void OnTriggerExit(Collider ColliderForWarning){

        isClimbing = false;
        
        if(ColliderForWarning.tag == "HookWarningTrigger"){
            warningActive = false;
            
            //Debug.Log("Got Out Of Danger Zone...");

        }
        if(ColliderForWarning.tag == "LadderHookWarningTrigger"){

            warningActive = false;
            isVibrationActive = false;
            //Debug.Log("Got Out Of Ladder Danger Zone...");

        }
    }
}
