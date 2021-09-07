using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    //Score variables
    private int score = 0;
    private int scoreBaseValue = 40;
    private int comboBaseValue = 55;
    public TextMeshProUGUI scoreUI;
    public TextMeshProUGUI comboUI;

    private bool gameOver = false;

    public void AddScore(int _chainSize)
    {
        score += scoreBaseValue * _chainSize;
        scoreUI.text = "SCORE: " + score.ToString();
    }

    public void ShowCurrentCombo(int _currentCombo)
    {
        comboUI.gameObject.SetActive(true);
        comboUI.text = _currentCombo + " combo chain";
    }

    public IEnumerator AddChainComboScore(int _comboSize)
    {
        yield return new WaitForSeconds(0.75f);
        score += comboBaseValue * _comboSize;
        scoreUI.text = "Score: " + score.ToString();
        comboUI.gameObject.SetActive(false);
    }

    public void GameOver()
    {
        gameOver = true;
    }
}
