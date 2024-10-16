using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Token: 0x02000013 RID: 19
public class GameControllerScript : MonoBehaviour
{
	// Token: 0x0600004B RID: 75 RVA: 0x000035B0 File Offset: 0x000019B0
	public GameControllerScript()
	{
		int[] array = new int[3];
		array[0] = -80;
		array[1] = -40;
		this.itemSelectOffset = array;
		base..ctor();
	}

	// Token: 0x0600004C RID: 76 RVA: 0x00003655 File Offset: 0x00001A55
	private void Start()
	{
		this.audioDevice = base.GetComponent<AudioSource>();
		this.schoolMusic.Play();
		this.LockMouse();
		this.UpdateNotebookCount();
		this.itemSelected = 0;
		this.gameOverDelay = 60f;
	}

	// Token: 0x0600004D RID: 77 RVA: 0x0000368C File Offset: 0x00001A8C
	private void Update()
	{
		if (!this.learningActive)
		{
			if (Input.GetButtonDown("Pause"))
			{
				if (!this.gamePaused)
				{
					this.PauseGame();
				}
				else
				{
					this.UnpauseGame();
				}
			}
			if (Input.GetKeyDown(KeyCode.Q) & this.gamePaused)
			{
				SceneManager.LoadScene("MainMenu");
			}
			if (!this.gamePaused & (Time.timeScale != 1f))
			{
				Time.timeScale = 1f;
			}
			if (Input.GetMouseButtonDown(1))
			{
				this.UseItem();
			}
			if (Input.GetAxis("Mouse ScrollWheel") > 0f)
			{
				this.DecreaseItemSelection();
			}
			else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
			{
				this.IncreaseItemSelection();
			}
		}
		else if (Time.timeScale != 0f)
		{
			Time.timeScale = 0f;
		}
		if ((this.player.stamina < 0f) & !this.warning.activeSelf)
		{
			this.warning.SetActive(true);
		}
		else if (this.warning.activeSelf)
		{
			this.warning.SetActive(false);
		}
		if (this.player.gameOver)
		{
			Time.timeScale = 0f;
			this.gameOverDelay -= 1f;
			this.audioDevice.PlayOneShot(this.aud_buzz);
			if (this.gameOverDelay <= 0f)
			{
				Time.timeScale = 1f;
				SceneManager.LoadScene("GameOver");
			}
		}
		if (this.finaleMode && !this.audioDevice.isPlaying)
		{
			if (this.exitsReached == 2)
			{
				this.audioDevice.PlayOneShot(this.aud_MachineStart);
			}
			else if (this.exitsReached == 3)
			{
				this.audioDevice.PlayOneShot(this.aud_MachineLoop);
			}
		}
	}

	// Token: 0x0600004E RID: 78 RVA: 0x0000388D File Offset: 0x00001C8D
	private void UpdateNotebookCount()
	{
		this.notebookCount.text = this.notebooks.ToString() + "/7";
		if (this.notebooks == 7)
		{
			this.ActivateFinaleMode();
		}
	}

	// Token: 0x0600004F RID: 79 RVA: 0x000038C7 File Offset: 0x00001CC7
	public void CollectNotebook()
	{
		this.notebooks++;
		this.UpdateNotebookCount();
	}

	// Token: 0x06000050 RID: 80 RVA: 0x000038DD File Offset: 0x00001CDD
	public void LockMouse()
	{
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		this.mouseLocked = true;
		this.reticle.SetActive(true);
	}

	// Token: 0x06000051 RID: 81 RVA: 0x000038FE File Offset: 0x00001CFE
	public void UnlockMouse()
	{
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
		this.mouseLocked = false;
		this.reticle.SetActive(false);
	}

	// Token: 0x06000052 RID: 82 RVA: 0x0000391F File Offset: 0x00001D1F
	private void PauseGame()
	{
		Time.timeScale = 0f;
		this.gamePaused = true;
		this.pauseText.SetActive(true);
	}

	// Token: 0x06000053 RID: 83 RVA: 0x0000393E File Offset: 0x00001D3E
	private void UnpauseGame()
	{
		Time.timeScale = 1f;
		this.gamePaused = false;
		this.pauseText.SetActive(false);
	}

