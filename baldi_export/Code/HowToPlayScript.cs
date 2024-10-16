using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x02000005 RID: 5
public class HowToPlayScript : MonoBehaviour
{
	// Token: 0x0600000F RID: 15 RVA: 0x0000249E File Offset: 0x0000089E
	private void Start()
	{
		this.button = base.GetComponent<Button>();
		this.button.onClick.AddListener(new UnityAction(this.OpenScreen));
	}

	// Token: 0x06000010 RID: 16 RVA: 0x000024C8 File Offset: 0x000008C8
	private void OpenScreen()
	{
		this.screen.SetActive(true);
	}

	// Token: 0x04000012 RID: 18
	private Button button;

	// Token: 0x04000013 RID: 19
	public GameObject screen;
}
