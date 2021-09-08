using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private AsyncOperation operation;
    private Canvas canvas;
    private static SceneLoader loaderInstance;

    private void Awake()
    {
        canvas = GetComponentInChildren<Canvas>(true);
        DontDestroyOnLoad(gameObject);

        if (loaderInstance == null)
        {
            loaderInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadScene(string _sceneName)
    {
        canvas.gameObject.SetActive(true);
        StartCoroutine(LoadAsyncScene(_sceneName));
    }

    IEnumerator LoadAsyncScene(string _sceneName)
    {
        operation = SceneManager.LoadSceneAsync(_sceneName);
        while (!operation.isDone)
        {
            yield return null;
        }
        operation = null;
        //Debug.Log(_sceneName + " Loaded");
        canvas.gameObject.SetActive(false);
    }

}
