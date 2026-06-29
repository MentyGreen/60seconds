using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Serialization;

namespace HighlightingSystem
{
	// Token: 0x0200017C RID: 380
	[DisallowMultipleComponent]
	[RequireComponent(typeof(Camera))]
	public class HighlightingBase : MonoBehaviour
	{
		// Token: 0x17000353 RID: 851
		// (get) Token: 0x060010CC RID: 4300 RVA: 0x00046E2A File Offset: 0x0004502A
		protected static bool uvStartsAtTop
		{
			get
			{
				return HighlightingBase.device == GraphicsDeviceType.Direct3D9 || HighlightingBase.device == GraphicsDeviceType.Xbox360 || HighlightingBase.device == GraphicsDeviceType.PlayStation3 || HighlightingBase.device == GraphicsDeviceType.Direct3D11 || HighlightingBase.device == GraphicsDeviceType.PlayStationVita || HighlightingBase.device == GraphicsDeviceType.PlayStation4 || HighlightingBase.device == GraphicsDeviceType.Metal;
			}
		}

		// Token: 0x17000354 RID: 852
		// (get) Token: 0x060010CD RID: 4301 RVA: 0x00046E69 File Offset: 0x00045069
		public bool isSupported
		{
			get
			{
				return this.CheckSupported(false);
			}
		}

		// Token: 0x17000355 RID: 853
		// (get) Token: 0x060010CE RID: 4302 RVA: 0x00046E72 File Offset: 0x00045072
		// (set) Token: 0x060010CF RID: 4303 RVA: 0x00046E7A File Offset: 0x0004507A
		public int downsampleFactor
		{
			get
			{
				return this._downsampleFactor;
			}
			set
			{
				if (this._downsampleFactor != value)
				{
					if (value != 0 && (value & value - 1) == 0)
					{
						this._downsampleFactor = value;
						return;
					}
					Debug.LogWarning("HighlightingSystem : Prevented attempt to set incorrect downsample factor value.");
				}
			}
		}

		// Token: 0x17000356 RID: 854
		// (get) Token: 0x060010D0 RID: 4304 RVA: 0x00046EA1 File Offset: 0x000450A1
		// (set) Token: 0x060010D1 RID: 4305 RVA: 0x00046EA9 File Offset: 0x000450A9
		public int iterations
		{
			get
			{
				return this._iterations;
			}
			set
			{
				if (this._iterations != value)
				{
					this._iterations = value;
				}
			}
		}

		// Token: 0x17000357 RID: 855
		// (get) Token: 0x060010D2 RID: 4306 RVA: 0x00046EBB File Offset: 0x000450BB
		// (set) Token: 0x060010D3 RID: 4307 RVA: 0x00046EC3 File Offset: 0x000450C3
		public float blurMinSpread
		{
			get
			{
				return this._blurMinSpread;
			}
			set
			{
				if (this._blurMinSpread != value)
				{
					this._blurMinSpread = value;
				}
			}
		}

		// Token: 0x17000358 RID: 856
		// (get) Token: 0x060010D4 RID: 4308 RVA: 0x00046ED5 File Offset: 0x000450D5
		// (set) Token: 0x060010D5 RID: 4309 RVA: 0x00046EDD File Offset: 0x000450DD
		public float blurSpread
		{
			get
			{
				return this._blurSpread;
			}
			set
			{
				if (this._blurSpread != value)
				{
					this._blurSpread = value;
				}
			}
		}

		// Token: 0x17000359 RID: 857
		// (get) Token: 0x060010D6 RID: 4310 RVA: 0x00046EEF File Offset: 0x000450EF
		// (set) Token: 0x060010D7 RID: 4311 RVA: 0x00046EF7 File Offset: 0x000450F7
		public float blurIntensity
		{
			get
			{
				return this._blurIntensity;
			}
			set
			{
				if (this._blurIntensity != value)
				{
					this._blurIntensity = value;
					if (Application.isPlaying)
					{
						this.blurMaterial.SetFloat(ShaderPropertyID._Intensity, this._blurIntensity);
					}
				}
			}
		}

		// Token: 0x1700035A RID: 858
		// (get) Token: 0x060010D8 RID: 4312 RVA: 0x00046F26 File Offset: 0x00045126
		// (set) Token: 0x060010D9 RID: 4313 RVA: 0x00046F2E File Offset: 0x0004512E
		public HighlightingBlitter blitter
		{
			get
			{
				return this._blitter;
			}
			set
			{
				if (this._blitter != null)
				{
					this._blitter.Unregister(this);
				}
				this._blitter = value;
				if (this._blitter != null)
				{
					this._blitter.Register(this);
				}
			}
		}

