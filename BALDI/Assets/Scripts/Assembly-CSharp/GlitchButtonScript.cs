using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlitchButtonScript : MonoBehaviour
{
    public Button button;
    public MouseoverScript moS;
    // Start is called before the first frame update
    private void Start()
    {
        if (PlayerPrefs.GetInt("ClassicSecret") == 1)
        {
            button.enabled = true;
            moS.enabled = true;
            return;
        }
        button.enabled = false;
        moS.enabled = false;
    }
}
