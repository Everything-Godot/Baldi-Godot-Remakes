using System;
using UnityEngine;

// Token: 0x02000009 RID: 9
public class Billboard : MonoBehaviour
{
	// Token: 0x06000025 RID: 37 RVA: 0x00002ACD File Offset: 0x00000ECD
	private void Start()
	{
		this.m_Camera = GameObject.Find("Main Camera").GetComponent<Camera>();
	}

	// Token: 0x06000026 RID: 38 RVA: 0x00002AE4 File Offset: 0x00000EE4
	private void LateUpdate()
	{
		base.transform.LookAt(base.transform.position + this.m_Camera.transform.rotation * Vector3.forward, this.m_Camera.transform.rotation * Vector3.up);
	}

	// Token: 0x04000033 RID: 51
	private Camera m_Camera;
}
