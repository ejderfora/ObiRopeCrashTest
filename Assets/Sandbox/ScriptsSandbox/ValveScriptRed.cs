using Autohand;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValveScriptRed : MonoBehaviour
{
    public GameObject turnAbleValve;
    public GameObject lotoField;
    private Grabbable grabbable;
    private BoxCollider boxCollider;

    private float rotationAngle=180;
    void Start()
    {
        lotoField.SetActive(false);
        grabbable = GetComponent<Grabbable>();
        boxCollider=GetComponent<BoxCollider>();
    }

    // Update is called once per frame//
    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.B))
        {
            StartCoroutine(RotateCoroutine(2f));
        }*/
    }
    public void ValveGrabbed()
    {
        StartCoroutine(RotateCoroutine(2f));
    }

    private IEnumerator RotateCoroutine(float duration)
    {
        yield return new WaitForSeconds(0.03f);
        grabbable.HandsRelease();
        yield return new WaitForSeconds(0.03f);
        grabbable.enabled = false;
        boxCollider.enabled = false;
        float elapsedTime = 0f;
        Quaternion startRotation = turnAbleValve.transform.rotation;
        Quaternion targetRotation = turnAbleValve.transform.rotation * Quaternion.Euler(0, rotationAngle, 0);

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            turnAbleValve.transform.rotation = Quaternion.Slerp(startRotation, targetRotation, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        if (LOTOSceneManagement.stage==9)
        {
            Debug.Log("k�rm�z� vana kapand�");
            LOTOSceneManagement.stage = 10;
        }
        if (LOTOSceneManagement.stage==19)
        {
            Debug.Log("k�rm�z� vana acildi");
            LOTOSceneManagement.stage = 20;
        }
        
        
        turnAbleValve.transform.rotation = targetRotation; // Animasyon sonunda hedef rotasyona ula��l�r
        lotoField.SetActive(true);
    }
    public void ValveRotateReset()
    {
        grabbable.enabled = true;
        boxCollider.enabled = true;
        lotoField.SetActive (false);
    }
    
    
}
