
﻿using UnityEngine;

// Token: 0x0200000B RID: 11
public class Billboard : MonoBehaviour
{
	// Token: 0x0600002E RID: 46 RVA: 0x00003CE0 File Offset: 0x00001EE0
	private void LateUpdate()
	{
		if (Time.timeScale != 0f)
		{
			base.transform.LookAt(new Vector3((float)UnityEngine.Random.Range(0, 360), (float)UnityEngine.Random.Range(0, 360), (float)UnityEngine.Random.Range(0, 360)));
		}
	}
}