using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public bool GameIsPaused = false;

    public GameObject pauseMenuUI;
    public GameObject gameMenuUI;
    public GameObject gameMenuUITwo;
    public GameObject playDisplay;

    public GameObject OptionsMenu;

    public Camera_Shake shakeCheck;
    public Player mouseControl;
    public Player audioCheck;


    // audiosource references
    public AudioSource _player;
    public List<AudioSource> _sentry;
    public List<AudioSource> _fence;


    // Update is called once per frame
    void Update()
    {
        if (IntroFade.introPlaying == false)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (GameIsPaused)
                {
                    Resume();
                }
                else
                {
                    Pause();
                }
            }
        }
    }

    public void Resume ()
    {
        _player.Play();
        for (int i = 0; i < _sentry.Count; i++)
        {
            _sentry[i].Play();
        }

        for (int i = 0; i < _fence.Count; i++)
        {
            _fence[i].Play();
        }

        mouseControl.TurnOffMouse();

        if (OptionsMenu.activeSelf == true)
        {
            OptionsMenu.SetActive(false);
        }

        playDisplay.SetActive(true);
        shakeCheck.shakeAmount = 1f;
        gameMenuUI.SetActive(true);
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false; 
    }

    void Pause ()
    {
        _player.Pause();
        for (int i = 0; i < _sentry.Count; i++)
        {
            _sentry[i].Pause();
        }

        for (int i = 0; i < _fence.Count; i++)
        {
            _fence[i].Pause();
        }

        playDisplay.SetActive(false);
        shakeCheck.shakeAmount = 0f;
        GameIsPaused = true;
        mouseControl.TurnOnMouse();
        gameMenuUI.SetActive(false);
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;

    }

    public void RestartLevel()
    {
        Debug.Log("Restarting Level...");
        Time.timeScale = 1f;
        SceneManager.LoadScene("LevelOne");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
