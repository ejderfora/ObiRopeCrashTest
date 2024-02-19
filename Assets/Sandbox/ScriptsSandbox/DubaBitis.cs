using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Platform;
using Oculus.Platform.Models;

public class DubaBitis : MonoBehaviour
{
    private Transform destination;
    public bool wentDestination=false;
    private void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag=="Duba" && other.gameObject!=gameObject.transform.parent.gameObject&&wentDestination==false)
        {
            //Debug.Log("enter calisti bitisteki");
            destination = other.gameObject.transform.Find("dubaSeritBasla");
            transform.position = destination.position;
            wentDestination = true;
        }
        
    }
}
