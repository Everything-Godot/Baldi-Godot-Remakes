using System;
using UnityEngine;

// Token: 0x0200000F RID: 15
public class SwingingDoorScript : MonoBehaviour
{
	// Token: 0x0600003D RID: 61 RVA: 0x0000321D File Offset: 0x0000161D
	private void Start()
	{
		this.myAudio = base.GetComponent<AudioSource>();
		this.bDoorLocked = true;
	}

	// Token: 0x0600003E RID: 62 RVA: 0x00003234 File Offset: 0x00001634
	private void Update()
	{
		if (!this.requirementMet & (this.gc.notebooks >= 2))
		{
			this.requirementMet = true;
			this.UnlockDoor();
		}
		if (this.openTime > 0f)
		{
			this.openTime -= 1f * Time.deltaTime;
		}
		if (this.lockTime > 0f)
		{
			this.lockTime -= Time.deltaTime;
		}
		else if (this.bDoorLocked & this.requirementMet)
		{
			this.UnlockDoor();
		}
		if ((this.openTime <= 0f) & this.bDoorOpen & !this.bDoorLocked)
		{
			this.bDoorOpen = false;
			this.inside.sharedMaterial = this.closed;
			this.outside.sharedMaterial = this.closed;
		}
	}

	// Token: 0x0600003F RID: 63 RVA: 0x00003330 File Offset: 0x00001730
	private void OnTriggerStay(Collider other)
	{
		if (!this.bDoorLocked)
		{
			this.bDoorOpen = true;
			this.inside.sharedMaterial = this.open;
			this.outside.sharedMaterial = this.open;
			this.openTime = 2f;
		}
	}

	// Token: 0x06000040 RID: 64 RVA: 0x0000337C File Offset: 0x0000177C
	private void OnTriggerEnter(Collider other)
	{
		if ((this.gc.notebooks < 2) & (other.tag == "Player"))
		{
			this.myAudio.PlayOneShot(this.baldiDoor, 1f);
		}
		else if (!this.bDoorLocked)
		{
			this.myAudio.PlayOneShot(this.doorOpen, 1f);
			if (other.tag == "Player" && this.baldi.isActiveAndEnabled)
			{
				this.baldi.Hear(base.transform.position, 1f);
			}
		}
	}

	// Token: 0x06000041 RID: 65 RVA: 0x0000342C File Offset: 0x0000182C
	public void LockDoor(float time)
	{
		this.barrier.enabled = true;
		this.obstacle.SetActive(true);
		this.bDoorLocked = true;
		this.lockTime = time;
		this.inside.sharedMaterial = this.locked;
		this.outside.sharedMaterial = this.locked;
	}

	// Token: 0x06000042 RID: 66 RVA: 0x00003484 File Offset: 0x00001884
	private void UnlockDoor()
	{
		this.barrier.enabled = false;
		this.obstacle.SetActive(false);
		this.bDoorLocked = false;
		this.inside.sharedMaterial = this.closed;
		this.outside.sharedMaterial = this.closed;
	}

	// Token: 0x04000055 RID: 85
	public GameControllerScript gc;

	// Token: 0x04000056 RID: 86
	public BaldiScript baldi;

	// Token: 0x04000057 RID: 87
	public MeshCollider barrier;

	// Token: 0x04000058 RID: 88
	public GameObject obstacle;

	// Token: 0x04000059 RID: 89
	public MeshCollider trigger;

	// Token: 0x0400005A RID: 90
	public MeshRenderer inside;

	// Token: 0x0400005B RID: 91
	public MeshRenderer outside;

	// Token: 0x0400005C RID: 92
	public Material closed;

	// Token: 0x0400005D RID: 93
	public Material open;

	// Token: 0x0400005E RID: 94
	public Material locked;

	// Token: 0x0400005F RID: 95
	public AudioClip doorOpen;

	// Token: 0x04000060 RID: 96
	public AudioClip baldiDoor;

	// Token: 0x04000061 RID: 97
	private float openTime;

	// Token: 0x04000062 RID: 98
	private float lockTime;

	// Token: 0x04000063 RID: 99
	private bool bDoorOpen;

	// Token: 0x04000064 RID: 100
	private bool bDoorLocked;

	// Token: 0x04000065 RID: 101
	private bool requirementMet;

	// Token: 0x04000066 RID: 102
	private AudioSource myAudio;
}
