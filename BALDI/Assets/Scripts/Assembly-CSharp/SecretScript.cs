using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SecretScript : MonoBehaviour
{
	public void LoadScene()
	{
		SceneManager.LoadScene("Secret");
	}

	
	public void unKillNull()
	{
		PlayerPrefs.SetInt("NullDefeated", 0);
	}
}
