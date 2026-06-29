using System;
using UnityEngine;

// Token: 0x02000082 RID: 130
public class dfMarkupBoxTexture : dfMarkupBox
{
	// Token: 0x170001E8 RID: 488
	// (get) Token: 0x0600086A RID: 2154 RVA: 0x00025443 File Offset: 0x00023643
	// (set) Token: 0x0600086B RID: 2155 RVA: 0x0002544B File Offset: 0x0002364B
	public Texture Texture { get; set; }

	// Token: 0x0600086C RID: 2156 RVA: 0x00025454 File Offset: 0x00023654
	public dfMarkupBoxTexture(dfMarkupElement element, dfMarkupDisplayType display, dfMarkupStyle style) : base(element, display, style)
	{
	}

	// Token: 0x0600086D RID: 2157 RVA: 0x0002546C File Offset: 0x0002366C
	internal void LoadTexture(Texture texture)
	{
		if (texture == null)
		{
			throw new InvalidOperationException();
		}
		this.Texture = texture;
		this.Size = new Vector2((float)texture.width, (float)texture.height);
		this.Baseline = (int)this.Size.y;
	}

	// Token: 0x0600086E RID: 2158 RVA: 0x000254BC File Offset: 0x000236BC
	protected override dfRenderData OnRebuildRenderData()
	{
		this.renderData.Clear();
		this.ensureMaterial();
		this.renderData.Material = this.material;
		this.renderData.Material.mainTexture = this.Texture;
		Vector3 zero = Vector3.zero;
		Vector3 vector = zero + Vector3.right * this.Size.x;
		Vector3 item = vector + Vector3.down * this.Size.y;
		Vector3 item2 = zero + Vector3.down * this.Size.y;
		this.renderData.Vertices.Add(zero);
		this.renderData.Vertices.Add(vector);
		this.renderData.Vertices.Add(item);
		this.renderData.Vertices.Add(item2);
		this.renderData.Triangles.AddRange(dfMarkupBoxTexture.TRIANGLE_INDICES);
		this.renderData.UV.Add(new Vector2(0f, 1f));
		this.renderData.UV.Add(new Vector2(1f, 1f));
		this.renderData.UV.Add(new Vector2(1f, 0f));
		this.renderData.UV.Add(new Vector2(0f, 0f));
		Color color = this.Style.Color;
		this.renderData.Colors.Add(color);
		this.renderData.Colors.Add(color);
		this.renderData.Colors.Add(color);
		this.renderData.Colors.Add(color);
		return this.renderData;
	}

	// Token: 0x0600086F RID: 2159 RVA: 0x000256A0 File Offset: 0x000238A0
	private void ensureMaterial()
	{
		if (this.material != null || this.Texture == null)
		{
			return;
		}
		Shader shader = Shader.Find("Daikon Forge/Default UI Shader");
		if (shader == null)
		{
			Debug.LogError("Failed to find default shader");
			return;
		}
		this.material = new Material(shader)
		{
			name = "Default Texture Shader",
			hideFlags = HideFlags.DontSave,
			mainTexture = this.Texture
		};
	}

	// Token: 0x06000870 RID: 2160 RVA: 0x00025714 File Offset: 0x00023914
	private static void addTriangleIndices(dfList<Vector3> verts, dfList<int> triangles)
	{
		int count = verts.Count;
		int[] triangle_INDICES = dfMarkupBoxTexture.TRIANGLE_INDICES;
		for (int i = 0; i < triangle_INDICES.Length; i++)
		{
			triangles.Add(count + triangle_INDICES[i]);
		}
	}

	// Token: 0x04000409 RID: 1033
	private static int[] TRIANGLE_INDICES = new int[]
	{
		0,
		1,
		2,
		0,
		2,
		3
	};

	// Token: 0x0400040B RID: 1035
	private dfRenderData renderData = new dfRenderData();

	// Token: 0x0400040C RID: 1036
	private Material material;
}
