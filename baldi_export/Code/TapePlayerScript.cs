using System;
using UnityEngine;

// Token: 0x02000026 RID: 38
public class TapePlayerScript : MonoBehaviour
{
	// Token: 0x060000C1 RID: 193 RVA: 0x0000684C File Offset: 0x00004C4C
	private void Start()
	{
		this.audioDevice = base.GetComponent<AudioSource>();
	}

	// Token: 0x060000C2 RID: 194 RVA: 0x0000685A File Offset: 0x00004C5A
	private void Update()
	{
	}

	// Token: 0x060000C3 RID: 195 RVA: 0x0000685C File Offset: 0x00004C5C
	public void Play()
	{
		this.sprite.sprite = this.closedSprite;
		this.audVal = Mathf.RoundToInt(global::UnityEngine.Random.Range(0f, 4f));
		this.audioDevice.PlayOneShot(this.recordings[this.audVal]);
		this.baldi.Hear(base.transform.position, 4f);
	}

	// Token: 0x04000142 RID: 322
	public Sprite closedSprite;

	// Token: 0x04000143 RID: 323
	public SpriteRenderer sprite;

	// Token: 0x04000144 RID: 324
	private int audVal;

	// Token: 0x04000145 RID: 325
	public AudioClip[] recordings = new AudioClip[5];

	// Token: 0x04000146 RID: 326
	public BaldiScript baldi;

	// Token: 0x04000147 RID: 327
	private AudioSource audioDevice;
}
