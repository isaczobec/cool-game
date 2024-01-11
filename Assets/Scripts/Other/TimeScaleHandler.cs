using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeScaleHandler : MonoBehaviour
{

    public static TimeScaleHandler Instance {get; private set;}

    private Coroutine fadeCouroutine;
    private bool currentlyFadingTimeScale = false;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetTimeScale(float targetTimeScale, float transitionDuration = 0f) {
        if (transitionDuration == 0f) {
            Time.timeScale = targetTimeScale;
        } else {
            if (currentlyFadingTimeScale) {
                StopCoroutine(fadeCouroutine);
            }
            fadeCouroutine = StartCoroutine(FadeTimeScale(targetTimeScale,transitionDuration));
        }
    }

    private IEnumerator FadeTimeScale(float targetTimeScale, float transitionDuration) {

        currentlyFadingTimeScale = true;

        float timePassed = 0f;

        float originalTimeScale = Time.timeScale;

        while (timePassed < transitionDuration) {
            
            Time.timeScale = Mathf.Lerp(originalTimeScale, targetTimeScale, timePassed/transitionDuration);
            
            timePassed += Time.fixedDeltaTime;

            yield return null;
        }

        Time.timeScale = targetTimeScale;

        currentlyFadingTimeScale = false;

        yield break;

    }
}
