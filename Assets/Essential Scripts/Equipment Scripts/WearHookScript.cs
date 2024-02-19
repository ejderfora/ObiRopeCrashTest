/*-----------------------------------------------------

PEILABS LLC.
info@peilabs.com

Creation Date: 01.01.2023
Author: Halil Doğan
Description: This script handles the Collision of Safety Harness in order to wear it.

-----------------------------------------------------*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WearHookScript : MonoBehaviour
{
    public WearPPEScript WearPPEScript;

    void OnTriggerEnter(Collider ColliderForTrigger){

        if(ColliderForTrigger.tag == "HandTrigger"){

            WearPPEScript.WoreHook();

        }
    }
}
