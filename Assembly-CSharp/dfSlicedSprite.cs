using System;
using UnityEngine;

// Token: 0x02000016 RID: 22
[dfCategory("Basic Controls")]
[dfTooltip("Displays a sprite from a Texture Atlas using 9-slice scaling")]
[dfHelp("http://www.daikonforge.com/docs/df-gui/classdf_sliced_sprite.html")]
[ExecuteInEditMode]
[AddComponentMenu("Daikon Forge/User Interface/Sprite/Sliced")]
[Serializable]
public class dfSlicedSprite : dfSprite
{
	// Token: 0x060003B4 RID: 948 RVA: 0x000124E4 File Offset: 0x000106E4
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
		if (spriteInfo.border.horizontal == 0 && spriteInfo.border.vertical == 0)
		{
			base.OnRebuildRenderData();
			return;
		}
		Color32 color = base.ApplyOpacity(base.IsEnabled ? this.color : this.disabledColor);
		dfSprite.RenderOptions options = new dfSprite.RenderOptions
		{
			atlas = this.atlas,
			color = color,
			fillAmount = this.fillAmount,
			fillDirection = this.fillDirection,
			flip = this.flip,
			invertFill = this.invertFill,
			offset = this.pivot.TransformToUpperLeft(base.Size),
			pixelsToUnits = base.PixelsToUnits(),
			size = base.Size,
			spriteInfo = base.SpriteInfo
		};
		dfSlicedSprite.renderSprite(this.renderData, options);
	}

	// Token: 0x060003B5 RID: 949 RVA: 0x00012604 File Offset: 0x00010804
	internal new static void renderSprite(dfRenderData renderData, dfSprite.RenderOptions options)
	{
		if (options.fillAmount <= 1E-45f)
		{
			return;
		}
		options.baseIndex = renderData.Vertices.Count;
		dfSlicedSprite.rebuildTriangles(renderData, options);
		dfSlicedSprite.rebuildVertices(renderData, options);
		dfSlicedSprite.rebuildUV(renderData, options);
		dfSlicedSprite.rebuildColors(renderData, options);
		if (options.fillAmount < 1f)
		{
			dfSlicedSprite.doFill(renderData, options);
		}
	}

	// Token: 0x060003B6 RID: 950 RVA: 0x00012664 File Offset: 0x00010864
	private static void rebuildTriangles(dfRenderData renderData, dfSprite.RenderOptions options)
	{
		int baseIndex = options.baseIndex;
		dfList<int> triangles = renderData.Triangles;
		for (int i = 0; i < dfSlicedSprite.triangleIndices.Length; i++)
		{
			triangles.Add(baseIndex + dfSlicedSprite.triangleIndices[i]);
		}
	}

	// Token: 0x060003B7 RID: 951 RVA: 0x000126A0 File Offset: 0x000108A0
	private static void doFill(dfRenderData renderData, dfSprite.RenderOptions options)
	{
		int baseIndex = options.baseIndex;
		dfList<Vector3> vertices = renderData.Vertices;
		dfList<Vector2> dfList = renderData.UV;
		int[][] array = dfSlicedSprite.getFillIndices(options.fillDirection, baseIndex);
		bool flag = options.invertFill;
		if (options.fillDirection == dfFillDirection.Vertical)
		{
			flag = !flag;
		}
		if (flag)
		{
			for (int i = 0; i < array.Length; i++)
			{
				Array.Reverse(array[i]);
			}
		}
		int index = (options.fillDirection == dfFillDirection.Horizontal) ? 0 : 1;
		float num = vertices[array[0][(!flag) ? 0 : 3]][index];
		float num2 = vertices[array[0][(!flag) ? 3 : 0]][index];
		float num3 = Mathf.Abs(num2 - num);
		float num4 = (!flag) ? (num + options.fillAmount * num3) : (num2 - options.fillAmount * num3);
		for (int j = 0; j < array.Length; j++)
		{
			if (!flag)
			{
				for (int k = 3; k > 0; k--)
				{
					float num5 = vertices[array[j][k]][index];
					if (num5 >= num4)
					{
						Vector3 value = vertices[array[j][k]];
						value[index] = num4;
						vertices[array[j][k]] = value;
						float num6 = vertices[array[j][k - 1]][index];
						if (num6 <= num4)
						{
							float num7 = num5 - num6;
							float t = (num4 - num6) / num7;
							float b = dfList[array[j][k]][index];
							float a = dfList[array[j][k - 1]][index];
							Vector2 value2 = dfList[array[j][k]];
							value2[index] = Mathf.Lerp(a, b, t);
							dfList[array[j][k]] = value2;
						}
					}
				}
			}
			else
			{
				for (int l = 1; l < 4; l++)
				{
					float num8 = vertices[array[j][l]][index];
					if (num8 <= num4)
					{
						Vector3 value3 = vertices[array[j][l]];
						value3[index] = num4;
						vertices[array[j][l]] = value3;
						float num9 = vertices[array[j][l - 1]][index];
						if (num9 >= num4)
						{
							float num10 = num8 - num9;
							float t2 = (num4 - num9) / num10;
							float b2 = dfList[array[j][l]][index];
							float a2 = dfList[array[j][l - 1]][index];
							Vector2 value4 = dfList[array[j][l]];
							value4[index] = Mathf.Lerp(a2, b2, t2);
							dfList[array[j][l]] = value4;
						}
					}
				}
			}
		}
	}

	// Token: 0x060003B8 RID: 952 RVA: 0x0001299C File Offset: 0x00010B9C
	private static int[][] getFillIndices(dfFillDirection fillDirection, int baseIndex)
	{
		int[][] array = (fillDirection == dfFillDirection.Horizontal) ? dfSlicedSprite.horzFill : dfSlicedSprite.vertFill;
		for (int i = 0; i < 4; i++)
		{
			for (int j = 0; j < 4; j++)
			{
				dfSlicedSprite.fillIndices[i][j] = baseIndex + array[i][j];
			}
		}
		return dfSlicedSprite.fillIndices;
	}

	// Token: 0x060003B9 RID: 953 RVA: 0x000129E8 File Offset: 0x00010BE8
	private static void rebuildVertices(dfRenderData renderData, dfSprite.RenderOptions options)
	{
		float x = 0f;
		float y = 0f;
		float num = Mathf.Ceil(options.size.x);
		float num2 = Mathf.Ceil(-options.size.y);
		dfAtlas.ItemInfo spriteInfo = options.spriteInfo;
		float num3 = (float)spriteInfo.border.left;
		float num4 = (float)spriteInfo.border.top;
		float num5 = (float)spriteInfo.border.right;
		float num6 = (float)spriteInfo.border.bottom;
		if (options.flip.IsSet(dfSpriteFlip.FlipHorizontal))
		{
			float num7 = num5;
			num5 = num3;
			num3 = num7;
		}
		if (options.flip.IsSet(dfSpriteFlip.FlipVertical))
		{
			float num8 = num6;
			num6 = num4;
			num4 = num8;
		}
		dfSlicedSprite.verts[0] = new Vector3(x, y, 0f) + options.offset;
		dfSlicedSprite.verts[1] = dfSlicedSprite.verts[0] + new Vector3(num3, 0f, 0f);
		dfSlicedSprite.verts[2] = dfSlicedSprite.verts[0] + new Vector3(num3, -num4, 0f);
		dfSlicedSprite.verts[3] = dfSlicedSprite.verts[0] + new Vector3(0f, -num4, 0f);
		dfSlicedSprite.verts[4] = new Vector3(num - num5, y, 0f) + options.offset;
		dfSlicedSprite.verts[5] = dfSlicedSprite.verts[4] + new Vector3(num5, 0f, 0f);
		dfSlicedSprite.verts[6] = dfSlicedSprite.verts[4] + new Vector3(num5, -num4, 0f);
		dfSlicedSprite.verts[7] = dfSlicedSprite.verts[4] + new Vector3(0f, -num4, 0f);
		dfSlicedSprite.verts[8] = new Vector3(x, num2 + num6, 0f) + options.offset;
		dfSlicedSprite.verts[9] = dfSlicedSprite.verts[8] + new Vector3(num3, 0f, 0f);
		dfSlicedSprite.verts[10] = dfSlicedSprite.verts[8] + new Vector3(num3, -num6, 0f);
		dfSlicedSprite.verts[11] = dfSlicedSprite.verts[8] + new Vector3(0f, -num6, 0f);
		dfSlicedSprite.verts[12] = new Vector3(num - num5, num2 + num6, 0f) + options.offset;
		dfSlicedSprite.verts[13] = dfSlicedSprite.verts[12] + new Vector3(num5, 0f, 0f);
		dfSlicedSprite.verts[14] = dfSlicedSprite.verts[12] + new Vector3(num5, -num6, 0f);
		dfSlicedSprite.verts[15] = dfSlicedSprite.verts[12] + new Vector3(0f, -num6, 0f);
		for (int i = 0; i < dfSlicedSprite.verts.Length; i++)
		{
			renderData.Vertices.Add((dfSlicedSprite.verts[i] * options.pixelsToUnits).Quantize(options.pixelsToUnits));
		}
	}

	// Token: 0x060003BA RID: 954 RVA: 0x00012D7C File Offset: 0x00010F7C
	private static void rebuildUV(dfRenderData renderData, dfSprite.RenderOptions options)
	{
		dfAtlas atlas = options.atlas;
		Vector2 vector = new Vector2((float)atlas.Texture.width, (float)atlas.Texture.height);
		dfAtlas.ItemInfo spriteInfo = options.spriteInfo;
		float num = (float)spriteInfo.border.top / vector.y;
		float num2 = (float)spriteInfo.border.bottom / vector.y;
		float num3 = (float)spriteInfo.border.left / vector.x;
		float num4 = (float)spriteInfo.border.right / vector.x;
		Rect region = spriteInfo.region;
		dfSlicedSprite.uv[0] = new Vector2(region.x, region.yMax);
		dfSlicedSprite.uv[1] = new Vector2(region.x + num3, region.yMax);
		dfSlicedSprite.uv[2] = new Vector2(region.x + num3, region.yMax - num);
		dfSlicedSprite.uv[3] = new Vector2(region.x, region.yMax - num);
		dfSlicedSprite.uv[4] = new Vector2(region.xMax - num4, region.yMax);
		dfSlicedSprite.uv[5] = new Vector2(region.xMax, region.yMax);
		dfSlicedSprite.uv[6] = new Vector2(region.xMax, region.yMax - num);
		dfSlicedSprite.uv[7] = new Vector2(region.xMax - num4, region.yMax - num);
		dfSlicedSprite.uv[8] = new Vector2(region.x, region.y + num2);
		dfSlicedSprite.uv[9] = new Vector2(region.x + num3, region.y + num2);
		dfSlicedSprite.uv[10] = new Vector2(region.x + num3, region.y);
		dfSlicedSprite.uv[11] = new Vector2(region.x, region.y);
		dfSlicedSprite.uv[12] = new Vector2(region.xMax - num4, region.y + num2);
		dfSlicedSprite.uv[13] = new Vector2(region.xMax, region.y + num2);
		dfSlicedSprite.uv[14] = new Vector2(region.xMax, region.y);
		dfSlicedSprite.uv[15] = new Vector2(region.xMax - num4, region.y);
		if (options.flip != dfSpriteFlip.None)
		{
			for (int i = 0; i < dfSlicedSprite.uv.Length; i += 4)
			{
				Vector2 vector2 = Vector2.zero;
				if (options.flip.IsSet(dfSpriteFlip.FlipHorizontal))
				{
					vector2 = dfSlicedSprite.uv[i];
					dfSlicedSprite.uv[i] = dfSlicedSprite.uv[i + 1];
					dfSlicedSprite.uv[i + 1] = vector2;
					vector2 = dfSlicedSprite.uv[i + 2];
					dfSlicedSprite.uv[i + 2] = dfSlicedSprite.uv[i + 3];
					dfSlicedSprite.uv[i + 3] = vector2;
				}
				if (options.flip.IsSet(dfSpriteFlip.FlipVertical))
				{
					vector2 = dfSlicedSprite.uv[i];
					dfSlicedSprite.uv[i] = dfSlicedSprite.uv[i + 3];
					dfSlicedSprite.uv[i + 3] = vector2;
					vector2 = dfSlicedSprite.uv[i + 1];
					dfSlicedSprite.uv[i + 1] = dfSlicedSprite.uv[i + 2];
					dfSlicedSprite.uv[i + 2] = vector2;
				}
			}
			if (options.flip.IsSet(dfSpriteFlip.FlipHorizontal))
			{
				Vector2[] array = new Vector2[dfSlicedSprite.uv.Length];
				Array.Copy(dfSlicedSprite.uv, array, dfSlicedSprite.uv.Length);
				Array.Copy(dfSlicedSprite.uv, 0, dfSlicedSprite.uv, 4, 4);
				Array.Copy(array, 4, dfSlicedSprite.uv, 0, 4);
				Array.Copy(dfSlicedSprite.uv, 8, dfSlicedSprite.uv, 12, 4);
				Array.Copy(array, 12, dfSlicedSprite.uv, 8, 4);
			}
			if (options.flip.IsSet(dfSpriteFlip.FlipVertical))
			{
				Vector2[] array2 = new Vector2[dfSlicedSprite.uv.Length];
				Array.Copy(dfSlicedSprite.uv, array2, dfSlicedSprite.uv.Length);
				Array.Copy(dfSlicedSprite.uv, 0, dfSlicedSprite.uv, 8, 4);
				Array.Copy(array2, 8, dfSlicedSprite.uv, 0, 4);
				Array.Copy(dfSlicedSprite.uv, 4, dfSlicedSprite.uv, 12, 4);
				Array.Copy(array2, 12, dfSlicedSprite.uv, 4, 4);
			}
		}
		for (int j = 0; j < dfSlicedSprite.uv.Length; j++)
		{
			renderData.UV.Add(dfSlicedSprite.uv[j]);
		}
	}

	// Token: 0x060003BB RID: 955 RVA: 0x00013264 File Offset: 0x00011464
	private static void rebuildColors(dfRenderData renderData, dfSprite.RenderOptions options)
	{
		for (int i = 0; i < 16; i++)
		{
			renderData.Colors.Add(options.color);
		}
	}

	// Token: 0x04000145 RID: 325
	private static int[] triangleIndices = new int[]
	{
		0,
		1,
		2,
		2,
		3,
		0,
		4,
		5,
		6,
		6,
		7,
		4,
		8,
		9,
		10,
		10,
		11,
		8,
		12,
		13,
		14,
		14,
		15,
		12,
		1,
		4,
		7,
		7,
		2,
		1,
		9,
		12,
		15,
		15,
		10,
		9,
		3,
		2,
		9,
		9,
		8,
		3,
		7,
		6,
		13,
		13,
		12,
		7,
		2,
		7,
		12,
		12,
		9,
		2
	};

	// Token: 0x04000146 RID: 326
	private static int[][] horzFill = new int[][]
	{
		new int[]
		{
			0,
			1,
			4,
			5
		},
		new int[]
		{
			3,
			2,
			7,
			6
		},
		new int[]
		{
			8,
			9,
			12,
			13
		},
		new int[]
		{
			11,
			10,
			15,
			14
		}
	};

	// Token: 0x04000147 RID: 327
	private static int[][] vertFill = new int[][]
	{
		new int[]
		{
			11,
			8,
			3,
			0
		},
		new int[]
		{
			10,
			9,
			2,
			1
		},
		new int[]
		{
			15,
			12,
			7,
			4
		},
		new int[]
		{
			14,
			13,
			6,
			5
		}
	};

	// Token: 0x04000148 RID: 328
	private static int[][] fillIndices = new int[][]
	{
		new int[4],
		new int[4],
		new int[4],
		new int[4]
	};

	// Token: 0x04000149 RID: 329
	private static Vector3[] verts = new Vector3[16];

	// Token: 0x0400014A RID: 330
	private static Vector2[] uv = new Vector2[16];
}
