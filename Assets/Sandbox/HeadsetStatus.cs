using UnityEngine;
using UnityEngine.UI;
public class HeadsetStatus : MonoBehaviour
{
    //public TextMeshProUGUI statusText;
    public GameObject HeadsetOffCanvas;

    void Start()
    {
        OVRManager.HMDMounted += HandleHMDMounted;
        OVRManager.HMDUnmounted += HandleHMDUnmounted;
        HeadsetOffCanvas.gameObject.SetActive(false);
    }

    void HandleHMDMounted()
    {
        // Baþlýk takýlý olduðunda yapýlacak iþlemler
        HeadsetOffCanvas.gameObject.SetActive(false);

        Debug.Log("baþlýk takýldý");
        
    }

    void HandleHMDUnmounted()
    {
        HeadsetOffCanvas?.gameObject.SetActive(true);

        Debug.Log("baþlýk takýlmadý");
        // Baþlýk çýkartýldýðýnda yapýlacak iþlemler
    }
}
