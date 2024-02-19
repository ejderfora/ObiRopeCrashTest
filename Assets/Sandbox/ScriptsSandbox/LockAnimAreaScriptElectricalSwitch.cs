using Autohand;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockAnimAreaScriptElectricalSwitch : MonoBehaviour
{
    private BoxCollider boxCollider;
    private MeshRenderer meshRenderer;
    private Animator lockAnimatior;
    private GameObject lockGameobject;
    private bool lockEntered = false;
    private Grabbable lockGrabble;
    private float elapsedTime = 0.0f;
    private ElectricalSwitchScript electricalSwitchScript;

    [HideInInspector] public bool animationDone = false;
    public Transform animationStartPos;
    private string lockTag = "MultipleLock";
    public float duration = 2.0f;


    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        electricalSwitchScript=GetComponentInParent<ElectricalSwitchScript>();
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
        lockGameobject.GetComponent<Rigidbody>().useGravity = false;
        //lockGameobject.GetComponent<Rigidbody>().mass = 1;
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
        if (other.CompareTag(lockTag) && lockEntered == false)
        {
            StartCoroutine(OnTriggerEnterCoroutine(other));

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(lockTag))
        {

            Debug.Log("triggerExit tag: " + other.tag);
            meshRenderer.enabled = false;

            lockGrabble.HandsRelease();
            lockGrabble.enabled = false;
            //lockGameobject.SetActive(false);
            
            if (electricalSwitchScript != null)
            {
                electricalSwitchScript.SwitchRotateReset();
            }

            boxCollider.enabled = false;
            Debug.Log("Multiple loto c�karildi");
            LOTOSceneManagement.stage = 21;
            lockGameobject.GetComponent<Rigidbody>().useGravity = true;
        }
    }

    public void moveLockToAnimStart()
    {
        if (lockEntered == true && animationDone == false)
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
            else if (lockAnimatior.enabled == false)
            {
                //lockGameobject.GetComponent<Rigidbody>().mass = 100000000;
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
        Debug.Log("MultipleLOto tak�ldi");
        LOTOSceneManagement.stage = 9;
        
    }
}
