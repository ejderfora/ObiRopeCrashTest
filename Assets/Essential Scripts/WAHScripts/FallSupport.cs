/*-----------------------------------------------------

PEILABS LLC.
info@peilabs.com

Creation Date: 01.01.2023
Author: Halil Doğan
Description: This script handles the Fall Support Mechanics in Work at Height VR simulation. Which is invisible and under the ladder at the catwalk
for players not to fall if they let go of the ladder when they are hooked.

-----------------------------------------------------*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallSupport : MonoBehaviour
{
    public HookMechanics rightHookMechanics;
    public HookMechanics leftHookMechanics;
    Vector3 startPos;
    private Vector3 distance;
    public float inceY = 1.5f;
    public bool entered;

    public void Start()
    {
        entered = false;
        startPos = transform.position;
    }
    public void Update()
    {
        followPlayer();
    }

    public void followPlayer()
    {
        if (rightHookMechanics.isHooked || leftHookMechanics.isHooked)
        {
            //Debug.Log("biz burdayız");
            Debug.Log("HookMechanics.y: " + rightHookMechanics.gameObject.transform.position.y);
            distance.y = rightHookMechanics.gameObject.transform.position.y - transform.position.y;
            Debug.Log("distance.y: " + distance.y);
            if (distance.y > inceY)
            {
                transform.Translate(0, distance.y - inceY, 0);
            }
        }
        else
        {
            transform.position = startPos;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 28)
        {
            entered = true;
        }
    }
}