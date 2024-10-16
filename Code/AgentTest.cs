using System;
using UnityEngine;
using UnityEngine.AI;

// Token: 0x02000019 RID: 25
public class AgentTest : MonoBehaviour
{
	// Token: 0x06000078 RID: 120 RVA: 0x00004E32 File Offset: 0x00003232
	private void Start()
	{
		this.agent = base.GetComponent<NavMeshAgent>();
		this.Wander();
	}

	// Token: 0x06000079 RID: 121 RVA: 0x00004E46 File Offset: 0x00003246
	private void Update()
	{
		if (this.coolDown > 0f)
		{
			this.coolDown -= 1f * Time.deltaTime;
		}
	}

	// Token: 0x0600007A RID: 122 RVA: 0x00004E70 File Offset: 0x00003270
	private void FixedUpdate()
	{
		Vector3 vector = this.player.position - base.transform.position;
		RaycastHit raycastHit;
		if (Physics.Raycast(base.transform.position, vector, out raycastHit, float.PositiveInfinity, 3, QueryTriggerInteraction.Ignore) & (raycastHit.transform.tag == "Player"))
		{
			this.db = true;
			this.TargetPlayer();
		}
		else
		{
			this.db = false;
			if ((this.agent.velocity.magnitude <= 1f) & (this.coolDown <= 0f))
			{
				this.Wander();
			}
		}
	}

	// Token: 0x0600007B RID: 123 RVA: 0x00004F21 File Offset: 0x00003321
	private void Wander()
	{
		this.wanderer.GetNewTarget();
		this.agent.SetDestination(this.wanderTarget.position);
		this.coolDown = 1f;
	}

	// Token: 0x0600007C RID: 124 RVA: 0x00004F50 File Offset: 0x00003350
	private void TargetPlayer()
	{
		this.agent.SetDestination(this.player.position);
		this.coolDown = 1f;
	}

	// Token: 0x040000CD RID: 205
	public bool db;

	// Token: 0x040000CE RID: 206
	public Transform player;

	// Token: 0x040000CF RID: 207
	public Transform wanderTarget;

	// Token: 0x040000D0 RID: 208
	public AILocationSelectorScript wanderer;

	// Token: 0x040000D1 RID: 209
	public float coolDown;

	// Token: 0x040000D2 RID: 210
	private NavMeshAgent agent;
}
