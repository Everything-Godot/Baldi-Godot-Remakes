using System;
using UnityEngine;
using UnityEngine.AI;

// Token: 0x0200001E RID: 30
public class PrincipalScript : MonoBehaviour
{
	// Token: 0x06000095 RID: 149 RVA: 0x00005697 File Offset: 0x00003A97
	private void Start()
	{
		this.agent = base.GetComponent<NavMeshAgent>();
		this.audioQueue = base.GetComponent<AudioQueueScript>();
		this.audioDevice = base.GetComponent<AudioSource>();
		this.Wander();
	}

	// Token: 0x06000096 RID: 150 RVA: 0x000056C4 File Offset: 0x00003AC4
	private void Update()
	{
		if (this.seesRuleBreak)
		{
			this.timeSeenRuleBreak += 1f * Time.deltaTime;
			if (((double)this.timeSeenRuleBreak >= 0.5) & !this.angry)
			{
				this.angry = true;
				this.seesRuleBreak = false;
				this.timeSeenRuleBreak = 0f;
				this.TargetPlayer();
				this.CorrectPlayer();
			}
		}
		else
		{
			this.timeSeenRuleBreak = 0f;
		}
		if (this.coolDown > 0f)
		{
			this.coolDown -= 1f * Time.deltaTime;
		}
	}

	// Token: 0x06000097 RID: 151 RVA: 0x00005778 File Offset: 0x00003B78
	private void FixedUpdate()
	{
		if (!this.angry)
		{
			Vector3 vector = this.player.position - base.transform.position;
			RaycastHit raycastHit;
			if (Physics.Raycast(base.transform.position, vector, out raycastHit, float.PositiveInfinity, 3, QueryTriggerInteraction.Ignore) & (raycastHit.transform.tag == "Player") & (this.playerScript.guilt > 0f) & !this.inOffice & !this.angry)
			{
				this.seesRuleBreak = true;
			}
			else
			{
				this.seesRuleBreak = false;
				if ((this.agent.velocity.magnitude <= 1f) & (this.coolDown <= 0f))
				{
					this.Wander();
				}
			}
		}
		else
		{
			this.TargetPlayer();
		}
	}

	// Token: 0x06000098 RID: 152 RVA: 0x00005860 File Offset: 0x00003C60
	private void Wander()
	{
		this.wanderer.GetNewTarget();
		this.agent.SetDestination(this.wanderTarget.position);
		if (this.agent.isStopped)
		{
			this.agent.isStopped = false;
		}
		this.coolDown = 1f;
		if (global::UnityEngine.Random.Range(0f, 10f) <= 1f)
		{
			this.audioDevice.PlayOneShot(this.aud_Whistle);
		}
	}

	// Token: 0x06000099 RID: 153 RVA: 0x000058E0 File Offset: 0x00003CE0
	private void TargetPlayer()
	{
		this.agent.SetDestination(this.player.position);
		this.coolDown = 1f;
	}

	// Token: 0x0600009A RID: 154 RVA: 0x00005904 File Offset: 0x00003D04
	private void CorrectPlayer()
	{
		this.audioQueue.ClearAudioQueue();
		if (this.playerScript.guiltType == "faculty")
		{
			this.audioQueue.QueueAudio(this.audNoFaculty);
		}
		else if (this.playerScript.guiltType == "running")
		{
			this.audioQueue.QueueAudio(this.audNoRunning);
		}
		else if (this.playerScript.guiltType == "food")
		{
			this.audioQueue.QueueAudio(this.audNoEating);
		}
		else if (this.playerScript.guiltType == "drink")
		{
			this.audioQueue.QueueAudio(this.audNoDrinking);
		}
		else if (this.playerScript.guiltType == "escape")
		{
			this.audioQueue.QueueAudio(this.audNoEscaping);
		}
	}

	// Token: 0x0600009B RID: 155 RVA: 0x00005A08 File Offset: 0x00003E08
	private void OnTriggerStay(Collider other)
	{
		if (other.name == "Office Trigger")
		{
			this.inOffice = true;
		}
		if ((other.tag == "Player") & this.angry & !this.inOffice)
		{
			this.inOffice = true;
			this.agent.Warp(new Vector3(10f, 0f, 170f));
			this.agent.isStopped = true;
			other.transform.position = new Vector3(10f, 4f, 160f);
			other.transform.LookAt(new Vector3(base.transform.position.x, other.transform.position.y, base.transform.position.z));
			this.audioQueue.QueueAudio(this.aud_Delay);
			this.audioQueue.QueueAudio(this.audTimes[this.detentions]);
			this.audioQueue.QueueAudio(this.audDetention);
			this.audioQueue.QueueAudio(this.audScolds[global::UnityEngine.Random.Range(0, 2)]);
			this.officeDoor.LockDoor((float)this.lockTime[this.detentions]);
			this.baldiScript.Hear(base.transform.position, 5f);
			this.coolDown = 5f;
			this.angry = false;
			this.detentions++;
			if (this.detentions > 4)
			{
				this.detentions = 4;
			}
		}
	}

	// Token: 0x0600009C RID: 156 RVA: 0x00005BAF File Offset: 0x00003FAF
	private void OnTriggerExit(Collider other)
	{
		if (other.name == "Office Trigger")
		{
			this.inOffice = false;
		}
	}

	// Token: 0x040000FB RID: 251
	public bool seesRuleBreak;

	// Token: 0x040000FC RID: 252
	public Transform player;

	// Token: 0x040000FD RID: 253
	public PlayerScript playerScript;

	// Token: 0x040000FE RID: 254
	public BaldiScript baldiScript;

	// Token: 0x040000FF RID: 255
	public Transform wanderTarget;

	// Token: 0x04000100 RID: 256
	public AILocationSelectorScript wanderer;

	// Token: 0x04000101 RID: 257
	public DoorScript officeDoor;

	// Token: 0x04000102 RID: 258
	public float coolDown;

	// Token: 0x04000103 RID: 259
	public float timeSeenRuleBreak;

	// Token: 0x04000104 RID: 260
	public bool angry;

	// Token: 0x04000105 RID: 261
	public bool inOffice;

	// Token: 0x04000106 RID: 262
	private int detentions;

	// Token: 0x04000107 RID: 263
	private int[] lockTime = new int[] { 15, 30, 45, 60, 99 };

	// Token: 0x04000108 RID: 264
	public AudioClip[] audTimes = new AudioClip[5];

	// Token: 0x04000109 RID: 265
	public AudioClip[] audScolds = new AudioClip[3];

	// Token: 0x0400010A RID: 266
	public AudioClip audDetention;

	// Token: 0x0400010B RID: 267
	public AudioClip audNoDrinking;

	// Token: 0x0400010C RID: 268
	public AudioClip audNoEating;

	// Token: 0x0400010D RID: 269
	public AudioClip audNoFaculty;

	// Token: 0x0400010E RID: 270
	public AudioClip audNoLockers;

	// Token: 0x0400010F RID: 271
	public AudioClip audNoRunning;

	// Token: 0x04000110 RID: 272
	public AudioClip audNoStabbing;

	// Token: 0x04000111 RID: 273
	public AudioClip audNoEscaping;

	// Token: 0x04000112 RID: 274
	public AudioClip aud_Whistle;

	// Token: 0x04000113 RID: 275
	public AudioClip aud_Delay;

	// Token: 0x04000114 RID: 276
	private NavMeshAgent agent;

	// Token: 0x04000115 RID: 277
	private AudioQueueScript audioQueue;

	// Token: 0x04000116 RID: 278
	private AudioSource audioDevice;
}
