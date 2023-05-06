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
		
		// N?u s? m?ng = 0 thì gán l?i s? m?ng = t?ng s? m?ng
		int lives = PlayerPrefs.GetInt(GameConstant.livesRest) > 0 ? PlayerPrefs.GetInt(GameConstant.livesRest) : GameConstant.defaultLives;
		if (lives == GameConstant.defaultLives)
		{
            PlayerPrefs.SetInt(GameConstant.livesRest, lives);	// S? m?ng còn l?i = t?ng s? m?ng
			PlayerPrefs.Save();
        }
        livesText.text = "Lives: " + lives;
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

		// ??t s? m?ng còn l?i -= 1
		PlayerPrefs.SetInt(GameConstant.livesRest, PlayerPrefs.GetInt(GameConstant.livesRest) - 1);
		PlayerPrefs.Save();
		Debug.Log(PlayerPrefs.GetInt(GameConstant.livesRest));
	}

    private void RestartLevel()
	{
		// N?u còn m?ng thì load l?i, h?t m?ng thì quay v? menu
		if (PlayerPrefs.GetInt(GameConstant.livesRest) > 0)
		{
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
		else
		{
			BackToMenu();
        }
    }

	private void BackToMenu()
	{
		SceneManager.LoadScene(0);
    }
}