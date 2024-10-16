using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x02000002 RID: 2
public class BackButtonScript : MonoBehaviour
{
	// Token: 0x06000002 RID: 2 RVA: 0x00002058 File Offset: 0x00000458
	private void Start()
	{
		this.button = base.GetComponent<Button>();
		this.button.onClick.AddListener(new UnityAction(this.CloseScreen));
	}

	// Token: 0x06000003 RID: 3 RVA: 0x00002082 File Offset: 0x00000482
	private void CloseScreen()
	{
		this.screen.SetActive(false);
	}

	// Token: 0x04000001 RID: 1
	private Button button;

	// Token: 0x04000002 RID: 2
	public GameObject screen;
}
