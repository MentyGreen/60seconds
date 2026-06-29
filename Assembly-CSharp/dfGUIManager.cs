using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000066 RID: 102
[ExecuteInEditMode]
[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(dfInputManager))]
[AddComponentMenu("Daikon Forge/User Interface/GUI Manager")]
[Serializable]
public class dfGUIManager : MonoBehaviour, IDFControlHost, IComparable<dfGUIManager>
{
	// Token: 0x1400004B RID: 75
	// (add) Token: 0x060006F0 RID: 1776 RVA: 0x0001DBD4 File Offset: 0x0001BDD4
	// (remove) Token: 0x060006F1 RID: 1777 RVA: 0x0001DC08 File Offset: 0x0001BE08
	public static event dfGUIManager.RenderCallback BeforeRender;

	// Token: 0x1400004C RID: 76
	// (add) Token: 0x060006F2 RID: 1778 RVA: 0x0001DC3C File Offset: 0x0001BE3C
	// (remove) Token: 0x060006F3 RID: 1779 RVA: 0x0001DC70 File Offset: 0x0001BE70
	public static event dfGUIManager.RenderCallback AfterRender;

	// Token: 0x170001B7 RID: 439
	// (get) Token: 0x060006F4 RID: 1780 RVA: 0x0001DCA3 File Offset: 0x0001BEA3
	public static IEnumerable<dfGUIManager> ActiveManagers
	{
		get
		{
			return dfGUIManager.activeInstances;
		}
	}

	// Token: 0x170001B8 RID: 440
	// (get) Token: 0x060006F5 RID: 1781 RVA: 0x0001DCAA File Offset: 0x0001BEAA
	// (set) Token: 0x060006F6 RID: 1782 RVA: 0x0001DCB2 File Offset: 0x0001BEB2
	public int TotalDrawCalls { get; private set; }

	// Token: 0x170001B9 RID: 441
	// (get) Token: 0x060006F7 RID: 1783 RVA: 0x0001DCBB File Offset: 0x0001BEBB
	// (set) Token: 0x060006F8 RID: 1784 RVA: 0x0001DCC3 File Offset: 0x0001BEC3
	public int TotalTriangles { get; private set; }

	// Token: 0x170001BA RID: 442
	// (get) Token: 0x060006F9 RID: 1785 RVA: 0x0001DCCC File Offset: 0x0001BECC
	// (set) Token: 0x060006FA RID: 1786 RVA: 0x0001DCD4 File Offset: 0x0001BED4
	public int NumControlsRendered { get; private set; }

	// Token: 0x170001BB RID: 443
	// (get) Token: 0x060006FB RID: 1787 RVA: 0x0001DCDD File Offset: 0x0001BEDD
	// (set) Token: 0x060006FC RID: 1788 RVA: 0x0001DCE5 File Offset: 0x0001BEE5
	public int FramesRendered { get; private set; }

	// Token: 0x170001BC RID: 444
	// (get) Token: 0x060006FD RID: 1789 RVA: 0x0001DCEE File Offset: 0x0001BEEE
	public IList<dfControl> ControlsRendered
	{
		get
		{
			return this.controlsRendered;
		}
	}

	// Token: 0x170001BD RID: 445
	// (get) Token: 0x060006FE RID: 1790 RVA: 0x0001DCF6 File Offset: 0x0001BEF6
	public IList<int> DrawCallStartIndices
	{
		get
		{
			return this.drawCallIndices;
		}
	}

	// Token: 0x170001BE RID: 446
	// (get) Token: 0x060006FF RID: 1791 RVA: 0x0001DCFE File Offset: 0x0001BEFE
	// (set) Token: 0x06000700 RID: 1792 RVA: 0x0001DD06 File Offset: 0x0001BF06
	public int RenderQueueBase
	{
		get
		{
			return this.renderQueueBase;
		}
		set
		{
			if (value != this.renderQueueBase)
			{
				this.renderQueueBase = value;
				dfGUIManager.RefreshAll();
			}
		}
	}

	// Token: 0x170001BF RID: 447
	// (get) Token: 0x06000701 RID: 1793 RVA: 0x0001DD1D File Offset: 0x0001BF1D
	public static dfControl ActiveControl
	{
		get
		{
			return dfGUIManager.activeControl;
		}
	}

	// Token: 0x170001C0 RID: 448
	// (get) Token: 0x06000702 RID: 1794 RVA: 0x0001DD24 File Offset: 0x0001BF24
	// (set) Token: 0x06000703 RID: 1795 RVA: 0x0001DD2C File Offset: 0x0001BF2C
	public float UIScale
	{
		get
		{
			return this.uiScale;
		}
		set
		{
			if (!Mathf.Approximately(value, this.uiScale))
			{
				this.uiScale = value;
				this.onResolutionChanged();
			}
		}
	}

	// Token: 0x170001C1 RID: 449
	// (get) Token: 0x06000704 RID: 1796 RVA: 0x0001DD49 File Offset: 0x0001BF49
	// (set) Token: 0x06000705 RID: 1797 RVA: 0x0001DD51 File Offset: 0x0001BF51
	public bool UIScaleLegacyMode
	{
		get
		{
			return this.uiScaleLegacy;
		}
		set
		{
			if (value != this.uiScaleLegacy)
			{
				this.uiScaleLegacy = value;
				this.onResolutionChanged();
			}
		}
	}

