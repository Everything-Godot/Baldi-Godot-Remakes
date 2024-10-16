using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000015 RID: 21
public class MathGameScript : MonoBehaviour
{
	// Token: 0x06000066 RID: 102 RVA: 0x00004350 File Offset: 0x00002750
	private void Start()
	{
		this.baldiAudio = base.GetComponent<AudioSource>();
		this.gc.ActivateLearningGame();
		this.QueueAudio(this.bal_intro);
		this.QueueAudio(this.bal_howto);
		this.NewProblem();
		if (this.gc.spoopMode)
		{
			this.baldiFeedTransform.position = new Vector3(-1000f, -1000f, 0f);
		}
	}

	// Token: 0x06000067 RID: 103 RVA: 0x000043C4 File Offset: 0x000027C4
	private void Update()
	{
		if (!this.baldiAudio.isPlaying)
		{
			if ((this.audioInQueue > 0) & !this.gc.spoopMode)
			{
				this.PlayQueue();
			}
			this.baldiFeed.SetBool("talking", false);
		}
		else
		{
			this.baldiFeed.SetBool("talking", true);
		}
		if ((Input.GetKeyDown("return") || Input.GetKeyDown("enter")) & this.questionInProgress)
		{
			this.questionInProgress = false;
			this.CheckAnswer();
		}
		if (this.problem > 3)
		{
			this.endDelay--;
			if (this.endDelay <= 0)
			{
				this.ExitGame();
			}
		}
	}

	// Token: 0x06000068 RID: 104 RVA: 0x00004490 File Offset: 0x00002890
	private void NewProblem()
	{
		this.playerAnswer.text = string.Empty;
		this.problem++;
		if (this.problem <= 3)
		{
			this.QueueAudio(this.bal_problems[this.problem - 1]);
			if (this.problem <= 2 || this.gc.notebooks <= 1)
			{
				this.num1 = (float)Mathf.RoundToInt(global::UnityEngine.Random.Range(0f, 9f));
				this.num2 = (float)Mathf.RoundToInt(global::UnityEngine.Random.Range(0f, 9f));
				this.sign = Mathf.RoundToInt(global::UnityEngine.Random.Range(0f, 1f));
				this.QueueAudio(this.bal_numbers[Mathf.RoundToInt(this.num1)]);
				if (this.sign == 0)
				{
					this.solution = this.num1 + this.num2;
					this.questionText.text = string.Concat(new object[] { "SOLVE MATH Q", this.problem, ": \n \n", this.num1, "+", this.num2, "=" });
					this.QueueAudio(this.bal_plus);
				}
				else if (this.sign == 1)
				{
					this.solution = this.num1 - this.num2;
					this.questionText.text = string.Concat(new object[] { "SOLVE MATH Q", this.problem, ": \n \n", this.num1, "-", this.num2, "=" });
					this.QueueAudio(this.bal_minus);
				}
				this.QueueAudio(this.bal_numbers[Mathf.RoundToInt(this.num2)]);
				this.QueueAudio(this.bal_equals);
			}
			else
			{
				this.impossibleMode = true;
				this.num1 = global::UnityEngine.Random.Range(1f, 9999f);
				this.num2 = global::UnityEngine.Random.Range(1f, 9999f);
				this.num3 = global::UnityEngine.Random.Range(1f, 9999f);
				this.sign = Mathf.RoundToInt((float)global::UnityEngine.Random.Range(0, 1));
				this.QueueAudio(this.bal_screech);
				if (this.sign == 0)
				{
					this.questionText.text = string.Concat(new object[] { "SOLVE MATH Q", this.problem, ": \n", this.num1, "+(", this.num2, "X", this.num3, "=" });
					this.QueueAudio(this.bal_plus);
					this.QueueAudio(this.bal_screech);
					this.QueueAudio(this.bal_times);
					this.QueueAudio(this.bal_screech);
				}
				else if (this.sign == 1)
				{
					this.questionText.text = string.Concat(new object[] { "SOLVE MATH Q", this.problem, ": \n (", this.num1, "/", this.num2, ")+", this.num3, "=" });
					this.QueueAudio(this.bal_divided);
					this.QueueAudio(this.bal_screech);
					this.QueueAudio(this.bal_plus);
					this.QueueAudio(this.bal_screech);
				}
				this.num1 = global::UnityEngine.Random.Range(1f, 9999f);
				this.num2 = global::UnityEngine.Random.Range(1f, 9999f);
				this.num3 = global::UnityEngine.Random.Range(1f, 9999f);
				this.sign = Mathf.RoundToInt((float)global::UnityEngine.Random.Range(0, 1));
				if (this.sign == 0)
				{
					this.questionText2.text = string.Concat(new object[] { "SOLVE MATH Q", this.problem, ": \n", this.num1, "+(", this.num2, "X", this.num3, "=" });
				}
				else if (this.sign == 1)
				{
					this.questionText2.text = string.Concat(new object[] { "SOLVE MATH Q", this.problem, ": \n (", this.num1, "/", this.num2, ")+", this.num3, "=" });
				}
				this.num1 = global::UnityEngine.Random.Range(1f, 9999f);
				this.num2 = global::UnityEngine.Random.Range(1f, 9999f);
				this.num3 = global::UnityEngine.Random.Range(1f, 9999f);
				this.sign = Mathf.RoundToInt((float)global::UnityEngine.Random.Range(0, 1));
				if (this.sign == 0)
				{
					this.questionText3.text = string.Concat(new object[] { "SOLVE MATH Q", this.problem, ": \n", this.num1, "+(", this.num2, "X", this.num3, "=" });
				}
				else if (this.sign == 1)
				{
					this.questionText3.text = string.Concat(new object[] { "SOLVE MATH Q", this.problem, ": \n (", this.num1, "/", this.num2, ")+", this.num3, "=" });
				}
				this.QueueAudio(this.bal_equals);
			}
			this.playerAnswer.ActivateInputField();
			this.questionInProgress = true;
		}
		else
		{
			this.endDelay = 300;
			this.questionText.text = "WOW! YOU EXIST!";
		}
	}

