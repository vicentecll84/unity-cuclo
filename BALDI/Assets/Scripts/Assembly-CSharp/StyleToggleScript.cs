using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StyleToggleScript : MonoBehaviour
{
    [SerializeField] private TMP_Text instructionsText;

    [SerializeField] private string StyleRequirement;

    [SerializeField] private Toggle setting;

    [Header("PLAYERPREFS ARE CaSe-SeNsItIvE!")]
    [Header("SCRIPT ONLY SUPPORTS INT PLAYERPREFS")]

    [SerializeField] private string playerPrefRequirement;

    [SerializeField] private string playerPref;

    private void Start()
    {
        instructionsText.text = "Beat " + StyleRequirement + " Story Mode to unlock!";
        if (PlayerPrefs.GetInt(playerPrefRequirement) == 1)
        {
            instructionsText.gameObject.SetActive(false);
            setting.gameObject.SetActive(true);
        }
    	if (PlayerPrefs.GetInt(playerPref) == 1)
		{
			setting.isOn = true;
		}
		else
		{
			setting.isOn = false;
		}
    }

    private void Update()
    {
        if (setting.isOn)
        {
            PlayerPrefs.SetInt(playerPref, 1);
        }
        else
        {
            PlayerPrefs.SetInt(playerPref, 0);
        }
    }
}