	// Token: 0x170001C2 RID: 450
	// (get) Token: 0x06000706 RID: 1798 RVA: 0x0001DD69 File Offset: 0x0001BF69
	// (set) Token: 0x06000707 RID: 1799 RVA: 0x0001DD71 File Offset: 0x0001BF71
	public Vector2 UIOffset
	{
		get
		{
			return this.uiOffset;
		}
		set
		{
			if (!object.Equals(this.uiOffset, value))
			{
				this.uiOffset = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x170001C3 RID: 451
	// (get) Token: 0x06000708 RID: 1800 RVA: 0x0001DD98 File Offset: 0x0001BF98
	// (set) Token: 0x06000709 RID: 1801 RVA: 0x0001DDA0 File Offset: 0x0001BFA0
	public Camera RenderCamera
	{
		get
		{
			return this.renderCamera;
		}
		set
		{
			if (this.renderCamera != value)
			{
				this.renderCamera = value;
				this.Invalidate();
				if (value != null && value.gameObject.GetComponent<dfGUICamera>() == null)
				{
					value.gameObject.AddComponent<dfGUICamera>();
				}
				if (this.inputManager != null)
				{
					this.inputManager.RenderCamera = value;
				}
			}
		}
	}

	// Token: 0x170001C4 RID: 452
	// (get) Token: 0x0600070A RID: 1802 RVA: 0x0001DE05 File Offset: 0x0001C005
	// (set) Token: 0x0600070B RID: 1803 RVA: 0x0001DE0D File Offset: 0x0001C00D
	public bool MergeMaterials
	{
		get
		{
			return this.mergeMaterials;
		}
		set
		{
			if (value != this.mergeMaterials)
			{
				this.mergeMaterials = value;
				this.invalidateAllControls();
			}
		}
	}

	// Token: 0x170001C5 RID: 453
	// (get) Token: 0x0600070C RID: 1804 RVA: 0x0001DE25 File Offset: 0x0001C025
	// (set) Token: 0x0600070D RID: 1805 RVA: 0x0001DE2D File Offset: 0x0001C02D
	public bool GenerateNormals
	{
		get
		{
			return this.generateNormals;
		}
		set
		{
			if (value != this.generateNormals)
			{
				this.generateNormals = value;
				if (this.renderMesh != null)
				{
					this.renderMesh[0].Clear();
					this.renderMesh[1].Clear();
				}
				dfRenderData.FlushObjectPool();
				this.invalidateAllControls();
			}
		}
	}

	// Token: 0x170001C6 RID: 454
	// (get) Token: 0x0600070E RID: 1806 RVA: 0x0001DE6C File Offset: 0x0001C06C
	// (set) Token: 0x0600070F RID: 1807 RVA: 0x0001DE74 File Offset: 0x0001C074
	public bool PixelPerfectMode
	{
		get
		{
			return this.pixelPerfectMode;
		}
		set
		{
			if (value != this.pixelPerfectMode)
			{
				this.pixelPerfectMode = value;
				this.onResolutionChanged();
				this.Invalidate();
			}
		}
	}

	// Token: 0x170001C7 RID: 455
	// (get) Token: 0x06000710 RID: 1808 RVA: 0x0001DE92 File Offset: 0x0001C092
	// (set) Token: 0x06000711 RID: 1809 RVA: 0x0001DE9A File Offset: 0x0001C09A
	public dfAtlas DefaultAtlas
	{
		get
		{
			return this.atlas;
		}
		set
		{
			if (!dfAtlas.Equals(value, this.atlas))
			{
				this.atlas = value;
				this.invalidateAllControls();
			}
		}
	}

	// Token: 0x170001C8 RID: 456
	// (get) Token: 0x06000712 RID: 1810 RVA: 0x0001DEB7 File Offset: 0x0001C0B7
	// (set) Token: 0x06000713 RID: 1811 RVA: 0x0001DEBF File Offset: 0x0001C0BF
	public dfFontBase DefaultFont
	{
		get
		{
			return this.defaultFont;
		}
		set
		{
			if (value != this.defaultFont)
			{
				this.defaultFont = value;
				this.invalidateAllControls();
			}
		}
	}

	// Token: 0x170001C9 RID: 457
	// (get) Token: 0x06000714 RID: 1812 RVA: 0x0001DEDC File Offset: 0x0001C0DC
	// (set) Token: 0x06000715 RID: 1813 RVA: 0x0001DEE4 File Offset: 0x0001C0E4
	public int FixedWidth
	{
		get
		{
			return this.fixedWidth;
		}
		set
		{
			if (value != this.fixedWidth)
			{
				this.fixedWidth = value;
				this.onResolutionChanged();
			}
		}
	}

	// Token: 0x170001CA RID: 458
	// (get) Token: 0x06000716 RID: 1814 RVA: 0x0001DEFC File Offset: 0x0001C0FC
	// (set) Token: 0x06000717 RID: 1815 RVA: 0x0001DF04 File Offset: 0x0001C104
	public int FixedHeight
	{
		get
		{
			return this.fixedHeight;
		}
		set
		{
			if (value != this.fixedHeight)
			{
				int oldSize = this.fixedHeight;
				this.fixedHeight = value;
				this.onResolutionChanged(oldSize, value);
			}
		}
	}

	// Token: 0x170001CB RID: 459
	// (get) Token: 0x06000718 RID: 1816 RVA: 0x0001DF30 File Offset: 0x0001C130
	// (set) Token: 0x06000719 RID: 1817 RVA: 0x0001DF38 File Offset: 0x0001C138
	public bool ConsumeMouseEvents
	{
		get
		{
			return this.consumeMouseEvents;
		}
		set
		{
			this.consumeMouseEvents = value;
		}
	}

	// Token: 0x170001CC RID: 460
	// (get) Token: 0x0600071A RID: 1818 RVA: 0x0001DF41 File Offset: 0x0001C141
	// (set) Token: 0x0600071B RID: 1819 RVA: 0x0001DF49 File Offset: 0x0001C149
	public bool OverrideCamera
	{
		get
		{
			return this.overrideCamera;
		}
		set
		{
			this.overrideCamera = value;
		}
	}

	// Token: 0x0600071C RID: 1820 RVA: 0x0001DF52 File Offset: 0x0001C152
	public void OnApplicationQuit()
	{
		this.shutdownInProcess = true;
	}

	// Token: 0x0600071D RID: 1821 RVA: 0x0001DF5C File Offset: 0x0001C15C
	public void OnGUI()
	{
		if (this.overrideCamera || !this.consumeMouseEvents || !Application.isPlaying || this.occluders == null)
		{
			return;
		}
		Vector3 mousePosition = Input.mousePosition;
		mousePosition.y = (float)Screen.height - mousePosition.y;
		if (dfGUIManager.modalControlStack.Count > 0)
		{
			GUI.Box(new Rect(0f, 0f, (float)Screen.width, (float)Screen.height), GUIContent.none, GUIStyle.none);
		}
		else
		{
			Rect rect = default(Rect);
			for (int i = 0; i < this.occluders.Count; i++)
			{
				Rect rect2 = this.occluders[i];
				if (Event.current.isMouse && rect2.Contains(mousePosition))
				{
					Event.current.Use();
				}
				else if (!rect.Contains(rect2))
				{
					GUI.Box(rect2, GUIContent.none, GUIStyle.none);
				}
				rect = rect2;
			}
		}
		if (Event.current.isMouse && Input.touchCount > 0)
		{
			int touchCount = Input.touchCount;
			for (int j = 0; j < touchCount; j++)
			{
				Touch touch = Input.GetTouch(j);
				if (touch.phase == TouchPhase.Began)
				{
					Vector2 touchPosition = touch.position;
					touchPosition.y = (float)Screen.height - touchPosition.y;
					if (this.occluders.Any((Rect x) => x.Contains(touchPosition)))
					{
						Event.current.Use();
						return;
					}
				}
			}
		}
	}

	// Token: 0x0600071E RID: 1822 RVA: 0x0001E0E1 File Offset: 0x0001C2E1
	public void Awake()
	{
		dfRenderData.FlushObjectPool();
	}

	// Token: 0x0600071F RID: 1823 RVA: 0x0001E0E8 File Offset: 0x0001C2E8
	public void OnEnable()
	{
		Camera[] allCameras = Camera.allCameras;
		for (int i = 0; i < allCameras.Length; i++)
		{
			allCameras[i].eventMask &= ~(1 << base.gameObject.layer);
		}
		if (this.meshRenderer == null)
		{
			this.initialize();
		}
		base.useGUILayout = !this.ConsumeMouseEvents;
		dfGUIManager.activeInstances.Add(this);
		this.FramesRendered = 0;
		this.TotalDrawCalls = 0;
		this.TotalTriangles = 0;
		if (this.meshRenderer != null)
		{
			this.meshRenderer.enabled = true;
		}
		this.inputManager = (base.GetComponent<dfInputManager>() ?? base.gameObject.AddComponent<dfInputManager>());
		this.inputManager.RenderCamera = this.RenderCamera;
		this.FramesRendered = 0;
		if (this.meshRenderer != null)
		{
			this.meshRenderer.enabled = true;
		}
		if (Application.isPlaying)
		{
			this.onResolutionChanged();
		}
		this.Invalidate();
	}

	// Token: 0x06000720 RID: 1824 RVA: 0x0001E1E6 File Offset: 0x0001C3E6
	public void OnDisable()
	{
		dfGUIManager.activeInstances.Remove(this);
		if (this.meshRenderer != null)
		{
			this.meshRenderer.enabled = false;
		}
		this.resetDrawCalls();
	}

	// Token: 0x06000721 RID: 1825 RVA: 0x0001E214 File Offset: 0x0001C414
	public void OnDestroy()
	{
		if (dfGUIManager.activeInstances.Count == 0)
		{
			dfMaterialCache.Clear();
		}
		if (this.renderMesh == null || this.renderFilter == null)
		{
			return;
		}
		this.renderFilter.sharedMesh = null;
		Object.DestroyImmediate(this.renderMesh[0]);
		Object.DestroyImmediate(this.renderMesh[1]);
		this.renderMesh = null;
	}

	// Token: 0x06000722 RID: 1826 RVA: 0x0001E278 File Offset: 0x0001C478
	public void Start()
	{
		Camera[] allCameras = Camera.allCameras;
		for (int i = 0; i < allCameras.Length; i++)
		{
			allCameras[i].eventMask &= ~(1 << base.gameObject.layer);
		}
	}

	// Token: 0x06000723 RID: 1827 RVA: 0x0001E2BC File Offset: 0x0001C4BC
	public void Update()
	{
		dfGUIManager.activeInstances.Sort();
		if (this.renderCamera == null || !base.enabled)
		{
			if (this.meshRenderer != null)
			{
				this.meshRenderer.enabled = false;
			}
			return;
		}
		if (this.renderMesh == null || this.renderMesh.Length == 0)
		{
			this.initialize();
			if (Application.isEditor && !Application.isPlaying)
			{
				this.Render();
			}
		}
		if (this.cachedChildCount != base.transform.childCount)
		{
			this.cachedChildCount = base.transform.childCount;
			this.Invalidate();
		}
		Vector2 screenSize = this.GetScreenSize();
		if ((screenSize - this.cachedScreenSize).sqrMagnitude > 1E-45f)
		{
			this.onResolutionChanged(this.cachedScreenSize, screenSize);
			this.cachedScreenSize = screenSize;
		}
	}

	// Token: 0x06000724 RID: 1828 RVA: 0x0001E390 File Offset: 0x0001C590
	public void LateUpdate()
	{
		if (this.renderMesh == null || this.renderMesh.Length == 0)
		{
			this.initialize();
		}
		if (!Application.isPlaying)
		{
			BoxCollider boxCollider = base.GetComponent<Collider>() as BoxCollider;
			if (boxCollider != null)
			{
				Vector2 v = this.GetScreenSize() * this.PixelsToUnits();
				boxCollider.center = Vector3.zero;
				boxCollider.size = v;
			}
		}
		if (dfGUIManager.activeInstances[0] == this)
		{
			dfFontManager.RebuildDynamicFonts();
			bool flag = false;
			for (int i = 0; i < dfGUIManager.activeInstances.Count; i++)
			{
				dfGUIManager dfGUIManager = dfGUIManager.activeInstances[i];
				if (dfGUIManager.isDirty && dfGUIManager.suspendCount <= 0)
				{
					flag = true;
					dfGUIManager.abortRendering = false;
					dfGUIManager.isDirty = false;
					dfGUIManager.Render();
				}
			}
			if (flag)
			{
				dfMaterialCache.Reset();
				this.updateDrawCalls();
				for (int j = 0; j < dfGUIManager.activeInstances.Count; j++)
				{
					dfGUIManager.activeInstances[j].updateDrawCalls();
				}
			}
		}
	}

	// Token: 0x06000725 RID: 1829 RVA: 0x0001E49D File Offset: 0x0001C69D
	public void SuspendRendering()
	{
		this.suspendCount++;
	}

	// Token: 0x06000726 RID: 1830 RVA: 0x0001E4B0 File Offset: 0x0001C6B0
	public void ResumeRendering()
	{
		if (this.suspendCount == 0)
		{
			return;
		}
		int num = this.suspendCount - 1;
		this.suspendCount = num;
		if (num == 0)
		{
			this.Invalidate();
		}
	}

	// Token: 0x06000727 RID: 1831 RVA: 0x0001E4E0 File Offset: 0x0001C6E0
	public static dfControl HitTestAll(Vector2 screenPosition)
	{
		dfControl result = null;
		float num = float.MinValue;
		for (int i = 0; i < dfGUIManager.activeInstances.Count; i++)
		{
			dfGUIManager dfGUIManager = dfGUIManager.activeInstances[i];
			Camera camera = dfGUIManager.RenderCamera;
			if (camera.depth >= num)
			{
				dfControl dfControl = dfGUIManager.HitTest(screenPosition);
				if (dfControl != null)
				{
					result = dfControl;
					num = camera.depth;
				}
			}
		}
		return result;
	}

	// Token: 0x06000728 RID: 1832 RVA: 0x0001E548 File Offset: 0x0001C748
	public dfControl HitTest(Vector2 screenPosition)
	{
		Ray ray = this.renderCamera.ScreenPointToRay(screenPosition);
		float maxDistance = this.renderCamera.farClipPlane - this.renderCamera.nearClipPlane;
		dfControl modalControl = dfGUIManager.GetModalControl();
		dfList<dfControl> dfList = this.controlsRendered;
		int count = dfList.Count;
		dfControl[] items = dfList.Items;
		if (this.occluders.Count != count)
		{
			Debug.LogWarning("Occluder count does not match control count");
			return null;
		}
		Vector2 point = screenPosition;
		point.y = (float)this.RenderCamera.pixelHeight - screenPosition.y;
		for (int i = count - 1; i >= 0; i--)
		{
			if (this.occluders[i].Contains(point))
			{
				dfControl dfControl = items[i];
				RaycastHit raycastHit;
				if (!(dfControl == null) && !(dfControl.GetComponent<Collider>() == null) && dfControl.GetComponent<Collider>().Raycast(ray, out raycastHit, maxDistance) && (!(modalControl != null) || dfControl.transform.IsChildOf(modalControl.transform)) && dfControl.IsInteractive && dfControl.IsEnabled && dfGUIManager.isInsideClippingRegion(raycastHit.point, dfControl))
				{
					return dfControl;
				}
			}
		}
		return null;
	}

	// Token: 0x06000729 RID: 1833 RVA: 0x0001E680 File Offset: 0x0001C880
	public Vector2 WorldPointToGUI(Vector3 worldPoint)
	{
		Camera camera = Camera.main ?? this.renderCamera;
		return this.ScreenToGui(camera.WorldToScreenPoint(worldPoint));
	}

	// Token: 0x0600072A RID: 1834 RVA: 0x0001E6AF File Offset: 0x0001C8AF
	public float PixelsToUnits()
	{
		return 2f / (float)this.FixedHeight * this.UIScale;
	}

	// Token: 0x0600072B RID: 1835 RVA: 0x0001E6C8 File Offset: 0x0001C8C8
	public Plane[] GetClippingPlanes()
	{
		Vector3[] array = this.GetCorners();
		Vector3 inNormal = base.transform.TransformDirection(Vector3.right);
		Vector3 inNormal2 = base.transform.TransformDirection(Vector3.left);
		Vector3 inNormal3 = base.transform.TransformDirection(Vector3.up);
		Vector3 inNormal4 = base.transform.TransformDirection(Vector3.down);
		if (dfGUIManager.clippingPlanes == null)
		{
			dfGUIManager.clippingPlanes = new Plane[4];
		}
		dfGUIManager.clippingPlanes[0] = new Plane(inNormal, array[0]);
		dfGUIManager.clippingPlanes[1] = new Plane(inNormal2, array[1]);
		dfGUIManager.clippingPlanes[2] = new Plane(inNormal3, array[2]);
		dfGUIManager.clippingPlanes[3] = new Plane(inNormal4, array[0]);
		return dfGUIManager.clippingPlanes;
	}

	// Token: 0x0600072C RID: 1836 RVA: 0x0001E79C File Offset: 0x0001C99C
	public Vector3[] GetCorners()
	{
		float d = this.PixelsToUnits();
		Vector2 vector = this.GetScreenSize() * d;
		float x = vector.x;
		float y = vector.y;
		Vector3 vector2 = new Vector3(-x * 0.5f, y * 0.5f);
		Vector3 vector3 = vector2 + new Vector3(x, 0f);
		Vector3 point = vector2 + new Vector3(0f, -y);
		Vector3 point2 = vector3 + new Vector3(0f, -y);
		Matrix4x4 localToWorldMatrix = base.transform.localToWorldMatrix;
		this.corners[0] = localToWorldMatrix.MultiplyPoint(vector2);
		this.corners[1] = localToWorldMatrix.MultiplyPoint(vector3);
		this.corners[2] = localToWorldMatrix.MultiplyPoint(point2);
		this.corners[3] = localToWorldMatrix.MultiplyPoint(point);
		return this.corners;
	}

	// Token: 0x0600072D RID: 1837 RVA: 0x0001E884 File Offset: 0x0001CA84
	public Vector2 GetScreenSize()
	{
		Camera camera = this.RenderCamera;
		bool flag = Application.isPlaying && camera != null;
		Vector2 vector = Vector2.zero;
		if (flag)
		{
			float d = this.PixelPerfectMode ? 1f : ((float)camera.pixelHeight / (float)this.fixedHeight);
			vector = (new Vector2((float)camera.pixelWidth, (float)camera.pixelHeight) / d).CeilToInt();
			if (this.uiScaleLegacy)
			{
				vector *= this.uiScale;
			}
			else
			{
				vector /= this.uiScale;
			}
		}
		else
		{
			vector = new Vector2((float)this.FixedWidth, (float)this.FixedHeight);
			if (!this.uiScaleLegacy)
			{
				vector /= this.uiScale;
			}
		}
		return vector;
	}

	// Token: 0x0600072E RID: 1838 RVA: 0x0001E940 File Offset: 0x0001CB40
	public T AddControl<T>() where T : dfControl
	{
		return (T)((object)this.AddControl(typeof(T)));
	}

	// Token: 0x0600072F RID: 1839 RVA: 0x0001E958 File Offset: 0x0001CB58
	public dfControl AddControl(Type controlType)
	{
		if (!typeof(dfControl).IsAssignableFrom(controlType))
		{
			throw new InvalidCastException();
		}
		dfControl dfControl = new GameObject(controlType.Name)
		{
			transform = 
			{
				parent = base.transform
			},
			layer = base.gameObject.layer
		}.AddComponent(controlType) as dfControl;
		dfControl.ZOrder = this.getMaxZOrder() + 1;
		return dfControl;
	}

	// Token: 0x06000730 RID: 1840 RVA: 0x0001E9C3 File Offset: 0x0001CBC3
	public void AddControl(dfControl child)
	{
		child.transform.parent = base.transform;
	}

	// Token: 0x06000731 RID: 1841 RVA: 0x0001E9D8 File Offset: 0x0001CBD8
	public dfControl AddPrefab(GameObject prefab)
	{
		if (prefab.GetComponent<dfControl>() == null)
		{
			throw new InvalidCastException();
		}
		GameObject gameObject = Object.Instantiate<GameObject>(prefab);
		gameObject.transform.parent = base.transform;
		gameObject.layer = base.gameObject.layer;
		dfControl component = gameObject.GetComponent<dfControl>();
		component.transform.parent = base.transform;
		component.PerformLayout();
		this.BringToFront(component);
		return component;
	}

	// Token: 0x06000732 RID: 1842 RVA: 0x0001EA46 File Offset: 0x0001CC46
	public dfRenderData GetDrawCallBuffer(int drawCallNumber)
	{
		return this.drawCallBuffers[drawCallNumber];
	}

	// Token: 0x06000733 RID: 1843 RVA: 0x0001EA54 File Offset: 0x0001CC54
	public static dfControl GetModalControl()
	{
		if (dfGUIManager.modalControlStack.Count <= 0)
		{
			return null;
		}
		return dfGUIManager.modalControlStack.Peek().control;
	}

	// Token: 0x06000734 RID: 1844 RVA: 0x0001EA74 File Offset: 0x0001CC74
	public Vector2 ScreenToGui(Vector2 position)
	{
		Vector2 screenSize = this.GetScreenSize();
		Camera camera = Camera.main ?? this.renderCamera;
		position.x = screenSize.x * (position.x / (float)camera.pixelWidth);
		position.y = screenSize.y * (position.y / (float)camera.pixelHeight);
		position.y = screenSize.y - position.y;
		return position;
	}

	// Token: 0x06000735 RID: 1845 RVA: 0x0001EAE5 File Offset: 0x0001CCE5
	public static void PushModal(dfControl control)
	{
		dfGUIManager.PushModal(control, null);
	}

	// Token: 0x06000736 RID: 1846 RVA: 0x0001EAF0 File Offset: 0x0001CCF0
	public static void PushModal(dfControl control, dfGUIManager.ModalPoppedCallback callback)
	{
		if (control == null)
		{
			throw new NullReferenceException("Cannot call PushModal() with a null reference");
		}
		dfGUIManager.modalControlStack.Push(new dfGUIManager.ModalControlReference
		{
			control = control,
			callback = callback
		});
	}

	// Token: 0x06000737 RID: 1847 RVA: 0x0001EB34 File Offset: 0x0001CD34
	public static void PopModal()
	{
		if (dfGUIManager.modalControlStack.Count == 0)
		{
			throw new InvalidOperationException("Modal stack is empty");
		}
		dfGUIManager.ModalControlReference modalControlReference = dfGUIManager.modalControlStack.Pop();
		if (modalControlReference.callback != null)
		{
			modalControlReference.callback(modalControlReference.control);
		}
	}

	// Token: 0x06000738 RID: 1848 RVA: 0x0001EB7C File Offset: 0x0001CD7C
	public static void SetFocus(dfControl control)
	{
		if (dfGUIManager.activeControl == control || (control != null && !control.CanFocus))
		{
			return;
		}
		dfControl dfControl = dfGUIManager.activeControl;
		dfGUIManager.activeControl = control;
		dfFocusEventArgs args = new dfFocusEventArgs(control, dfControl);
		dfList<dfControl> prevFocusChain = dfList<dfControl>.Obtain();
		if (dfControl != null)
		{
			dfControl dfControl2 = dfControl;
			while (dfControl2 != null)
			{
				prevFocusChain.Add(dfControl2);
				dfControl2 = dfControl2.Parent;
			}
		}
		dfList<dfControl> newFocusChain = dfList<dfControl>.Obtain();
		if (control != null)
		{
			dfControl dfControl3 = control;
			while (dfControl3 != null)
			{
				newFocusChain.Add(dfControl3);
				dfControl3 = dfControl3.Parent;
			}
		}
		if (dfControl != null)
		{
			prevFocusChain.ForEach(delegate(dfControl c)
			{
				if (!newFocusChain.Contains(c))
				{
					c.OnLeaveFocus(args);
				}
			});
			dfControl.OnLostFocus(args);
		}
		if (control != null)
		{
			newFocusChain.ForEach(delegate(dfControl c)
			{
				if (!prevFocusChain.Contains(c))
				{
					c.OnEnterFocus(args);
				}
			});
			control.OnGotFocus(args);
		}
		newFocusChain.Release();
		prevFocusChain.Release();
	}

	// Token: 0x06000739 RID: 1849 RVA: 0x0001EC9D File Offset: 0x0001CE9D
	public static bool HasFocus(dfControl control)
	{
		return !(control == null) && dfGUIManager.activeControl == control;
	}

	// Token: 0x0600073A RID: 1850 RVA: 0x0001ECB8 File Offset: 0x0001CEB8
	public static bool ContainsFocus(dfControl control)
	{
		if (dfGUIManager.activeControl == control)
		{
			return true;
		}
		if (dfGUIManager.activeControl == null || control == null)
		{
			return dfGUIManager.activeControl == control;
		}
		return dfGUIManager.activeControl.transform.IsChildOf(control.transform);
	}

	// Token: 0x0600073B RID: 1851 RVA: 0x0001ED08 File Offset: 0x0001CF08
	public void BringToFront(dfControl control)
	{
		if (control.Parent != null)
		{
			control = control.GetRootContainer();
		}
		using (dfList<dfControl> topLevelControls = this.getTopLevelControls())
		{
			int zorder = 0;
			for (int i = 0; i < topLevelControls.Count; i++)
			{
				dfControl dfControl = topLevelControls[i];
				if (dfControl != control)
				{
					dfControl.ZOrder = zorder++;
				}
			}
			control.ZOrder = zorder;
			this.Invalidate();
		}
	}

	// Token: 0x0600073C RID: 1852 RVA: 0x0001ED8C File Offset: 0x0001CF8C
	public void SendToBack(dfControl control)
	{
		if (control.Parent != null)
		{
			control = control.GetRootContainer();
		}
		using (dfList<dfControl> topLevelControls = this.getTopLevelControls())
		{
			int num = 1;
			for (int i = 0; i < topLevelControls.Count; i++)
			{
				dfControl dfControl = topLevelControls[i];
				if (dfControl != control)
				{
					dfControl.ZOrder = num++;
				}
			}
			control.ZOrder = 0;
			this.Invalidate();
		}
	}

	// Token: 0x0600073D RID: 1853 RVA: 0x0001EE10 File Offset: 0x0001D010
	public void Invalidate()
	{
		if (this.isDirty)
		{
			return;
		}
		this.isDirty = true;
		this.updateRenderSettings();
	}

	// Token: 0x0600073E RID: 1854 RVA: 0x0001EE28 File Offset: 0x0001D028
	public static void InvalidateAll()
	{
		for (int i = 0; i < dfGUIManager.activeInstances.Count; i++)
		{
			dfGUIManager.activeInstances[i].Invalidate();
		}
	}

	// Token: 0x0600073F RID: 1855 RVA: 0x0001EE5A File Offset: 0x0001D05A
	public static void RefreshAll()
	{
		dfGUIManager.RefreshAll(false);
	}

	// Token: 0x06000740 RID: 1856 RVA: 0x0001EE64 File Offset: 0x0001D064
	public static void RefreshAll(bool force)
	{
		List<dfGUIManager> list = dfGUIManager.activeInstances;
		for (int i = 0; i < list.Count; i++)
		{
			dfGUIManager dfGUIManager = list[i];
			if (dfGUIManager.renderMesh != null && dfGUIManager.renderMesh.Length != 0)
			{
				dfGUIManager.invalidateAllControls();
				if (force || !Application.isPlaying)
				{
					dfGUIManager.Render();
				}
			}
		}
	}

	// Token: 0x06000741 RID: 1857 RVA: 0x0001EEB7 File Offset: 0x0001D0B7
	internal void AbortRender()
	{
		this.abortRendering = true;
	}

	// Token: 0x06000742 RID: 1858 RVA: 0x0001EEC0 File Offset: 0x0001D0C0
	public void Render()
	{
		if (this.meshRenderer == null)
		{
			return;
		}
		this.meshRenderer.enabled = false;
		this.FramesRendered++;
		if (dfGUIManager.BeforeRender != null)
		{
			dfGUIManager.BeforeRender(this);
		}
		try
		{
			this.occluders.Clear();
			this.occluders.EnsureCapacity(this.NumControlsRendered);
			this.NumControlsRendered = 0;
			this.controlsRendered.Clear();
			this.drawCallIndices.Clear();
			this.renderGroups.Clear();
			this.TotalDrawCalls = 0;
			this.TotalTriangles = 0;
			if (this.RenderCamera == null || !base.enabled)
			{
				if (this.meshRenderer != null)
				{
					this.meshRenderer.enabled = false;
				}
			}
			else
			{
				if (this.meshRenderer != null && !this.meshRenderer.enabled)
				{
					this.meshRenderer.enabled = true;
				}
				if (this.renderMesh == null || this.renderMesh.Length == 0)
				{
					Debug.LogError("GUI Manager not initialized before Render() called");
				}
				else
				{
					this.resetDrawCalls();
					dfRenderData dfRenderData = null;
					this.clipStack.Clear();
					this.clipStack.Push(dfTriangleClippingRegion.Obtain());
					uint start_VALUE = dfChecksumUtil.START_VALUE;
					using (dfList<dfControl> topLevelControls = this.getTopLevelControls())
					{
						this.updateRenderOrder(topLevelControls);
						int num = 0;
						while (num < topLevelControls.Count && !this.abortRendering)
						{
							dfControl control = topLevelControls[num];
							this.renderControl(ref dfRenderData, control, start_VALUE, 1f);
							num++;
						}
					}
					if (this.abortRendering)
					{
						this.clipStack.Clear();
						throw new dfAbortRenderingException();
					}
					this.drawCallBuffers.RemoveAll(new Predicate<dfRenderData>(this.isEmptyBuffer));
					this.drawCallCount = this.drawCallBuffers.Count;
					this.TotalDrawCalls = this.drawCallCount;
					if (this.drawCallBuffers.Count == 0)
					{
						if (this.renderFilter.sharedMesh != null)
						{
							this.renderFilter.sharedMesh.Clear();
						}
					}
					else
					{
						dfRenderData dfRenderData2 = this.compileMasterBuffer();
						this.TotalTriangles = dfRenderData2.Triangles.Count / 3;
						Mesh mesh = this.renderFilter.sharedMesh = this.getRenderMesh();
						mesh.Clear(true);
						mesh.vertices = dfRenderData2.Vertices.Items;
						mesh.uv = dfRenderData2.UV.Items;
						mesh.colors32 = dfRenderData2.Colors.Items;
						if (this.generateNormals && dfRenderData2.Normals.Items.Length == dfRenderData2.Vertices.Items.Length)
						{
							mesh.normals = dfRenderData2.Normals.Items;
							mesh.tangents = dfRenderData2.Tangents.Items;
						}
						mesh.subMeshCount = this.submeshes.Count;
						for (int i = 0; i < this.submeshes.Count; i++)
						{
							int num2 = this.submeshes[i];
							int length = dfRenderData2.Triangles.Count - num2;
							if (i < this.submeshes.Count - 1)
							{
								length = this.submeshes[i + 1] - num2;
							}
							int[] array = dfTempArray<int>.Obtain(length);
							dfRenderData2.Triangles.CopyTo(num2, array, 0, length);
							mesh.SetTriangles(array, i);
						}
						if (this.clipStack.Count != 1)
						{
							Debug.LogError("Clip stack not properly maintained");
						}
						this.clipStack.Pop().Release();
						this.clipStack.Clear();
						this.updateRenderSettings();
					}
				}
			}
		}
		catch (dfAbortRenderingException)
		{
			this.isDirty = true;
			this.abortRendering = false;
		}
		finally
		{
			this.meshRenderer.enabled = true;
			if (dfGUIManager.AfterRender != null)
			{
				dfGUIManager.AfterRender(this);
			}
		}
	}

	// Token: 0x06000743 RID: 1859 RVA: 0x0001F2D8 File Offset: 0x0001D4D8
	private void updateDrawCalls()
	{
		if (this.meshRenderer == null)
		{
			this.initialize();
		}
		Material[] array = this.gatherMaterials();
		this.meshRenderer.sharedMaterials = array;
		int num = this.renderQueueBase + array.Length;
		dfRenderGroup[] items = this.renderGroups.Items;
		int count = this.renderGroups.Count;
		for (int i = 0; i < count; i++)
		{
			items[i].UpdateRenderQueue(ref num);
		}
	}

	// Token: 0x06000744 RID: 1860 RVA: 0x0001F34C File Offset: 0x0001D54C
	private static bool isInsideClippingRegion(Vector3 point, dfControl control)
	{
		while (control != null)
		{
			Plane[] array = control.ClipChildren ? control.GetClippingPlanes() : null;
			if (array != null && array.Length != 0)
			{
				for (int i = 0; i < array.Length; i++)
				{
					if (!array[i].GetSide(point))
					{
						return false;
					}
				}
			}
			control = control.Parent;
		}
		return true;
	}

	// Token: 0x06000745 RID: 1861 RVA: 0x0001F3A8 File Offset: 0x0001D5A8
	private int getMaxZOrder()
	{
		int num = -1;
		using (dfList<dfControl> topLevelControls = this.getTopLevelControls())
		{
			for (int i = 0; i < topLevelControls.Count; i++)
			{
				num = Mathf.Max(num, topLevelControls[i].ZOrder);
			}
		}
		return num;
	}

	// Token: 0x06000746 RID: 1862 RVA: 0x0001F400 File Offset: 0x0001D600
	private bool isEmptyBuffer(dfRenderData buffer)
	{
		return buffer.Vertices.Count == 0;
	}

	// Token: 0x06000747 RID: 1863 RVA: 0x0001F410 File Offset: 0x0001D610
	private dfList<dfControl> getTopLevelControls()
	{
		dfList<dfControl> dfList = dfList<dfControl>.Obtain(base.transform.childCount);
		dfControl[] items = dfControl.ActiveInstances.Items;
		int count = dfControl.ActiveInstances.Count;
		for (int i = 0; i < count; i++)
		{
			dfControl dfControl = items[i];
			if (dfControl.IsTopLevelControl(this))
			{
				dfList.Add(dfControl);
			}
		}
		dfList.Sort();
		return dfList;
	}

	// Token: 0x06000748 RID: 1864 RVA: 0x0001F470 File Offset: 0x0001D670
	private void updateRenderSettings()
	{
		Camera camera = this.RenderCamera;
		if (camera == null)
		{
			return;
		}
		if (!this.overrideCamera)
		{
			this.updateRenderCamera(camera);
		}
		if (base.transform.hasChanged)
		{
			Vector3 localScale = base.transform.localScale;
			if (localScale.x < 1E-45f || !Mathf.Approximately(localScale.x, localScale.y) || !Mathf.Approximately(localScale.x, localScale.z))
			{
				localScale.y = (localScale.z = (localScale.x = Mathf.Max(localScale.x, 0.001f)));
				base.transform.localScale = localScale;
			}
		}
		if (!this.overrideCamera && Application.isPlaying && this.PixelPerfectMode)
		{
			float num = (float)camera.pixelHeight / (float)this.fixedHeight;
			camera.orthographicSize = num;
			camera.fieldOfView = 60f * num;
		}
		camera.transparencySortMode = TransparencySortMode.Orthographic;
		if (this.cachedScreenSize.sqrMagnitude <= 1E-45f)
		{
			this.cachedScreenSize = new Vector2((float)this.FixedWidth, (float)this.FixedHeight);
		}
		base.transform.hasChanged = false;
	}

	// Token: 0x06000749 RID: 1865 RVA: 0x0001F5A0 File Offset: 0x0001D7A0
	private void updateRenderCamera(Camera camera)
	{
		if (Application.isPlaying && camera.targetTexture != null)
		{
			camera.clearFlags = CameraClearFlags.Color;
			camera.backgroundColor = Color.clear;
		}
		else
		{
			camera.clearFlags = CameraClearFlags.Depth;
		}
		Vector3 vector = Application.isPlaying ? (-this.uiOffset * this.PixelsToUnits()) : Vector3.zero;
		if (camera.orthographic)
		{
			camera.nearClipPlane = Mathf.Min(camera.nearClipPlane, -1f);
			camera.farClipPlane = Mathf.Max(camera.farClipPlane, 1f);
		}
		else
		{
			float num = camera.fieldOfView * 0.017453292f;
			Vector3[] array = this.GetCorners();
			float num2 = this.uiScaleLegacy ? 1f : this.uiScale;
			float num3 = Vector3.Distance(array[3], array[0]) * num2 / (2f * Mathf.Tan(num / 2f));
			Vector3 a = base.transform.TransformDirection(Vector3.back) * num3;
			camera.farClipPlane = Mathf.Max(num3 * 2f, camera.farClipPlane);
			vector += a / this.uiScale;
		}
		int pixelHeight = camera.pixelHeight;
		float num4 = 2f / (float)pixelHeight * ((float)pixelHeight / (float)this.FixedHeight);
		if (Application.isPlaying && this.needHalfPixelOffset())
		{
			Vector3 b = new Vector3(num4 * 0.5f, num4 * -0.5f, 0f);
			vector += b;
		}
		if (!this.overrideCamera)
		{
			camera.renderingPath = RenderingPath.Forward;
			if (Screen.width % 2 != 0)
			{
				vector.x += num4 * 0.5f;
			}
			if (Screen.height % 2 != 0)
			{
				vector.y += num4 * 0.5f;
			}
			if (Vector3.SqrMagnitude(camera.transform.localPosition - vector) > 1E-45f)
			{
				camera.transform.localPosition = vector;
			}
			camera.transform.hasChanged = false;
		}
	}

	// Token: 0x0600074A RID: 1866 RVA: 0x0001F7A8 File Offset: 0x0001D9A8
	private dfRenderData compileMasterBuffer()
	{
		this.submeshes.Clear();
		dfGUIManager.masterBuffer.Clear();
		dfRenderData[] items = this.drawCallBuffers.Items;
		int num = 0;
		for (int i = 0; i < this.drawCallCount; i++)
		{
			num += items[i].Vertices.Count;
		}
		dfGUIManager.masterBuffer.EnsureCapacity(num);
		for (int j = 0; j < this.drawCallCount; j++)
		{
			this.submeshes.Add(dfGUIManager.masterBuffer.Triangles.Count);
			dfRenderData dfRenderData = items[j];
			if (this.generateNormals && dfRenderData.Normals.Count == 0)
			{
				this.generateNormalsAndTangents(dfRenderData);
			}
			dfGUIManager.masterBuffer.Merge(dfRenderData, false);
		}
		dfGUIManager.masterBuffer.ApplyTransform(base.transform.worldToLocalMatrix);
		return dfGUIManager.masterBuffer;
	}

	// Token: 0x0600074B RID: 1867 RVA: 0x0001F87C File Offset: 0x0001DA7C
	private void generateNormalsAndTangents(dfRenderData buffer)
	{
		Vector3 normalized = buffer.Transform.MultiplyVector(Vector3.back).normalized;
		Vector4 item = buffer.Transform.MultiplyVector(Vector3.right).normalized;
		item.w = -1f;
		for (int i = 0; i < buffer.Vertices.Count; i++)
		{
			buffer.Normals.Add(normalized);
			buffer.Tangents.Add(item);
		}
	}

	// Token: 0x0600074C RID: 1868 RVA: 0x0001F8FC File Offset: 0x0001DAFC
	private bool needHalfPixelOffset()
	{
		if (this.applyHalfPixelOffset != null)
		{
			return this.applyHalfPixelOffset.Value;
		}
		RuntimePlatform platform = Application.platform;
		bool flag = this.pixelPerfectMode && (platform == RuntimePlatform.WindowsPlayer || platform == RuntimePlatform.WindowsEditor) && SystemInfo.graphicsDeviceVersion.ToLower().StartsWith("direct");
		bool flag2 = SystemInfo.graphicsShaderLevel >= 40;
		this.applyHalfPixelOffset = new bool?((Application.isEditor || flag) && !flag2);
		return flag;
	}

	// Token: 0x0600074D RID: 1869 RVA: 0x0001F97C File Offset: 0x0001DB7C
	private Material[] gatherMaterials()
	{
		int materialCount = this.getMaterialCount();
		int num = 0;
		int num2 = this.renderQueueBase;
		Material[] array = dfTempArray<Material>.Obtain(materialCount);
		for (int i = 0; i < this.drawCallBuffers.Count; i++)
		{
			dfRenderData dfRenderData = this.drawCallBuffers[i];
			if (!(dfRenderData.Material == null))
			{
				Material material = dfMaterialCache.Lookup(dfRenderData.Material);
				material.mainTexture = dfRenderData.Material.mainTexture;
				material.shader = (dfRenderData.Shader ?? material.shader);
				material.renderQueue = num2++;
				material.mainTextureOffset = Vector2.zero;
				material.mainTextureScale = Vector2.zero;
				array[num++] = material;
			}
		}
		return array;
	}

	// Token: 0x0600074E RID: 1870 RVA: 0x0001FA40 File Offset: 0x0001DC40
	private int getMaterialCount()
	{
		int num = 0;
		for (int i = 0; i < this.drawCallCount; i++)
		{
			if (this.drawCallBuffers[i] != null && this.drawCallBuffers[i].Material != null)
			{
				num++;
			}
		}
		return num;
	}

	// Token: 0x0600074F RID: 1871 RVA: 0x0001FA8C File Offset: 0x0001DC8C
	private void resetDrawCalls()
	{
		this.drawCallCount = 0;
		for (int i = 0; i < this.drawCallBuffers.Count; i++)
		{
			this.drawCallBuffers[i].Release();
		}
		this.drawCallBuffers.Clear();
	}

	// Token: 0x06000750 RID: 1872 RVA: 0x0001FAD4 File Offset: 0x0001DCD4
	private dfRenderData getDrawCallBuffer(Material material)
	{
		dfRenderData dfRenderData;
		if (this.MergeMaterials && material != null)
		{
			dfRenderData = this.findDrawCallBufferByMaterial(material);
			if (dfRenderData != null)
			{
				return dfRenderData;
			}
		}
		dfRenderData = dfRenderData.Obtain();
		dfRenderData.Material = material;
		this.drawCallBuffers.Add(dfRenderData);
		this.drawCallCount++;
		return dfRenderData;
	}

	// Token: 0x06000751 RID: 1873 RVA: 0x0001FB2C File Offset: 0x0001DD2C
	private dfRenderData findDrawCallBufferByMaterial(Material material)
	{
		for (int i = 0; i < this.drawCallCount; i++)
		{
			if (this.drawCallBuffers[i].Material == material)
			{
				return this.drawCallBuffers[i];
			}
		}
		return null;
	}

	// Token: 0x06000752 RID: 1874 RVA: 0x0001FB71 File Offset: 0x0001DD71
	private Mesh getRenderMesh()
	{
		this.activeRenderMesh = ((this.activeRenderMesh == 1) ? 0 : 1);
		return this.renderMesh[this.activeRenderMesh];
	}

	// Token: 0x06000753 RID: 1875 RVA: 0x0001FB94 File Offset: 0x0001DD94
	private void renderControl(ref dfRenderData buffer, dfControl control, uint checksum, float opacity)
	{
		if (!control.enabled || !control.gameObject.activeSelf)
		{
			return;
		}
		float num = opacity * control.Opacity;
		dfRenderGroup renderGroupForControl = dfRenderGroup.GetRenderGroupForControl(control, true);
		if (renderGroupForControl != null && renderGroupForControl.enabled)
		{
			this.renderGroups.Add(renderGroupForControl);
			renderGroupForControl.Render(this.renderCamera, control, this.occluders, this.controlsRendered, checksum, num);
			return;
		}
		if (num <= 0.001f || !control.GetIsVisibleRaw())
		{
			return;
		}
		dfTriangleClippingRegion dfTriangleClippingRegion = this.clipStack.Peek();
		checksum = dfChecksumUtil.Calculate(checksum, control.Version);
		Bounds bounds = control.GetBounds();
		bool clippingState = false;
		if (!(control is IDFMultiRender))
		{
			dfRenderData dfRenderData = control.Render();
			if (dfRenderData != null)
			{
				this.processRenderData(ref buffer, dfRenderData, ref bounds, checksum, dfTriangleClippingRegion, ref clippingState);
			}
		}
		else
		{
			dfList<dfRenderData> dfList = ((IDFMultiRender)control).RenderMultiple();
			if (dfList != null)
			{
				dfRenderData[] items = dfList.Items;
				int count = dfList.Count;
				for (int i = 0; i < count; i++)
				{
					dfRenderData dfRenderData2 = items[i];
					if (dfRenderData2 != null)
					{
						this.processRenderData(ref buffer, dfRenderData2, ref bounds, checksum, dfTriangleClippingRegion, ref clippingState);
					}
				}
			}
		}
		control.setClippingState(clippingState);
		this.NumControlsRendered++;
		this.occluders.Add(this.getControlOccluder(control));
		this.controlsRendered.Add(control);
		this.drawCallIndices.Add(this.drawCallBuffers.Count - 1);
		if (control.ClipChildren)
		{
			dfTriangleClippingRegion = dfTriangleClippingRegion.Obtain(dfTriangleClippingRegion, control);
			this.clipStack.Push(dfTriangleClippingRegion);
		}
		dfControl[] items2 = control.Controls.Items;
		int count2 = control.Controls.Count;
		this.controlsRendered.EnsureCapacity(this.controlsRendered.Count + count2);
		this.occluders.EnsureCapacity(this.occluders.Count + count2);
		for (int j = 0; j < count2; j++)
		{
			this.renderControl(ref buffer, items2[j], checksum, num);
		}
		if (control.ClipChildren)
		{
			this.clipStack.Pop().Release();
		}
	}

	// Token: 0x06000754 RID: 1876 RVA: 0x0001FD98 File Offset: 0x0001DF98
	private Rect getControlOccluder(dfControl control)
	{
		if (!control.IsInteractive)
		{
			return default(Rect);
		}
		Rect screenRect = control.GetScreenRect();
		Vector2 vector = new Vector2(screenRect.width * control.HotZoneScale.x, screenRect.height * control.HotZoneScale.y);
		Vector2 vector2 = new Vector2(vector.x - screenRect.width, vector.y - screenRect.height) * 0.5f;
		return new Rect(screenRect.x - vector2.x, screenRect.y - vector2.y, vector.x, vector.y);
	}

	// Token: 0x06000755 RID: 1877 RVA: 0x0001FE44 File Offset: 0x0001E044
	private bool processRenderData(ref dfRenderData buffer, dfRenderData controlData, ref Bounds bounds, uint checksum, dfTriangleClippingRegion clipInfo, ref bool wasClipped)
	{
		wasClipped = false;
		if (controlData == null || controlData.Material == null || !controlData.IsValid())
		{
			return false;
		}
		bool flag = false;
		if (buffer == null)
		{
			flag = true;
		}
		else if (!object.Equals(controlData.Material, buffer.Material))
		{
			flag = true;
		}
		else if (!this.textureEqual(controlData.Material.mainTexture, buffer.Material.mainTexture))
		{
			flag = true;
		}
		else if (!this.shaderEqual(buffer.Shader, controlData.Shader))
		{
			flag = true;
		}
		if (flag)
		{
			buffer = this.getDrawCallBuffer(controlData.Material);
			buffer.Material = controlData.Material;
			buffer.Material.mainTexture = controlData.Material.mainTexture;
			buffer.Material.shader = (controlData.Shader ?? controlData.Material.shader);
		}
		if (clipInfo.PerformClipping(buffer, ref bounds, checksum, controlData))
		{
			return true;
		}
		wasClipped = true;
		return false;
	}

	// Token: 0x06000756 RID: 1878 RVA: 0x0001FF38 File Offset: 0x0001E138
	private bool textureEqual(Texture lhs, Texture rhs)
	{
		return object.Equals(lhs, rhs);
	}

	// Token: 0x06000757 RID: 1879 RVA: 0x0001FF41 File Offset: 0x0001E141
	private bool shaderEqual(Shader lhs, Shader rhs)
	{
		if (lhs == null || rhs == null)
		{
			return lhs == rhs;
		}
		return lhs.name.Equals(rhs.name);
	}

	// Token: 0x06000758 RID: 1880 RVA: 0x0001FF6C File Offset: 0x0001E16C
	private void initialize()
	{
		if (Application.isPlaying && this.renderCamera == null)
		{
			Debug.LogError("No camera is assigned to the GUIManager");
			return;
		}
		this.meshRenderer = base.GetComponent<MeshRenderer>();
		if (this.meshRenderer == null)
		{
			this.meshRenderer = base.gameObject.AddComponent<MeshRenderer>();
		}
		this.meshRenderer.hideFlags = HideFlags.HideInInspector;
		this.renderFilter = base.GetComponent<MeshFilter>();
		if (this.renderFilter == null)
		{
			this.renderFilter = base.gameObject.AddComponent<MeshFilter>();
		}
		this.renderFilter.hideFlags = HideFlags.HideInInspector;
		this.renderMesh = new Mesh[]
		{
			new Mesh
			{
				hideFlags = HideFlags.DontSave
			},
			new Mesh
			{
				hideFlags = HideFlags.DontSave
			}
		};
		this.renderMesh[0].MarkDynamic();
		this.renderMesh[1].MarkDynamic();
		if (this.fixedWidth < 0)
		{
			this.fixedWidth = Mathf.RoundToInt((float)this.fixedHeight * 1.33333f);
			dfControl[] componentsInChildren = base.GetComponentsInChildren<dfControl>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].ResetLayout();
			}
		}
	}

	// Token: 0x06000759 RID: 1881 RVA: 0x0002008C File Offset: 0x0001E28C
	private void onResolutionChanged()
	{
		int currentSize = Application.isPlaying ? this.renderCamera.pixelHeight : this.FixedHeight;
		this.onResolutionChanged(this.FixedHeight, currentSize);
	}

	// Token: 0x0600075A RID: 1882 RVA: 0x000200C4 File Offset: 0x0001E2C4
	private void onResolutionChanged(int oldSize, int currentSize)
	{
		float aspect = this.RenderCamera.aspect;
		float x = (float)oldSize * aspect;
		float x2 = (float)currentSize * aspect;
		Vector2 oldSize2 = new Vector2(x, (float)oldSize);
		Vector2 currentSize2 = new Vector2(x2, (float)currentSize);
		this.onResolutionChanged(oldSize2, currentSize2);
	}

	// Token: 0x0600075B RID: 1883 RVA: 0x00020104 File Offset: 0x0001E304
	private void onResolutionChanged(Vector2 oldSize, Vector2 currentSize)
	{
		if (this.shutdownInProcess)
		{
			return;
		}
		this.cachedScreenSize = currentSize;
		this.applyHalfPixelOffset = null;
		float aspect = this.RenderCamera.aspect;
		float x = oldSize.y * aspect;
		float x2 = currentSize.y * aspect;
		Vector2 previousResolution = new Vector2(x, oldSize.y);
		Vector2 currentResolution = new Vector2(x2, currentSize.y);
		dfControl[] componentsInChildren = base.GetComponentsInChildren<dfControl>();
		Array.Sort<dfControl>(componentsInChildren, new Comparison<dfControl>(this.renderSortFunc));
		for (int i = componentsInChildren.Length - 1; i >= 0; i--)
		{
			if (this.pixelPerfectMode && componentsInChildren[i].Parent == null)
			{
				componentsInChildren[i].MakePixelPerfect();
			}
			componentsInChildren[i].OnResolutionChanged(previousResolution, currentResolution);
		}
		for (int j = 0; j < componentsInChildren.Length; j++)
		{
			componentsInChildren[j].PerformLayout();
		}
		int num = 0;
		while (num < componentsInChildren.Length && this.pixelPerfectMode)
		{
			if (componentsInChildren[num].Parent == null)
			{
				componentsInChildren[num].MakePixelPerfect();
			}
			num++;
		}
		this.isDirty = true;
		this.updateRenderSettings();
	}

	// Token: 0x0600075C RID: 1884 RVA: 0x0002022C File Offset: 0x0001E42C
	private void invalidateAllControls()
	{
		dfControl[] componentsInChildren = base.GetComponentsInChildren<dfControl>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].Invalidate();
		}
		this.updateRenderOrder();
	}

	// Token: 0x0600075D RID: 1885 RVA: 0x0002025C File Offset: 0x0001E45C
	private int renderSortFunc(dfControl lhs, dfControl rhs)
	{
		return lhs.RenderOrder.CompareTo(rhs.RenderOrder);
	}

	// Token: 0x0600075E RID: 1886 RVA: 0x0002027D File Offset: 0x0001E47D
	private void updateRenderOrder()
	{
		this.updateRenderOrder(null);
	}

	// Token: 0x0600075F RID: 1887 RVA: 0x00020288 File Offset: 0x0001E488
	private void updateRenderOrder(dfList<dfControl> list)
	{
		dfList<dfControl> dfList = list;
		bool flag = false;
		if (list == null)
		{
			dfList = this.getTopLevelControls();
			flag = true;
		}
		else
		{
			dfList.Sort();
		}
		int num = 0;
		int count = dfList.Count;
		dfControl[] items = dfList.Items;
		for (int i = 0; i < count; i++)
		{
			dfControl dfControl = items[i];
			if (dfControl.Parent == null)
			{
				dfControl.setRenderOrder(ref num);
			}
		}
		if (flag)
		{
			dfList.Release();
		}
	}

	// Token: 0x06000760 RID: 1888 RVA: 0x000202F8 File Offset: 0x0001E4F8
	public int CompareTo(dfGUIManager other)
	{
		int num = this.renderQueueBase.CompareTo(other.renderQueueBase);
		if (num == 0 && this.RenderCamera != null && other.RenderCamera != null)
		{
			return this.RenderCamera.depth.CompareTo(other.RenderCamera.depth);
		}
		return num;
	}

	// Token: 0x040002B6 RID: 694
	[SerializeField]
	protected float uiScale = 1f;

	// Token: 0x040002B7 RID: 695
	[SerializeField]
	protected bool uiScaleLegacy = true;

	// Token: 0x040002B8 RID: 696
	[SerializeField]
	protected dfInputManager inputManager;

	// Token: 0x040002B9 RID: 697
	[SerializeField]
	protected int fixedWidth = -1;

	// Token: 0x040002BA RID: 698
	[SerializeField]
	protected int fixedHeight = 600;

	// Token: 0x040002BB RID: 699
	[SerializeField]
	protected dfAtlas atlas;

	// Token: 0x040002BC RID: 700
	[SerializeField]
	protected dfFontBase defaultFont;

	// Token: 0x040002BD RID: 701
	[SerializeField]
	protected bool mergeMaterials;

	// Token: 0x040002BE RID: 702
	[SerializeField]
	protected bool pixelPerfectMode = true;

	// Token: 0x040002BF RID: 703
	[SerializeField]
	protected Camera renderCamera;

	// Token: 0x040002C0 RID: 704
	[SerializeField]
	protected bool generateNormals;

	// Token: 0x040002C1 RID: 705
	[SerializeField]
	protected bool consumeMouseEvents;

	// Token: 0x040002C2 RID: 706
	[SerializeField]
	protected bool overrideCamera;

	// Token: 0x040002C3 RID: 707
	[SerializeField]
	protected int renderQueueBase = 3000;

	// Token: 0x040002C4 RID: 708
	[SerializeField]
	public List<dfDesignGuide> guides = new List<dfDesignGuide>();

	// Token: 0x040002C5 RID: 709
	private static List<dfGUIManager> activeInstances = new List<dfGUIManager>();

	// Token: 0x040002C6 RID: 710
	private static dfControl activeControl = null;

	// Token: 0x040002C7 RID: 711
	private static Stack<dfGUIManager.ModalControlReference> modalControlStack = new Stack<dfGUIManager.ModalControlReference>();

	// Token: 0x040002C8 RID: 712
	private dfGUICamera guiCamera;

	// Token: 0x040002C9 RID: 713
	private Mesh[] renderMesh;

	// Token: 0x040002CA RID: 714
	private MeshFilter renderFilter;

	// Token: 0x040002CB RID: 715
	private MeshRenderer meshRenderer;

	// Token: 0x040002CC RID: 716
	private int activeRenderMesh;

	// Token: 0x040002CD RID: 717
	private int cachedChildCount;

	// Token: 0x040002CE RID: 718
	private bool isDirty;

	// Token: 0x040002CF RID: 719
	private bool abortRendering;

	// Token: 0x040002D0 RID: 720
	private Vector2 cachedScreenSize;

	// Token: 0x040002D1 RID: 721
	private Vector3[] corners = new Vector3[4];

	// Token: 0x040002D2 RID: 722
	private dfList<Rect> occluders = new dfList<Rect>(256);

	// Token: 0x040002D3 RID: 723
	private Stack<dfTriangleClippingRegion> clipStack = new Stack<dfTriangleClippingRegion>();

	// Token: 0x040002D4 RID: 724
	private static dfRenderData masterBuffer = new dfRenderData(4096);

	// Token: 0x040002D5 RID: 725
	private dfList<dfRenderData> drawCallBuffers = new dfList<dfRenderData>();

	// Token: 0x040002D6 RID: 726
	private dfList<dfRenderGroup> renderGroups = new dfList<dfRenderGroup>();

	// Token: 0x040002D7 RID: 727
	private List<int> submeshes = new List<int>();

	// Token: 0x040002D8 RID: 728
	private int drawCallCount;

	// Token: 0x040002D9 RID: 729
	private Vector2 uiOffset = Vector2.zero;

	// Token: 0x040002DA RID: 730
	private static Plane[] clippingPlanes;

	// Token: 0x040002DB RID: 731
	private dfList<int> drawCallIndices = new dfList<int>();

	// Token: 0x040002DC RID: 732
	private dfList<dfControl> controlsRendered = new dfList<dfControl>();

	// Token: 0x040002DD RID: 733
	private bool shutdownInProcess;

	// Token: 0x040002DE RID: 734
	private int suspendCount;

	// Token: 0x040002E3 RID: 739
	private bool? applyHalfPixelOffset;

	// Token: 0x02000371 RID: 881
	// (Invoke) Token: 0x06001CB7 RID: 7351
	[dfEventCategory("Modal Dialog")]
	public delegate void ModalPoppedCallback(dfControl control);

	// Token: 0x02000372 RID: 882
	// (Invoke) Token: 0x06001CBB RID: 7355
	[dfEventCategory("Global Callbacks")]
	public delegate void RenderCallback(dfGUIManager manager);

	// Token: 0x02000373 RID: 883
	private struct ModalControlReference
	{
		// Token: 0x0400165D RID: 5725
		public dfControl control;

		// Token: 0x0400165E RID: 5726
		public dfGUIManager.ModalPoppedCallback callback;
	}
}
