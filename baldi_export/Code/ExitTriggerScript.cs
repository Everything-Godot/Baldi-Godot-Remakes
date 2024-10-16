using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x02000012 RID: 18
public class ExitTriggerScript : MonoBehaviour
{
	// Token: 0x0600004A RID: 74 RVA: 0x0000357D File Offset: 0x0000197D
	private void OnTriggerEnter(Collider other)
	{
		if ((this.gc.notebooks >= 7) & (other.tag == "Player"))
		{
			SceneManager.LoadScene("Results");
		}
	}

	// Token: 0x04000069 RID: 105
	public GameControllerScript gc;
}
