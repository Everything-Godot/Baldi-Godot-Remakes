using System;
using UnityEngine;

// Token: 0x02000024 RID: 36
public class YouWonScript : MonoBehaviour
{
	// Token: 0x060000B6 RID: 182 RVA: 0x000065B9 File Offset: 0x000049B9
	private void Start()
	{
		this.delay = 10f;
	}

	// Token: 0x060000B7 RID: 183 RVA: 0x000065C6 File Offset: 0x000049C6
	private void Update()
	{
		this.delay -= Time.deltaTime;
		if (this.delay <= 0f)
		{
			Application.Quit();
		}
	}

	// Token: 0x04000136 RID: 310
	private float delay;
}
