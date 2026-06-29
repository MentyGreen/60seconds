using System;
using UnityEngine;

// Token: 0x0200002B RID: 43
[AddComponentMenu("Daikon Forge/Input/Debugging/Simulate Touch with Mouse")]
public class dfMouseTouchSourceComponent : dfTouchInputSourceComponent
{
	// Token: 0x1700013A RID: 314
	// (get) Token: 0x0600057B RID: 1403 RVA: 0x0001AC3F File Offset: 0x00018E3F
	public override IDFTouchInputSource Source
	{
		get
		{
			if (this.editorOnly && !Application.isEditor)
			{
				return null;
			}
			if (this.source == null)
			{
				this.source = new dfMouseTouchInputSource();
			}
			return this.source;
		}
	}

	// Token: 0x0600057C RID: 1404 RVA: 0x0001AC6B File Offset: 0x00018E6B
	public void Start()
	{
		base.useGUILayout = false;
	}

	// Token: 0x0600057D RID: 1405 RVA: 0x0001AC74 File Offset: 0x00018E74
	public void OnGUI()
	{
		if (this.source != null)
		{
			this.source.MirrorAlt = (!Event.current.control && !Event.current.shift);
			this.source.ParallelAlt = (!this.source.MirrorAlt && Event.current.shift);
		}
	}

	// Token: 0x040001E0 RID: 480
	public bool editorOnly = true;

	// Token: 0x040001E1 RID: 481
	private dfMouseTouchInputSource source;
}
