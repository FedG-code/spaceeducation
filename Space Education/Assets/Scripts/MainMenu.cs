 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MainMenu : MonoBehaviour


{
 public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

// load the last scene
 public void CreditsLoader()
    {
        SceneManager.LoadScene(SceneManager.sceneCountInBuildSettings-1);
    }

 public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    public AudioMixer audioMixer;
 public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume",volume);
    }
}
