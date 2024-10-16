using System;
using UnityEngine;
using UnityEngine.AI;

// Token: 0x02000007 RID: 7
public class PlaytimeScript : MonoBehaviour
{
	// Token: 0x06000018 RID: 24 RVA: 0x0000272B File Offset: 0x00000B2B
	private void Start()
	{
		this.agent = base.GetComponent<NavMeshAgent>();
		this.audioDevice = base.GetComponent<AudioSource>();
		this.Wander();
	}

	// Token: 0x06000019 RID: 25 RVA: 0x0000274C File Offset: 0x00000B4C
	private void Update()
	{
		if (this.coolDown > 0f)
		{
			this.coolDown -= 1f * Time.deltaTime;
		}
		if (this.playCool > 0f)
		{
			this.playCool -= Time.deltaTime;
		}
	}

	// Token: 0x0600001A RID: 26 RVA: 0x000027A4 File Offset: 0x00000BA4
	private void FixedUpdate()
	{
		if (!this.ps.jumpRope)
		{
			Vector3 vector = this.player.position - base.transform.position;
			RaycastHit raycastHit;
			if (Physics.Raycast(base.transform.position, vector, out raycastHit, float.PositiveInfinity, 3, QueryTriggerInteraction.Ignore) & (raycastHit.transform.tag == "Player") & ((base.transform.position - this.player.position).magnitude <= 80f) & (this.playCool <= 0f))
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
			this.jumpRopeStarted = false;
		}
		else
		{
			if (!this.jumpRopeStarted)
			{
				this.agent.Warp(base.transform.position - base.transform.forward * 10f);
			}
			this.jumpRopeStarted = true;
			this.agent.speed = 0f;
			this.playCool = 15f;
		}
	}

	// Token: 0x0600001B RID: 27 RVA: 0x00002910 File Offset: 0x00000D10
	private void Wander()
	{
		this.wanderer.GetNewTarget();
		this.agent.SetDestination(this.wanderTarget.position);
		this.agent.speed = 15f;
		this.playerSpotted = false;
		this.audVal = Mathf.RoundToInt(global::UnityEngine.Random.Range(0f, 1f));
		this.audioDevice.PlayOneShot(this.aud_Random[this.audVal]);
		this.coolDown = 1f;
	}

	// Token: 0x0600001C RID: 28 RVA: 0x00002994 File Offset: 0x00000D94
	private void TargetPlayer()
	{
		this.agent.SetDestination(this.player.position);
		this.agent.speed = 25f;
		this.coolDown = 1f;
		if (!this.playerSpotted)
		{
			this.playerSpotted = true;
			this.audioDevice.PlayOneShot(this.aud_LetsPlay);
		}
	}

	// Token: 0x0400001E RID: 30
	public bool db;

	// Token: 0x0400001F RID: 31
	public int audVal;

	// Token: 0x04000020 RID: 32
	public Transform player;

	// Token: 0x04000021 RID: 33
	public PlayerScript ps;

	// Token: 0x04000022 RID: 34
	public Transform wanderTarget;

	// Token: 0x04000023 RID: 35
	public AILocationSelectorScript wanderer;

	// Token: 0x04000024 RID: 36
	public float coolDown;

	// Token: 0x04000025 RID: 37
	public float playCool;

	// Token: 0x04000026 RID: 38
	public bool playerSpotted;

	// Token: 0x04000027 RID: 39
	public bool jumpRopeStarted;

	// Token: 0x04000028 RID: 40
	private NavMeshAgent agent;

	// Token: 0x04000029 RID: 41
	public AudioClip[] aud_Numbers = new AudioClip[10];

	// Token: 0x0400002A RID: 42
	public AudioClip[] aud_Random = new AudioClip[2];

	// Token: 0x0400002B RID: 43
	public AudioClip aud_Instrcutions;

	// Token: 0x0400002C RID: 44
	public AudioClip aud_Oops;

	// Token: 0x0400002D RID: 45
	public AudioClip aud_LetsPlay;

	// Token: 0x0400002E RID: 46
	public AudioClip aud_Congrats;

	// Token: 0x0400002F RID: 47
	public AudioSource audioDevice;
}
