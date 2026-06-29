using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000071 RID: 113
[ExecuteInEditMode]
[AddComponentMenu("Daikon Forge/User Interface/Render Group")]
[Serializable]
internal class dfRenderGroup : MonoBehaviour
{
	// Token: 0x170001DF RID: 479
	// (get) Token: 0x060007F4 RID: 2036 RVA: 0x000228E5 File Offset: 0x00020AE5
	// (set) Token: 0x060007F5 RID: 2037 RVA: 0x000228ED File Offset: 0x00020AED
	public dfClippingMethod ClipType
	{
		get
		{
			return this.clipType;
		}
		set
		{
			if (value != this.clipType)
			{
				this.clipType = value;
				if (this.attachedControl != null)
				{
					this.attachedControl.Invalidate();
				}
			}
		}
	}

	// Token: 0x060007F6 RID: 2038 RVA: 0x00022918 File Offset: 0x00020B18
	public void OnEnable()
	{
		dfRenderGroup.activeInstances.Add(this);
		this.isDirty = true;
		if (this.meshRenderer == null)
		{
			this.initialize();
		}
		this.meshRenderer.enabled = true;
		if (this.attachedControl != null)
		{
			this.attachedControl.Invalidate();
		}
		else
		{
			dfGUIManager.InvalidateAll();
		}
		this.attachedControl = base.GetComponent<dfControl>();
	}

	// Token: 0x060007F7 RID: 2039 RVA: 0x00022984 File Offset: 0x00020B84
	public void OnDisable()
	{
		dfRenderGroup.activeInstances.Remove(this);
		if (this.meshRenderer != null)
		{
			this.meshRenderer.enabled = false;
		}
		if (this.attachedControl != null)
		{
			this.attachedControl.Invalidate();
		}
	}

	// Token: 0x060007F8 RID: 2040 RVA: 0x000229D0 File Offset: 0x00020BD0
	public void OnDestroy()
	{
		if (this.renderFilter != null)
		{
			this.renderFilter.sharedMesh = null;
		}
		this.renderFilter = null;
		this.meshRenderer = null;
		if (this.renderMesh != null)
		{
			this.renderMesh.Clear();
			Object.DestroyImmediate(this.renderMesh);
			this.renderMesh = null;
		}
		dfGUIManager.InvalidateAll();
	}

	// Token: 0x060007F9 RID: 2041 RVA: 0x00022A35 File Offset: 0x00020C35
	internal static dfRenderGroup GetRenderGroupForControl(dfControl control)
	{
		return dfRenderGroup.GetRenderGroupForControl(control, false);
	}

	// Token: 0x060007FA RID: 2042 RVA: 0x00022A40 File Offset: 0x00020C40
	internal static dfRenderGroup GetRenderGroupForControl(dfControl control, bool directlyAttachedOnly)
	{
		Transform transform = control.transform;
		for (int i = 0; i < dfRenderGroup.activeInstances.Count; i++)
		{
			dfRenderGroup dfRenderGroup = dfRenderGroup.activeInstances[i];
			if (dfRenderGroup.attachedControl == control)
			{
				return dfRenderGroup;
			}
			if (!directlyAttachedOnly && transform.IsChildOf(dfRenderGroup.transform))
			{
				return dfRenderGroup;
			}
		}
		return null;
	}

	// Token: 0x060007FB RID: 2043 RVA: 0x00022A9C File Offset: 0x00020C9C
	internal static void InvalidateGroupForControl(dfControl control)
	{
		Transform transform = control.transform;
		for (int i = 0; i < dfRenderGroup.activeInstances.Count; i++)
		{
			dfRenderGroup dfRenderGroup = dfRenderGroup.activeInstances[i];
			if (transform.IsChildOf(dfRenderGroup.transform))
			{
				dfRenderGroup.isDirty = true;
			}
		}
	}

