using System;
using UnityEngine;

// Token: 0x0200001F RID: 31
public class PickupAnimationScript : MonoBehaviour
{
	// Token: 0x0600009E RID: 158 RVA: 0x00005BD5 File Offset: 0x00003FD5
	private void Start()
	{
		this.itemPosition = base.GetComponent<Transform>();
	}

	// Token: 0x0600009F RID: 159 RVA: 0x00005BE3 File Offset: 0x00003FE3
	private void Update()
	{
		this.itemPosition.localPosition = new Vector3(0f, Mathf.Sin((float)Time.frameCount * 0.017453292f) / 2f + 1f, 0f);
	}

	// Token: 0x04000117 RID: 279
	private Transform itemPosition;
}
