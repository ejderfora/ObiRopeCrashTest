using Autohand;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricalSwitchScript : MonoBehaviour
{
    public GameObject turnAbleSwitch;
    public GameObject lotoField;
    private Grabbable grabbable;
    private BoxCollider boxCollider;

    private float rotationAngle = 180;
    void Start()
    {
        lotoField.SetActive(false);
        grabbable = GetComponent<Grabbable>();
        boxCollider = GetComponent<BoxCollider>();
    }

    // Update is called once per frame//
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            StartCoroutine(RotateCoroutine(2f));
        }
    }
    public void SwitchGrabbed()
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
        Quaternion startRotation = turnAbleSwitch.transform.rotation;
        Quaternion targetRotation = turnAbleSwitch.transform.rotation * Quaternion.Euler(0,0, rotationAngle);

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            turnAbleSwitch.transform.rotation = Quaternion.Slerp(startRotation, targetRotation, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        turnAbleSwitch.transform.rotation = targetRotation; // Animasyon sonunda hedef rotasyona ulaþýlýr
        lotoField.SetActive(true);
        if (LOTOSceneManagement.stage==7)
        {
            Debug.Log("electric Salter kapatildii");
            LOTOSceneManagement.stage = 8;
        }
        if (LOTOSceneManagement.stage==21)
        {
            Debug.Log("electricalsalter acildi");
            LOTOSceneManagement.stage = 22;
        }
    }
    public void SwitchRotateReset()
    {
        grabbable.enabled = true;
        boxCollider.enabled = true;
        lotoField.SetActive(false);
    }

}
