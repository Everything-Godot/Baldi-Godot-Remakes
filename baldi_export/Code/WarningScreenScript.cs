using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x02000023 RID: 35
public class WarningScreenScript : MonoBehaviour
{
	// Token: 0x060000B4 RID: 180 RVA: 0x0000659B File Offset: 0x0000499B
	private void Update()
	{
		if (Input.anyKeyDown)
		{
			SceneManager.LoadScene("MainMenu");
		}
	}
}
