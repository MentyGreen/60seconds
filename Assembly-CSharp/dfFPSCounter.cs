using System;
using UnityEngine;

// Token: 0x020000B3 RID: 179
[AddComponentMenu("Daikon Forge/Examples/General/FPS Counter")]
public class dfFPSCounter : MonoBehaviour
{
	// Token: 0x06000A41 RID: 2625 RVA: 0x0002CA68 File Offset: 0x0002AC68
	private void Start()
	{
		this.label = base.GetComponent<dfLabel>();
		if (this.label == null)
		{
			Debug.LogError("FPS Counter needs a Label component!");
		}
		this.timeleft = this.updateInterval;
		this.label.Text = "";
	}

	// Token: 0x06000A42 RID: 2626 RVA: 0x0002CAB8 File Offset: 0x0002ACB8
	private void Update()
	{
		if (this.label == null)
		{
			return;
		}
		this.timeleft -= Time.deltaTime;
		this.accum += Time.timeScale / Time.deltaTime;
		this.frames++;
		if ((double)this.timeleft <= 0.0)
		{
			float num = this.accum / (float)this.frames;
			string text = string.Format("{0:F0} FPS", num);
			this.label.Text = text;
			if (num < 30f)
			{
				this.label.Color = Color.yellow;
			}
			else if (num < 10f)
			{
				this.label.Color = Color.red;
			}
			else
			{
				this.label.Color = Color.green;
			}
			this.timeleft = this.updateInterval;
			this.accum = 0f;
			this.frames = 0;
		}
	}

	// Token: 0x040004E3 RID: 1251
	public float updateInterval = 0.5f;

	// Token: 0x040004E4 RID: 1252
	private float accum;

	// Token: 0x040004E5 RID: 1253
	private int frames;

	// Token: 0x040004E6 RID: 1254
	private float timeleft;

	// Token: 0x040004E7 RID: 1255
	private dfLabel label;
}
