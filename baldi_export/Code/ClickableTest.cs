using System;
using UnityEngine;

// Token: 0x0200000C RID: 12
public class ClickableTest : MonoBehaviour
{
	// Token: 0x06000030 RID: 48 RVA: 0x00002EFD File Offset: 0x000012FD
	private void Start()
	{
	}

	// Token: 0x06000031 RID: 49 RVA: 0x00002F00 File Offset: 0x00001300
	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit raycastHit;
			if (Physics.Raycast(ray, out raycastHit) && raycastHit.transform.name == "MathNotebook")
			{
				base.gameObject.SetActive(false);
			}
		}
	}
}
