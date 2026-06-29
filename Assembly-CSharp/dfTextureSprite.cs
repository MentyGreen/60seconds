using System;
using UnityEngine;

// Token: 0x0200001C RID: 28
[dfCategory("Basic Controls")]
[dfTooltip("Implements a Sprite that allows the user to use any Texture and Material they wish without having to use a Texture Atlas")]
[dfHelp("http://www.daikonforge.com/docs/df-gui/classdf_texture_sprite.html")]
[ExecuteInEditMode]
[AddComponentMenu("Daikon Forge/User Interface/Sprite/Texture")]
[Serializable]
public class dfTextureSprite : dfControl
{
	// Token: 0x1400003C RID: 60
	// (add) Token: 0x060004C4 RID: 1220 RVA: 0x00017F2C File Offset: 0x0001612C
	// (remove) Token: 0x060004C5 RID: 1221 RVA: 0x00017F64 File Offset: 0x00016164
	public event PropertyChangedEventHandler<Texture> TextureChanged;

	// Token: 0x17000118 RID: 280
	// (get) Token: 0x060004C6 RID: 1222 RVA: 0x00017F99 File Offset: 0x00016199
	// (set) Token: 0x060004C7 RID: 1223 RVA: 0x00017FA1 File Offset: 0x000161A1
	public bool CropTexture
	{
		get
		{
			return this.cropImage;
		}
		set
		{
			if (value != this.cropImage)
			{
				this.cropImage = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000119 RID: 281
	// (get) Token: 0x060004C8 RID: 1224 RVA: 0x00017FB9 File Offset: 0x000161B9
	// (set) Token: 0x060004C9 RID: 1225 RVA: 0x00017FC1 File Offset: 0x000161C1
	public Rect CropRect
	{
		get
		{
			return this.cropRect;
		}
		set
		{
			value = this.validateCropRect(value);
			if (value != this.cropRect)
			{
				this.cropRect = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x1700011A RID: 282
	// (get) Token: 0x060004CA RID: 1226 RVA: 0x00017FE7 File Offset: 0x000161E7
	// (set) Token: 0x060004CB RID: 1227 RVA: 0x00017FF0 File Offset: 0x000161F0
	public Texture Texture
	{
		get
		{
			return this.texture;
		}
		set
		{
			if (value != this.texture)
			{
				this.texture = value;
				this.Invalidate();
				if (value != null && this.size.sqrMagnitude <= 1E-45f)
				{
					this.size = new Vector2((float)value.width, (float)value.height);
				}
				this.OnTextureChanged(value);
			}
		}
	}

	// Token: 0x1700011B RID: 283
	// (get) Token: 0x060004CC RID: 1228 RVA: 0x00018053 File Offset: 0x00016253
	// (set) Token: 0x060004CD RID: 1229 RVA: 0x0001805B File Offset: 0x0001625B
	public Material Material
	{
		get
		{
			return this.material;
		}
		set
		{
			if (value != this.material)
			{
				this.disposeCreatedMaterial();
				this.renderMaterial = null;
				this.material = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x1700011C RID: 284
	// (get) Token: 0x060004CE RID: 1230 RVA: 0x00018085 File Offset: 0x00016285
	// (set) Token: 0x060004CF RID: 1231 RVA: 0x0001808D File Offset: 0x0001628D
	public dfSpriteFlip Flip
	{
		get
		{
			return this.flip;
		}
		set
		{
			if (value != this.flip)
			{
				this.flip = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x1700011D RID: 285
	// (get) Token: 0x060004D0 RID: 1232 RVA: 0x000180A5 File Offset: 0x000162A5
	// (set) Token: 0x060004D1 RID: 1233 RVA: 0x000180AD File Offset: 0x000162AD
	public dfFillDirection FillDirection
	{
		get
		{
			return this.fillDirection;
		}
		set
		{
			if (value != this.fillDirection)
			{
				this.fillDirection = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x1700011E RID: 286
	// (get) Token: 0x060004D2 RID: 1234 RVA: 0x000180C5 File Offset: 0x000162C5
	// (set) Token: 0x060004D3 RID: 1235 RVA: 0x000180CD File Offset: 0x000162CD
	public float FillAmount
	{
		get
		{
			return this.fillAmount;
		}
		set
		{
			if (!Mathf.Approximately(value, this.fillAmount))
			{
				this.fillAmount = Mathf.Max(0f, Mathf.Min(1f, value));
				this.Invalidate();
			}
		}
	}

	// Token: 0x1700011F RID: 287
	// (get) Token: 0x060004D4 RID: 1236 RVA: 0x000180FE File Offset: 0x000162FE
	// (set) Token: 0x060004D5 RID: 1237 RVA: 0x00018106 File Offset: 0x00016306
	public bool InvertFill
	{
		get
		{
			return this.invertFill;
		}
		set
		{
			if (value != this.invertFill)
			{
				this.invertFill = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000120 RID: 288
	// (get) Token: 0x060004D6 RID: 1238 RVA: 0x0001811E File Offset: 0x0001631E
	public Material RenderMaterial
	{
		get
		{
			return this.renderMaterial;
		}
	}

	// Token: 0x060004D7 RID: 1239 RVA: 0x00018126 File Offset: 0x00016326
	public override void OnEnable()
	{
		base.OnEnable();
		this.renderMaterial = null;
	}

	// Token: 0x060004D8 RID: 1240 RVA: 0x00018135 File Offset: 0x00016335
	public override void OnDestroy()
	{
		this.disposeCreatedMaterial();
		base.OnDestroy();
		if (this.renderMaterial != null)
		{
			Object.DestroyImmediate(this.renderMaterial);
			this.renderMaterial = null;
		}
	}

	// Token: 0x060004D9 RID: 1241 RVA: 0x00018163 File Offset: 0x00016363
	public override void OnDisable()
	{
		base.OnDisable();
		if (Application.isPlaying && this.renderMaterial != null)
		{
			this.disposeCreatedMaterial();
			Object.DestroyImmediate(this.renderMaterial);
			this.renderMaterial = null;
		}
	}

	// Token: 0x060004DA RID: 1242 RVA: 0x00018198 File Offset: 0x00016398
	protected override void OnRebuildRenderData()
	{
		base.OnRebuildRenderData();
		if (this.texture == null)
		{
			return;
		}
		this.ensureMaterial();
		if (this.material == null)
		{
			return;
		}
		if (this.renderMaterial == null)
		{
			this.renderMaterial = new Material(this.material)
			{
				hideFlags = HideFlags.DontSave,
				name = this.material.name + " (copy)"
			};
		}
		this.renderMaterial.mainTexture = this.texture;
		this.renderData.Material = this.renderMaterial;
		float num = base.PixelsToUnits();
		float x = 0f;
		float y = 0f;
		float x2 = this.size.x * num;
		float y2 = -this.size.y * num;
		Vector3 b = this.pivot.TransformToUpperLeft(this.size).RoundToInt() * num;
		this.renderData.Vertices.Add(new Vector3(x, y, 0f) + b);
		this.renderData.Vertices.Add(new Vector3(x2, y, 0f) + b);
		this.renderData.Vertices.Add(new Vector3(x2, y2, 0f) + b);
		this.renderData.Vertices.Add(new Vector3(x, y2, 0f) + b);
		this.renderData.Triangles.AddRange(dfTextureSprite.TRIANGLE_INDICES);
		this.rebuildUV(this.renderData);
		Color32 item = base.ApplyOpacity(this.color);
		this.renderData.Colors.Add(item);
		this.renderData.Colors.Add(item);
		this.renderData.Colors.Add(item);
		this.renderData.Colors.Add(item);
		if (this.fillAmount < 1f)
		{
			this.doFill(this.renderData);
		}
	}

	// Token: 0x060004DB RID: 1243 RVA: 0x000183A0 File Offset: 0x000165A0
	protected virtual void disposeCreatedMaterial()
	{
		if (this.createdRuntimeMaterial)
		{
			Object.DestroyImmediate(this.material);
			this.material = null;
			this.createdRuntimeMaterial = false;
		}
	}

	// Token: 0x060004DC RID: 1244 RVA: 0x000183C4 File Offset: 0x000165C4
	protected virtual void rebuildUV(dfRenderData renderBuffer)
	{
		dfList<Vector2> uv = renderBuffer.UV;
		if (this.cropImage)
		{
			int width = this.texture.width;
			int height = this.texture.height;
			float num = Mathf.Max(0f, Mathf.Min(this.cropRect.x, (float)width));
			float num2 = Mathf.Max(0f, Mathf.Min(this.cropRect.xMax, (float)width));
			float num3 = Mathf.Max(0f, Mathf.Min(this.cropRect.y, (float)height));
			float num4 = Mathf.Max(0f, Mathf.Min(this.cropRect.yMax, (float)height));
			uv.Add(new Vector2(num / (float)width, num4 / (float)height));
			uv.Add(new Vector2(num2 / (float)width, num4 / (float)height));
			uv.Add(new Vector2(num2 / (float)width, num3 / (float)height));
			uv.Add(new Vector2(num / (float)width, num3 / (float)height));
		}
		else
		{
			uv.Add(new Vector2(0f, 1f));
			uv.Add(new Vector2(1f, 1f));
			uv.Add(new Vector2(1f, 0f));
			uv.Add(new Vector2(0f, 0f));
		}
		Vector2 value = Vector2.zero;
		if (this.flip.IsSet(dfSpriteFlip.FlipHorizontal))
		{
			value = uv[1];
			uv[1] = uv[0];
			uv[0] = value;
			value = uv[3];
			uv[3] = uv[2];
			uv[2] = value;
		}
		if (this.flip.IsSet(dfSpriteFlip.FlipVertical))
		{
			value = uv[0];
			uv[0] = uv[3];
			uv[3] = value;
			value = uv[1];
			uv[1] = uv[2];
			uv[2] = value;
		}
	}

	// Token: 0x060004DD RID: 1245 RVA: 0x000185B8 File Offset: 0x000167B8
	protected virtual void doFill(dfRenderData renderData)
	{
		dfList<Vector3> vertices = renderData.Vertices;
		dfList<Vector2> uv = renderData.UV;
		int index = 0;
		int index2 = 1;
		int index3 = 3;
		int index4 = 2;
		if (this.invertFill)
		{
			if (this.fillDirection == dfFillDirection.Horizontal)
			{
				index = 1;
				index2 = 0;
				index3 = 2;
				index4 = 3;
			}
			else
			{
				index = 3;
				index2 = 2;
				index3 = 0;
				index4 = 1;
			}
		}
		if (this.fillDirection == dfFillDirection.Horizontal)
		{
			vertices[index2] = Vector3.Lerp(vertices[index2], vertices[index], 1f - this.fillAmount);
			vertices[index4] = Vector3.Lerp(vertices[index4], vertices[index3], 1f - this.fillAmount);
			uv[index2] = Vector2.Lerp(uv[index2], uv[index], 1f - this.fillAmount);
			uv[index4] = Vector2.Lerp(uv[index4], uv[index3], 1f - this.fillAmount);
			return;
		}
		vertices[index3] = Vector3.Lerp(vertices[index3], vertices[index], 1f - this.fillAmount);
		vertices[index4] = Vector3.Lerp(vertices[index4], vertices[index2], 1f - this.fillAmount);
		uv[index3] = Vector2.Lerp(uv[index3], uv[index], 1f - this.fillAmount);
		uv[index4] = Vector2.Lerp(uv[index4], uv[index2], 1f - this.fillAmount);
	}

	// Token: 0x060004DE RID: 1246 RVA: 0x00018750 File Offset: 0x00016950
	private Rect validateCropRect(Rect rect)
	{
		if (this.texture == null)
		{
			return default(Rect);
		}
		int width = this.texture.width;
		int height = this.texture.height;
		float x = Mathf.Max(0f, Mathf.Min(rect.x, (float)width));
		float y = Mathf.Max(0f, Mathf.Min(rect.y, (float)height));
		float width2 = Mathf.Max(0f, Mathf.Min(rect.width, (float)width));
		float height2 = Mathf.Max(0f, Mathf.Min(rect.height, (float)height));
		return new Rect(x, y, width2, height2);
	}

	// Token: 0x060004DF RID: 1247 RVA: 0x000187FF File Offset: 0x000169FF
	protected internal virtual void OnTextureChanged(Texture value)
	{
		base.SignalHierarchy("OnTextureChanged", new object[]
		{
			this,
			value
		});
		if (this.TextureChanged != null)
		{
			this.TextureChanged(this, value);
		}
	}

	// Token: 0x060004E0 RID: 1248 RVA: 0x00018830 File Offset: 0x00016A30
	private void ensureMaterial()
	{
		if (this.material != null || this.texture == null)
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
			mainTexture = this.texture
		};
		this.createdRuntimeMaterial = true;
	}

	// Token: 0x0400019D RID: 413
	private static int[] TRIANGLE_INDICES = new int[]
	{
		0,
		1,
		3,
		3,
		1,
		2
	};

	// Token: 0x0400019F RID: 415
	[SerializeField]
	protected Texture texture;

	// Token: 0x040001A0 RID: 416
	[SerializeField]
	protected Material material;

	// Token: 0x040001A1 RID: 417
	[SerializeField]
	protected dfSpriteFlip flip;

	// Token: 0x040001A2 RID: 418
	[SerializeField]
	protected dfFillDirection fillDirection;

	// Token: 0x040001A3 RID: 419
	[SerializeField]
	protected float fillAmount = 1f;

	// Token: 0x040001A4 RID: 420
	[SerializeField]
	protected bool invertFill;

	// Token: 0x040001A5 RID: 421
	[SerializeField]
	protected Rect cropRect = new Rect(0f, 0f, 1f, 1f);

	// Token: 0x040001A6 RID: 422
	[SerializeField]
	protected bool cropImage;

	// Token: 0x040001A7 RID: 423
	private bool createdRuntimeMaterial;

	// Token: 0x040001A8 RID: 424
	private Material renderMaterial;
}
