using System;
using UnityEngine;
using UnityEngine.AI;

// Token: 0x0200000A RID: 10
public class BsodaSparyScript : MonoBehaviour
{
	// Token: 0x06000028 RID: 40 RVA: 0x00002B48 File Offset: 0x00000F48
	private void Start()
	{
		this.rb = base.GetComponent<Rigidbody>();
		this.rb.velocity = base.transform.forward * this.speed;
		this.lifeSpan = 30f;
	}

	// Token: 0x06000029 RID: 41 RVA: 0x00002B84 File Offset: 0x00000F84
	private void Update()
	{
		this.rb.velocity = base.transform.forward * this.speed;
		this.lifeSpan -= Time.deltaTime;
		if (this.lifeSpan < 0f)
		{
			global::UnityEngine.Object.Destroy(base.gameObject, 0f);
		}
	}

	// Token: 0x0600002A RID: 42 RVA: 0x00002BE4 File Offset: 0x00000FE4
	private void OnTriggerStay(Collider other)
	{
		if (other.tag == "NPC")
		{
			NavMeshAgent component = other.GetComponent<NavMeshAgent>();
			component.velocity = this.rb.velocity + component.velocity * 0.1f;
		}
	}

	// Token: 0x04000034 RID: 52
	public float speed;

	// Token: 0x04000035 RID: 53
	private float lifeSpan;

	// Token: 0x04000036 RID: 54
	private Rigidbody rb;
}
