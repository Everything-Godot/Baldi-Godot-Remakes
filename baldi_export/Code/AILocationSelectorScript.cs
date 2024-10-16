using System;
using UnityEngine;

// Token: 0x0200001A RID: 26
public class AILocationSelectorScript : MonoBehaviour
{
	// Token: 0x0600007E RID: 126 RVA: 0x00004F89 File Offset: 0x00003389
	public void GetNewTarget()
	{
		this.id = Mathf.RoundToInt(global::UnityEngine.Random.Range(0f, 28f));
		base.transform.position = this.newLocation[this.id].position;
	}

	// Token: 0x0600007F RID: 127 RVA: 0x00004FC2 File Offset: 0x000033C2
	public void GetNewTargetHallway()
	{
		this.id = Mathf.RoundToInt(global::UnityEngine.Random.Range(0f, 15f));
		base.transform.position = this.newLocation[this.id].position;
	}

	// Token: 0x040000D3 RID: 211
	public Transform[] newLocation = new Transform[29];

	// Token: 0x040000D4 RID: 212
	private int id;
}
