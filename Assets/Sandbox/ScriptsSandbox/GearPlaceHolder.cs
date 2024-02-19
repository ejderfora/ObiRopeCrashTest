using Autohand;
using Obi;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearPlaceHolder : MonoBehaviour
{
    private BoxCollider boxCollider;
    private GameObject carkGameobject;
    private MeshRenderer meshRenderer;
    public GameObject gearPlacePosition;

    private Grabbable carkGrabble;
    private bool canPut;

    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        meshRenderer = gameObject.GetComponent<MeshRenderer>();
        meshRenderer.enabled= false;
    }

    // Update is called once per frame
    void Update()
    {
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Gear"))
        {
            if (canPut==true && LOTOSceneManagement.stage == 15)
            {
                carkGameobject = other.gameObject;
                boxCollider.enabled = false;
                carkGrabble = carkGameobject.GetComponent<Grabbable>();
                carkGrabble.HandsRelease();
                carkGrabble.enabled = false;

                meshRenderer.enabled = false;
                carkGameobject.GetComponent<Rigidbody>().isKinematic = true;

                carkGameobject.transform.position = gearPlacePosition.transform.position;
                carkGameobject.transform.rotation = gearPlacePosition.transform.rotation;
                Debug.Log("bozuk parca cikti");
                LOTOSceneManagement.stage = 16;

            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("BrokenGear"))
        {
            meshRenderer.enabled = true;
            canPut = true;
            other.gameObject.GetComponent<Rigidbody>().useGravity = true;
            Debug.Log("bozuk parca cikti");
            LOTOSceneManagement.stage = 15;
        }
    }
}
