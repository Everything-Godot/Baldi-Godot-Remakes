using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000004 RID: 4
public class DetentionTextScript : MonoBehaviour
{
	// Token: 0x0600000C RID: 12 RVA: 0x0000241F File Offset: 0x0000081F
	private void Start()
	{
		this.text = base.GetComponent<Text>();
	}

	// Token: 0x0600000D RID: 13 RVA: 0x00002430 File Offset: 0x00000830
	private void Update()
	{
		if (this.door.lockTime > 0f)
		{
			this.text.text = "You have detention! \n" + Mathf.CeilToInt(this.door.lockTime) + " seconds remain!";
		}
		else
		{
			this.text.text = string.Empty;
		}
	}

	// Token: 0x04000010 RID: 16
	public DoorScript door;

	// Token: 0x04000011 RID: 17
	private Text text;
}
