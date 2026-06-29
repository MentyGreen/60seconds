using System;
using System.Collections;
using UnityEngine;

// Token: 0x020000DC RID: 220
[AddComponentMenu("Utilities/HUDFPS")]
public class FPSCounter : MonoBehaviour
{
	// Token: 0x06000B6E RID: 2926 RVA: 0x000317C9 File Offset: 0x0002F9C9
	private void Start()
	{
		this.startRect = new Rect((float)(Screen.width / 2) - 37f, 10f, 75f, 50f);
		base.StartCoroutine(this.FPS());
	}

	// Token: 0x06000B6F RID: 2927 RVA: 0x00031800 File Offset: 0x0002FA00
	private void Update()
	{
		this.accum += Time.timeScale / Time.deltaTime;
		this.frames++;
	}

	// Token: 0x06000B70 RID: 2928 RVA: 0x00031828 File Offset: 0x0002FA28
	private IEnumerator FPS()
	{
		for (;;)
		{
			float num = this.accum / (float)this.frames;
			this.sFPS = num.ToString("f" + Mathf.Clamp(this.nbDecimal, 0, 10).ToString());
			this.color = ((num >= 30f) ? Color.green : ((num > 10f) ? Color.yellow : Color.red));
			this.accum = 0f;
			this.frames = 0;
			yield return new WaitForSeconds(this.frequency);
		}
		yield break;
	}

	// Token: 0x06000B71 RID: 2929 RVA: 0x00031838 File Offset: 0x0002FA38
	private void OnGUI()
	{
		if (this.style == null)
		{
			this.style = new GUIStyle(GUI.skin.label);
			this.style.normal.textColor = Color.white;
			this.style.alignment = TextAnchor.MiddleCenter;
		}
		GUI.color = (this.updateColor ? this.color : Color.white);
		this.startRect = GUI.Window(0, this.startRect, new GUI.WindowFunction(this.DoMyWindow), "");
	}

	// Token: 0x06000B72 RID: 2930 RVA: 0x000318C0 File Offset: 0x0002FAC0
	private void DoMyWindow(int windowID)
	{
		GUI.Label(new Rect(0f, 0f, this.startRect.width, this.startRect.height), this.sFPS + " FPS", this.style);
		if (this.allowDrag)
		{
			GUI.DragWindow(new Rect(0f, 0f, (float)Screen.width, (float)Screen.height));
		}
	}

	// Token: 0x040005C8 RID: 1480
	public Rect startRect;

	// Token: 0x040005C9 RID: 1481
	public bool updateColor = true;

	// Token: 0x040005CA RID: 1482
	public bool allowDrag = true;

	// Token: 0x040005CB RID: 1483
	public float frequency = 0.5f;

	// Token: 0x040005CC RID: 1484
	public int nbDecimal = 1;

	// Token: 0x040005CD RID: 1485
	private float accum;

	// Token: 0x040005CE RID: 1486
	private int frames;

	// Token: 0x040005CF RID: 1487
	private Color color = Color.white;

	// Token: 0x040005D0 RID: 1488
	private string sFPS = "";

	// Token: 0x040005D1 RID: 1489
	private GUIStyle style;
}
