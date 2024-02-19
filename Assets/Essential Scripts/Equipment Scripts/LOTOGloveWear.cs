using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LOTOGloveWear : MonoBehaviour
{
    public LotoPPEWearScript lotoPPEWearScript;
    
    void OnTriggerEnter(Collider ColliderForTrigger){
        
        if(ColliderForTrigger.tag == "HandTrigger"){
            Debug.Log("Triggered");
            lotoPPEWearScript.WoreGloves();

        }
    }

}
