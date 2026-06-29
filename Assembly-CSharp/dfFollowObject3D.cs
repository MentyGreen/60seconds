using System;
using UnityEngine;

// Token: 0x020000B5 RID: 181
[ExecuteInEditMode]
[AddComponentMenu("Daikon Forge/Examples/3D/Follow Object (3D)")]
public class dfFollowObject3D : MonoBehaviour
{
	// Token: 0x06000A49 RID: 2633 RVA: 0x0002D0BF File Offset: 0x0002B2BF
	public void OnEnable()
	{
		this.control = base.GetComponent<dfControl>();
		this.Update();
	}

	// Token: 0x06000A4A RID: 2634 RVA: 0x0002D0D4 File Offset: 0x0002B2D4
	public void Update()
	{
		if (this.lastLiveUpdateValue != this.liveUpdate)
		{
			this.lastLiveUpdateValue = this.liveUpdate;
			if (!this.liveUpdate)
			{
				this.control.RelativePosition = this.designTimePosition;
				this.control.transform.localScale = Vector3.one;
				this.control.transform.localRotation = Quaternion.identity;
			}
			else
			{
				this.designTimePosition = this.control.RelativePosition;
			}
			this.control.Invalidate();
		}
		if (this.liveUpdate || Application.isPlaying)
		{
			this.updatePosition3D();
		}
	}

	// Token: 0x06000A4B RID: 2635 RVA: 0x0002D174 File Offset: 0x0002B374
	public void OnDrawGizmos()
	{
		if (this.control == null)
		{
			this.control = base.GetComponent<dfControl>();
		}
		Vector3 size = this.control.Size * this.control.PixelsToUnits();
		Gizmos.matrix = Matrix4x4.TRS(this.attachedTo.position, this.attachedTo.rotation, this.attachedTo.localScale);
		Gizmos.color = Color.clear;
		Gizmos.DrawCube(Vector3.zero, size);
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireCube(Vector3.zero, size);
	}

	// Token: 0x06000A4C RID: 2636 RVA: 0x0002D211 File Offset: 0x0002B411
	public void OnDrawGizmosSelected()
	{
		this.OnDrawGizmos();
	}

	// Token: 0x06000A4D RID: 2637 RVA: 0x0002D21C File Offset: 0x0002B41C
	private void updatePosition3D()
	{
		if (this.attachedTo == null)
		{
			return;
		}
		this.control.transform.position = this.attachedTo.position;
		this.control.transform.rotation = this.attachedTo.rotation;
		this.control.transform.localScale = this.attachedTo.localScale;
	}

	// Token: 0x040004F5 RID: 1269
	public Transform attachedTo;

	// Token: 0x040004F6 RID: 1270
	public bool liveUpdate;

	// Token: 0x040004F7 RID: 1271
	[HideInInspector]
	[SerializeField]
	protected Vector3 designTimePosition;

	// Token: 0x040004F8 RID: 1272
	private dfControl control;

	// Token: 0x040004F9 RID: 1273
	private bool lastLiveUpdateValue;
}
