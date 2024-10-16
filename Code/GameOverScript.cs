using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Token: 0x02000014 RID: 20
public class GameOverScript : MonoBehaviour
{
	// Token: 0x06000063 RID: 99 RVA: 0x000041B4 File Offset: 0x000025B4
	private void Start()
	{
		this.image = base.GetComponent<Image>();
		this.audioDevice = base.GetComponent<AudioSource>();
		this.delay = 5f;
		this.chance = global::UnityEngine.Random.Range(1f, 99f);
		if (this.chance < 98f)
		{
			this.image.sprite = this.images[global::UnityEngine.Random.Range(0, 4)];
		}
		else
		{
			this.image.sprite = this.rare;
		}
	}

	// Token: 0x06000064 RID: 100 RVA: 0x00004238 File Offset: 0x00002638
	private void Update()
	{
		this.delay -= 1f * Time.deltaTime;
		if (this.delay <= 0f)
		{
			if (this.chance < 98f)
			{
				Cursor.lockState = CursorLockMode.None;
				Cursor.visible = true;
				SceneManager.LoadScene("MainMenu");
			}
			else
			{
				this.image.transform.localScale = new Vector3(5f, 5f, 1f);
				this.image.color = Color.red;
				if (!this.audioDevice.isPlaying)
				{
					this.audioDevice.Play();
				}
				if (this.delay <= -5f)
				{
					Application.Quit();
				}
			}
		}
	}

	// Token: 0x0400009D RID: 157
	private Image image;

	// Token: 0x0400009E RID: 158
	private float delay;

	// Token: 0x0400009F RID: 159
	public Sprite[] images = new Sprite[5];

	// Token: 0x040000A0 RID: 160
	public Sprite rare;

	// Token: 0x040000A1 RID: 161
	private float chance;

	// Token: 0x040000A2 RID: 162
	private AudioSource audioDevice;
}
