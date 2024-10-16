using System;
using UnityEngine;

// Token: 0x02000016 RID: 22
public class NotebookScript : MonoBehaviour
{
	// Token: 0x06000070 RID: 112 RVA: 0x00004D04 File Offset: 0x00003104
	private void Start()
	{
	}

	// Token: 0x06000071 RID: 113 RVA: 0x00004D08 File Offset: 0x00003108
	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit raycastHit;
			if (Physics.Raycast(ray, out raycastHit) && ((raycastHit.transform.tag == "Notebook") & (Vector3.Distance(this.player.position, base.transform.position) < this.openingDistance)))
			{
				base.gameObject.SetActive(false);
				this.gc.CollectNotebook();
				this.learningGame.SetActive(true);
			}
		}
	}

	// Token: 0x040000C6 RID: 198
	public float openingDistance;

	// Token: 0x040000C7 RID: 199
	public GameControllerScript gc;

	// Token: 0x040000C8 RID: 200
	public Transform player;

	// Token: 0x040000C9 RID: 201
	public GameObject learningGame;
}
