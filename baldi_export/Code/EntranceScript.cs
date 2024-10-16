using System;
using UnityEngine;

// Token: 0x02000010 RID: 16
public class EntranceScript : MonoBehaviour
{
	// Token: 0x06000044 RID: 68 RVA: 0x000034DA File Offset: 0x000018DA
	public void Lower()
	{
		base.transform.position = base.transform.position - new Vector3(0f, 10f, 0f);
	}

	// Token: 0x06000045 RID: 69 RVA: 0x0000350B File Offset: 0x0000190B
	public void Raise()
	{
		base.transform.position = base.transform.position + new Vector3(0f, 10f, 0f);
	}

	// Token: 0x04000067 RID: 103
	public GameControllerScript gc;
}
