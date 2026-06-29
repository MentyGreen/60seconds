using System;
using System.Collections.Generic;
using DunGen;
using UnityEngine;

// Token: 0x020000D7 RID: 215
[AddComponentMenu("DunGen/Doorway")]
public class Doorway : MonoBehaviour
{
	// Token: 0x1700024D RID: 589
	// (get) Token: 0x06000B46 RID: 2886 RVA: 0x0002FBA6 File Offset: 0x0002DDA6
	// (set) Token: 0x06000B47 RID: 2887 RVA: 0x0002FBAE File Offset: 0x0002DDAE
	public Tile Tile { get; internal set; }

	// Token: 0x1700024E RID: 590
	// (get) Token: 0x06000B48 RID: 2888 RVA: 0x0002FBB7 File Offset: 0x0002DDB7
	public bool IsLocked
	{
		get
		{
			return this.LockID != null;
		}
	}

	// Token: 0x06000B49 RID: 2889 RVA: 0x0002FBC4 File Offset: 0x0002DDC4
	private void OnDrawGizmos()
	{
		if (!this.placedByGenerator)
		{
			this.DebugDraw();
		}
	}

	// Token: 0x06000B4A RID: 2890 RVA: 0x0002FBD4 File Offset: 0x0002DDD4
	internal void SetUsedPrefab(GameObject doorPrefab)
	{
		this.doorPrefab = doorPrefab;
	}

	// Token: 0x06000B4B RID: 2891 RVA: 0x0002FBDD File Offset: 0x0002DDDD
	internal void RemoveUsedPrefab()
	{
		if (this.doorPrefab != null)
		{
			Object.DestroyImmediate(this.doorPrefab);
		}
	}

	// Token: 0x06000B4C RID: 2892 RVA: 0x0002FBF8 File Offset: 0x0002DDF8
	internal void DebugDraw()
	{
		Vector2 vector = this.Size * 0.5f;
		Gizmos.color = EditorConstants.DoorDirectionColour;
		float d = Mathf.Min(this.Size.x, this.Size.y);
		Gizmos.DrawLine(base.transform.position + base.transform.up * vector.y, base.transform.position + base.transform.up * vector.y + base.transform.forward * d);
		Gizmos.color = EditorConstants.DoorRectColour;
		Vector3 vector2 = base.transform.position - base.transform.right * vector.x + base.transform.up * this.Size.y;
		Vector3 vector3 = base.transform.position + base.transform.right * vector.x + base.transform.up * this.Size.y;
		Vector3 vector4 = base.transform.position - base.transform.right * vector.x;
		Vector3 vector5 = base.transform.position + base.transform.right * vector.x;
		Gizmos.DrawLine(vector2, vector3);
		Gizmos.DrawLine(vector3, vector5);
		Gizmos.DrawLine(vector5, vector4);
		Gizmos.DrawLine(vector4, vector2);
	}

	// Token: 0x04000566 RID: 1382
	public DoorwaySocketType SocketGroup;

	// Token: 0x04000567 RID: 1383
	public List<GameObject> DoorPrefabs = new List<GameObject>();

	// Token: 0x04000568 RID: 1384
	public List<GameObject> AddWhenInUse = new List<GameObject>();

	// Token: 0x04000569 RID: 1385
	public List<GameObject> AddWhenNotInUse = new List<GameObject>();

	// Token: 0x0400056A RID: 1386
	public Vector2 Size = new Vector2(1f, 2f);

	// Token: 0x0400056C RID: 1388
	public int? LockID;

	// Token: 0x0400056D RID: 1389
	[SerializeField]
	[HideInInspector]
	private GameObject doorPrefab;

	// Token: 0x0400056E RID: 1390
	internal bool placedByGenerator;
}
