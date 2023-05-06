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
        // G�n s? ?i?m ?� ki?m ???c v�o s? ?i?m t?ng
        PlayerPrefs.SetInt(GameConstant.stageScore, PlayerPrefs.GetInt(GameConstant.currentScore));

        // Ki?m tra n?u ch?i h?t m�n cu?i v� ?i?m t?ng l?n h?n ?i?m cao nh?t th� g�n cho ?i?m cao nh?t
        if (SceneManager.GetActiveScene().buildIndex == SceneManager.sceneCountInBuildSettings - 2 
            && PlayerPrefs.GetInt(GameConstant.stageScore) > PlayerPrefs.GetInt(GameConstant.highScore))
        {
            PlayerPrefs.SetInt(GameConstant.highScore, PlayerPrefs.GetInt(GameConstant.stageScore));
        }

        PlayerPrefs.Save();

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