		// Token: 0x060010DA RID: 4314 RVA: 0x00046F6C File Offset: 0x0004516C
		protected virtual void OnEnable()
		{
			HighlightingBase.Initialize();
			if (!this.CheckSupported(true))
			{
				base.enabled = false;
				Debug.LogError("HighlightingSystem : Highlighting System has been disabled due to unsupported Unity features on the current platform!");
				return;
			}
			this.blurMaterial = new Material(HighlightingBase.materials[0]);
			this.cutMaterial = new Material(HighlightingBase.materials[1]);
			this.compMaterial = new Material(HighlightingBase.materials[2]);
			this.blurMaterial.SetFloat(ShaderPropertyID._Intensity, this._blurIntensity);
			this.renderBuffer = new CommandBuffer();
			this.renderBuffer.name = HighlightingBase.renderBufferName;
			this.cam = base.GetComponent<Camera>();
			HighlightingBase.cameras.Add(this.cam);
			this.cam.AddCommandBuffer(CameraEvent.BeforeImageEffectsOpaque, this.renderBuffer);
			if (this._blitter != null)
			{
				this._blitter.Register(this);
			}
		}

		// Token: 0x060010DB RID: 4315 RVA: 0x0004704C File Offset: 0x0004524C
		protected virtual void OnDisable()
		{
			HighlightingBase.cameras.Remove(this.cam);
			if (this.renderBuffer != null)
			{
				this.cam.RemoveCommandBuffer(CameraEvent.BeforeImageEffectsOpaque, this.renderBuffer);
				this.renderBuffer = null;
			}
			if (this.highlightingBuffer != null && this.highlightingBuffer.IsCreated())
			{
				this.highlightingBuffer.Release();
				this.highlightingBuffer = null;
			}
			if (this._blitter != null)
			{
				this._blitter.Unregister(this);
			}
		}

		// Token: 0x060010DC RID: 4316 RVA: 0x000470D4 File Offset: 0x000452D4
		protected virtual void OnPreRender()
		{
			bool flag = false;
			int aa = this.GetAA();
			bool flag2 = aa == 1;
			if (this.cam.actualRenderingPath == RenderingPath.Forward || this.cam.actualRenderingPath == RenderingPath.VertexLit)
			{
				if (aa > 1)
				{
					flag2 = false;
				}
				if (this.cam.clearFlags == CameraClearFlags.Depth || this.cam.clearFlags == CameraClearFlags.Nothing)
				{
					flag2 = false;
				}
			}
			if (this.isDepthAvailable != flag2)
			{
				flag = true;
				this.isDepthAvailable = flag2;
				Highlighter.SetZWrite(this.isDepthAvailable ? 0 : 1);
				if (this.isDepthAvailable)
				{
					Debug.LogWarning("HighlightingSystem : Framebuffer depth data is available back again. Depth occlusion enabled, highlighting occluders disabled. (This message is harmless)");
				}
				else
				{
					Debug.LogWarning("HighlightingSystem : Framebuffer depth data is not available. Depth occlusion disabled, highlighting occluders enabled. (This message is harmless)");
				}
			}
			flag |= (this.highlightingBuffer == null || this.cam.pixelWidth != this.cachedWidth || this.cam.pixelHeight != this.cachedHeight || aa != this.cachedAA);
			if (flag)
			{
				if (this.highlightingBuffer != null && this.highlightingBuffer.IsCreated())
				{
					this.highlightingBuffer.Release();
				}
				this.cachedWidth = this.cam.pixelWidth;
				this.cachedHeight = this.cam.pixelHeight;
				this.cachedAA = aa;
				this.highlightingBuffer = new RenderTexture(this.cachedWidth, this.cachedHeight, this.isDepthAvailable ? 0 : 24, RenderTextureFormat.ARGB32, RenderTextureReadWrite.Default);
				this.highlightingBuffer.antiAliasing = this.cachedAA;
				this.highlightingBuffer.filterMode = FilterMode.Point;
				this.highlightingBuffer.useMipMap = false;
				this.highlightingBuffer.wrapMode = TextureWrapMode.Clamp;
				if (!this.highlightingBuffer.Create())
				{
					Debug.LogError("HighlightingSystem : UpdateHighlightingBuffer() : Failed to create highlightingBuffer RenderTexture!");
				}
				this.highlightingBufferID = new RenderTargetIdentifier(this.highlightingBuffer);
				this.cutMaterial.SetTexture(ShaderPropertyID._HighlightingBuffer, this.highlightingBuffer);
				this.compMaterial.SetTexture(ShaderPropertyID._HighlightingBuffer, this.highlightingBuffer);
				Vector4 value = new Vector4(1f / (float)this.highlightingBuffer.width, 1f / (float)this.highlightingBuffer.height, 0f, 0f);
				this.cutMaterial.SetVector(ShaderPropertyID._HighlightingBufferTexelSize, value);
			}
			Highlighter.SetOffsetFactor(this.offsetFactor);
			Highlighter.SetOffsetUnits(this.offsetUnits);
			this.RebuildCommandBuffer();
		}

