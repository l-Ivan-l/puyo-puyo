using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    private void Start()
    {
        Application.targetFrameRate = 120;
    }

    public void Play()
    {
        FindObjectOfType<SceneLoader>().LoadScene("GameplayScene");
    }

    public void ReturnToMenu()
    {
        FindObjectOfType<SceneLoader>().LoadScene("MenuScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void SelectButton(Button _button)
    {
        _button.Select();
    }
}
