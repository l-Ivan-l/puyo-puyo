using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    //Score variables
    private int score = 0;
    private int scoreBaseValue = 40;
    private int comboBaseValue = 55;
    public TextMeshProUGUI scoreUI;
    public TextMeshProUGUI comboUI;
    private Animator scoreAnim;
    public AnimationClip scoreAddedClip;
    private Animator comboAnim;
    public AnimationClip comboHideClip;
    public Animator ecoloAnim;
    public SpriteRenderer ecoloFaceRenderer;
    public Sprite ecoloSadFaceSprite;

    public GameObject pausePanel;
    public GameObject gameplayPanel;
    public Button resumeButton;
    private AudioSource gameplayMusic;

    public bool paused;

    private void Awake()
    {
        scoreAnim = scoreUI.GetComponent<Animator>();
        comboAnim = comboUI.GetComponent<Animator>();
        gameplayMusic = GetComponent<AudioSource>();
    }

    public void AddScore(int _chainSize)
    {
        score += scoreBaseValue * _chainSize;
        scoreUI.text = "SCORE: " + score.ToString();
        scoreAnim.Play(scoreAddedClip.name);
        ecoloAnim.SetTrigger("Shake");
    }

    public void ShowCurrentCombo(int _currentCombo)
    {
        SoundManager.instance.PlayComboSound(1f);
        comboUI.gameObject.SetActive(true);
        comboUI.text = _currentCombo + " combo chain";
    }

    public IEnumerator AddChainComboScore(int _comboSize)
    {
        yield return new WaitForSeconds(0.7f);
        score += comboBaseValue * _comboSize;
        scoreUI.text = "SCORE: " + score.ToString();
        ecoloAnim.SetTrigger("Shake");
        scoreAnim.Play(scoreAddedClip.name);
        comboAnim.Play(comboHideClip.name);
        yield return new WaitForSeconds(0.1f);
        comboUI.gameObject.SetActive(false);
    }

    public IEnumerator GameOver()
    {
        ecoloAnim.SetTrigger("Shake");
        SoundManager.instance.PlayGameOverSound(1f);
        ecoloFaceRenderer.sprite = ecoloSadFaceSprite;
        if(score > Singleton.Instance.Score) //New record achieved
        {
            Singleton.Instance.NewRecord = true;
        }
        Singleton.Instance.Score = score;
        yield return new WaitForSeconds(1.25f);
        FindObjectOfType<SceneLoader>().LoadScene("GameOverScene");
    }

    public void Pause()
    {
        if(!paused)
        {
            paused = true;
            gameplayMusic.Pause();
            SoundManager.instance.PlayButtonSound(1f);
            gameplayPanel.SetActive(false);
            pausePanel.SetActive(true);
            resumeButton.Select();
            Time.timeScale = 0f;
        }
        else
        {
            Resume();
        }
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        paused = false;
        gameplayMusic.Play();
        SoundManager.instance.PlayButtonSound(1f);
        pausePanel.SetActive(false);
        gameplayPanel.SetActive(true);
    }

    public void ReturnToMenu()
    {
        Time.timeScale = 1f;
        SoundManager.instance.PlayButtonSound(1f);
        FindObjectOfType<SceneLoader>().LoadScene("MenuScene");
    }
}
