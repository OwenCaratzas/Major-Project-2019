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

    public GameObject OptionsMenu;

    public Player mouseControl;
    public Player audioCheck;


    // audiosource references
    public AudioSource _player;
    public List<AudioSource> _sentry;
    public List<AudioSource> _fence;


    // Update is called once per frame
    void Update()
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
        SceneManager.LoadScene("LevelSelect");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