		// Token: 0x060010DD RID: 4317 RVA: 0x0004731B File Offset: 0x0004551B
		protected virtual void OnRenderImage(RenderTexture src, RenderTexture dst)
		{
			if (this.blitter == null)
			{
				this.Blit(src, dst);
				return;
			}
			Graphics.Blit(src, dst);
		}

		// Token: 0x060010DE RID: 4318 RVA: 0x0004733B File Offset: 0x0004553B
		public static bool IsHighlightingCamera(Camera cam)
		{
			return HighlightingBase.cameras.Contains(cam);
		}

		// Token: 0x060010DF RID: 4319 RVA: 0x00047348 File Offset: 0x00045548
		protected static void Initialize()
		{
			if (HighlightingBase.initialized)
			{
				return;
			}
			HighlightingBase.device = SystemInfo.graphicsDeviceType;
			ShaderPropertyID.Initialize();
			int num = HighlightingBase.shaderPaths.Length;
			HighlightingBase.shaders = new Shader[num];
			HighlightingBase.materials = new Material[num];
			for (int i = 0; i < num; i++)
			{
				Shader shader = Shader.Find(HighlightingBase.shaderPaths[i]);
				HighlightingBase.shaders[i] = shader;
				Material material = new Material(shader);
				HighlightingBase.materials[i] = material;
			}
			HighlightingBase.cameraTargetID = new RenderTargetIdentifier(BuiltinRenderTextureType.CameraTarget);
			HighlightingBase.CreateQuad();
			HighlightingBase.initialized = true;
		}

		// Token: 0x060010E0 RID: 4320 RVA: 0x000473D0 File Offset: 0x000455D0
		protected static void CreateQuad()
		{
			if (HighlightingBase.quad == null)
			{
				HighlightingBase.quad = new Mesh();
			}
			else
			{
				HighlightingBase.quad.Clear();
			}
			float y = -1f;
			float y2 = 1f;
			if (HighlightingBase.uvStartsAtTop)
			{
				y = 1f;
				y2 = -1f;
			}
			HighlightingBase.quad.vertices = new Vector3[]
			{
				new Vector3(-1f, y, 0f),
				new Vector3(-1f, y2, 0f),
				new Vector3(1f, y2, 0f),
				new Vector3(1f, y, 0f)
			};
			HighlightingBase.quad.uv = new Vector2[]
			{
				new Vector2(0f, 0f),
				new Vector2(0f, 1f),
				new Vector2(1f, 1f),
				new Vector2(1f, 0f)
			};
			HighlightingBase.quad.colors = new Color[]
			{
				HighlightingBase.colorClear,
				HighlightingBase.colorClear,
				HighlightingBase.colorClear,
				HighlightingBase.colorClear
			};
			HighlightingBase.quad.triangles = new int[]
			{
				0,
				1,
				2,
				2,
				3,
				0
			};
		}

		// Token: 0x060010E1 RID: 4321 RVA: 0x00047550 File Offset: 0x00045750
		protected virtual int GetAA()
		{
			int num = QualitySettings.antiAliasing;
			if (num == 0)
			{
				num = 1;
			}
			if (this.cam.actualRenderingPath == RenderingPath.DeferredLighting || this.cam.actualRenderingPath == RenderingPath.DeferredShading)
			{
				num = 1;
			}
			return num;
		}

