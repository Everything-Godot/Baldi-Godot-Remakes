using System;
using UnityEngine;

// Token: 0x0200000D RID: 13
public class CraftersTriggerScript : MonoBehaviour
{
	// Token: 0x06000033 RID: 51 RVA: 0x00002F64 File Offset: 0x00001364
	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			this.crafters.GiveLocation(this.goTarget.position, false);
		}
	}

	// Token: 0x06000034 RID: 52 RVA: 0x00002F92 File Offset: 0x00001392
	private void OnTriggerExit(Collider other)
	{
		if (other.tag == "Player")
		{
			this.crafters.GiveLocation(this.fleeTarget.position, true);
		}
	}

	// Token: 0x04000041 RID: 65
	public Transform goTarget;

	// Token: 0x04000042 RID: 66
	public Transform fleeTarget;

	// Token: 0x04000043 RID: 67
	public CraftersScript crafters;
}
