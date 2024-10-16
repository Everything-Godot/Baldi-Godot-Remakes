using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Token: 0x02000022 RID: 34
public class StartButton : MonoBehaviour
{
	// Token: 0x060000B1 RID: 177 RVA: 0x00006551 File Offset: 0x00004951
	private void Start()
	{
		this.button = base.GetComponent<Button>();
		this.button.onClick.AddListener(new UnityAction(this.StartGame));
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
	}

	// Token: 0x060000B2 RID: 178 RVA: 0x00006587 File Offset: 0x00004987
	private void StartGame()
	{
		SceneManager.LoadScene("School");
	}

	// Token: 0x04000135 RID: 309
	private Button button;
}
