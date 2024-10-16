using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000006 RID: 6
public class JumpRopeScript : MonoBehaviour
{
	// Token: 0x06000012 RID: 18 RVA: 0x000024E0 File Offset: 0x000008E0
	private void OnEnable()
	{
		this.jumpDelay = 7f;
		this.ropeHit = true;
		this.jumps = 0;
		this.jumpCount.text = 0 + "/5";
		this.playtime.audioDevice.PlayOneShot(this.playtime.aud_Instrcutions);
	}

	// Token: 0x06000013 RID: 19 RVA: 0x0000253C File Offset: 0x0000093C
	private void Update()
	{
		if (this.jumpDelay > 0f)
		{
			this.jumpDelay -= Time.deltaTime;
		}
		else if (!this.jumpStarted)
		{
			this.jumpStarted = true;
			this.ropePosition = 1f;
			this.rope.SetTrigger("ActivateJumpRope");
			this.ropeHit = false;
		}
		if (this.ropePosition > 0f)
		{
			this.ropePosition -= Time.deltaTime;
		}
		else if (!this.ropeHit)
		{
			this.RopeHit();
		}
	}

	// Token: 0x06000014 RID: 20 RVA: 0x000025DC File Offset: 0x000009DC
	private void RopeHit()
	{
		this.ropeHit = true;
		if (this.cs.jumpHeight == 0f)
		{
			this.Fail();
		}
		else
		{
			this.Success();
		}
		this.jumpStarted = false;
	}

	// Token: 0x06000015 RID: 21 RVA: 0x00002614 File Offset: 0x00000A14
	private void Success()
	{
		this.playtime.audioDevice.PlayOneShot(this.playtime.aud_Numbers[this.jumps]);
		this.jumps++;
		this.jumpCount.text = this.jumps + "/5";
		this.jumpDelay = 0.5f;
		if (this.jumps >= 5)
		{
			this.ps.DeactivateJumpRope();
			this.playtime.audioDevice.PlayOneShot(this.playtime.aud_Congrats);
		}
	}

	// Token: 0x06000016 RID: 22 RVA: 0x000026B0 File Offset: 0x00000AB0
	private void Fail()
	{
		this.jumps = 0;
		this.jumpCount.text = this.jumps + "/5";
		this.jumpDelay = 8f;
		this.playtime.audioDevice.PlayOneShot(this.playtime.aud_Oops);
	}

	// Token: 0x04000014 RID: 20
	public Text jumpCount;

	// Token: 0x04000015 RID: 21
	public Animator rope;

	// Token: 0x04000016 RID: 22
	public CameraScript cs;

	// Token: 0x04000017 RID: 23
	public PlayerScript ps;

	// Token: 0x04000018 RID: 24
	public PlaytimeScript playtime;

	// Token: 0x04000019 RID: 25
	public int jumps;

	// Token: 0x0400001A RID: 26
	public float jumpDelay;

	// Token: 0x0400001B RID: 27
	public float ropePosition;

	// Token: 0x0400001C RID: 28
	public bool ropeHit;

	// Token: 0x0400001D RID: 29
	public bool jumpStarted;
}
