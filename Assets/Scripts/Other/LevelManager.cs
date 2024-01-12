using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    public static LevelManager Instance;
    [SerializeField] private ScreenFade screenFade;

    [SerializeField] private string firstSceneToLoad;

    private bool waitingToLoad = false;

    private Vector3 playerSpawnPosition;

    private string sceneToLoadNext;
    

    public event EventHandler<EventArgs> sceneLoadInitiated;
    public event EventHandler<EventArgs> sceneChanged;




    private void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    private void Start() {
        screenFade.fadeInFinnished += ScreenFade_FadeInFinnished;
        SceneManager.LoadScene(firstSceneToLoad);
    }


    public void LoadScene(string sceneName, Vector3 playerSpawnPosition) {

        if (!waitingToLoad) {
            sceneLoadInitiated?.Invoke(this,EventArgs.Empty); // call the scene change event
            screenFade.StartFadeIn();
            waitingToLoad = true;
            sceneToLoadNext = sceneName;

            this.playerSpawnPosition = playerSpawnPosition;

        } else {
            Debug.Log("Already trying to load a scene!");
        }
    }


    private void ScreenFade_FadeInFinnished(object sender, EventArgs e)
    {
        if (waitingToLoad) {

            StartCoroutine(LoadLevelAsync());

            // SceneManager.LoadScene(sceneToLoadNext);
            // sceneChanged?.Invoke(this,EventArgs.Empty);
            // screenFade.StartFadeOut();
            // waitingToLoad = false;

            // if (playerSpawnPosition != null) {
            //     Player.Instance.transform.position = playerSpawnPosition;
            // }

            
        }
    }

    private IEnumerator LoadLevelAsync() {


        AsyncOperation progress = SceneManager.LoadSceneAsync(sceneToLoadNext,LoadSceneMode.Single);

        while (!progress.isDone) {
            yield return null;
        }

        sceneChanged?.Invoke(this,EventArgs.Empty);
        screenFade.StartFadeOut();
        waitingToLoad = false;

        if (playerSpawnPosition != null) {
            Player.Instance.transform.position = playerSpawnPosition;
        }
    }
    



}
