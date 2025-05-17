using System;
using UnityEngine;

// Token: 0x020000C5 RID: 197
public class NearExitTriggerScript : MonoBehaviour
{
	private void Update()
	{
		if (gc.exitsReached >= 3 & this.gc.style == "glitch")
		{
			base.gameObject.transform.localScale = new Vector3(80, 5, 80);
		}
	}
	// Token: 0x06000995 RID: 2453 RVA: 0x00024288 File Offset: 0x00022688
	private void OnTriggerEnter(Collider other)
	{
		if (PlayerPrefs.GetInt("FreeRun") == 0)
		{
			if (gc.style != "glitch")
			{
				if (this.gc.exitsReached < this.gc.entrances.Length - 1 & this.gc.finaleMode)
				{
					if (other.tag == "Player")
					{
						GetComponent<BoxCollider>().enabled = false;
						this.gc.lastestExit = this.es.EntranceID;
						this.gc.ExitReached();
						this.es.Lower();
						if (this.gc.baldiScrpt.isActiveAndEnabled) this.gc.baldiScrpt.Hear(base.transform.position, 8f);
					}
				}
			}
			else
			{
				if (this.gc.exitsReached < this.gc.entrances.Length & this.gc.finaleMode)
				{
					if (other.tag == "Player")
					{
						GetComponent<BoxCollider>().enabled = false;
						this.gc.lastestExit = this.es.EntranceID;
						this.gc.ExitReached();
						if (gc.exitsReached < this.gc.entrances.Length)
						{
							this.es.Lower();
						}
						if (this.gc.baldiScrpt.isActiveAndEnabled) this.gc.baldiScrpt.Hear(base.transform.position, 8f);
					}
				}
			}

		}
	}

	// Token: 0x04000674 RID: 1652
	public GameControllerScript gc;

	// Token: 0x04000675 RID: 1653
	public EntranceScript es;
}