	// Token: 0x06000069 RID: 105 RVA: 0x00004B6C File Offset: 0x00002F6C
	private void CheckAnswer()
	{
		if ((this.playerAnswer.text == this.solution.ToString()) & !this.impossibleMode)
		{
			this.results[this.problem - 1].texture = this.correct;
			this.baldiAudio.Stop();
			this.ClearAudioQueue();
			this.QueueAudio(this.bal_praises[global::UnityEngine.Random.Range(0, 4)]);
			this.NewProblem();
		}
		else
		{
			this.results[this.problem - 1].texture = this.incorrect;
			if (!this.gc.spoopMode)
			{
				this.baldiFeed.SetTrigger("angry");
				this.gc.ActivateSpoopMode();
			}
			this.baldiScript.GetAngry((float)this.problem);
			this.ClearAudioQueue();
			this.baldiAudio.Stop();
			this.NewProblem();
		}
	}

	// Token: 0x0600006A RID: 106 RVA: 0x00004C61 File Offset: 0x00003061
	private void QueueAudio(AudioClip sound)
	{
		this.audioQueue[this.audioInQueue] = sound;
		this.audioInQueue++;
	}

	// Token: 0x0600006B RID: 107 RVA: 0x00004C7F File Offset: 0x0000307F
	private void PlayQueue()
	{
		this.baldiAudio.PlayOneShot(this.audioQueue[0]);
		this.UnqueueAudio();
	}

	// Token: 0x0600006C RID: 108 RVA: 0x00004C9C File Offset: 0x0000309C
	private void UnqueueAudio()
	{
		for (int i = 1; i < this.audioInQueue; i++)
		{
			this.audioQueue[i - 1] = this.audioQueue[i];
		}
		this.audioInQueue--;
	}

	// Token: 0x0600006D RID: 109 RVA: 0x00004CE0 File Offset: 0x000030E0
	private void ClearAudioQueue()
	{
		this.audioInQueue = 0;
	}

	// Token: 0x0600006E RID: 110 RVA: 0x00004CE9 File Offset: 0x000030E9
	private void ExitGame()
	{
		this.gc.DeactivateLearningGame(this.mathGame);
	}

	// Token: 0x040000A3 RID: 163
	public GameControllerScript gc;

	// Token: 0x040000A4 RID: 164
	public BaldiScript baldiScript;

	// Token: 0x040000A5 RID: 165
	public GameObject mathGame;

	// Token: 0x040000A6 RID: 166
	public RawImage[] results = new RawImage[3];

	// Token: 0x040000A7 RID: 167
	public Texture correct;

	// Token: 0x040000A8 RID: 168
	public Texture incorrect;

	// Token: 0x040000A9 RID: 169
	public InputField playerAnswer;

	// Token: 0x040000AA RID: 170
	public Text questionText;

	// Token: 0x040000AB RID: 171
	public Text questionText2;

	// Token: 0x040000AC RID: 172
	public Text questionText3;

	// Token: 0x040000AD RID: 173
	public Animator baldiFeed;

	// Token: 0x040000AE RID: 174
	public Transform baldiFeedTransform;

	// Token: 0x040000AF RID: 175
	public AudioClip bal_plus;

	// Token: 0x040000B0 RID: 176
	public AudioClip bal_minus;

	// Token: 0x040000B1 RID: 177
	public AudioClip bal_times;

	// Token: 0x040000B2 RID: 178
	public AudioClip bal_divided;

	// Token: 0x040000B3 RID: 179
	public AudioClip bal_equals;

	// Token: 0x040000B4 RID: 180
	public AudioClip bal_howto;

	// Token: 0x040000B5 RID: 181
	public AudioClip bal_intro;

	// Token: 0x040000B6 RID: 182
	public AudioClip bal_screech;

	// Token: 0x040000B7 RID: 183
	public AudioClip[] bal_numbers = new AudioClip[10];

	// Token: 0x040000B8 RID: 184
	public AudioClip[] bal_praises = new AudioClip[5];

	// Token: 0x040000B9 RID: 185
	public AudioClip[] bal_problems = new AudioClip[3];

	// Token: 0x040000BA RID: 186
	private int endDelay;

	// Token: 0x040000BB RID: 187
	private int problem;

	// Token: 0x040000BC RID: 188
	private int audioInQueue;

	// Token: 0x040000BD RID: 189
	private float num1;

	// Token: 0x040000BE RID: 190
	private float num2;

	// Token: 0x040000BF RID: 191
	private float num3;

	// Token: 0x040000C0 RID: 192
	private int sign;

	// Token: 0x040000C1 RID: 193
	private float solution;

	// Token: 0x040000C2 RID: 194
	private bool questionInProgress;

	// Token: 0x040000C3 RID: 195
	private bool impossibleMode;

	// Token: 0x040000C4 RID: 196
	private AudioClip[] audioQueue = new AudioClip[20];

	// Token: 0x040000C5 RID: 197
	private AudioSource baldiAudio;
}
