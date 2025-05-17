using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x020000D2 RID: 210
public class StyleButton : MonoBehaviour
{
    public void SetStyle()
    {
        PlayerPrefs.SetString("CurrentStyle", currentStyle.ToString().ToLower());
    }

	// Token: 0x04000715 RID: 1813
	public StyleButton.Style currentStyle;

	// Token: 0x020000D3 RID: 211
	public enum Style
	{
		// Token: 0x04000717 RID: 1815
		Classic,
		// Token: 0x04000718 RID: 1816
		Glitch
	}
}