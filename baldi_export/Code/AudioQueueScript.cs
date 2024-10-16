using System;
using UnityEngine;

// Token: 0x02000008 RID: 8
public class AudioQueueScript : MonoBehaviour
{
	// Token: 0x0600001E RID: 30 RVA: 0x00002A0B File Offset: 0x00000E0B
	private void Start()
	{
		this.audioDevice = base.GetComponent<AudioSource>();
	}

	// Token: 0x0600001F RID: 31 RVA: 0x00002A19 File Offset: 0x00000E19
	private void Update()
	{
		if (!this.audioDevice.isPlaying && this.audioInQueue > 0)
		{
			this.PlayQueue();
		}
	}

	// Token: 0x06000020 RID: 32 RVA: 0x00002A3D File Offset: 0x00000E3D
	public void QueueAudio(AudioClip sound)
	{
		this.audioQueue[this.audioInQueue] = sound;
		this.audioInQueue++;
	}

	// Token: 0x06000021 RID: 33 RVA: 0x00002A5B File Offset: 0x00000E5B
	private void PlayQueue()
	{
		this.audioDevice.PlayOneShot(this.audioQueue[0]);
		this.UnqueueAudio();
	}

	// Token: 0x06000022 RID: 34 RVA: 0x00002A78 File Offset: 0x00000E78
	private void UnqueueAudio()
	{
		for (int i = 1; i < this.audioInQueue; i++)
		{
			this.audioQueue[i - 1] = this.audioQueue[i];
		}
		this.audioInQueue--;
	}

	// Token: 0x06000023 RID: 35 RVA: 0x00002ABC File Offset: 0x00000EBC
	public void ClearAudioQueue()
	{
		this.audioInQueue = 0;
	}

	// Token: 0x04000030 RID: 48
	private AudioSource audioDevice;

	// Token: 0x04000031 RID: 49
	private int audioInQueue;

	// Token: 0x04000032 RID: 50
	private AudioClip[] audioQueue = new AudioClip[100];
}