	// Token: 0x06000054 RID: 84 RVA: 0x00003960 File Offset: 0x00001D60
	public void ActivateSpoopMode()
	{
		this.spoopMode = true;
		this.entrance_0.Lower();
		this.entrance_1.Lower();
		this.entrance_2.Lower();
		this.entrance_3.Lower();
		this.baldiTutor.SetActive(false);
		this.baldi.SetActive(true);
		this.principal.SetActive(true);
		this.crafters.SetActive(true);
		this.playtime.SetActive(true);
		this.gottaSweep.SetActive(true);
		this.bully.SetActive(true);
		this.audioDevice.PlayOneShot(this.aud_Hang);
		this.learnMusic.Stop();
		this.schoolMusic.Stop();
	}

	// Token: 0x06000055 RID: 85 RVA: 0x00003A1B File Offset: 0x00001E1B
	private void ActivateFinaleMode()
	{
		this.finaleMode = true;
		this.entrance_0.Raise();
		this.entrance_1.Raise();
		this.entrance_2.Raise();
		this.entrance_3.Raise();
	}

	// Token: 0x06000056 RID: 86 RVA: 0x00003A50 File Offset: 0x00001E50
	public void GetAngry(float value)
	{
		if (!this.spoopMode)
		{
			this.ActivateSpoopMode();
		}
		this.baldiScrpt.GetAngry(value);
	}

	// Token: 0x06000057 RID: 87 RVA: 0x00003A6F File Offset: 0x00001E6F
	public void ActivateLearningGame()
	{
		this.learningActive = true;
		this.UnlockMouse();
		this.tutorBaldi.Stop();
		if (!this.spoopMode)
		{
			this.schoolMusic.Stop();
			this.learnMusic.Play();
		}
	}

	// Token: 0x06000058 RID: 88 RVA: 0x00003AAC File Offset: 0x00001EAC
	public void DeactivateLearningGame(GameObject subject)
	{
		this.learningActive = false;
		this.LockMouse();
		subject.SetActive(false);
		if (!this.spoopMode)
		{
			this.schoolMusic.Play();
			this.learnMusic.Stop();
		}
		if ((this.notebooks == 1) & !this.spoopMode)
		{
			this.quarter.SetActive(true);
			this.tutorBaldi.PlayOneShot(this.aud_Prize);
		}
		else if (this.notebooks == 7)
		{
			this.audioDevice.PlayOneShot(this.aud_AllNotebooks, 0.8f);
		}
	}

	// Token: 0x06000059 RID: 89 RVA: 0x00003B4C File Offset: 0x00001F4C
	private void IncreaseItemSelection()
	{
		this.itemSelected++;
		if (this.itemSelected > 2)
		{
			this.itemSelected = 0;
		}
		this.itemSelect.anchoredPosition = new Vector3((float)this.itemSelectOffset[this.itemSelected], 0f, 0f);
		this.UpdateItemName();
	}

	// Token: 0x0600005A RID: 90 RVA: 0x00003BB0 File Offset: 0x00001FB0
	private void DecreaseItemSelection()
	{
		this.itemSelected--;
		if (this.itemSelected < 0)
		{
			this.itemSelected = 2;
		}
		this.itemSelect.anchoredPosition = new Vector3((float)this.itemSelectOffset[this.itemSelected], 0f, 0f);
		this.UpdateItemName();
	}

	// Token: 0x0600005B RID: 91 RVA: 0x00003C14 File Offset: 0x00002014
	public void CollectItem(int item_ID)
	{
		if (this.item[0] == 0)
		{
			this.item[0] = item_ID;
			this.itemSlot[0].texture = this.itemTextures[item_ID];
		}
		else if (this.item[1] == 0)
		{
			this.item[1] = item_ID;
			this.itemSlot[1].texture = this.itemTextures[item_ID];
		}
		else if (this.item[2] == 0)
		{
			this.item[2] = item_ID;
			this.itemSlot[2].texture = this.itemTextures[item_ID];
		}
		else
		{
			this.item[this.itemSelected] = item_ID;
			this.itemSlot[this.itemSelected].texture = this.itemTextures[item_ID];
		}
		this.UpdateItemName();
	}

