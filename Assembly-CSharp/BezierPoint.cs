using System;
using UnityEngine;

// Token: 0x02000005 RID: 5
[Serializable]
public class BezierPoint : MonoBehaviour
{
	// Token: 0x17000006 RID: 6
	// (get) Token: 0x06000024 RID: 36 RVA: 0x00002901 File Offset: 0x00000B01
	// (set) Token: 0x06000025 RID: 37 RVA: 0x00002909 File Offset: 0x00000B09
	public BezierCurve curve
	{
		get
		{
			return this._curve;
		}
		set
		{
			if (this._curve)
			{
				this._curve.RemovePoint(this);
			}
			this._curve = value;
			this._curve.AddPoint(this);
		}
	}

	// Token: 0x17000007 RID: 7
	// (get) Token: 0x06000026 RID: 38 RVA: 0x00002937 File Offset: 0x00000B37
	// (set) Token: 0x06000027 RID: 39 RVA: 0x00002944 File Offset: 0x00000B44
	public Vector3 position
	{
		get
		{
			return base.transform.position;
		}
		set
		{
			base.transform.position = value;
		}
	}

	// Token: 0x17000008 RID: 8
	// (get) Token: 0x06000028 RID: 40 RVA: 0x00002952 File Offset: 0x00000B52
	// (set) Token: 0x06000029 RID: 41 RVA: 0x0000295F File Offset: 0x00000B5F
	public Vector3 localPosition
	{
		get
		{
			return base.transform.localPosition;
		}
		set
		{
			base.transform.localPosition = value;
		}
	}

	// Token: 0x17000009 RID: 9
	// (get) Token: 0x0600002A RID: 42 RVA: 0x0000296D File Offset: 0x00000B6D
	// (set) Token: 0x0600002B RID: 43 RVA: 0x00002978 File Offset: 0x00000B78
	public Vector3 handle1
	{
		get
		{
			return this._handle1;
		}
		set
		{
			if (this._handle1 == value)
			{
				return;
			}
			this._handle1 = value;
			if (this.handleStyle == BezierPoint.HandleStyle.None)
			{
				this.handleStyle = BezierPoint.HandleStyle.Broken;
			}
			else if (this.handleStyle == BezierPoint.HandleStyle.Connected)
			{
				this._handle2 = -value;
			}
			this._curve.SetDirty();
		}
	}

	// Token: 0x1700000A RID: 10
	// (get) Token: 0x0600002C RID: 44 RVA: 0x000029CC File Offset: 0x00000BCC
	// (set) Token: 0x0600002D RID: 45 RVA: 0x000029DF File Offset: 0x00000BDF
	public Vector3 globalHandle1
	{
		get
		{
			return base.transform.TransformPoint(this.handle1);
		}
		set
		{
			this.handle1 = base.transform.InverseTransformPoint(value);
		}
	}

	// Token: 0x1700000B RID: 11
	// (get) Token: 0x0600002E RID: 46 RVA: 0x000029F3 File Offset: 0x00000BF3
	// (set) Token: 0x0600002F RID: 47 RVA: 0x000029FC File Offset: 0x00000BFC
	public Vector3 handle2
	{
		get
		{
			return this._handle2;
		}
		set
		{
			if (this._handle2 == value)
			{
				return;
			}
			this._handle2 = value;
			if (this.handleStyle == BezierPoint.HandleStyle.None)
			{
				this.handleStyle = BezierPoint.HandleStyle.Broken;
			}
			else if (this.handleStyle == BezierPoint.HandleStyle.Connected)
			{
				this._handle1 = -value;
			}
			this._curve.SetDirty();
		}
	}

	// Token: 0x1700000C RID: 12
	// (get) Token: 0x06000030 RID: 48 RVA: 0x00002A50 File Offset: 0x00000C50
	// (set) Token: 0x06000031 RID: 49 RVA: 0x00002A63 File Offset: 0x00000C63
	public Vector3 globalHandle2
	{
		get
		{
			return base.transform.TransformPoint(this.handle2);
		}
		set
		{
			this.handle2 = base.transform.InverseTransformPoint(value);
		}
	}

	// Token: 0x06000032 RID: 50 RVA: 0x00002A78 File Offset: 0x00000C78
	private void Update()
	{
		if (!this._curve.dirty && base.transform.position != this.lastPosition)
		{
			this._curve.SetDirty();
			this.lastPosition = base.transform.position;
		}
	}

	// Token: 0x04000012 RID: 18
	[SerializeField]
	private BezierCurve _curve;

	// Token: 0x04000013 RID: 19
	public BezierPoint.HandleStyle handleStyle;

	// Token: 0x04000014 RID: 20
	[SerializeField]
	private Vector3 _handle1;

	// Token: 0x04000015 RID: 21
	[SerializeField]
	private Vector3 _handle2;

	// Token: 0x04000016 RID: 22
	private Vector3 lastPosition;

	// Token: 0x0200034F RID: 847
	public enum HandleStyle
	{
		// Token: 0x040015CF RID: 5583
		Connected,
		// Token: 0x040015D0 RID: 5584
		Broken,
		// Token: 0x040015D1 RID: 5585
		None
	}
}
