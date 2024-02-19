using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // UnityEngine.UI eklemeyi unutmayýn.

public class FadeScreen : MonoBehaviour
{
    public float fadeDuration = 2f;
    public bool fadeOnStart = true;
    public Image loadingImage; // Image türünde deðiþken olarak kullanacaðýz.

    void Start()
    {
        if (fadeOnStart)
        {
            FadeIn();
        }
    }

    public void FadeIn()
    {
        Fade(1, 0);
    }

    public void FadeOut()
    {
        Fade(0, 1);
    }

    public void Fade(float alphaIn, float alphaOut)
    {
        StartCoroutine(FadeRoutine(alphaIn, alphaOut));
    }

    public IEnumerator FadeRoutine(float alphaIn, float alphaOut)
    {
        float timer = 0;
        Color currentColor = loadingImage.color;
        while (timer < fadeDuration)
        {
            float newAlpha = Mathf.Lerp(alphaIn, alphaOut, timer / (fadeDuration*1.5f));
            currentColor.a = newAlpha;
            loadingImage.color = currentColor; // Alpha deðerini güncelliyoruz.
            timer += Time.deltaTime;
            yield return null;
        }
        currentColor.a = alphaOut;
        loadingImage.color = currentColor; // Alpha deðerini son deðere ayarlýyoruz.
    }
}