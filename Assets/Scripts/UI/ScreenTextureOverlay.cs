using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenTextureOverlay : MonoBehaviour
{

    public static ScreenTextureOverlay Instance;

    [SerializeField] private RawImage rawImage;

    private Color transparentColor;

    Coroutine fadeCoroutine;
    private bool fadeIsRunning = false;





    private void Awake(){
        if (Instance == null) {
            Instance = this;
        } else {
            Debug.LogError("There are more than one screentextureOverlays!");
        }

        transparentColor = Color.clear;
    }
    


    public void SetTexture(Texture texture) {
        rawImage.texture = texture;

        
    }

    public void SetColor(Color color) {
        rawImage.color = color;
    }

    public void FadeToColor(Color color, float fadeTime) {
        if (fadeIsRunning) {
            StopCoroutine(fadeCoroutine);
        }
        fadeCoroutine = StartCoroutine(FadeColorCoroutine(color,fadeTime));
    }
    public void FadeOut(float fadeTime) {
        FadeToColor(Color.clear,fadeTime);
    }

    private IEnumerator FadeColorCoroutine(Color colorToFadeTo, float fadeTime) {

        fadeIsRunning = true;

        float timePassed = 0f;
        Color startColor = rawImage.color;

        while (timePassed < fadeTime) {
            rawImage.color = Color.Lerp(startColor, colorToFadeTo, timePassed/fadeTime);
            timePassed += Time.deltaTime;
            yield return null;
        } 
        rawImage.color = colorToFadeTo;

        fadeIsRunning = false;

    }


}
