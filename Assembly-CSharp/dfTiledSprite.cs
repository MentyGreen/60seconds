using System;
using UnityEngine;

// Token: 0x0200001D RID: 29
[dfCategory("Basic Controls")]
[dfTooltip("Implements a Sprite that can be tiled horizontally and vertically")]
[dfHelp("http://www.daikonforge.com/docs/df-gui/classdf_tiled_sprite.html")]
[ExecuteInEditMode]
[AddComponentMenu("Daikon Forge/User Interface/Sprite/Tiled")]
[Serializable]
public class dfTiledSprite : dfSprite
{
	// Token: 0x17000121 RID: 289
	// (get) Token: 0x060004E3 RID: 1251 RVA: 0x000188F5 File Offset: 0x00016AF5
	// (set) Token: 0x060004E4 RID: 1252 RVA: 0x000188FD File Offset: 0x00016AFD
	public Vector2 TileScale
	{
		get
		{
			return this.tileScale;
		}
		set
		{
			if (Vector2.Distance(value, this.tileScale) > 1E-45f)
			{
				this.tileScale = Vector2.Max(Vector2.one * 0.1f, value);
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000122 RID: 290
	// (get) Token: 0x060004E5 RID: 1253 RVA: 0x00018933 File Offset: 0x00016B33
	// (set) Token: 0x060004E6 RID: 1254 RVA: 0x0001893B File Offset: 0x00016B3B
	public Vector2 TileScroll
	{
		get
		{
			return this.tileScroll;
		}
		set
		{
			if (Vector2.Distance(value, this.tileScroll) > 1E-45f)
			{
				this.tileScroll = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x060004E7 RID: 1255 RVA: 0x00018960 File Offset: 0x00016B60
	protected override void OnRebuildRenderData()
	{
		if (base.Atlas == null)
		{
			return;
		}
		dfAtlas.ItemInfo spriteInfo = base.SpriteInfo;
		if (spriteInfo == null)
		{
			return;
		}
		this.renderData.Material = base.Atlas.Material;
		dfList<Vector3> vertices = this.renderData.Vertices;
		dfList<Vector2> uv = this.renderData.UV;
		dfList<Color32> colors = this.renderData.Colors;
		dfList<int> triangles = this.renderData.Triangles;
		Vector2[] spriteUV = this.buildQuadUV();
		Vector2 vector = Vector2.Scale(spriteInfo.sizeInPixels, this.tileScale);
		Vector2 vector2 = new Vector2(this.tileScroll.x % 1f, this.tileScroll.y % 1f);
		for (float num = -Mathf.Abs(vector2.y * vector.y); num < this.size.y; num += vector.y)
		{
			for (float num2 = -Mathf.Abs(vector2.x * vector.x); num2 < this.size.x; num2 += vector.x)
			{
				int count = vertices.Count;
				vertices.Add(new Vector3(num2, -num));
				vertices.Add(new Vector3(num2 + vector.x, -num));
				vertices.Add(new Vector3(num2 + vector.x, -num + -vector.y));
				vertices.Add(new Vector3(num2, -num + -vector.y));
				this.addQuadTriangles(triangles, count);
				this.addQuadUV(uv, spriteUV);
				this.addQuadColors(colors);
			}
		}
		this.clipQuads(vertices, uv);
		float d = base.PixelsToUnits();
		Vector3 b = this.pivot.TransformToUpperLeft(this.size);
		for (int i = 0; i < vertices.Count; i++)
		{
			vertices[i] = (vertices[i] + b) * d;
		}
	}

	// Token: 0x060004E8 RID: 1256 RVA: 0x00018B68 File Offset: 0x00016D68
	private void clipQuads(dfList<Vector3> verts, dfList<Vector2> uv)
	{
		float num = 0f;
		float num2 = this.size.x;
		float num3 = -this.size.y;
		float num4 = 0f;
		if (this.fillAmount < 1f)
		{
			if (this.fillDirection == dfFillDirection.Horizontal)
			{
				if (!this.invertFill)
				{
					num2 = this.size.x * this.fillAmount;
				}
				else
				{
					num = this.size.x - this.size.x * this.fillAmount;
				}
			}
			else if (!this.invertFill)
			{
				num3 = -this.size.y * this.fillAmount;
			}
			else
			{
				num4 = -this.size.y * (1f - this.fillAmount);
			}
		}
		for (int i = 0; i < verts.Count; i += 4)
		{
			Vector3 vector = verts[i];
			Vector3 vector2 = verts[i + 1];
			Vector3 vector3 = verts[i + 2];
			Vector3 vector4 = verts[i + 3];
			float num5 = vector2.x - vector.x;
			float num6 = vector.y - vector4.y;
			if (vector.x < num)
			{
				float t = (num - vector.x) / num5;
				int index = i;
				vector = new Vector3(Mathf.Max(num, vector.x), vector.y, vector.z);
				verts[index] = vector;
				int index2 = i + 1;
				vector2 = new Vector3(Mathf.Max(num, vector2.x), vector2.y, vector2.z);
				verts[index2] = vector2;
				int index3 = i + 2;
				vector3 = new Vector3(Mathf.Max(num, vector3.x), vector3.y, vector3.z);
				verts[index3] = vector3;
				int index4 = i + 3;
				vector4 = new Vector3(Mathf.Max(num, vector4.x), vector4.y, vector4.z);
				verts[index4] = vector4;
				float x = Mathf.Lerp(uv[i].x, uv[i + 1].x, t);
				uv[i] = new Vector2(x, uv[i].y);
				uv[i + 3] = new Vector2(x, uv[i + 3].y);
				num5 = vector2.x - vector.x;
			}
			if (vector2.x > num2)
			{
				float t2 = 1f - (num2 - vector2.x + num5) / num5;
				int index5 = i;
				vector = new Vector3(Mathf.Min(vector.x, num2), vector.y, vector.z);
				verts[index5] = vector;
				int index6 = i + 1;
				vector2 = new Vector3(Mathf.Min(vector2.x, num2), vector2.y, vector2.z);
				verts[index6] = vector2;
				int index7 = i + 2;
				vector3 = new Vector3(Mathf.Min(vector3.x, num2), vector3.y, vector3.z);
				verts[index7] = vector3;
				int index8 = i + 3;
				vector4 = new Vector3(Mathf.Min(vector4.x, num2), vector4.y, vector4.z);
				verts[index8] = vector4;
				float x2 = Mathf.Lerp(uv[i + 1].x, uv[i].x, t2);
				uv[i + 1] = new Vector2(x2, uv[i + 1].y);
				uv[i + 2] = new Vector2(x2, uv[i + 2].y);
				num5 = vector2.x - vector.x;
			}
			if (vector4.y < num3)
			{
				float t3 = 1f - Mathf.Abs(-num3 + vector.y) / num6;
				int index9 = i;
				vector = new Vector3(vector.x, Mathf.Max(vector.y, num3), vector2.z);
				verts[index9] = vector;
				int index10 = i + 1;
				vector2 = new Vector3(vector2.x, Mathf.Max(vector2.y, num3), vector2.z);
				verts[index10] = vector2;
				int index11 = i + 2;
				vector3 = new Vector3(vector3.x, Mathf.Max(vector3.y, num3), vector3.z);
				verts[index11] = vector3;
				int index12 = i + 3;
				vector4 = new Vector3(vector4.x, Mathf.Max(vector4.y, num3), vector4.z);
				verts[index12] = vector4;
				float y = Mathf.Lerp(uv[i + 3].y, uv[i].y, t3);
				uv[i + 3] = new Vector2(uv[i + 3].x, y);
				uv[i + 2] = new Vector2(uv[i + 2].x, y);
				num6 = Mathf.Abs(vector4.y - vector.y);
			}
			if (vector.y > num4)
			{
				float t4 = Mathf.Abs(num4 - vector.y) / num6;
				int index13 = i;
				vector = new Vector3(vector.x, Mathf.Min(num4, vector.y), vector.z);
				verts[index13] = vector;
				int index14 = i + 1;
				vector2 = new Vector3(vector2.x, Mathf.Min(num4, vector2.y), vector2.z);
				verts[index14] = vector2;
				int index15 = i + 2;
				vector3 = new Vector3(vector3.x, Mathf.Min(num4, vector3.y), vector3.z);
				verts[index15] = vector3;
				int index16 = i + 3;
				vector4 = new Vector3(vector4.x, Mathf.Min(num4, vector4.y), vector4.z);
				verts[index16] = vector4;
				float y2 = Mathf.Lerp(uv[i].y, uv[i + 3].y, t4);
				uv[i] = new Vector2(uv[i].x, y2);
				uv[i + 1] = new Vector2(uv[i + 1].x, y2);
			}
		}
	}

	// Token: 0x060004E9 RID: 1257 RVA: 0x000191B4 File Offset: 0x000173B4
	private void addQuadTriangles(dfList<int> triangles, int baseIndex)
	{
		for (int i = 0; i < dfTiledSprite.quadTriangles.Length; i++)
		{
			triangles.Add(dfTiledSprite.quadTriangles[i] + baseIndex);
		}
	}

	// Token: 0x060004EA RID: 1258 RVA: 0x000191E4 File Offset: 0x000173E4
	private void addQuadColors(dfList<Color32> colors)
	{
		colors.EnsureCapacity(colors.Count + 4);
		Color32 item = base.ApplyOpacity(base.IsEnabled ? this.color : this.disabledColor);
		for (int i = 0; i < 4; i++)
		{
			colors.Add(item);
		}
	}

	// Token: 0x060004EB RID: 1259 RVA: 0x0001922F File Offset: 0x0001742F
	private void addQuadUV(dfList<Vector2> uv, Vector2[] spriteUV)
	{
		uv.AddRange(spriteUV);
	}

	// Token: 0x060004EC RID: 1260 RVA: 0x00019238 File Offset: 0x00017438
	private Vector2[] buildQuadUV()
	{
		Rect region = base.SpriteInfo.region;
		dfTiledSprite.quadUV[0] = new Vector2(region.x, region.yMax);
		dfTiledSprite.quadUV[1] = new Vector2(region.xMax, region.yMax);
		dfTiledSprite.quadUV[2] = new Vector2(region.xMax, region.y);
		dfTiledSprite.quadUV[3] = new Vector2(region.x, region.y);
		Vector2 vector = Vector2.zero;
		if (this.flip.IsSet(dfSpriteFlip.FlipHorizontal))
		{
			vector = dfTiledSprite.quadUV[1];
			dfTiledSprite.quadUV[1] = dfTiledSprite.quadUV[0];
			dfTiledSprite.quadUV[0] = vector;
			vector = dfTiledSprite.quadUV[3];
			dfTiledSprite.quadUV[3] = dfTiledSprite.quadUV[2];
			dfTiledSprite.quadUV[2] = vector;
		}
		if (this.flip.IsSet(dfSpriteFlip.FlipVertical))
		{
			vector = dfTiledSprite.quadUV[0];
			dfTiledSprite.quadUV[0] = dfTiledSprite.quadUV[3];
			dfTiledSprite.quadUV[3] = vector;
			vector = dfTiledSprite.quadUV[1];
			dfTiledSprite.quadUV[1] = dfTiledSprite.quadUV[2];
			dfTiledSprite.quadUV[2] = vector;
		}
		return dfTiledSprite.quadUV;
	}

	// Token: 0x040001A9 RID: 425
	private static int[] quadTriangles = new int[]
	{
		0,
		1,
		3,
		3,
		1,
		2
	};

	// Token: 0x040001AA RID: 426
	private static Vector2[] quadUV = new Vector2[4];

	// Token: 0x040001AB RID: 427
	[SerializeField]
	protected Vector2 tileScale = Vector2.one;

	// Token: 0x040001AC RID: 428
	[SerializeField]
	protected Vector2 tileScroll = Vector2.zero;
}
