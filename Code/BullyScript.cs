using System;
using UnityEngine;

// Token: 0x02000003 RID: 3
public class BullyScript : MonoBehaviour
{
	// Token: 0x06000005 RID: 5 RVA: 0x000020B0 File Offset: 0x000004B0
	private void Start()
	{
		this.audioDevice = base.GetComponent<AudioSource>();
		this.waitTime = global::UnityEngine.Random.Range(10f, 20f);
	}

	// Token: 0x06000006 RID: 6 RVA: 0x000020D4 File Offset: 0x000004D4
	private void Update()
	{
		if (this.waitTime > 0f)
		{
			this.waitTime -= Time.deltaTime;
		}
		else if (!this.active)
		{
			this.Activate();
		}
		if (this.active)
		{
			this.activeTime += Time.deltaTime;
			if ((this.activeTime >= 60f) & ((base.transform.position - this.player.position).magnitude >= 120f))
			{
				this.Reset();
			}
		}
	}

	// Token: 0x06000007 RID: 7 RVA: 0x00002180 File Offset: 0x00000580
	private void FixedUpdate()
	{
		Vector3 vector = this.player.position - base.transform.position;
		RaycastHit raycastHit;
		if (Physics.Raycast(base.transform.position + new Vector3(0f, 4f, 0f), vector, out raycastHit, float.PositiveInfinity, 3, QueryTriggerInteraction.Ignore) & (raycastHit.transform.tag == "Player") & this.bullyRenderer.isVisible & ((base.transform.position - this.player.position).magnitude <= 20f) & !this.spoken & this.active)
		{
			int num = Mathf.RoundToInt(global::UnityEngine.Random.Range(0f, 1f));
			this.audioDevice.PlayOneShot(this.aud_Taunts[num]);
			this.spoken = true;
		}
	}

	// Token: 0x06000008 RID: 8 RVA: 0x00002278 File Offset: 0x00000678
	private void Activate()
	{
		this.wanderer.GetNewTargetHallway();
		base.transform.position = this.wanderTarget.position + new Vector3(0f, 5f, 0f);
		this.active = true;
	}

	// Token: 0x06000009 RID: 9 RVA: 0x000022C8 File Offset: 0x000006C8
	private void OnTriggerEnter(Collider other)
	{
		if (other.transform.tag == "Player")
		{
			if ((this.gc.item[0] == 0) & (this.gc.item[1] == 0) & (this.gc.item[2] == 0))
			{
				this.audioDevice.PlayOneShot(this.aud_Denied);
			}
			else
			{
				int num = Mathf.RoundToInt(global::UnityEngine.Random.Range(0f, 2f));
				while (this.gc.item[num] == 0)
				{
					num = Mathf.RoundToInt(global::UnityEngine.Random.Range(0f, 2f));
				}
				this.gc.LoseItem(num);
				int num2 = Mathf.RoundToInt(global::UnityEngine.Random.Range(0f, 1f));
				this.audioDevice.PlayOneShot(this.aud_Thanks[num2]);
				this.Reset();
			}
		}
	}

	// Token: 0x0600000A RID: 10 RVA: 0x000023B8 File Offset: 0x000007B8
	private void Reset()
	{
		base.transform.position = base.transform.position - new Vector3(0f, 20f, 0f);
		this.waitTime = global::UnityEngine.Random.Range(60f, 120f);
		this.active = false;
		this.spoken = false;
	}

	// Token: 0x04000003 RID: 3
	public Transform player;

	// Token: 0x04000004 RID: 4
	public GameControllerScript gc;

	// Token: 0x04000005 RID: 5
	public Renderer bullyRenderer;

	// Token: 0x04000006 RID: 6
	public Transform wanderTarget;

	// Token: 0x04000007 RID: 7
	public AILocationSelectorScript wanderer;

	// Token: 0x04000008 RID: 8
	public float waitTime;

	// Token: 0x04000009 RID: 9
	public float activeTime;

	// Token: 0x0400000A RID: 10
	public bool active;

	// Token: 0x0400000B RID: 11
	public bool spoken;

	// Token: 0x0400000C RID: 12
	private AudioSource audioDevice;

	// Token: 0x0400000D RID: 13
	public AudioClip[] aud_Taunts = new AudioClip[2];

	// Token: 0x0400000E RID: 14
	public AudioClip[] aud_Thanks = new AudioClip[2];

	// Token: 0x0400000F RID: 15
	public AudioClip aud_Denied;
}
