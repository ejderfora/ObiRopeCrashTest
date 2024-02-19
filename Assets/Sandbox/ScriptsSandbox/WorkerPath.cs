using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerPath : MonoBehaviour
{
    public float speed = 0.03f;
    public Rigidbody workerRb;
    public Vector3 move = Vector3.forward;
    public float count = 0;
    private bool walking = false;
    void Start()
    {
        workerRb = GetComponent<Rigidbody>();
    }
    IEnumerator waitAndGo()
    {
        
        if(walking==false) 
        {
            walking = true;
            if (count % 2 == 0)
            {

                yield return new WaitForSeconds(5.5f);

                transform.Rotate(0, 90, 0, Space.Self);
                count++;
                
            }
            else
            {
                yield return new WaitForSeconds(2.4f);

                transform.Rotate(0, 90, 0, Space.Self);
                count++;
                
            }

            walking=false;
        }
        
    }
    void FixedUpdate()
    {
        StartCoroutine(waitAndGo());
        Vector3 hareket = move * speed;
        workerRb.transform.Translate(hareket, Space.Self);
        waitAndGo();
        
         
    }
}
