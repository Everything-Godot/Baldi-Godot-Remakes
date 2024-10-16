using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x02000011 RID: 17
public class ExitButtonScript : MonoBehaviour
{
	// Token: 0x06000047 RID: 71 RVA: 0x00003544 File Offset: 0x00001944
	private void Start()
	{
		this.button = base.GetComponent<Button>();
		this.button.onClick.AddListener(new UnityAction(this.ExitGame));
	}

	// Token: 0x06000048 RID: 72 RVA: 0x0000356E File Offset: 0x0000196E
	private void ExitGame()
	{
		Application.Quit();
	}

	// Token: 0x04000068 RID: 104
	private Button button;
}
