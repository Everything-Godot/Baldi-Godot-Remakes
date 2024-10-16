using System;
using UnityEngine;

// Token: 0x0200000E RID: 14
public class DoorScript : MonoBehaviour
{
	// Token: 0x06000036 RID: 54 RVA: 0x00002FC8 File Offset: 0x000013C8
	private void Start()
	{
		this.myAudio = base.GetComponent<AudioSource>();
	}

	// Token: 0x06000037 RID: 55 RVA: 0x00002FD8 File Offset: 0x000013D8
	private void Update()
	{
		if (this.lockTime > 0f)
		{
			this.lockTime -= 1f * Time.deltaTime;
		}
		else if (this.bDoorLocked)
		{
			this.UnlockDoor();
		}
		if (this.openTime > 0f)
		{
			this.openTime -= 1f * Time.deltaTime;
		}
		if ((this.openTime <= 0f) & this.bDoorOpen)
		{
			this.barrier.enabled = true;
			this.invisibleBarrier.enabled = true;
			this.bDoorOpen = false;
			this.inside.sharedMaterial = this.closed;
			this.outside.sharedMaterial = this.closed;
			this.myAudio.PlayOneShot(this.doorClose, 1f);
		}
		if (Input.GetMouseButtonDown(0))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit raycastHit;
			if (Physics.Raycast(ray, out raycastHit) && ((raycastHit.collider == this.trigger) & (Vector3.Distance(this.player.position, base.transform.position) < this.openingDistance) & !this.bDoorLocked))
			{
				if (this.baldi.isActiveAndEnabled)
				{
					this.baldi.Hear(base.transform.position, 1f);
				}
				this.OpenDoor();
			}
		}
	}

	// Token: 0x06000038 RID: 56 RVA: 0x00003160 File Offset: 0x00001560
	public void OpenDoor()
	{
		if (!this.bDoorOpen)
		{
			this.myAudio.PlayOneShot(this.doorOpen, 1f);
		}
		this.barrier.enabled = false;
		this.invisibleBarrier.enabled = false;
		this.bDoorOpen = true;
		this.inside.sharedMaterial = this.open;
		this.outside.sharedMaterial = this.open;
		this.openTime = 3f;
	}

	// Token: 0x06000039 RID: 57 RVA: 0x000031DA File Offset: 0x000015DA
	private void OnTriggerStay(Collider other)
	{
		if (!this.bDoorLocked & other.CompareTag("NPC"))
		{
			this.OpenDoor();
		}
	}

	// Token: 0x0600003A RID: 58 RVA: 0x000031FC File Offset: 0x000015FC
	public void LockDoor(float time)
	{
		this.bDoorLocked = true;
		this.lockTime = time;
	}

	// Token: 0x0600003B RID: 59 RVA: 0x0000320C File Offset: 0x0000160C
	public void UnlockDoor()
	{
		this.bDoorLocked = false;
	}

	// Token: 0x04000044 RID: 68
	public float openingDistance;

	// Token: 0x04000045 RID: 69
	public Transform player;

	// Token: 0x04000046 RID: 70
	public BaldiScript baldi;

	// Token: 0x04000047 RID: 71
	public MeshCollider barrier;

	// Token: 0x04000048 RID: 72
	public MeshCollider trigger;

	// Token: 0x04000049 RID: 73
	public MeshCollider invisibleBarrier;

	// Token: 0x0400004A RID: 74
	public MeshRenderer inside;

	// Token: 0x0400004B RID: 75
	public MeshRenderer outside;

	// Token: 0x0400004C RID: 76
	public AudioClip doorOpen;

	// Token: 0x0400004D RID: 77
	public AudioClip doorClose;

	// Token: 0x0400004E RID: 78
	public Material closed;

	// Token: 0x0400004F RID: 79
	public Material open;

	// Token: 0x04000050 RID: 80
	private bool bDoorOpen;

	// Token: 0x04000051 RID: 81
	private bool bDoorLocked;

	// Token: 0x04000052 RID: 82
	private float openTime;

	// Token: 0x04000053 RID: 83
	public float lockTime;

	// Token: 0x04000054 RID: 84
	private AudioSource myAudio;
}
