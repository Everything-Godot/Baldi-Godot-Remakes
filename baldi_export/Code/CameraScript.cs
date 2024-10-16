using System;
using UnityEngine;

// Token: 0x0200000B RID: 11
public class CameraScript : MonoBehaviour
{
	// Token: 0x0600002C RID: 44 RVA: 0x00002C3B File Offset: 0x0000103B
	private void Start()
	{
		this.offset = base.transform.position - this.player.transform.position;
	}

	// Token: 0x0600002D RID: 45 RVA: 0x00002C64 File Offset: 0x00001064
	private void Update()
	{
		if (this.ps.jumpRope)
		{
			this.velocity -= this.gravity * Time.deltaTime;
			this.jumpHeight += this.velocity * Time.deltaTime;
			if (this.jumpHeight <= 0f)
			{
				this.jumpHeight = 0f;
				if (Input.GetKeyDown(KeyCode.Space))
				{
					this.velocity = this.initVelocity;
				}
			}
			this.jumpHeightV3 = new Vector3(0f, this.jumpHeight, 0f);
		}
		else if (Input.GetButton("Look Behind"))
		{
			this.lookBehind = 180;
		}
		else
		{
			this.lookBehind = 0;
		}
	}

	// Token: 0x0600002E RID: 46 RVA: 0x00002D2C File Offset: 0x0000112C
	private void LateUpdate()
	{
		base.transform.position = this.player.transform.position + this.offset;
		if (!this.ps.gameOver & !this.ps.jumpRope)
		{
			base.transform.position = this.player.transform.position + this.offset;
			base.transform.rotation = this.player.transform.rotation * Quaternion.Euler(0f, (float)this.lookBehind, 0f);
		}
		else if (this.ps.gameOver)
		{
			base.transform.position = this.baldi.transform.position + this.baldi.transform.forward * 2f + new Vector3(0f, 5f, 0f);
			base.transform.LookAt(new Vector3(this.baldi.position.x, this.baldi.position.y + 5f, this.baldi.position.z));
		}
		else if (this.ps.jumpRope)
		{
			base.transform.position = this.player.transform.position + this.offset + this.jumpHeightV3;
			base.transform.rotation = this.player.transform.rotation;
		}
	}

	// Token: 0x04000037 RID: 55
	public GameObject player;

	// Token: 0x04000038 RID: 56
	public PlayerScript ps;

	// Token: 0x04000039 RID: 57
	public Transform baldi;

	// Token: 0x0400003A RID: 58
	public float initVelocity;

	// Token: 0x0400003B RID: 59
	public float velocity;

	// Token: 0x0400003C RID: 60
	public float gravity;

	// Token: 0x0400003D RID: 61
	private int lookBehind;

	// Token: 0x0400003E RID: 62
	private Vector3 offset;

	// Token: 0x0400003F RID: 63
	public float jumpHeight;

	// Token: 0x04000040 RID: 64
	private Vector3 jumpHeightV3;
}
