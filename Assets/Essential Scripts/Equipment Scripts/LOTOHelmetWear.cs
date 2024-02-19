using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LOTOHelmetWear : MonoBehaviour
{
    public LotoPPEWearScript lotoPPEWearScript;
    
    void OnTriggerEnter(Collider ColliderForTrigger){

        if(ColliderForTrigger.tag == "HandTrigger")
        {

            lotoPPEWearScript.WoreHelmet();

        }
    }

    
}
