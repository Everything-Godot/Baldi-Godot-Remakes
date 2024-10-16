using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

// Token: 0x02000021 RID: 33
public class PlayerScript : MonoBehaviour
{
	// Token: 0x060000A4 RID: 164 RVA: 0x00005EA6 File Offset: 0x000042A6
	private void Start()
	{
		this.rb = base.GetComponent<Rigidbody>();
		this.stamina = this.maxStamina;
		this.mouseSensitivity = PlayerPrefs.GetFloat("MouseSensitivity");
	}

	// Token: 0x060000A5 RID: 165 RVA: 0x00005ED0 File Offset: 0x000042D0
	private void Update()
	{
		this.StaminaCheck();
		this.GuiltCheck();
		if (this.moveZ != 0f || this.moveX != 0f)
		{
			this.gc.LockMouse();
		}
		if (this.jumpRope & ((base.transform.position - this.frozenPosition).magnitude >= 1f))
		{
			this.DeactivateJumpRope();
		}
		if (this.sweepingFailsave > 0f)
		{
			this.sweepingFailsave -= Time.deltaTime;
		}
		else
		{
			this.sweeping = false;
		}
	}

	// Token: 0x060000A6 RID: 166 RVA: 0x00005F80 File Offset: 0x00004380
	private void FixedUpdate()
	{
		this.PlayerMove();
		if (!this.jumpRope & !this.sweeping)
		{
			this.rb.velocity = new Vector3(this.moveX, 0f, this.moveZ);
		}
		else if (this.sweeping)
		{
			this.rb.velocity = this.gottaSweep.velocity + new Vector3(this.moveX, 0f, this.moveZ) * 0.2f;
		}
		else
		{
			this.rb.velocity = new Vector3(0f, 0f, 0f);
		}
	}

	// Token: 0x060000A7 RID: 167 RVA: 0x0000603C File Offset: 0x0000443C
	private void PlayerMove()
	{
		this.moveX = 0f;
		this.moveZ = 0f;
		if (this.gc.mouseLocked)
		{
			base.transform.Rotate(0f, Input.GetAxis("Mouse X") * this.mouseSensitivity, 0f);
		}
		float y = base.transform.eulerAngles.y;
		this.db = Input.GetAxisRaw("Forward");
		if (this.stamina > 0f)
		{
			if (Input.GetAxisRaw("Run") > 0f)
			{
				this.playerSpeed = this.runSpeed;
				if (this.rb.velocity.magnitude > 0f)
				{
					this.ResetGuilt("running", 0.2f);
				}
			}
			else
			{
				this.playerSpeed = this.walkSpeed;
			}
		}
		else
		{
			this.playerSpeed = this.slowSpeed;
		}
		if (Input.GetAxis("Forward") > 0f)
		{
			this.moveZ += Mathf.Cos(y * 0.017453292f) * this.playerSpeed;
			this.moveX += Mathf.Sin(y * 0.017453292f) * this.playerSpeed;
		}
		else if (Input.GetAxis("Forward") < 0f)
		{
			this.moveZ += -(Mathf.Cos(y * 0.017453292f) * this.playerSpeed);
			this.moveX += -(Mathf.Sin(y * 0.017453292f) * this.playerSpeed);
		}
		if (Input.GetAxis("Strafe") > 0f)
		{
			this.moveZ += Mathf.Cos((y + 90f) * 0.017453292f) * this.playerSpeed;
			this.moveX += Mathf.Sin((y + 90f) * 0.017453292f) * this.playerSpeed;
		}
		else if (Input.GetAxis("Strafe") < 0f)
		{
			this.moveZ += -(Mathf.Cos((y + 90f) * 0.017453292f) * this.playerSpeed);
			this.moveX += -(Mathf.Sin((y + 90f) * 0.017453292f) * this.playerSpeed);
		}
	}

	// Token: 0x060000A8 RID: 168 RVA: 0x000062B0 File Offset: 0x000046B0
	private void StaminaCheck()
	{
		if (this.moveZ != 0f || this.moveX != 0f)
		{
			if ((Input.GetAxisRaw("Run") > 0f) & (this.stamina > 0f))
			{
				this.stamina -= this.staminaRate * Time.deltaTime;
			}
			if ((this.stamina < 0f) & (this.stamina > -25f))
			{
				this.stamina = -25f;
			}
		}
		else if (this.stamina < this.maxStamina)
		{
			this.stamina += this.staminaRate * Time.deltaTime;
		}
		this.staminaBar.value = this.stamina / this.maxStamina * 100f;
	}

