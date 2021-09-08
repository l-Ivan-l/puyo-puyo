using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverController : MonoBehaviour
{
    public TextMeshProUGUI scoreUI;
    public GameObject newRecordUI;
    // Start is called before the first frame update
    void Start()
    {
        ShowScore();
    }

    void ShowScore()
    {
        scoreUI.text = "SCORE: " + Singleton.Instance.Score.ToString();
        if(Singleton.Instance.NewRecord)
        {
            newRecordUI.SetActive(true);
            Singleton.Instance.NewRecord = false;
        }
    }

    public void PlayAgain()
    {
        FindObjectOfType<SceneLoader>().LoadScene("GameplayScene");
    }

    public void ReturnToMenu()
    {
        FindObjectOfType<SceneLoader>().LoadScene("MenuScene");
    }
}
