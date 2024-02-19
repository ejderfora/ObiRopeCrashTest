using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Autohand;

public class PlaceHolderScript : MonoBehaviour
{
    private Renderer cubeRenderer; // Küpün renderer bileşeni
    private float cubeTransparancy = 0.25f;
    private Color baseColor;
    public bool isColliding = true; // "PlaceAble" tag'ine sahip bir nesne ile temas durumu
    private Transform otherTransform; // "PlaceAble" tag'ine sahip nesnenin transformu
    private Collider holderCollider;
    private Grabbable extinguisherGrabbable;

    private GameObject currentGameobject;



    private void Start()
    {
        cubeRenderer = GetComponent<Renderer>();
        cubeRenderer.enabled = false; // Başlangıçta renderer'ı kapat
        holderCollider = GetComponent<Collider>();
        baseColor = cubeRenderer.material.color;
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((other.CompareTag("PlaceAble") && currentGameobject == other.transform.parent.gameObject) || (other.CompareTag("PlaceAble") && currentGameobject == null))
        {
            //Debug.Log("entercalisti");//
            currentGameobject = other.transform.parent.gameObject;
            otherTransform = other.transform.parent;
            extinguisherGrabbable = otherTransform.gameObject.GetComponent<Grabbable>();

            TpBack();


        }
    }

    private void OnTriggerExit(Collider other)
    {
        if ((other.CompareTag("PlaceAble") && currentGameobject == null) || (other.CompareTag("PlaceAble") && currentGameobject == other.transform.parent.gameObject))
        {
            //RenkAyarla();
            //isColliding = false;&&isColliding==true
            // Debug.Log("exit çalıştı");
            holderCollider.enabled = false;
            StartCoroutine(EnableColliderDelay(1f));

        }
    }

    private void OnTriggerStay(Collider other)
    {

    }
    private void TpBack()
    {
        extinguisherGrabbable.HandsRelease();
        DisableCubeRenderer();
        otherTransform.position = transform.position;
        otherTransform.rotation = transform.rotation;
        otherTransform.gameObject.GetComponent<Rigidbody>().isKinematic = true;
    }

    private void EnableCubeRenderer()
    {
        //Debug.Log("Enable calisti");
        cubeRenderer.enabled = true;
    }

    private void DisableCubeRenderer()
    {

        //Debug.Log("disableda calisti");
        cubeRenderer.enabled = false;
    }

    private IEnumerator EnableRendererAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        EnableCubeRenderer();
    }
    private IEnumerator EnableColliderDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        holderCollider.enabled = true;
        cubeRenderer.material.color = baseColor;
    }
    private void RenkAyarla()
    {
        Color hologramColor = cubeRenderer.material.color;
        hologramColor.a = cubeTransparancy;
        cubeRenderer.material.color = hologramColor;
    }
    public void KinematicDisable()
    {
        otherTransform.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        EnableCubeRenderer();
    }
    public void GoBackCaller()
    {
        StartCoroutine(GoBackDelay(2f));
    }
    private IEnumerator GoBackDelay(float delay)
    {
        //Debug.Log("before tp isheld: " + extinguisherGrabbable.IsHeld());
        //Debug.Log("before tp beniggrabbed: " + extinguisherGrabbable.beingGrabbed);
        yield return new WaitForSeconds(delay);
        if (extinguisherGrabbable.IsHeld() == false)
        {
            // Debug.Log("isheld: " + extinguisherGrabbable.IsHeld());
            //Debug.Log("beniggrabbed: " + extinguisherGrabbable.beingGrabbed);
            TpBack();
        }
    }
}
