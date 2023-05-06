using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
	[SerializeField] private Text highScoreText;
	private void Start()
	{
		// Hi?n th? high score, n?u không có thì m?c ??nh là 0
		highScoreText.text = "HIGH SCORE: " + PlayerPrefs.GetInt(GameConstant.highScore, 0);
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
}
