using Autohand;
//using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class WarningSignPlaceHolder : MonoBehaviour
{
    private BoxCollider boxCollider;
    private GameObject placeAbleGameObject;
    private Grabbable placeAbleObjectGrabble;
    private string ObjectTag = "WarningSign";

    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(ObjectTag) && LOTOSceneManagement.stage == 2)
        {

            placeAbleGameObject = other.gameObject;
            //boxCollider.enabled = false;
            placeAbleObjectGrabble = placeAbleGameObject.GetComponent<Grabbable>();
            placeAbleObjectGrabble.HandsRelease();
            placeAbleObjectGrabble.enabled = false;

            gameObject.GetComponent<MeshRenderer>().enabled = false;
            placeAbleGameObject.GetComponent<Rigidbody>().isKinematic = true;
            placeAbleGameObject.transform.position = transform.position;
            placeAbleGameObject.transform.rotation = transform.rotation;
            LOTOSceneManagement.stage = 3;
            Debug.Log("WarningSignAs�ldi");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(ObjectTag) && LOTOSceneManagement.stage == 24)
        {
            placeAbleGameObject.GetComponent<Rigidbody>().useGravity = true;
            //Destroy(placeAbleGameObject, 1f);
            Debug.Log("warning Sign kald�r�ld�");
            LOTOSceneManagement.stage = 25;
        }

    }
}
