using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerLife : MonoBehaviour
{
	private Animator animator;
	private Rigidbody2D rb;
	private PlayerMovement pm;
	[SerializeField] private AudioSource deathSoundEffect;

    [SerializeField] private Text livesText;

    // Start is called before the first frame update
    private void Start()
	{
		animator = GetComponent<Animator>();
		rb = GetComponent<Rigidbody2D>();
		pm = GetComponent<PlayerMovement>();

		int playerState = PlayerPrefs.GetInt(GameConstant.playerState);
		if (playerState == 0)
		{
            PlayerPrefs.SetInt(GameConstant.livesRest, PlayerPrefs.GetInt(GameConstant.livesRest) - 1);
			PlayerPrefs.SetInt(GameConstant.playerState, 1);
            PlayerPrefs.Save();
        }

        livesText.text = "Lives: " + PlayerPrefs.GetInt(GameConstant.livesRest);
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("Trap"))
		{
			Die();
        }
	}

	private void Die()
	{
        pm.keepIdle = true;
        deathSoundEffect.Play();
		rb.bodyType = RigidbodyType2D.Static;
		animator.SetTrigger("death");

		PlayerPrefs.SetInt(GameConstant.playerState, 0);
		PlayerPrefs.Save();
	}

    private void RestartLevel()
	{
		// N?u còn m?ng thì load l?i, h?t m?ng thì quay v? menu
		if (PlayerPrefs.GetInt(GameConstant.livesRest) - 1 > 0)
		{
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
		else
		{
            PlayerPrefs.SetInt(GameConstant.playerState, 1);
            PlayerPrefs.Save();
            BackToMenu();
        }
    }

	private void BackToMenu()
	{
        if (PlayerPrefs.GetInt(GameConstant.currentScore) > PlayerPrefs.GetInt(GameConstant.highScore))
        {
            PlayerPrefs.SetInt(GameConstant.highScore, PlayerPrefs.GetInt(GameConstant.currentScore));
            PlayerPrefs.Save();
        }
        SceneManager.LoadScene(0);
    }
}