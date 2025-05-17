using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x020000B1 RID: 177
public class Script : MonoBehaviour
{
	// Token: 0x0600091C RID: 2332 RVA: 0x00020AA5 File Offset: 0x0001EEA5
	private void Awake()
	{
		renderer = GetComponent<SpriteRenderer>();
		if (PlayerPrefs.GetInt("NullDefeated") >= 1)
		{
			fakebaldi.SetActive(false);
			renderer.enabled = false;
			baldiSprite.SetActive(true);
			banana.SetActive(false);
			apple.SetActive(true);
			return;
		}
	}

	// Token: 0x0600091D RID: 2333 RVA: 0x00020AA7 File Offset: 0x0001EEA7
	private void Update()
	{
		if (!this.audioDevice.isPlaying & this.played)
		{
			if (PlayerPrefs.GetInt("NullDefeated") == 0)
			{
				if (Application.isEditor)
				{
					SceneManager.LoadScene("MainMenu");
				}
				else
				{
					Application.Quit();
				}
			}
			else
			{
				SceneManager.LoadScene("MainMenu");
			}
		}
	}

	// Token: 0x0600091E RID: 2334 RVA: 0x00020ACB File Offset: 0x0001EECB
	private void OnTriggerEnter(Collider other)
	{
		if (other.name == "Player" & !this.played)
		{
			PlayerPrefs.SetInt("ClassicSecret", 1);
			if (PlayerPrefs.GetInt("NullDefeated") == 0)
			{
				this.audioDevice.clip = this.audios[0];
			}
			else
			{
				this.audioDevice.clip = this.audios[1];
			}
			this.audioDevice.Play(); // Play the audio
			this.played = true;
		}
	}

	// Token: 0x040005A7 RID: 1447
	public AudioSource audioDevice;

	public AudioClip[] audios;

	private SpriteRenderer renderer;

	public GameObject fakebaldi;

	public GameObject banana;

	public GameObject apple;

	public GameObject baldiSprite;

	// Token: 0x040005A8 RID: 1448
	private bool played;
}
