using System;
using UnityEngine;
using UnityEngine.AI;

// Token: 0x02000025 RID: 37
public class SweepScript : MonoBehaviour
{
	// Token: 0x060000B9 RID: 185 RVA: 0x000065F7 File Offset: 0x000049F7
	private void Start()
	{
		this.agent = base.GetComponent<NavMeshAgent>();
		this.audioDevice = base.GetComponent<AudioSource>();
		this.origin = base.transform.position;
		this.waitTime = global::UnityEngine.Random.Range(120f, 180f);
	}

	// Token: 0x060000BA RID: 186 RVA: 0x00006638 File Offset: 0x00004A38
	private void Update()
	{
		if (this.coolDown > 0f)
		{
			this.coolDown -= 1f * Time.deltaTime;
		}
		if (this.waitTime > 0f)
		{
			this.waitTime -= Time.deltaTime;
		}
		else if (!this.active)
		{
			this.active = true;
			this.wanders = 0;
			this.Wander();
			this.audioDevice.PlayOneShot(this.aud_Intro);
		}
	}

	// Token: 0x060000BB RID: 187 RVA: 0x000066C4 File Offset: 0x00004AC4
	private void FixedUpdate()
	{
		if ((this.agent.velocity.magnitude <= 1f) & (this.coolDown <= 0f) & (this.wanders < 5) & this.active)
		{
			this.Wander();
		}
		else if (this.wanders >= 5)
		{
			this.GoHome();
		}
	}

	// Token: 0x060000BC RID: 188 RVA: 0x00006735 File Offset: 0x00004B35
	private void Wander()
	{
		this.wanderer.GetNewTargetHallway();
		this.agent.SetDestination(this.wanderTarget.position);
		this.coolDown = 1f;
		this.wanders++;
	}

	// Token: 0x060000BD RID: 189 RVA: 0x00006772 File Offset: 0x00004B72
	private void GoHome()
	{
		this.agent.SetDestination(this.origin);
		this.waitTime = global::UnityEngine.Random.Range(120f, 180f);
		this.wanders = 0;
		this.active = false;
	}

	// Token: 0x060000BE RID: 190 RVA: 0x000067AC File Offset: 0x00004BAC
	private void OnTriggerStay(Collider other)
	{
		if (other.tag == "NPC")
		{
			NavMeshAgent component = other.GetComponent<NavMeshAgent>();
			component.velocity = this.agent.velocity + component.velocity * 0.2f;
		}
	}

	// Token: 0x060000BF RID: 191 RVA: 0x000067FB File Offset: 0x00004BFB
	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "NPC" || other.tag == "Player")
		{
			this.audioDevice.PlayOneShot(this.aud_Sweep);
		}
	}

	// Token: 0x04000137 RID: 311
	public Transform wanderTarget;

	// Token: 0x04000138 RID: 312
	public AILocationSelectorScript wanderer;

	// Token: 0x04000139 RID: 313
	public float coolDown;

	// Token: 0x0400013A RID: 314
	public float waitTime;

	// Token: 0x0400013B RID: 315
	public int wanders;

	// Token: 0x0400013C RID: 316
	public bool active;

	// Token: 0x0400013D RID: 317
	private Vector3 origin;

	// Token: 0x0400013E RID: 318
	public AudioClip aud_Sweep;

	// Token: 0x0400013F RID: 319
	public AudioClip aud_Intro;

	// Token: 0x04000140 RID: 320
	private NavMeshAgent agent;

	// Token: 0x04000141 RID: 321
	private AudioSource audioDevice;
}
