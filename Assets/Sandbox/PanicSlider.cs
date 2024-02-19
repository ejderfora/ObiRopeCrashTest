using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PanicSlider : MonoBehaviour
{
    public Text TimerText;
    public PanicButton panicButton;
    float time = 0;
    public Image fill;
    public float Max;
    
    public Slider slider1;
    // Update is called once per frame
    void Update()
    {
        time = panicButton.timer;
        TimerText.text = "" + (int)time;
        fill.fillAmount = time / panicButton.panicExitTime;
        

    }
}
