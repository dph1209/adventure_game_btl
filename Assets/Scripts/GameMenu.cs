using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{
    [SerializeField] private Text highScoreText;
    [SerializeField] private Text soundText;
    
    public GameObject pauseMenu;
    public static bool IsPause;
    public static bool IsMuted = false;


    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
        soundText.text = IsMuted ? "Sound: Off" : "Sound: On";
    }

    void OnEnable()
    {
        soundText.text = IsMuted ? "Sound: Off" : "Sound: On";
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name.Equals("Start Screen")
            || SceneManager.GetActiveScene().name.Equals("End Screen"))
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsPause)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void StartGame()
    {
        // Khi b?t ??u game, s? m?ng = 3, s? ?i?m = 0
        PlayerPrefs.SetInt(GameConstant.livesRest, GameConstant.defaultLives);
        PlayerPrefs.Save();
        PlayerPrefs.SetInt(GameConstant.currentScore, 0);
        PlayerPrefs.Save();
        PlayerPrefs.SetInt(GameConstant.stageScore, 0);
        PlayerPrefs.Save();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    
    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        IsPause = true;
    }
    
    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        IsPause = false;
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("Start Screen");
        ResumeGame();
    }
    
    public void SoundSwitch()
    {
        IsMuted = !IsMuted;
        AudioListener.volume = IsMuted ? 0 : 1;
        soundText.text = IsMuted ? "Sound: Off" : "Sound: On";
    }

    public void QuitGame()
    {
#if UNITY_STANDALONE
        Application.Quit();
#endif

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}