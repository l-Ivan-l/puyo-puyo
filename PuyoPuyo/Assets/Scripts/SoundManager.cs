using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    private AudioSource audioMaster;
    public List<AudioClip> audioClipsList = new List<AudioClip>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        audioMaster = GetComponent<AudioSource>();
    }

    public void PlayMoveSound(float volume)
    {
        audioMaster.PlayOneShot(audioClipsList[0], volume);
    }

    public void PlayPlacedSound(float volume)
    {
        audioMaster.PlayOneShot(audioClipsList[1], volume);
    }

    public void PlayChainSound(float volume)
    {
        audioMaster.PlayOneShot(audioClipsList[2], volume);
    }

    public void PlayComboSound(float volume)
    {
        audioMaster.PlayOneShot(audioClipsList[3], volume);
    }

    public void PlayButtonSound(float volume)
    {
        audioMaster.PlayOneShot(audioClipsList[4], volume);
    }

    public void PlayGameOverSound(float volume)
    {
        audioMaster.PlayOneShot(audioClipsList[5], volume);
    }
}
