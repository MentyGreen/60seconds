using System;
using UnityEngine;

// Token: 0x0200000A RID: 10
[ExecuteInEditMode]
[AddComponentMenu("Daikon Forge/User Interface/Drag Handle")]
[Serializable]
public class dfDragHandle : dfControl
{
	// Token: 0x060001B5 RID: 437 RVA: 0x00008848 File Offset: 0x00006A48
	public override void Start()
	{
		base.Start();
		if (base.Size.magnitude <= 1E-45f)
		{
			if (base.Parent != null)
			{
				base.Size = new Vector2(base.Parent.Width, 30f);
				base.Anchor = (dfAnchorStyle.Top | dfAnchorStyle.Left | dfAnchorStyle.Right);
				base.RelativePosition = Vector2.zero;
				return;
			}
			base.Size = new Vector2(200f, 25f);
		}
	}

	// Token: 0x060001B6 RID: 438 RVA: 0x000088C8 File Offset: 0x00006AC8
	protected internal override void OnMouseDown(dfMouseEventArgs args)
	{
		base.GetRootContainer().BringToFront();
		base.Parent.BringToFront();
		args.Use();
		Plane plane = new Plane(this.parent.transform.TransformDirection(Vector3.back), this.parent.transform.position);
		Ray ray = args.Ray;
		float d = 0f;
		plane.Raycast(args.Ray, out d);
		this.lastPosition = ray.origin + ray.direction * d;
		base.OnMouseDown(args);
	}

	// Token: 0x060001B7 RID: 439 RVA: 0x00008960 File Offset: 0x00006B60
	protected internal override void OnMouseMove(dfMouseEventArgs args)
	{
		args.Use();
		if (args.Buttons.IsSet(dfMouseButtons.Left))
		{
			Ray ray = args.Ray;
			float d = 0f;
			Vector3 inNormal = base.GetCamera().transform.TransformDirection(Vector3.back);
			Plane plane = new Plane(inNormal, this.lastPosition);
			plane.Raycast(ray, out d);
			Vector3 a = (ray.origin + ray.direction * d).Quantize(this.parent.PixelsToUnits());
			Vector3 b = a - this.lastPosition;
			Vector3 position = (this.parent.transform.position + b).Quantize(this.parent.PixelsToUnits());
			this.parent.transform.position = position;
			this.lastPosition = a;
		}
		base.OnMouseMove(args);
	}

	// Token: 0x060001B8 RID: 440 RVA: 0x00008A44 File Offset: 0x00006C44
	protected internal override void OnMouseUp(dfMouseEventArgs args)
	{
		base.OnMouseUp(args);
		base.Parent.MakePixelPerfect();
	}

	// Token: 0x0400009A RID: 154
	private Vector3 lastPosition;
}
