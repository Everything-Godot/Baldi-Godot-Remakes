using System;
using UnityEngine;

// Token: 0x0200001D RID: 29
public class FacultyTriggerScript : MonoBehaviour
{
	// Token: 0x06000091 RID: 145 RVA: 0x00005624 File Offset: 0x00003A24
	private void Start()
	{
		this.hitBox = base.GetComponent<BoxCollider>();
	}

	// Token: 0x06000092 RID: 146 RVA: 0x00005632 File Offset: 0x00003A32
	private void Update()
	{
	}

	// Token: 0x06000093 RID: 147 RVA: 0x00005634 File Offset: 0x00003A34
	private void OnTriggerStay(Collider other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			this.ps.ResetGuilt("faculty", 2f);
		}
	}

	// Token: 0x040000F9 RID: 249
	public PlayerScript ps;

	// Token: 0x040000FA RID: 250
	private BoxCollider hitBox;
}
