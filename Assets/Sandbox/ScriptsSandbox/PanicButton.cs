using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.SceneManagement;

public class PanicButton : MonoBehaviour
{
    public GameObject Timer;
    private InputDevice targetDevice;
    List<InputDevice> devices = new List<InputDevice>();
    
    private InputDevice targetLeftDevice;
    private InputDevice targetRightDevice;

    
    [Header("Parameters")]
    [SerializeField] public float panicExitTime=3f;
    public string panicText = "Panik Çıkış Yapıldı";



    public float timer;
    private bool rightPrimaryButton;
    private bool rightSecondaryButton;
    private bool leftPrimaryButton;
    private bool leftSecondaryButton;
    void GetDevice()
    {
        /*InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.Right, devices);
        targetDevice = devices.FirstOrDefault();
        Debug.Log(targetDevice);
        */
        devices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.Controller, devices);

        foreach (var device in devices)
        {
            if (device.name.Contains("Oculus"))
            {
                if (device.characteristics.HasFlag(InputDeviceCharacteristics.Left))
                    targetLeftDevice = device;
                else if (device.characteristics.HasFlag(InputDeviceCharacteristics.Right))
                    targetRightDevice = device;
            }
        }
    }

    private void OnEnable()
    {
        if (!targetDevice.isValid)
        {
            GetDevice();
        }
    }

    private void Start()
    {
        timer = panicExitTime;
    }

    void Update()
    {
        if (!targetDevice.isValid)
        {
            GetDevice();
        }
        CheckInputs(targetRightDevice,targetLeftDevice);
        CheckPanicExit();
     
        
    }

    private void CheckPanicExit()
    {
        if (rightPrimaryButton && rightSecondaryButton)
        {
            Timer.gameObject.SetActive(true);
            panicText = $"ÇIKIŞ YAPILIYOR\nKalan Süre:{timer}";
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                Scene activeScene = SceneManager.GetActiveScene();
                SceneManager.LoadScene(activeScene.name);
            }
        }
        else
        {
            timer = panicExitTime;
            Timer.gameObject.SetActive(false);
        }
    }

    void CheckInputs(InputDevice rightTargetDevice, InputDevice leftTargetDevice)
    {
        if (rightTargetDevice != null && leftTargetDevice != null)
        {
            // Sağ eldeki tuşların kontrolü
            if (rightTargetDevice.TryGetFeatureValue(CommonUsages.primaryButton, out rightPrimaryButton) && rightPrimaryButton)
            {
                Debug.Log("Sağ elinde PrimaryTouch basıldı!");
            }

            if (rightTargetDevice.TryGetFeatureValue(CommonUsages.secondaryButton, out rightSecondaryButton) && rightSecondaryButton)
            {
                Debug.Log("Sağ elinde SecondaryTouch basıldı!");
            }

            // Sol eldeki tuşların kontrolü
            if (leftTargetDevice.TryGetFeatureValue(CommonUsages.primaryButton, out leftPrimaryButton) && leftPrimaryButton)
            {
                Debug.Log("Sol elinde PrimaryTouch basıldı!");
            }

            if (leftTargetDevice.TryGetFeatureValue(CommonUsages.secondaryButton, out leftSecondaryButton) && leftSecondaryButton)
            {
                Debug.Log("Sol elinde SecondaryTouch basıldı!");
            }
        }
    }

    
}
