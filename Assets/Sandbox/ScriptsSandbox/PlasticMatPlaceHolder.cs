using Autohand;
//using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class PlasticMatPlaceHolder : MonoBehaviour
{
    private BoxCollider boxCollider;
    private GameObject placeAbleGameObject;
    private Grabbable placeAbleObjectGrabble;
    private string ObjectTag = "PlasticMat";

    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(ObjectTag) && LOTOSceneManagement.stage == 6)
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
            Debug.Log("Plastic mat koyuldu");
            LOTOSceneManagement.stage = 7;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(ObjectTag) && LOTOSceneManagement.stage == 22)
        {
            placeAbleGameObject.GetComponent<Rigidbody>().useGravity = true;
            //Destroy(placeAbleGameObject, 1f);
            Debug.Log("Plastic kald�r�ld�");
            LOTOSceneManagement.stage = 23;
        }

    }
}
