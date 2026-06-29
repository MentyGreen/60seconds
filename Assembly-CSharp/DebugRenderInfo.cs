using System;
using UnityEngine;
using UnityEngine.Profiling;

// Token: 0x020000AD RID: 173
[AddComponentMenu("Daikon Forge/Examples/General/Debug Render Info")]
public class DebugRenderInfo : MonoBehaviour
{
	// Token: 0x06000A2E RID: 2606 RVA: 0x0002C683 File Offset: 0x0002A883
	private void Start()
	{
		this.info = base.GetComponent<dfLabel>();
		if (this.info == null)
		{
			base.enabled = false;
			throw new Exception("No Label component found");
		}
		this.info.Text = "";
	}

	// Token: 0x06000A2F RID: 2607 RVA: 0x0002C6C4 File Offset: 0x0002A8C4
	private void Update()
	{
		if (this.view == null)
		{
			this.view = this.info.GetManager();
		}
		this.frameCount++;
		float num = Time.realtimeSinceStartup - this.lastUpdate;
		if (num < this.interval)
		{
			return;
		}
		this.lastUpdate = Time.realtimeSinceStartup;
		float num2 = 1f / (num / (float)this.frameCount);
		Vector2 vector = new Vector2((float)Screen.width, (float)Screen.height);
		string text = string.Format("{0}x{1}", (int)vector.x, (int)vector.y);
		string format = "Screen : {0}, DrawCalls: {1}, Triangles: {2}, Mem: {3:F0}MB, FPS: {4:F0}";
		float num3 = Profiler.supported ? (Profiler.GetMonoUsedSize() / 1048576f) : ((float)GC.GetTotalMemory(false) / 1048576f);
		string text2 = string.Format(format, new object[]
		{
			text,
			this.view.TotalDrawCalls,
			this.view.TotalTriangles,
			num3,
			num2
		});
		this.info.Text = text2.Trim();
		this.frameCount = 0;
	}

	// Token: 0x040004D3 RID: 1235
	public float interval = 0.5f;

	// Token: 0x040004D4 RID: 1236
	private dfLabel info;

	// Token: 0x040004D5 RID: 1237
	private dfGUIManager view;

	// Token: 0x040004D6 RID: 1238
	private float lastUpdate;

	// Token: 0x040004D7 RID: 1239
	private int frameCount;
}
