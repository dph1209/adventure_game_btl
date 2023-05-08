using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    private AudioSource finishSound;
    private GameObject _gameObject = null;

    // Start is called before the first frame update
    private void Start()
    {
        finishSound = GetComponent<AudioSource>();
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.gameObject.name == "Player" && _gameObject == null)
        {
            finishSound.Play();
            _gameObject = collision.gameObject;
            Invoke("SetToStatic", 1f);
            Invoke("CompleteLevel", 2f);
        }
	}

    private void SetToStatic()
    {
        if (_gameObject != null)
        {
            _gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            _gameObject.GetComponent<PlayerMovement>().keepIdle = true;
        }
    }

    private void CompleteLevel()
    {
        // Gán s? ?i?m ?ã ki?m ???c vào s? ?i?m t?ng
        PlayerPrefs.SetInt(GameConstant.stageScore, PlayerPrefs.GetInt(GameConstant.currentScore));

        if (SceneManager.GetActiveScene().buildIndex == SceneManager.sceneCountInBuildSettings - 2 
            && PlayerPrefs.GetInt(GameConstant.currentScore) > PlayerPrefs.GetInt(GameConstant.highScore))
        {
            PlayerPrefs.SetInt(GameConstant.highScore, PlayerPrefs.GetInt(GameConstant.currentScore));
        }

        PlayerPrefs.Save();

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