		// Token: 0x060010E2 RID: 4322 RVA: 0x00047588 File Offset: 0x00045788
		protected virtual bool CheckSupported(bool verbose)
		{
			bool result = true;
			if (!SystemInfo.supportsImageEffects)
			{
				if (verbose)
				{
					Debug.LogError("HighlightingSystem : Image effects is not supported on this platform!");
				}
				result = false;
			}
			if (!SystemInfo.supportsRenderTextures)
			{
				if (verbose)
				{
					Debug.LogError("HighlightingSystem : RenderTextures is not supported on this platform!");
				}
				result = false;
			}
			if (!SystemInfo.SupportsRenderTextureFormat(RenderTextureFormat.ARGB32))
			{
				if (verbose)
				{
					Debug.LogError("HighlightingSystem : RenderTextureFormat.ARGB32 is not supported on this platform!");
				}
				result = false;
			}
			if (SystemInfo.supportsStencil < 1)
			{
				if (verbose)
				{
					Debug.LogError("HighlightingSystem : Stencil buffer is not supported on this platform!");
				}
				result = false;
			}
			if (!Highlighter.opaqueShader.isSupported)
			{
				if (verbose)
				{
					Debug.LogError("HighlightingSystem : HighlightingOpaque shader is not supported on this platform!");
				}
				result = false;
			}
			if (!Highlighter.transparentShader.isSupported)
			{
				if (verbose)
				{
					Debug.LogError("HighlightingSystem : HighlightingTransparent shader is not supported on this platform!");
				}
				result = false;
			}
			for (int i = 0; i < HighlightingBase.shaders.Length; i++)
			{
				Shader shader = HighlightingBase.shaders[i];
				if (!shader.isSupported)
				{
					if (verbose)
					{
						Debug.LogError("HighlightingSystem : Shader '" + shader.name + "' is not supported on this platform!");
					}
					result = false;
				}
			}
			return result;
		}

		// Token: 0x060010E3 RID: 4323 RVA: 0x0004766C File Offset: 0x0004586C
		protected virtual void RebuildCommandBuffer()
		{
			this.renderBuffer.Clear();
			RenderTargetIdentifier depth = this.isDepthAvailable ? HighlightingBase.cameraTargetID : this.highlightingBufferID;
			this.renderBuffer.SetRenderTarget(this.highlightingBufferID, depth);
			this.renderBuffer.ClearRenderTarget(!this.isDepthAvailable, true, HighlightingBase.colorClear);
			Highlighter.FillBuffer(this.renderBuffer, this.isDepthAvailable);
			RenderTargetIdentifier renderTargetIdentifier = new RenderTargetIdentifier(ShaderPropertyID._HighlightingBlur1);
			RenderTargetIdentifier renderTargetIdentifier2 = new RenderTargetIdentifier(ShaderPropertyID._HighlightingBlur2);
			int width = this.highlightingBuffer.width / this._downsampleFactor;
			int height = this.highlightingBuffer.height / this._downsampleFactor;
			this.renderBuffer.GetTemporaryRT(ShaderPropertyID._HighlightingBlur1, width, height, 0, FilterMode.Bilinear, RenderTextureFormat.ARGB32, RenderTextureReadWrite.Default);
			this.renderBuffer.GetTemporaryRT(ShaderPropertyID._HighlightingBlur2, width, height, 0, FilterMode.Bilinear, RenderTextureFormat.ARGB32, RenderTextureReadWrite.Default);
			this.renderBuffer.Blit(this.highlightingBufferID, renderTargetIdentifier);
			bool flag = true;
			for (int i = 0; i < this._iterations; i++)
			{
				float value = this._blurMinSpread + this._blurSpread * (float)i;
				this.renderBuffer.SetGlobalFloat(ShaderPropertyID._HighlightingBlurOffset, value);
				if (flag)
				{
					this.renderBuffer.Blit(renderTargetIdentifier, renderTargetIdentifier2, this.blurMaterial);
				}
				else
				{
					this.renderBuffer.Blit(renderTargetIdentifier2, renderTargetIdentifier, this.blurMaterial);
				}
				flag = !flag;
			}
			this.renderBuffer.SetGlobalTexture(ShaderPropertyID._HighlightingBlurred, flag ? renderTargetIdentifier : renderTargetIdentifier2);
			this.renderBuffer.SetRenderTarget(this.highlightingBufferID, depth);
			this.renderBuffer.DrawMesh(HighlightingBase.quad, HighlightingBase.identityMatrix, this.cutMaterial);
			this.renderBuffer.ReleaseTemporaryRT(ShaderPropertyID._HighlightingBlur1);
			this.renderBuffer.ReleaseTemporaryRT(ShaderPropertyID._HighlightingBlur2);
		}