	// Token: 0x060007FC RID: 2044 RVA: 0x00022AE8 File Offset: 0x00020CE8
	internal void Render(Camera renderCamera, dfControl control, dfList<Rect> occluders, dfList<dfControl> controlsRendered, uint checksum, float opacity)
	{
		if (this.meshRenderer == null)
		{
			this.initialize();
		}
		this.renderCamera = renderCamera;
		this.attachedControl = control;
		if (!this.isDirty)
		{
			occluders.AddRange(this.groupOccluders);
			controlsRendered.AddRange(this.groupControls);
			return;
		}
		this.groupOccluders.Clear();
		this.groupControls.Clear();
		this.renderGroups.Clear();
		this.resetDrawCalls();
		this.clipInfo = default(dfRenderGroup.ClipRegionInfo);
		this.clipRect = default(Rect);
		dfRenderData dfRenderData = null;
		using (dfTriangleClippingRegion dfTriangleClippingRegion = dfTriangleClippingRegion.Obtain())
		{
			this.clipStack.Clear();
			this.clipStack.Push(dfTriangleClippingRegion);
			this.renderControl(ref dfRenderData, control, checksum, opacity);
			this.clipStack.Pop();
		}
		this.drawCallBuffers.RemoveAll(new Predicate<dfRenderData>(this.isEmptyBuffer));
		this.drawCallCount = this.drawCallBuffers.Count;
		if (this.drawCallBuffers.Count == 0)
		{
			this.meshRenderer.enabled = false;
			return;
		}
		this.meshRenderer.enabled = true;
		dfRenderData dfRenderData2 = this.compileMasterBuffer();
		Mesh mesh = this.renderMesh;
		mesh.Clear(true);
		mesh.vertices = dfRenderData2.Vertices.Items;
		mesh.uv = dfRenderData2.UV.Items;
		mesh.colors32 = dfRenderData2.Colors.Items;
		mesh.subMeshCount = this.submeshes.Count;
		for (int i = 0; i < this.submeshes.Count; i++)
		{
			int num = this.submeshes[i];
			int length = dfRenderData2.Triangles.Count - num;
			if (i < this.submeshes.Count - 1)
			{
				length = this.submeshes[i + 1] - num;
			}
			int[] array = dfTempArray<int>.Obtain(length);
			dfRenderData2.Triangles.CopyTo(num, array, 0, length);
			mesh.SetTriangles(array, i);
		}
		this.isDirty = false;
		occluders.AddRange(this.groupOccluders);
		controlsRendered.AddRange(this.groupControls);
	}

	// Token: 0x060007FD RID: 2045 RVA: 0x00022D18 File Offset: 0x00020F18
	internal void UpdateRenderQueue(ref int renderQueueBase)
	{
		int materialCount = this.getMaterialCount();
		int num = 0;
		Material[] array = dfTempArray<Material>.Obtain(materialCount);
		for (int i = 0; i < this.drawCallBuffers.Count; i++)
		{
			if (!(this.drawCallBuffers[i].Material == null))
			{
				Material material = dfMaterialCache.Lookup(this.drawCallBuffers[i].Material);
				material.mainTexture = this.drawCallBuffers[i].Material.mainTexture;
				material.shader = (this.drawCallBuffers[i].Shader ?? material.shader);
				material.mainTextureScale = Vector2.zero;
				material.mainTextureOffset = Vector2.zero;
				Material material2 = material;
				int num2 = renderQueueBase + 1;
				renderQueueBase = num2;
				material2.renderQueue = num2;
				if (Application.isPlaying && this.clipType == dfClippingMethod.Shader && !this.clipInfo.IsEmpty && i > 0)
				{
					Vector3 vector = this.attachedControl.Pivot.TransformToCenter(this.attachedControl.Size);
					float num3 = vector.x + this.clipInfo.Offset.x;
					float num4 = vector.y + this.clipInfo.Offset.y;
					float num5 = this.attachedControl.PixelsToUnits();
					material.mainTextureScale = new Vector2(1f / (-this.clipInfo.Size.x * 0.5f * num5), 1f / (-this.clipInfo.Size.y * 0.5f * num5));
					material.mainTextureOffset = new Vector2(num3 / this.clipInfo.Size.x * 2f, num4 / this.clipInfo.Size.y * 2f);
				}
				array[num++] = material;
			}
		}
		this.meshRenderer.sharedMaterials = array;
		dfRenderGroup[] items = this.renderGroups.Items;
		int count = this.renderGroups.Count;
		for (int j = 0; j < count; j++)
		{
			items[j].UpdateRenderQueue(ref renderQueueBase);
		}
	}

