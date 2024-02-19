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
        // Ba�l�k tak�l� oldu�unda yap�lacak i�lemler
        HeadsetOffCanvas.gameObject.SetActive(false);

        Debug.Log("ba�l�k tak�ld�");
        
    }

    void HandleHMDUnmounted()
    {
        HeadsetOffCanvas?.gameObject.SetActive(true);

        Debug.Log("ba�l�k tak�lmad�");
        // Ba�l�k ��kart�ld���nda yap�lacak i�lemler
    }
}
