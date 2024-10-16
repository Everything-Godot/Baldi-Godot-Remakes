using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000017 RID: 23
public class MouseSliderScript : MonoBehaviour
{
	// Token: 0x06000073 RID: 115 RVA: 0x00004DA7 File Offset: 0x000031A7
	private void Start()
	{
		this.slider = base.GetComponent<Slider>();
	}

	// Token: 0x06000074 RID: 116 RVA: 0x00004DB5 File Offset: 0x000031B5
	private void Update()
	{
		PlayerPrefs.SetFloat("MouseSensitivity", this.slider.value);
	}

	// Token: 0x040000CA RID: 202
	public Slider slider;
}
