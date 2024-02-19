using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
    /*
    public static LoadingManager instance;
    

    public GameObject LoadingPanel;
    private float MinLoadTime = 2f;

    public GameObject LoadingWheel;
    private float WheelSpeed=0.5f;

    private string targetScene;
    private bool isLoading;

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            LoadScene("ValeoFireTraining");
        }
    }
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        LoadingPanel.SetActive(false);
    }
    public void LoadScene(string sceneName)
    {
        targetScene = sceneName;
        StartCoroutine(LoadSceneRoutine());
    }
    private IEnumerator LoadSceneRoutine()
    {
        isLoading = true;

        LoadingPanel.SetActive (true);
        StartCoroutine(SpinWheelRoutine());
        AsyncOperation op =SceneManager.LoadSceneAsync(targetScene);
        float elapsedLoadTime = 0f;

        while (!op.isDone) 
        {
            elapsedLoadTime += Time.deltaTime;
            yield return null;

        }
        while (elapsedLoadTime < MinLoadTime) 
        {
            elapsedLoadTime += Time.deltaTime;
            yield return null;
        }
        LoadingPanel.SetActive (false);
        isLoading = false;
    }
    private IEnumerator SpinWheelRoutine() 
    {
        while (isLoading)
        {
            LoadingWheel.transform.Rotate(0, 0, -WheelSpeed);
            yield return null;
        }
    }
    
    */

    //public GameObject canvas;
    //public GameObject LoadingWheel;
    public FadeScreen fadeScreen;
    public void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("space");
            GoToScene(1);
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            Debug.Log("space");
            GoToScene(2);
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            Debug.Log("space");
            GoToScene(3);
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            Debug.Log("space");
            GoToScene(4);
        }*/
    }
    public void GoToScene(int sceneIndex)
    {

        StartCoroutine(GotoSceneRoutine(sceneIndex));

    }

    IEnumerator GotoSceneRoutine(int sceneIndex)
    {
        fadeScreen.FadeOut();
        float timer = 0;
        while (timer < fadeScreen.fadeDuration)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        operation.allowSceneActivation = false;
        operation.allowSceneActivation = true;
    }


}
