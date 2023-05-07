using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ItemCollector : MonoBehaviour
{

	[SerializeField] private Text cherriesText;
    [SerializeField] private Text livesText;
	[SerializeField] private AudioSource collectSoundEffect;

	private void Start()
	{
        // Khi b?t ??u, ??t s? ?i?m hi?n t?i = s? ?i?m t?ng c�c v�ng tr??c
        PlayerPrefs.SetInt(GameConstant.currentScore, PlayerPrefs.GetInt(GameConstant.stageScore));
		PlayerPrefs.Save();
        cherriesText.text = "Cherries: " + PlayerPrefs.GetInt(GameConstant.currentScore);
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Cherry"))
		{
			collectSoundEffect.Play();
			Destroy(collision.gameObject);

			// ?n cherry t?ng ?i?m hi?n t?i l�n 1
			PlayerPrefs.SetInt(GameConstant.currentScore, PlayerPrefs.GetInt(GameConstant.currentScore) + 1);
			PlayerPrefs.Save();
			cherriesText.text = "Cherries: " + PlayerPrefs.GetInt(GameConstant.currentScore);
		}

        if (collision.gameObject.CompareTag("Melon"))
        {
            collectSoundEffect.Play();
            Destroy(collision.gameObject);

            // ?n melon t?ng ?i?m hi?n t?i l�n 2
            PlayerPrefs.SetInt(GameConstant.currentScore, PlayerPrefs.GetInt(GameConstant.currentScore) + 2);
            PlayerPrefs.Save();
            cherriesText.text = "Cherries: " + PlayerPrefs.GetInt(GameConstant.currentScore);
        }

        if (collision.gameObject.CompareTag("Apple"))
        {
            collectSoundEffect.Play();
            Destroy(collision.gameObject);

            // ?n t�o t?ng m?ng l�n 1
            PlayerPrefs.SetInt(GameConstant.livesRest, PlayerPrefs.GetInt(GameConstant.livesRest) + 1);
            PlayerPrefs.Save();
            livesText.text = "Lives: " + PlayerPrefs.GetInt(GameConstant.livesRest);
        }
    }
}
