/*-----------------------------------------------------

PEILABS LLC.
info@peilabs.com

Creation Date: 01.01.2023
Author: Halil Doğan
Description: This script handles the Collision of Gloves in order to wear it.

-----------------------------------------------------*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WearGloveScript : MonoBehaviour
{
    public WearPPEScript wearPPEScript;
    
    void OnTriggerEnter(Collider ColliderForTrigger){
        
        if(ColliderForTrigger.tag == "HandTrigger"){
            Debug.Log("Triggered");
            wearPPEScript.WoreGloves();

        }
    }
}