	// Token: 0x0600005C RID: 92 RVA: 0x00003CE0 File Offset: 0x000020E0
	private void UseItem()
	{
		if (this.item[this.itemSelected] != 0)
		{
			if (this.item[this.itemSelected] == 1)
			{
				this.player.stamina = this.player.maxStamina * 2f;
				this.ResetItem();
				this.player.ResetGuilt("food", 3f);
			}
			else if (this.item[this.itemSelected] == 2)
			{
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				RaycastHit raycastHit;
				if (Physics.Raycast(ray, out raycastHit) && ((raycastHit.collider.tag == "SwingingDoor") & (Vector3.Distance(this.playerTransform.position, raycastHit.transform.position) <= 10f)))
				{
					raycastHit.collider.gameObject.GetComponent<SwingingDoorScript>().LockDoor(15f);
					this.ResetItem();
				}
			}
			else if (this.item[this.itemSelected] == 3)
			{
				Ray ray2 = Camera.main.ScreenPointToRay(Input.mousePosition);
				RaycastHit raycastHit2;
				if (Physics.Raycast(ray2, out raycastHit2) && ((raycastHit2.collider.tag == "Door") & (Vector3.Distance(this.playerTransform.position, raycastHit2.transform.position) <= 10f)))
				{
					raycastHit2.collider.gameObject.GetComponent<DoorScript>().UnlockDoor();
					raycastHit2.collider.gameObject.GetComponent<DoorScript>().OpenDoor();
					this.ResetItem();
				}
			}
			else if (this.item[this.itemSelected] == 4)
			{
				global::UnityEngine.Object.Instantiate<GameObject>(this.bsodaSpray, this.playerTransform.position, this.cameraTransform.rotation);
				this.ResetItem();
				this.player.ResetGuilt("drink", 3f);
			}
			else if (this.item[this.itemSelected] == 5)
			{
				Ray ray3 = Camera.main.ScreenPointToRay(Input.mousePosition);
				RaycastHit raycastHit3;
				if (Physics.Raycast(ray3, out raycastHit3))
				{
					if ((raycastHit3.collider.name == "BSODAMachine") & (Vector3.Distance(this.playerTransform.position, raycastHit3.transform.position) <= 10f))
					{
						this.ResetItem();
						this.CollectItem(4);
					}
					else if ((raycastHit3.collider.name == "ZestyMachine") & (Vector3.Distance(this.playerTransform.position, raycastHit3.transform.position) <= 10f))
					{
						this.ResetItem();
						this.CollectItem(1);
					}
					else if ((raycastHit3.collider.name == "PayPhone") & (Vector3.Distance(this.playerTransform.position, raycastHit3.transform.position) <= 10f))
					{
						raycastHit3.collider.gameObject.GetComponent<TapePlayerScript>().Play();
						this.ResetItem();
					}
				}
			}
			else if (this.item[this.itemSelected] == 6)
			{
				Ray ray4 = Camera.main.ScreenPointToRay(Input.mousePosition);
				RaycastHit raycastHit4;
				if (Physics.Raycast(ray4, out raycastHit4) && ((raycastHit4.collider.name == "TapePlayer") & (Vector3.Distance(this.playerTransform.position, raycastHit4.transform.position) <= 10f)))
				{
					raycastHit4.collider.gameObject.GetComponent<TapePlayerScript>().Play();
					this.ResetItem();
				}
			}
		}
	}

	// Token: 0x0600005D RID: 93 RVA: 0x000040B6 File Offset: 0x000024B6
	private void ResetItem()
	{
		this.item[this.itemSelected] = 0;
		this.itemSlot[this.itemSelected].texture = this.itemTextures[0];
		this.UpdateItemName();
	}

	// Token: 0x0600005E RID: 94 RVA: 0x000040E6 File Offset: 0x000024E6
	public void LoseItem(int id)
	{
		this.item[id] = 0;
		this.itemSlot[id].texture = this.itemTextures[0];
		this.UpdateItemName();
	}

	// Token: 0x0600005F RID: 95 RVA: 0x0000410C File Offset: 0x0000250C
	private void UpdateItemName()
	{
		this.itemText.text = this.itemNames[this.item[this.itemSelected]];
	}

	// Token: 0x06000060 RID: 96 RVA: 0x00004130 File Offset: 0x00002530
	public void ExitReached()
	{
		this.exitsReached++;
		if (this.exitsReached == 1)
		{
			RenderSettings.ambientLight = Color.red;
			this.audioDevice.PlayOneShot(this.aud_Switch);
		}
		if (this.exitsReached == 3)
		{
			this.audioDevice.PlayOneShot(this.aud_MachineRev);
		}
	}