		// Token: 0x060010E4 RID: 4324 RVA: 0x0004782B File Offset: 0x00045A2B
		public virtual void Blit(RenderTexture src, RenderTexture dst)
		{
			Graphics.Blit(src, dst, this.compMaterial);
		}

		// Token: 0x04000AD5 RID: 2773
		protected static readonly Color colorClear = new Color(0f, 0f, 0f, 0f);

		// Token: 0x04000AD6 RID: 2774
		protected static readonly string renderBufferName = "HighlightingSystem";

		// Token: 0x04000AD7 RID: 2775
		protected static readonly Matrix4x4 identityMatrix = Matrix4x4.identity;

		// Token: 0x04000AD8 RID: 2776
		protected const CameraEvent queue = CameraEvent.BeforeImageEffectsOpaque;

		// Token: 0x04000AD9 RID: 2777
		protected static RenderTargetIdentifier cameraTargetID;

		// Token: 0x04000ADA RID: 2778
		protected static Mesh quad;

		// Token: 0x04000ADB RID: 2779
		protected static GraphicsDeviceType device = GraphicsDeviceType.Null;

		// Token: 0x04000ADC RID: 2780
		public float offsetFactor;

		// Token: 0x04000ADD RID: 2781
		public float offsetUnits;

		// Token: 0x04000ADE RID: 2782
		protected CommandBuffer renderBuffer;

		// Token: 0x04000ADF RID: 2783
		protected int cachedWidth = -1;

		// Token: 0x04000AE0 RID: 2784
		protected int cachedHeight = -1;

		// Token: 0x04000AE1 RID: 2785
		protected int cachedAA = -1;

		// Token: 0x04000AE2 RID: 2786
		[FormerlySerializedAs("downsampleFactor")]
		[SerializeField]
		protected int _downsampleFactor = 4;

		// Token: 0x04000AE3 RID: 2787
		[FormerlySerializedAs("iterations")]
		[SerializeField]
		protected int _iterations = 2;

		// Token: 0x04000AE4 RID: 2788
		[FormerlySerializedAs("blurMinSpread")]
		[SerializeField]
		protected float _blurMinSpread = 0.65f;

		// Token: 0x04000AE5 RID: 2789
		[FormerlySerializedAs("blurSpread")]
		[SerializeField]
		protected float _blurSpread = 0.25f;

		// Token: 0x04000AE6 RID: 2790
		[SerializeField]
		protected float _blurIntensity = 0.3f;

		// Token: 0x04000AE7 RID: 2791
		[SerializeField]
		protected HighlightingBlitter _blitter;

		// Token: 0x04000AE8 RID: 2792
		protected RenderTargetIdentifier highlightingBufferID;

		// Token: 0x04000AE9 RID: 2793
		protected RenderTexture highlightingBuffer;

		// Token: 0x04000AEA RID: 2794
		protected Camera cam;

		// Token: 0x04000AEB RID: 2795
		protected bool isDepthAvailable = true;

		// Token: 0x04000AEC RID: 2796
		protected const int BLUR = 0;

		// Token: 0x04000AED RID: 2797
		protected const int CUT = 1;

		// Token: 0x04000AEE RID: 2798
		protected const int COMP = 2;

		// Token: 0x04000AEF RID: 2799
		protected static readonly string[] shaderPaths = new string[]
		{
			"Hidden/Highlighted/Blur",
			"Hidden/Highlighted/Cut",
			"Hidden/Highlighted/Composite"
		};

		// Token: 0x04000AF0 RID: 2800
		protected static Shader[] shaders;

		// Token: 0x04000AF1 RID: 2801
		protected static Material[] materials;

		// Token: 0x04000AF2 RID: 2802
		protected Material blurMaterial;

		// Token: 0x04000AF3 RID: 2803
		protected Material cutMaterial;

		// Token: 0x04000AF4 RID: 2804
		protected Material compMaterial;

		// Token: 0x04000AF5 RID: 2805
		protected static bool initialized = false;

		// Token: 0x04000AF6 RID: 2806
		protected static HashSet<Camera> cameras = new HashSet<Camera>();
	}
}
