using System;
using UnityEngine;
using UnityEngine.AI;

// Token: 0x0200001B RID: 27
public class BaldiScript : MonoBehaviour
{
	// Token: 0x06000081 RID: 129 RVA: 0x0000500F File Offset: 0x0000340F
	private void Start()
	{
		this.baldiAudio = base.GetComponent<AudioSource>();
		this.agent = base.GetComponent<NavMeshAgent>();
		this.timeToMove = this.baseTime;
		this.Wander();
	}

	// Token: 0x06000082 RID: 130 RVA: 0x0000503C File Offset: 0x0000343C
	private void Update()
	{
		if (this.timeToMove > 0f)
		{
			this.timeToMove -= 1f * Time.deltaTime;
		}
		else
		{
			this.Move();
		}
		if (this.coolDown > 0f)
		{
			this.coolDown -= 1f * Time.deltaTime;
		}
	}

	// Token: 0x06000083 RID: 131 RVA: 0x000050A4 File Offset: 0x000034A4
	private void FixedUpdate()
	{
		if (this.moveFrames > 0f)
		{
			this.moveFrames -= 1f;
			this.agent.speed = this.speed;
		}
		else
		{
			this.agent.speed = 0f;
		}
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
		}
	}

	// Token: 0x06000084 RID: 132 RVA: 0x00005164 File Offset: 0x00003564
	private void Wander()
	{
		this.wanderer.GetNewTarget();
		this.agent.SetDestination(this.wanderTarget.position);
		this.coolDown = 1f;
		this.currentPriority = 0f;
	}

	// Token: 0x06000085 RID: 133 RVA: 0x0000519E File Offset: 0x0000359E
	private void TargetPlayer()
	{
		this.agent.SetDestination(this.player.position);
		this.coolDown = 1f;
		this.currentPriority = 0f;
	}

	// Token: 0x06000086 RID: 134 RVA: 0x000051D0 File Offset: 0x000035D0
	private void Move()
	{
		if ((base.transform.position == this.previous) & (this.coolDown < 0f))
		{
			this.Wander();
		}
		this.moveFrames = 10f;
		this.timeToMove = this.baseTime - this.baldiAnger;
		this.previous = base.transform.position;
		this.baldiAudio.PlayOneShot(this.slap);
		this.baldiAnimator.SetTrigger("slap");
	}

	// Token: 0x06000087 RID: 135 RVA: 0x0000525C File Offset: 0x0000365C
	public void GetAngry(float value)
	{
		this.baldiAnger += this.angerRate * value;
	}

	// Token: 0x06000088 RID: 136 RVA: 0x00005273 File Offset: 0x00003673
	public void Hear(Vector3 soundLocation, float priority)
	{
		if (priority >= this.currentPriority)
		{
			this.agent.SetDestination(soundLocation);
			this.currentPriority = priority;
		}
	}

	// Token: 0x040000D5 RID: 213
	public bool db;

	// Token: 0x040000D6 RID: 214
	public float baseTime;

	// Token: 0x040000D7 RID: 215
	public float speed;

	// Token: 0x040000D8 RID: 216
	public float timeToMove;

	// Token: 0x040000D9 RID: 217
	public float baldiAnger;

	// Token: 0x040000DA RID: 218
	public float angerRate;

	// Token: 0x040000DB RID: 219
	private float moveFrames;

	// Token: 0x040000DC RID: 220
	private float currentPriority;

	// Token: 0x040000DD RID: 221
	public Transform player;

	// Token: 0x040000DE RID: 222
	public Transform wanderTarget;

	// Token: 0x040000DF RID: 223
	public AILocationSelectorScript wanderer;

	// Token: 0x040000E0 RID: 224
	private AudioSource baldiAudio;

	// Token: 0x040000E1 RID: 225
	public AudioClip slap;

	// Token: 0x040000E2 RID: 226
	public AudioClip[] speech = new AudioClip[3];

	// Token: 0x040000E3 RID: 227
	public Animator baldiAnimator;

	// Token: 0x040000E4 RID: 228
	public float coolDown;

	// Token: 0x040000E5 RID: 229
	private Vector3 previous;

	// Token: 0x040000E6 RID: 230
	private NavMeshAgent agent;
}
