using System;
using UnityEngine;

// Token: 0x020000BA RID: 186
[AddComponentMenu("Daikon Forge/Examples/Radar/Radar Marker")]
public class dfRadarMarker : MonoBehaviour
{
	// Token: 0x06000A64 RID: 2660 RVA: 0x0002D87C File Offset: 0x0002BA7C
	public void OnEnable()
	{
		if (string.IsNullOrEmpty(this.markerType))
		{
			return;
		}
		if (this.radar == null)
		{
			this.radar = (Object.FindObjectOfType(typeof(dfRadarMain)) as dfRadarMain);
			if (this.radar == null)
			{
				Debug.LogWarning("No radar found");
				return;
			}
		}
		this.radar.AddMarker(this);
	}

	// Token: 0x06000A65 RID: 2661 RVA: 0x0002D8E4 File Offset: 0x0002BAE4
	public void OnDisable()
	{
		if (this.radar != null)
		{
			this.radar.RemoveMarker(this);
		}
	}

	// Token: 0x04000503 RID: 1283
	public dfRadarMain radar;

	// Token: 0x04000504 RID: 1284
	public string markerType;

	// Token: 0x04000505 RID: 1285
	public string outOfRangeType;

	// Token: 0x04000506 RID: 1286
	[NonSerialized]
	internal dfControl marker;

	// Token: 0x04000507 RID: 1287
	[NonSerialized]
	internal dfControl outOfRangeMarker;
}
