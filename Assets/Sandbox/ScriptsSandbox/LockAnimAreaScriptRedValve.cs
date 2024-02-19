using Autohand;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockAnimAreaScriptRedValve : MonoBehaviour
{
    private BoxCollider boxCollider;
    private MeshRenderer meshRenderer;
    private Animator lockAnimatior;
    private GameObject lockGameobject;
    private bool lockEntered = false;
    private Grabbable lockGrabble;
    private float elapsedTime = 0.0f;
    private ValveScriptRed valveScriptRed;
    //private ElectricalSwitchScript electricalSwitchScript;

    [HideInInspector] public bool animationDone = false;
    public Transform animationStartPos;
    private string lockTag="Lock";
    public float duration = 2.0f;
    

    void Start()
    {
        meshRenderer=GetComponent<MeshRenderer>();
        valveScriptRed = GetComponentInParent<ValveScriptRed>();
        //electricalSwitchScript=GetComponentInParent<ElectricalSwitchScript>();
        boxCollider = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        moveLockToAnimStart();
    }
    private IEnumerator OnTriggerEnterCoroutine(Collider other)
    {
        Debug.Log("trigger entered=true  tag: " + other.tag);
        lockGameobject = other.gameObject;
        lockAnimatior = lockGameobject.GetComponent<Animator>();
        lockGrabble = other.gameObject.GetComponent<Grabbable>();
        meshRenderer.enabled = false;
        yield return new WaitForSeconds(0.03f);
        lockGrabble.HandsRelease();
        yield return new WaitForSeconds(0.03f);
        boxCollider.enabled = false;
        lockGameobject.GetComponent<Rigidbody>().isKinematic = true;

        lockGrabble.enabled = false;
        lockEntered = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(lockTag)&&lockEntered==false)
        {
            StartCoroutine(OnTriggerEnterCoroutine(other));
            
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(lockTag) && LOTOSceneManagement.stage==18)
        {
            //Debug.Log("triggerExit tag: " + other.tag);
            meshRenderer.enabled = false;
            lockGrabble.HandsRelease();
            lockGrabble.enabled = false;
            if (valveScriptRed != null)
            {
                valveScriptRed.ValveRotateReset();
            }  
            boxCollider.enabled = false;
            Debug.Log("KIrm�z� vananan�n lotosu cikarildi");
            LOTOSceneManagement.stage = 19;
            lockGameobject.GetComponent<Rigidbody>().useGravity = true;
        }
    }
   
    public void moveLockToAnimStart()
    {
        if (lockEntered==true && animationDone==false)
        {
            if (elapsedTime < duration)
            {
                // �lerleme y�zdesi hesaplan�r (0 ile 1 aras�nda)
                float progress = elapsedTime / duration;

                // Slerp i�lemi ger�ekle�tirilir
                Vector3 lerpedPosition = Vector3.Slerp(lockGameobject.transform.position, animationStartPos.position, progress);
                lockGameobject.transform.position = lerpedPosition;
                Quaternion targetRotation = Quaternion.RotateTowards(lockGameobject.transform.rotation, animationStartPos.rotation, 3f);
                lockGameobject.transform.rotation = targetRotation;
                // Zaman sayac� g�ncellenir
                elapsedTime += Time.deltaTime;
            }
            else if(lockAnimatior.enabled==false)
            {
                lockAnimatior.enabled = true;
                Invoke("AnimationDoneSet", 4);
            }

        }
    }//
    private void AnimationDoneSet()
    {
        
        animationDone = true;
        boxCollider.enabled = true;
        //lockGrabble.enabled = true;
        lockGameobject.GetComponent<Rigidbody>().isKinematic = false;
        lockGameobject.GetComponent<Rigidbody>().useGravity = false;
        Debug.Log("K�rm�z� valva LotoTak�ld�");
        LOTOSceneManagement.stage = 11;
        
    }
}