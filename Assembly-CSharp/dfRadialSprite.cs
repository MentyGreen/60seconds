using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000012 RID: 18
[dfCategory("Basic Controls")]
[dfTooltip("Implements a sprite that can be filled in a radial fashion instead of strictly horizontally or vertically like other sprite classes. Useful for spell cooldown effects, map effects, etc.")]
[dfHelp("http://www.daikonforge.com/docs/df-gui/classdf_radial_sprite.html")]
[ExecuteInEditMode]
[AddComponentMenu("Daikon Forge/User Interface/Sprite/Radial")]
[Serializable]
public class dfRadialSprite : dfSprite
{
	// Token: 0x170000B4 RID: 180
	// (get) Token: 0x0600030A RID: 778 RVA: 0x0000E649 File Offset: 0x0000C849
	// (set) Token: 0x0600030B RID: 779 RVA: 0x0000E651 File Offset: 0x0000C851
	public dfPivotPoint FillOrigin
	{
		get
		{
			return this.fillOrigin;
		}
		set
		{
			if (value != this.fillOrigin)
			{
				this.fillOrigin = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x0600030C RID: 780 RVA: 0x0000E66C File Offset: 0x0000C86C
	protected override void OnRebuildRenderData()
	{
		if (base.Atlas == null)
		{
			return;
		}
		if (base.SpriteInfo == null)
		{
			return;
		}
		this.renderData.Material = base.Atlas.Material;
		List<Vector3> list = null;
		List<int> list2 = null;
		List<Vector2> list3 = null;
		this.buildMeshData(ref list, ref list2, ref list3);
		Color32[] list4 = this.buildColors(list.Count);
		this.renderData.Vertices.AddRange(list);
		this.renderData.Triangles.AddRange(list2);
		this.renderData.UV.AddRange(list3);
		this.renderData.Colors.AddRange(list4);
	}

	// Token: 0x0600030D RID: 781 RVA: 0x0000E710 File Offset: 0x0000C910
	private void buildMeshData(ref List<Vector3> verts, ref List<int> indices, ref List<Vector2> uv)
	{
		List<Vector3> list;
		verts = (list = new List<Vector3>());
		List<Vector3> list2 = list;
		verts.AddRange(dfRadialSprite.baseVerts);
		int num;
		int index;
		switch (this.fillOrigin)
		{
		case dfPivotPoint.TopLeft:
			num = 4;
			index = 5;
			list2.RemoveAt(6);
			list2.RemoveAt(0);
			break;
		case dfPivotPoint.TopCenter:
			num = 6;
			index = 0;
			break;
		case dfPivotPoint.TopRight:
			num = 4;
			index = 0;
			list2.RemoveAt(2);
			list2.RemoveAt(0);
			break;
		case dfPivotPoint.MiddleLeft:
			num = 6;
			index = 6;
			break;
		case dfPivotPoint.MiddleCenter:
			num = 8;
			list2.Add(list2[0]);
			list2.Insert(0, Vector3.zero);
			index = 0;
			break;
		case dfPivotPoint.MiddleRight:
			num = 6;
			index = 2;
			break;
		case dfPivotPoint.BottomLeft:
			num = 4;
			index = 4;
			list2.RemoveAt(6);
			list2.RemoveAt(4);
			break;
		case dfPivotPoint.BottomCenter:
			num = 6;
			index = 4;
			break;
		case dfPivotPoint.BottomRight:
			num = 4;
			index = 2;
			list2.RemoveAt(4);
			list2.RemoveAt(2);
			break;
		default:
			throw new NotImplementedException();
		}
		this.makeFirst(list2, index);
		List<int> list3;
		indices = (list3 = this.buildTriangles(list2));
		List<int> list4 = list3;
		float num2 = 1f / (float)num;
		float num3 = this.fillAmount.Quantize(num2);
		for (int i = Mathf.CeilToInt(num3 / num2) + 1; i < num; i++)
		{
			if (this.invertFill)
			{
				list4.RemoveRange(0, 3);
			}
			else
			{
				list2.RemoveAt(list2.Count - 1);
				list4.RemoveRange(list4.Count - 3, 3);
			}
		}
		if (this.fillAmount < 1f)
		{
			int index2 = list4[this.invertFill ? 2 : (list4.Count - 2)];
			int index3 = list4[this.invertFill ? 1 : (list4.Count - 1)];
			float t = (base.FillAmount - num3) / num2;
			list2[index3] = Vector3.Lerp(list2[index2], list2[index3], t);
		}
		uv = this.buildUV(list2);
		float d = base.PixelsToUnits();
		Vector2 v = d * this.size;
		Vector3 b = this.pivot.TransformToCenter(this.size) * d;
		for (int j = 0; j < list2.Count; j++)
		{
			list2[j] = Vector3.Scale(list2[j], v) + b;
		}
	}

	// Token: 0x0600030E RID: 782 RVA: 0x0000E95C File Offset: 0x0000CB5C
	private void makeFirst(List<Vector3> list, int index)
	{
		if (index == 0)
		{
			return;
		}
		List<Vector3> range = list.GetRange(index, list.Count - index);
		list.RemoveRange(index, list.Count - index);
		list.InsertRange(0, range);
	}

	// Token: 0x0600030F RID: 783 RVA: 0x0000E994 File Offset: 0x0000CB94
	private List<int> buildTriangles(List<Vector3> verts)
	{
		List<int> list = new List<int>();
		int count = verts.Count;
		for (int i = 1; i < count - 1; i++)
		{
			list.Add(0);
			list.Add(i);
			list.Add(i + 1);
		}
		return list;
	}

	// Token: 0x06000310 RID: 784 RVA: 0x0000E9D4 File Offset: 0x0000CBD4
	private List<Vector2> buildUV(List<Vector3> verts)
	{
		dfAtlas.ItemInfo spriteInfo = base.SpriteInfo;
		if (spriteInfo == null)
		{
			return null;
		}
		Rect region = spriteInfo.region;
		if (this.flip.IsSet(dfSpriteFlip.FlipHorizontal))
		{
			region = new Rect(region.xMax, region.y, -region.width, region.height);
		}
		if (this.flip.IsSet(dfSpriteFlip.FlipVertical))
		{
			region = new Rect(region.x, region.yMax, region.width, -region.height);
		}
		Vector2 b = new Vector2(region.x, region.y);
		Vector2 b2 = new Vector2(0.5f, 0.5f);
		Vector2 b3 = new Vector2(region.width, region.height);
		List<Vector2> list = new List<Vector2>(verts.Count);
		for (int i = 0; i < verts.Count; i++)
		{
			Vector2 a = verts[i] + b2;
			list.Add(Vector2.Scale(a, b3) + b);
		}
		return list;
	}

	// Token: 0x06000311 RID: 785 RVA: 0x0000EAE8 File Offset: 0x0000CCE8
	private Color32[] buildColors(int vertCount)
	{
		Color32 color = base.ApplyOpacity(base.IsEnabled ? this.color : this.disabledColor);
		Color32[] array = new Color32[vertCount];
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = color;
		}
		return array;
	}

	// Token: 0x04000110 RID: 272
	private static Vector3[] baseVerts = new Vector3[]
	{
		new Vector3(0f, 0.5f, 0f),
		new Vector3(0.5f, 0.5f, 0f),
		new Vector3(0.5f, 0f, 0f),
		new Vector3(0.5f, -0.5f, 0f),
		new Vector3(0f, -0.5f, 0f),
		new Vector3(-0.5f, -0.5f, 0f),
		new Vector3(-0.5f, 0f, 0f),
		new Vector3(-0.5f, 0.5f, 0f)
	};

	// Token: 0x04000111 RID: 273
	[SerializeField]
	protected dfPivotPoint fillOrigin = dfPivotPoint.MiddleCenter;
}