	// Token: 0x06000061 RID: 97 RVA: 0x0000418F File Offset: 0x0000258F
	public void DespawnCrafters()
	{
		this.crafters.SetActive(false);
	}

	// Token: 0x0400006A RID: 106
	public PlayerScript player;

	// Token: 0x0400006B RID: 107
	public Transform playerTransform;

	// Token: 0x0400006C RID: 108
	public Transform cameraTransform;

	// Token: 0x0400006D RID: 109
	public EntranceScript entrance_0;

	// Token: 0x0400006E RID: 110
	public EntranceScript entrance_1;

	// Token: 0x0400006F RID: 111
	public EntranceScript entrance_2;

	// Token: 0x04000070 RID: 112
	public EntranceScript entrance_3;

	// Token: 0x04000071 RID: 113
	public GameObject baldiTutor;

	// Token: 0x04000072 RID: 114
	public GameObject baldi;

	// Token: 0x04000073 RID: 115
	public BaldiScript baldiScrpt;

	// Token: 0x04000074 RID: 116
	public AudioClip aud_Prize;

	// Token: 0x04000075 RID: 117
	public AudioClip aud_AllNotebooks;

	// Token: 0x04000076 RID: 118
	public GameObject principal;

	// Token: 0x04000077 RID: 119
	public GameObject crafters;

	// Token: 0x04000078 RID: 120
	public GameObject playtime;

	// Token: 0x04000079 RID: 121
	public GameObject gottaSweep;

	// Token: 0x0400007A RID: 122
	public GameObject bully;

	// Token: 0x0400007B RID: 123
	public GameObject quarter;

	// Token: 0x0400007C RID: 124
	public AudioSource tutorBaldi;

	// Token: 0x0400007D RID: 125
	public int notebooks;

	// Token: 0x0400007E RID: 126
	public bool spoopMode;

	// Token: 0x0400007F RID: 127
	public bool finaleMode;

	// Token: 0x04000080 RID: 128
	public bool debugMode;

	// Token: 0x04000081 RID: 129
	public bool mouseLocked;

	// Token: 0x04000082 RID: 130
	public int exitsReached;

	// Token: 0x04000083 RID: 131
	public int itemSelected;

	// Token: 0x04000084 RID: 132
	public int[] item = new int[3];

	// Token: 0x04000085 RID: 133
	public RawImage[] itemSlot = new RawImage[3];

	// Token: 0x04000086 RID: 134
	private string[] itemNames = new string[] { "Nothing", "Energy flavored Zesty Bar", "Yellow Door Lock", "Key", "BSODA", "Quarter", "Tape", "Alarm Clock" };

	// Token: 0x04000087 RID: 135
	public Text itemText;

	// Token: 0x04000088 RID: 136
	public global::UnityEngine.Object[] items = new global::UnityEngine.Object[8];

	// Token: 0x04000089 RID: 137
	public Texture[] itemTextures = new Texture[8];

	// Token: 0x0400008A RID: 138
	public GameObject bsodaSpray;

	// Token: 0x0400008B RID: 139
	public Text notebookCount;

	// Token: 0x0400008C RID: 140
	public GameObject pauseText;

	// Token: 0x0400008D RID: 141
	public GameObject warning;

	// Token: 0x0400008E RID: 142
	public GameObject reticle;

	// Token: 0x0400008F RID: 143
	public RectTransform itemSelect;

	// Token: 0x04000090 RID: 144
	private int[] itemSelectOffset;

	// Token: 0x04000091 RID: 145
	private bool gamePaused;

	// Token: 0x04000092 RID: 146
	private bool learningActive;

	// Token: 0x04000093 RID: 147
	private float gameOverDelay;

	// Token: 0x04000094 RID: 148
	private AudioSource audioDevice;

	// Token: 0x04000095 RID: 149
	public AudioClip aud_buzz;

	// Token: 0x04000096 RID: 150
	public AudioClip aud_Hang;

	// Token: 0x04000097 RID: 151
	public AudioClip aud_MachineStart;

	// Token: 0x04000098 RID: 152
	public AudioClip aud_MachineRev;

	// Token: 0x04000099 RID: 153
	public AudioClip aud_MachineLoop;

	// Token: 0x0400009A RID: 154
	public AudioClip aud_Switch;

	// Token: 0x0400009B RID: 155
	public AudioSource schoolMusic;

	// Token: 0x0400009C RID: 156
	public AudioSource learnMusic;
}
