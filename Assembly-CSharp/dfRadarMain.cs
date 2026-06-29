using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000B9 RID: 185
[AddComponentMenu("Daikon Forge/Examples/Radar/Radar Main")]
public class dfRadarMain : MonoBehaviour
{
	// Token: 0x06000A5B RID: 2651 RVA: 0x0002D4D0 File Offset: 0x0002B6D0
	public void Start()
	{
		this.ensureControlReference();
		for (int i = 0; i < this.markerTypes.Count; i++)
		{
			this.markerTypes[i].IsVisible = false;
		}
	}

	// Token: 0x06000A5C RID: 2652 RVA: 0x0002D50B File Offset: 0x0002B70B
	public void LateUpdate()
	{
		this.updateMarkers();
	}

	// Token: 0x06000A5D RID: 2653 RVA: 0x0002D514 File Offset: 0x0002B714
	public void AddMarker(dfRadarMarker item)
	{
		if (string.IsNullOrEmpty(item.markerType))
		{
			return;
		}
		this.ensureControlReference();
		item.marker = this.instantiateMarker(item.markerType);
		if (item.marker == null)
		{
			return;
		}
		if (!string.IsNullOrEmpty(item.outOfRangeType))
		{
			item.outOfRangeMarker = this.instantiateMarker(item.outOfRangeType);
		}
		this.markers.Add(item);
	}

	// Token: 0x06000A5E RID: 2654 RVA: 0x0002D584 File Offset: 0x0002B784
	private dfControl instantiateMarker(string markerName)
	{
		dfControl dfControl = this.markerTypes.Find((dfControl x) => x.name == markerName);
		if (dfControl == null)
		{
			Debug.LogError("Marker type not found: " + markerName);
			return null;
		}
		dfControl dfControl2 = Object.Instantiate<dfControl>(dfControl);
		dfControl2.hideFlags = HideFlags.DontSave;
		dfControl2.IsVisible = true;
		this.control.AddControl(dfControl2);
		return dfControl2;
	}

	// Token: 0x06000A5F RID: 2655 RVA: 0x0002D5FC File Offset: 0x0002B7FC
	public void RemoveMarker(dfRadarMarker item)
	{
		if (this.markers.Remove(item))
		{
			this.ensureControlReference();
			if (item.marker != null)
			{
				Object.Destroy(item.marker);
			}
			if (item.outOfRangeMarker != null)
			{
				Object.Destroy(item.outOfRangeMarker);
			}
			this.control.RemoveControl(item.marker);
		}
	}

	// Token: 0x06000A60 RID: 2656 RVA: 0x0002D660 File Offset: 0x0002B860
	private void ensureControlReference()
	{
		this.control = base.GetComponent<dfControl>();
		if (this.control == null)
		{
			Debug.LogError("Host control not found");
			base.enabled = false;
			return;
		}
		this.control.Pivot = dfPivotPoint.MiddleCenter;
	}

	// Token: 0x06000A61 RID: 2657 RVA: 0x0002D69C File Offset: 0x0002B89C
	private void updateMarkers()
	{
		for (int i = 0; i < this.markers.Count; i++)
		{
			this.updateMarker(this.markers[i]);
		}
	}

	// Token: 0x06000A62 RID: 2658 RVA: 0x0002D6D4 File Offset: 0x0002B8D4
	private void updateMarker(dfRadarMarker item)
	{
		Vector3 position = this.target.transform.position;
		Vector3 position2 = item.transform.position;
		float y = position.x - position2.x;
		float num = position.z - position2.z;
		float num2 = Mathf.Atan2(y, -num) * 57.29578f + 90f + this.target.transform.eulerAngles.y;
		float num3 = Vector3.Distance(position, position2);
		if (num3 > this.maxDetectDistance)
		{
			item.marker.IsVisible = false;
			if (item.outOfRangeMarker != null)
			{
				dfControl outOfRangeMarker = item.outOfRangeMarker;
				outOfRangeMarker.IsVisible = true;
				outOfRangeMarker.transform.position = this.control.transform.position;
				outOfRangeMarker.transform.eulerAngles = new Vector3(0f, 0f, num2 - 90f);
			}
			return;
		}
		if (item.outOfRangeMarker != null)
		{
			item.outOfRangeMarker.IsVisible = false;
		}
		float num4 = num3 * Mathf.Cos(num2 * 0.017453292f);
		float num5 = num3 * Mathf.Sin(num2 * 0.017453292f);
		float num6 = (float)this.radarRadius / this.maxDetectDistance * this.control.PixelsToUnits();
		num4 *= num6;
		num5 *= num6;
		item.marker.transform.localPosition = new Vector3(num4, num5, 0f);
		item.marker.IsVisible = true;
		item.marker.Pivot = dfPivotPoint.MiddleCenter;
	}

	// Token: 0x040004FD RID: 1277
	public GameObject target;

	// Token: 0x040004FE RID: 1278
	public float maxDetectDistance = 100f;

	// Token: 0x040004FF RID: 1279
	public int radarRadius = 100;

	// Token: 0x04000500 RID: 1280
	public List<dfControl> markerTypes;

	// Token: 0x04000501 RID: 1281
	private List<dfRadarMarker> markers = new List<dfRadarMarker>();

	// Token: 0x04000502 RID: 1282
	private dfControl control;
}