	// Token: 0x060007FE RID: 2046 RVA: 0x00022F4C File Offset: 0x0002114C
	private void renderControl(ref dfRenderData buffer, dfControl control, uint checksum, float opacity)
	{
		if (!control.enabled || !control.gameObject.activeSelf)
		{
			return;
		}
		float num = opacity * control.Opacity;
		if (num <= 0.001f)
		{
			return;
		}
		dfRenderGroup renderGroupForControl = dfRenderGroup.GetRenderGroupForControl(control, true);
		if (renderGroupForControl != null && renderGroupForControl != this && renderGroupForControl.enabled)
		{
			this.renderGroups.Add(renderGroupForControl);
			renderGroupForControl.Render(this.renderCamera, control, this.groupOccluders, this.groupControls, checksum, num);
			return;
		}
		if (!control.GetIsVisibleRaw())
		{
			return;
		}
		dfTriangleClippingRegion dfTriangleClippingRegion = this.clipStack.Peek();
		checksum = dfChecksumUtil.Calculate(checksum, control.Version);
		Bounds bounds = control.GetBounds();
		Rect screenRect = control.GetScreenRect();
		Rect controlOccluder = this.getControlOccluder(ref screenRect, control);
		bool clippingState = false;
		if (!(control is IDFMultiRender))
		{
			dfRenderData dfRenderData = control.Render();
			if (dfRenderData != null)
			{
				this.processRenderData(ref buffer, dfRenderData, ref bounds, ref screenRect, checksum, dfTriangleClippingRegion, ref clippingState);
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
						this.processRenderData(ref buffer, dfRenderData2, ref bounds, ref screenRect, checksum, dfTriangleClippingRegion, ref clippingState);
					}
				}
			}
		}
		control.setClippingState(clippingState);
		this.groupOccluders.Add(controlOccluder);
		this.groupControls.Add(control);
		if (control.ClipChildren)
		{
			if (!Application.isPlaying || this.clipType == dfClippingMethod.Software)
			{
				dfTriangleClippingRegion = dfTriangleClippingRegion.Obtain(dfTriangleClippingRegion, control);
				this.clipStack.Push(dfTriangleClippingRegion);
			}
			else if (this.clipInfo.IsEmpty)
			{
				this.setClipRegion(control, ref screenRect);
			}
		}
		dfControl[] items2 = control.Controls.Items;
		int count2 = control.Controls.Count;
		this.groupControls.EnsureCapacity(this.groupControls.Count + count2);
		this.groupOccluders.EnsureCapacity(this.groupOccluders.Count + count2);
		for (int j = 0; j < count2; j++)
		{
			this.renderControl(ref buffer, items2[j], checksum, num);
		}
		if (control.ClipChildren && (!Application.isPlaying || this.clipType == dfClippingMethod.Software))
		{
			this.clipStack.Pop().Release();
		}
	}

	// Token: 0x060007FF RID: 2047 RVA: 0x0002317C File Offset: 0x0002137C
	private void setClipRegion(dfControl control, ref Rect screenRect)
	{
		Vector2 size = control.Size;
		RectOffset clipPadding = control.GetClipPadding();
		float num = Mathf.Min(Mathf.Max(0f, Mathf.Min(size.x, (float)clipPadding.horizontal)), size.x);
		float num2 = Mathf.Min(Mathf.Max(0f, Mathf.Min(size.y, (float)clipPadding.vertical)), size.y);
		this.clipInfo = default(dfRenderGroup.ClipRegionInfo);
		this.clipInfo.Size = Vector2.Max(new Vector2(size.x - num, size.y - num2), Vector3.zero);
		this.clipInfo.Offset = new Vector3((float)(clipPadding.left - clipPadding.right), (float)(-(float)(clipPadding.top - clipPadding.bottom))) * 0.5f;
		this.clipRect = (this.containerRect.IsEmpty() ? screenRect : this.containerRect.Intersection(screenRect));
	}

	// Token: 0x06000800 RID: 2048 RVA: 0x0002328C File Offset: 0x0002148C
	private bool processRenderData(ref dfRenderData buffer, dfRenderData controlData, ref Bounds bounds, ref Rect screenRect, uint checksum, dfTriangleClippingRegion clipInfo, ref bool wasClipped)
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
		else if (!this.clipInfo.IsEmpty && this.drawCallBuffers.Count == 1)
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
		if (!Application.isPlaying || this.clipType == dfClippingMethod.Software)
		{
			if (clipInfo.PerformClipping(buffer, ref bounds, checksum, controlData))
			{
				return true;
			}
			wasClipped = true;
		}
		else if (this.clipRect.IsEmpty() || screenRect.Intersects(this.clipRect))
		{
			buffer.Merge(controlData);
		}
		else
		{
			wasClipped = true;
		}
		return false;
	}

	// Token: 0x06000801 RID: 2049 RVA: 0x000233E0 File Offset: 0x000215E0
	private dfRenderData compileMasterBuffer()
	{
		this.submeshes.Clear();
		dfRenderGroup.masterBuffer.Clear();
		dfRenderData[] items = this.drawCallBuffers.Items;
		int num = 0;
		for (int i = 0; i < this.drawCallCount; i++)
		{
			num += items[i].Vertices.Count;
		}
		dfRenderGroup.masterBuffer.EnsureCapacity(num);
		for (int j = 0; j < this.drawCallCount; j++)
		{
			this.submeshes.Add(dfRenderGroup.masterBuffer.Triangles.Count);
			dfRenderData buffer = items[j];
			dfRenderGroup.masterBuffer.Merge(buffer, false);
		}
		dfRenderGroup.masterBuffer.ApplyTransform(base.transform.worldToLocalMatrix);
		return dfRenderGroup.masterBuffer;
	}

	// Token: 0x06000802 RID: 2050 RVA: 0x00023493 File Offset: 0x00021693
	private bool isEmptyBuffer(dfRenderData buffer)
	{
		return buffer.Vertices.Count == 0;
	}

	// Token: 0x06000803 RID: 2051 RVA: 0x000234A4 File Offset: 0x000216A4
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

	// Token: 0x06000804 RID: 2052 RVA: 0x000234F0 File Offset: 0x000216F0
	private void resetDrawCalls()
	{
		this.drawCallCount = 0;
		for (int i = 0; i < this.drawCallBuffers.Count; i++)
		{
			this.drawCallBuffers[i].Release();
		}
		this.drawCallBuffers.Clear();
	}

	// Token: 0x06000805 RID: 2053 RVA: 0x00023538 File Offset: 0x00021738
	private dfRenderData getDrawCallBuffer(Material material)
	{
		dfRenderData dfRenderData = dfRenderData.Obtain();
		dfRenderData.Material = material;
		this.drawCallBuffers.Add(dfRenderData);
		this.drawCallCount++;
		return dfRenderData;
	}

	// Token: 0x06000806 RID: 2054 RVA: 0x00023570 File Offset: 0x00021770
	private Rect getControlOccluder(ref Rect screenRect, dfControl control)
	{
		if (!control.IsInteractive)
		{
			return default(Rect);
		}
		Vector2 vector = new Vector2(screenRect.width * control.HotZoneScale.x, screenRect.height * control.HotZoneScale.y);
		Vector2 vector2 = new Vector2(vector.x - screenRect.width, vector.y - screenRect.height) * 0.5f;
		return new Rect(screenRect.x - vector2.x, screenRect.y - vector2.y, vector.x, vector.y);
	}

	// Token: 0x06000807 RID: 2055 RVA: 0x0002360F File Offset: 0x0002180F
	private bool textureEqual(Texture lhs, Texture rhs)
	{
		return object.Equals(lhs, rhs);
	}

	// Token: 0x06000808 RID: 2056 RVA: 0x00023618 File Offset: 0x00021818
	private bool shaderEqual(Shader lhs, Shader rhs)
	{
		if (lhs == null || rhs == null)
		{
			return lhs == rhs;
		}
		return lhs.name.Equals(rhs.name);
	}

	// Token: 0x06000809 RID: 2057 RVA: 0x00023644 File Offset: 0x00021844
	private void initialize()
	{
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
		this.renderMesh = new Mesh
		{
			hideFlags = HideFlags.DontSave
		};
		this.renderMesh.MarkDynamic();
		this.renderFilter.sharedMesh = this.renderMesh;
	}

	// Token: 0x040003C4 RID: 964
	private static List<dfRenderGroup> activeInstances = new List<dfRenderGroup>();

	// Token: 0x040003C5 RID: 965
	[SerializeField]
	protected dfClippingMethod clipType;

	// Token: 0x040003C6 RID: 966
	private Mesh renderMesh;

	// Token: 0x040003C7 RID: 967
	private MeshFilter renderFilter;

	// Token: 0x040003C8 RID: 968
	private MeshRenderer meshRenderer;

	// Token: 0x040003C9 RID: 969
	private Camera renderCamera;

	// Token: 0x040003CA RID: 970
	private dfControl attachedControl;

	// Token: 0x040003CB RID: 971
	private static dfRenderData masterBuffer = new dfRenderData(4096);

	// Token: 0x040003CC RID: 972
	private dfList<dfRenderData> drawCallBuffers = new dfList<dfRenderData>();

	// Token: 0x040003CD RID: 973
	private List<int> submeshes = new List<int>();

	// Token: 0x040003CE RID: 974
	private Stack<dfTriangleClippingRegion> clipStack = new Stack<dfTriangleClippingRegion>();

	// Token: 0x040003CF RID: 975
	private dfList<Rect> groupOccluders = new dfList<Rect>();

	// Token: 0x040003D0 RID: 976
	private dfList<dfControl> groupControls = new dfList<dfControl>();

	// Token: 0x040003D1 RID: 977
	private dfList<dfRenderGroup> renderGroups = new dfList<dfRenderGroup>();

	// Token: 0x040003D2 RID: 978
	private dfRenderGroup.ClipRegionInfo clipInfo;

	// Token: 0x040003D3 RID: 979
	private Rect clipRect;

	// Token: 0x040003D4 RID: 980
	private Rect containerRect;

	// Token: 0x040003D5 RID: 981
	private int drawCallCount;

	// Token: 0x040003D6 RID: 982
	private bool isDirty;

	// Token: 0x02000378 RID: 888
	private struct ClipRegionInfo
	{
		// Token: 0x1700050A RID: 1290
		// (get) Token: 0x06001CD5 RID: 7381 RVA: 0x0007C58A File Offset: 0x0007A78A
		public bool IsEmpty
		{
			get
			{
				return this.Offset == Vector2.zero && this.Size == Vector2.zero;
			}
		}

		// Token: 0x0400166B RID: 5739
		public Vector2 Offset;

		// Token: 0x0400166C RID: 5740
		public Vector2 Size;
	}
}