	// Token: 0x060000A9 RID: 169 RVA: 0x00006394 File Offset: 0x00004794
	private void OnTriggerEnter(Collider other)
	{
		if ((other.transform.name == "Baldi") & !this.gc.debugMode)
		{
			this.gameOver = true;
		}
		else if ((other.transform.name == "Playtime") & !this.jumpRope & (this.playtime.playCool <= 0f))
		{
			this.ActivateJumpRope();
		}
	}

	// Token: 0x060000AA RID: 170 RVA: 0x00006416 File Offset: 0x00004816
	private void OnTriggerStay(Collider other)
	{
		if (other.transform.name == "Gotta Sweep")
		{
			this.sweeping = true;
			this.sweepingFailsave = 1f;
		}
	}

	// Token: 0x060000AB RID: 171 RVA: 0x00006444 File Offset: 0x00004844
	private void OnTriggerExit(Collider other)
	{
		if (other.transform.name == "Office Trigger")
		{
			this.ResetGuilt("escape", this.door.lockTime);
		}
		else if (other.transform.name == "Gotta Sweep")
		{
			this.sweeping = false;
		}
	}

	// Token: 0x060000AC RID: 172 RVA: 0x000064A7 File Offset: 0x000048A7
	public void ResetGuilt(string type, float amount)
	{
		if (amount >= this.guilt)
		{
			this.guilt = amount;
			this.guiltType = type;
		}
	}

	// Token: 0x060000AD RID: 173 RVA: 0x000064C3 File Offset: 0x000048C3
	private void GuiltCheck()
	{
		if (this.guilt > 0f)
		{
			this.guilt -= Time.deltaTime;
		}
	}

	// Token: 0x060000AE RID: 174 RVA: 0x000064E8 File Offset: 0x000048E8
	public void ActivateJumpRope()
	{
		this.baldi.Hear(base.transform.position, 2f);
		this.jumpRopeScreen.SetActive(true);
		this.jumpRope = true;
		this.frozenPosition = base.transform.position;
	}

	// Token: 0x060000AF RID: 175 RVA: 0x00006534 File Offset: 0x00004934
	public void DeactivateJumpRope()
	{
		this.jumpRopeScreen.SetActive(false);
		this.jumpRope = false;
	}

	// Token: 0x0400011A RID: 282
	public GameControllerScript gc;

	// Token: 0x0400011B RID: 283
	public BaldiScript baldi;

	// Token: 0x0400011C RID: 284
	public DoorScript door;

	// Token: 0x0400011D RID: 285
	public PlaytimeScript playtime;

	// Token: 0x0400011E RID: 286
	public bool gameOver;

	// Token: 0x0400011F RID: 287
	public bool jumpRope;

	// Token: 0x04000120 RID: 288
	public bool sweeping;

	// Token: 0x04000121 RID: 289
	public float sweepingFailsave;

	// Token: 0x04000122 RID: 290
	public Vector3 frozenPosition;

	// Token: 0x04000123 RID: 291
	public float mouseSensitivity;

	// Token: 0x04000124 RID: 292
	public float walkSpeed;

	// Token: 0x04000125 RID: 293
	public float runSpeed;

	// Token: 0x04000126 RID: 294
	public float slowSpeed;

	// Token: 0x04000127 RID: 295
	public float maxStamina;

	// Token: 0x04000128 RID: 296
	public float staminaRate;

	// Token: 0x04000129 RID: 297
	public float guilt;

	// Token: 0x0400012A RID: 298
	public float initGuilt;

	// Token: 0x0400012B RID: 299
	private float moveX;

	// Token: 0x0400012C RID: 300
	private float moveZ;

	// Token: 0x0400012D RID: 301
	private float playerSpeed;

	// Token: 0x0400012E RID: 302
	public float stamina;

	// Token: 0x0400012F RID: 303
	public Rigidbody rb;

	// Token: 0x04000130 RID: 304
	public NavMeshAgent gottaSweep;

	// Token: 0x04000131 RID: 305
	public Slider staminaBar;

	// Token: 0x04000132 RID: 306
	public float db;

	// Token: 0x04000133 RID: 307
	public string guiltType;

	// Token: 0x04000134 RID: 308
	public GameObject jumpRopeScreen;
}
