using System;
using UnityEngine;

// Token: 0x020000B4 RID: 180
[AddComponentMenu("Daikon Forge/Examples/General/Follow Object")]
public class dfFollowObject : MonoBehaviour
{
	// Token: 0x06000A44 RID: 2628 RVA: 0x0002CBD4 File Offset: 0x0002ADD4
	private void OnEnable()
	{
		if (this.mainCamera == null)
		{
			this.mainCamera = Camera.main;
			if (this.mainCamera == null)
			{
				Debug.Log("dfFollowObject component is unable to determine which camera is the MainCamera", base.gameObject);
				base.enabled = false;
				return;
			}
		}
		this.myControl = base.GetComponent<dfControl>();
		if (this.myControl == null)
		{
			Debug.LogError("No dfControl component on this GameObject: " + base.gameObject.name, base.gameObject);
			base.enabled = false;
		}
		if (this.myControl == null || this.attach == null)
		{
			Debug.LogWarning("Configuration incomplete: " + base.name);
			base.enabled = false;
			return;
		}
		this.controlTransform = this.myControl.transform;
		this.followTransform = this.attach.transform;
		this.manager = this.myControl.GetManager();
		dfFollowObjectSorter.Register(this);
	}

	// Token: 0x06000A45 RID: 2629 RVA: 0x0002CCD3 File Offset: 0x0002AED3
	private void OnDisable()
	{
		dfFollowObjectSorter.Unregister(this);
	}

	// Token: 0x06000A46 RID: 2630 RVA: 0x0002CCDC File Offset: 0x0002AEDC
	private void Update()
	{
		Vector3 position = this.followTransform.position;
		float num = Vector3.Distance(this.mainCamera.transform.position, position);
		if (num > this.hideDistance)
		{
			this.myControl.Opacity = 0f;
			return;
		}
		if (num > this.fadeDistance)
		{
			this.myControl.Opacity = 1f - (num - this.fadeDistance) / (this.hideDistance - this.fadeDistance);
		}
		else
		{
			this.myControl.Opacity = 1f;
		}
		Vector3 position2 = this.followTransform.position + this.offset;
		Vector2 vector = this.manager.ScreenToGui(this.mainCamera.WorldToScreenPoint(position2));
		if (!this.manager.PixelPerfectMode)
		{
			if (this.constantScale)
			{
				this.controlTransform.localScale = Vector3.one * (float)(this.manager.FixedHeight / this.mainCamera.pixelHeight);
			}
			else
			{
				this.controlTransform.localScale = Vector3.one;
			}
		}
		Vector2 anchoredControlPosition = this.getAnchoredControlPosition();
		vector.x -= anchoredControlPosition.x * this.controlTransform.localScale.x;
		vector.y -= anchoredControlPosition.y * this.controlTransform.localScale.y;
		if (this.stickToScreenEdge)
		{
			Vector2 screenSize = this.manager.GetScreenSize();
			vector.x = Mathf.Clamp(vector.x, 0f, screenSize.x - this.myControl.Width);
			vector.y = Mathf.Clamp(vector.y, 0f, screenSize.y - this.myControl.Height);
		}
		if (Vector2.Distance(vector, this.lastPosition) <= 0.5f)
		{
			return;
		}
		this.lastPosition = vector;
		Vector3 normalized = (this.attach.transform.position - this.mainCamera.transform.position).normalized;
		if (Vector3.Dot(this.mainCamera.transform.forward, normalized) <= 1E-45f)
		{
			this.myControl.IsVisible = false;
			return;
		}
		this.myControl.IsVisible = true;
		this.myControl.RelativePosition = vector;
	}

	// Token: 0x06000A47 RID: 2631 RVA: 0x0002CF3C File Offset: 0x0002B13C
	private Vector2 getAnchoredControlPosition()
	{
		float height = this.myControl.Height;
		float x = this.myControl.Width / 2f;
		float width = this.myControl.Width;
		float y = this.myControl.Height / 2f;
		Vector2 result = default(Vector2);
		switch (this.anchor)
		{
		case dfPivotPoint.TopLeft:
			result.x = width;
			result.y = height;
			break;
		case dfPivotPoint.TopCenter:
			result.x = x;
			result.y = height;
			break;
		case dfPivotPoint.TopRight:
			result.x = 0f;
			result.y = height;
			break;
		case dfPivotPoint.MiddleLeft:
			result.x = width;
			result.y = y;
			break;
		case dfPivotPoint.MiddleCenter:
			result.x = x;
			result.y = y;
			break;
		case dfPivotPoint.MiddleRight:
			result.x = 0f;
			result.y = y;
			break;
		case dfPivotPoint.BottomLeft:
			result.x = width;
			result.y = 0f;
			break;
		case dfPivotPoint.BottomCenter:
			result.x = x;
			result.y = 0f;
			break;
		case dfPivotPoint.BottomRight:
			result.x = 0f;
			result.y = 0f;
			break;
		}
		return result;
	}

	// Token: 0x040004E8 RID: 1256
	public Camera mainCamera;

	// Token: 0x040004E9 RID: 1257
	public GameObject attach;

	// Token: 0x040004EA RID: 1258
	public dfPivotPoint anchor = dfPivotPoint.MiddleCenter;

	// Token: 0x040004EB RID: 1259
	public Vector3 offset;

	// Token: 0x040004EC RID: 1260
	public float hideDistance = 20f;

	// Token: 0x040004ED RID: 1261
	public float fadeDistance = 15f;

	// Token: 0x040004EE RID: 1262
	public bool constantScale;

	// Token: 0x040004EF RID: 1263
	public bool stickToScreenEdge;

	// Token: 0x040004F0 RID: 1264
	private Transform controlTransform;

	// Token: 0x040004F1 RID: 1265
	private Transform followTransform;

	// Token: 0x040004F2 RID: 1266
	private dfControl myControl;

	// Token: 0x040004F3 RID: 1267
	private dfGUIManager manager;

	// Token: 0x040004F4 RID: 1268
	private Vector2 lastPosition = Vector2.one * float.MinValue;
}
