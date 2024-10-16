using System;
using UnityEngine;

// Token: 0x02000018 RID: 24
public class NearExitTriggerScript : MonoBehaviour
{
	// Token: 0x06000076 RID: 118 RVA: 0x00004DD4 File Offset: 0x000031D4
	private void OnTriggerEnter(Collider other)
	{
		if ((this.gc.exitsReached < 3) & this.gc.finaleMode & (other.tag == "Player"))
		{
			this.gc.ExitReached();
			this.es.Lower();
		}
	}

	// Token: 0x040000CB RID: 203
	public GameControllerScript gc;

	// Token: 0x040000CC RID: 204
	public EntranceScript es;
}
