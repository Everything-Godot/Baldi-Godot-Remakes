using System;
using UnityEngine;

// Token: 0x02000020 RID: 32
public class PickupScript : MonoBehaviour
{
	// Token: 0x060000A1 RID: 161 RVA: 0x00005C24 File Offset: 0x00004024
	private void Start()
	{
	}

	// Token: 0x060000A2 RID: 162 RVA: 0x00005C28 File Offset: 0x00004028
	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit raycastHit;
			if (Physics.Raycast(ray, out raycastHit))
			{
				if ((raycastHit.transform.name == "Pickup_EnergyFlavoredZestyBar") & (Vector3.Distance(this.player.position, base.transform.position) < 10f))
				{
					raycastHit.transform.gameObject.SetActive(false);
					this.gc.CollectItem(1);
				}
				else if ((raycastHit.transform.name == "Pickup_YellowDoorLock") & (Vector3.Distance(this.player.position, base.transform.position) < 10f))
				{
					raycastHit.transform.gameObject.SetActive(false);
					this.gc.CollectItem(2);
				}
				else if ((raycastHit.transform.name == "Pickup_Key") & (Vector3.Distance(this.player.position, base.transform.position) < 10f))
				{
					raycastHit.transform.gameObject.SetActive(false);
					this.gc.CollectItem(3);
				}
				else if ((raycastHit.transform.name == "Pickup_BSODA") & (Vector3.Distance(this.player.position, base.transform.position) < 10f))
				{
					raycastHit.transform.gameObject.SetActive(false);
					this.gc.CollectItem(4);
				}
				else if ((raycastHit.transform.name == "Pickup_Quarter") & (Vector3.Distance(this.player.position, base.transform.position) < 10f))
				{
					raycastHit.transform.gameObject.SetActive(false);
					this.gc.CollectItem(5);
				}
				else if ((raycastHit.transform.name == "Pickup_Tape") & (Vector3.Distance(this.player.position, base.transform.position) < 10f))
				{
					raycastHit.transform.gameObject.SetActive(false);
					this.gc.CollectItem(6);
				}
			}
		}
	}

	// Token: 0x04000118 RID: 280
	public GameControllerScript gc;

	// Token: 0x04000119 RID: 281
	public Transform player;
}
