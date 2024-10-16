using System;
using UnityEngine;
using UnityEngine.AI;

// Token: 0x0200001C RID: 28
public class CraftersScript : MonoBehaviour
{
	// Token: 0x0600008A RID: 138 RVA: 0x0000529D File Offset: 0x0000369D
	private void Start()
	{
		this.agent = base.GetComponent<NavMeshAgent>();
		this.audioDevice = base.GetComponent<AudioSource>();
		this.sprite.SetActive(false);
	}

	// Token: 0x0600008B RID: 139 RVA: 0x000052C4 File Offset: 0x000036C4
	private void Update()
	{
		if (this.forceShowTime > 0f)
		{
			this.forceShowTime -= Time.deltaTime;
		}
		if (this.gettingAngry)
		{
			this.anger += Time.deltaTime;
			if ((this.anger >= 1f) & !this.angry)
			{
				this.angry = true;
				this.audioDevice.PlayOneShot(this.aud_Intro);
				this.spriteImage.sprite = this.angrySprite;
			}
		}
		if (!this.angry)
		{
			if ((((base.transform.position - this.agent.destination).magnitude <= 20f) & ((base.transform.position - this.player.position).magnitude >= 60f)) || this.forceShowTime > 0f)
			{
				this.sprite.SetActive(true);
			}
			else
			{
				this.sprite.SetActive(false);
			}
		}
		else
		{
			this.agent.speed = this.agent.speed + 60f * Time.deltaTime;
			this.TargetPlayer();
			if (!this.audioDevice.isPlaying)
			{
				this.audioDevice.PlayOneShot(this.aud_Loop);
			}
		}
	}

	// Token: 0x0600008C RID: 140 RVA: 0x00005440 File Offset: 0x00003840
	private void FixedUpdate()
	{
		if (this.gc.notebooks >= 7)
		{
			Vector3 vector = this.player.position - base.transform.position;
			RaycastHit raycastHit;
			if (Physics.Raycast(base.transform.position, vector, out raycastHit, float.PositiveInfinity, 3, QueryTriggerInteraction.Ignore) & (raycastHit.transform.tag == "Player") & this.craftersRenderer.isVisible & this.sprite.activeSelf)
			{
				this.gettingAngry = true;
			}
			else
			{
				this.gettingAngry = false;
				this.anger -= 0.1f * Time.deltaTime;
			}
		}
	}

	// Token: 0x0600008D RID: 141 RVA: 0x000054F9 File Offset: 0x000038F9
	public void GiveLocation(Vector3 location, bool flee)
	{
		if (!this.angry)
		{
			this.agent.SetDestination(location);
			if (flee)
			{
				this.forceShowTime = 3f;
			}
		}
	}

	// Token: 0x0600008E RID: 142 RVA: 0x00005524 File Offset: 0x00003924
	private void TargetPlayer()
	{
		this.agent.SetDestination(this.player.position);
	}

	// Token: 0x0600008F RID: 143 RVA: 0x00005540 File Offset: 0x00003940
	private void OnTriggerEnter(Collider other)
	{
		if ((other.tag == "Player") & this.angry)
		{
			this.player.position = new Vector3(5f, this.player.position.y, 80f);
			this.baldiAgent.Warp(new Vector3(5f, this.baldi.position.y, 125f));
			this.player.LookAt(new Vector3(this.baldi.position.x, this.player.position.y, this.baldi.position.z));
			this.gc.DespawnCrafters();
		}
	}

	// Token: 0x040000E7 RID: 231
	public bool db;

	// Token: 0x040000E8 RID: 232
	public bool angry;

	// Token: 0x040000E9 RID: 233
	public bool gettingAngry;

	// Token: 0x040000EA RID: 234
	public float anger;

	// Token: 0x040000EB RID: 235
	private float forceShowTime;

	// Token: 0x040000EC RID: 236
	public Transform player;

	// Token: 0x040000ED RID: 237
	public Transform playerCamera;

	// Token: 0x040000EE RID: 238
	public Transform baldi;

	// Token: 0x040000EF RID: 239
	public NavMeshAgent baldiAgent;

	// Token: 0x040000F0 RID: 240
	public GameObject sprite;

	// Token: 0x040000F1 RID: 241
	public GameControllerScript gc;

	// Token: 0x040000F2 RID: 242
	private NavMeshAgent agent;

	// Token: 0x040000F3 RID: 243
	public Renderer craftersRenderer;

	// Token: 0x040000F4 RID: 244
	public SpriteRenderer spriteImage;

	// Token: 0x040000F5 RID: 245
	public Sprite angrySprite;

	// Token: 0x040000F6 RID: 246
	private AudioSource audioDevice;

	// Token: 0x040000F7 RID: 247
	public AudioClip aud_Intro;

	// Token: 0x040000F8 RID: 248
	public AudioClip aud_Loop;
}
