using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace HighlightingSystem
{
	// Token: 0x02000177 RID: 375
	[DisallowMultipleComponent]
	public class Highlighter : MonoBehaviour
	{
		// Token: 0x06001085 RID: 4229 RVA: 0x00045ED8 File Offset: 0x000440D8
		public void ReinitMaterials()
		{
			this.renderersDirty = true;
		}

		// Token: 0x06001086 RID: 4230 RVA: 0x00045EE1 File Offset: 0x000440E1
		public void OnParams(Color color)
		{
			this.onceColor = color;
		}

		// Token: 0x06001087 RID: 4231 RVA: 0x00045EEA File Offset: 0x000440EA
		public void On()
		{
			this.once = true;
		}

		// Token: 0x06001088 RID: 4232 RVA: 0x00045EF3 File Offset: 0x000440F3
		public void On(Color color)
		{
			this.onceColor = color;
			this.once = true;
		}

		// Token: 0x06001089 RID: 4233 RVA: 0x00045F03 File Offset: 0x00044103
		public void FlashingParams(Color color1, Color color2, float freq)
		{
			this.flashingColorMin = color1;
			this.flashingColorMax = color2;
			this.flashingFreq = freq;
		}

		// Token: 0x0600108A RID: 4234 RVA: 0x00045F1A File Offset: 0x0004411A
		public void FlashingOn()
		{
			this.flashing = true;
		}

		// Token: 0x0600108B RID: 4235 RVA: 0x00045F23 File Offset: 0x00044123
		public void FlashingOn(Color color1, Color color2)
		{
			this.flashingColorMin = color1;
			this.flashingColorMax = color2;
			this.flashing = true;
		}

		// Token: 0x0600108C RID: 4236 RVA: 0x00045F3A File Offset: 0x0004413A
		public void FlashingOn(Color color1, Color color2, float freq)
		{
			this.flashingColorMin = color1;
			this.flashingColorMax = color2;
			this.flashingFreq = freq;
			this.flashing = true;
		}

		// Token: 0x0600108D RID: 4237 RVA: 0x00045F58 File Offset: 0x00044158
		public void FlashingOn(float freq)
		{
			this.flashingFreq = freq;
			this.flashing = true;
		}

		// Token: 0x0600108E RID: 4238 RVA: 0x00045F68 File Offset: 0x00044168
		public void FlashingOff()
		{
			this.flashing = false;
		}

		// Token: 0x0600108F RID: 4239 RVA: 0x00045F71 File Offset: 0x00044171
		public void FlashingSwitch()
		{
			this.flashing = !this.flashing;
		}

		// Token: 0x06001090 RID: 4240 RVA: 0x00045F82 File Offset: 0x00044182
		public void ConstantParams(Color color)
		{
			this.constantColor = color;
		}

		// Token: 0x06001091 RID: 4241 RVA: 0x00045F8B File Offset: 0x0004418B
		public void ConstantOn(float time = 0.25f)
		{
			this.transitionTime = ((time >= 0f) ? time : 0f);
			this.transitionTarget = 1f;
		}

		// Token: 0x06001092 RID: 4242 RVA: 0x00045FAE File Offset: 0x000441AE
		public void ConstantOn(Color color, float time = 0.25f)
		{
			this.constantColor = color;
			this.transitionTime = ((time >= 0f) ? time : 0f);
			this.transitionTarget = 1f;
		}

		// Token: 0x06001093 RID: 4243 RVA: 0x00045FD8 File Offset: 0x000441D8
		public void ConstantOff(float time = 0.25f)
		{
			this.transitionTime = ((time >= 0f) ? time : 0f);
			this.transitionTarget = 0f;
		}

		// Token: 0x06001094 RID: 4244 RVA: 0x00045FFB File Offset: 0x000441FB
		public void ConstantSwitch(float time = 0.25f)
		{
			this.transitionTime = ((time >= 0f) ? time : 0f);
			this.transitionTarget = ((this.transitionTarget > 0f) ? 0f : 1f);
		}

		// Token: 0x06001095 RID: 4245 RVA: 0x00046034 File Offset: 0x00044234
		public void ConstantOnImmediate()
		{
			this.transitionValue = (this.transitionTarget = 1f);
		}

		// Token: 0x06001096 RID: 4246 RVA: 0x00046058 File Offset: 0x00044258
		public void ConstantOnImmediate(Color color)
		{
			this.constantColor = color;
			this.transitionValue = (this.transitionTarget = 1f);
		}

		// Token: 0x06001097 RID: 4247 RVA: 0x00046080 File Offset: 0x00044280
		public void ConstantOffImmediate()
		{
			this.transitionValue = (this.transitionTarget = 0f);
		}

		// Token: 0x06001098 RID: 4248 RVA: 0x000460A4 File Offset: 0x000442A4
		public void ConstantSwitchImmediate()
		{
			this.transitionValue = (this.transitionTarget = ((this.transitionTarget > 0f) ? 0f : 1f));
		}

		// Token: 0x06001099 RID: 4249 RVA: 0x000460DC File Offset: 0x000442DC
		public void Off()
		{
			this.once = false;
			this.flashing = false;
			this.transitionValue = (this.transitionTarget = 0f);
		}

		// Token: 0x0600109A RID: 4250 RVA: 0x0004610B File Offset: 0x0004430B
		public void Die()
		{
			Object.Destroy(this);
		}

		// Token: 0x0600109B RID: 4251 RVA: 0x00046113 File Offset: 0x00044313
		public void SeeThrough(bool state)
		{
			this.seeThrough = state;
		}

		// Token: 0x0600109C RID: 4252 RVA: 0x0004611C File Offset: 0x0004431C
		public void SeeThroughOn()
		{
			this.seeThrough = true;
		}

		// Token: 0x0600109D RID: 4253 RVA: 0x00046125 File Offset: 0x00044325
		public void SeeThroughOff()
		{
			this.seeThrough = false;
		}

		// Token: 0x0600109E RID: 4254 RVA: 0x0004612E File Offset: 0x0004432E
		public void SeeThroughSwitch()
		{
			this.seeThrough = !this.seeThrough;
		}

		// Token: 0x0600109F RID: 4255 RVA: 0x0004613F File Offset: 0x0004433F
		public void OccluderOn()
		{
			this.occluder = true;
		}

		// Token: 0x060010A0 RID: 4256 RVA: 0x00046148 File Offset: 0x00044348
		public void OccluderOff()
		{
			this.occluder = false;
		}

		// Token: 0x060010A1 RID: 4257 RVA: 0x00046151 File Offset: 0x00044351
		public void OccluderSwitch()
		{
			this.occluder = !this.occluder;
		}

		// Token: 0x1700034F RID: 847
		// (get) Token: 0x060010A2 RID: 4258 RVA: 0x00046162 File Offset: 0x00044362
		// (set) Token: 0x060010A3 RID: 4259 RVA: 0x00046171 File Offset: 0x00044371
		private bool once
		{
			get
			{
				return this._once == Time.frameCount;
			}
			set
			{
				this._once = (value ? Time.frameCount : -1);
			}
		}

		// Token: 0x17000350 RID: 848
		// (get) Token: 0x060010A4 RID: 4260 RVA: 0x00046184 File Offset: 0x00044384
		public static Shader opaqueShader
		{
			get
			{
				if (Highlighter._opaqueShader == null)
				{
					Highlighter._opaqueShader = Shader.Find("Hidden/Highlighted/Opaque");
				}
				return Highlighter._opaqueShader;
			}
		}

		// Token: 0x17000351 RID: 849
		// (get) Token: 0x060010A5 RID: 4261 RVA: 0x000461A7 File Offset: 0x000443A7
		public static Shader transparentShader
		{
			get
			{
				if (Highlighter._transparentShader == null)
				{
					Highlighter._transparentShader = Shader.Find("Hidden/Highlighted/Transparent");
				}
				return Highlighter._transparentShader;
			}
		}

		// Token: 0x17000352 RID: 850
		// (get) Token: 0x060010A6 RID: 4262 RVA: 0x000461CC File Offset: 0x000443CC
		private Material opaqueMaterial
		{
			get
			{
				if (this._opaqueMaterial == null)
				{
					this._opaqueMaterial = new Material(Highlighter.opaqueShader);
					ShaderPropertyID.Initialize();
					this._opaqueMaterial.SetInt(ShaderPropertyID._ZTest, Highlighter.GetZTest(this.zTest));
					this._opaqueMaterial.SetInt(ShaderPropertyID._StencilRef, Highlighter.GetStencilRef(this.stencilRef));
				}
				return this._opaqueMaterial;
			}
		}

		// Token: 0x060010A7 RID: 4263 RVA: 0x00046238 File Offset: 0x00044438
		private void Awake()
		{
			ShaderPropertyID.Initialize();
			this.tr = base.GetComponent<Transform>();
			this.renderersDirty = true;
			this.seeThrough = (this.zTest = true);
			this.mode = Highlighter.Mode.None;
			this.stencilRef = true;
			this.once = false;
			this.flashing = false;
			this.occluder = false;
			this.transitionValue = (this.transitionTarget = 0f);
			this.onceColor = Color.red;
			this.flashingFreq = 2f;
			this.flashingColorMin = new Color(0f, 1f, 1f, 0f);
			this.flashingColorMax = new Color(0f, 1f, 1f, 1f);
			this.constantColor = Color.yellow;
		}

		// Token: 0x060010A8 RID: 4264 RVA: 0x00046303 File Offset: 0x00044503
		private void OnEnable()
		{
			Highlighter.highlighters.Add(this);
		}

		// Token: 0x060010A9 RID: 4265 RVA: 0x00046314 File Offset: 0x00044514
		private void OnDisable()
		{
			Highlighter.highlighters.Remove(this);
			this.ClearRenderers();
			this.renderersDirty = true;
			this.once = false;
			this.flashing = false;
			this.transitionValue = (this.transitionTarget = 0f);
		}

		// Token: 0x060010AA RID: 4266 RVA: 0x0004635C File Offset: 0x0004455C
		private void Update()
		{
			this.UpdateTransition();
		}

		// Token: 0x060010AB RID: 4267 RVA: 0x00046364 File Offset: 0x00044564
		private void ClearRenderers()
		{
			for (int i = this.highlightableRenderers.Count - 1; i >= 0; i--)
			{
				this.highlightableRenderers[i].SetState(false);
			}
			this.highlightableRenderers.Clear();
		}

		// Token: 0x060010AC RID: 4268 RVA: 0x000463A8 File Offset: 0x000445A8
		private void UpdateRenderers()
		{
			if (this.renderersDirty)
			{
				this.ClearRenderers();
				List<Renderer> list = new List<Renderer>();
				this.GrabRenderers(this.tr, list);
				int i = 0;
				int count = list.Count;
				while (i < count)
				{
					GameObject gameObject = list[i].gameObject;
					HighlighterRenderer highlighterRenderer = gameObject.GetComponent<HighlighterRenderer>();
					if (highlighterRenderer == null)
					{
						highlighterRenderer = gameObject.AddComponent<HighlighterRenderer>();
					}
					highlighterRenderer.SetState(true);
					highlighterRenderer.Initialize(this.opaqueMaterial, Highlighter.transparentShader);
					this.highlightableRenderers.Add(highlighterRenderer);
					i++;
				}
				this.renderersDirty = false;
			}
		}

		// Token: 0x060010AD RID: 4269 RVA: 0x00046444 File Offset: 0x00044644
		private void GrabRenderers(Transform t, List<Renderer> renderers)
		{
			GameObject gameObject = t.gameObject;
			int i = 0;
			int count = Highlighter.types.Count;
			while (i < count)
			{
				gameObject.GetComponents(Highlighter.types[i], Highlighter.sComponents);
				int j = 0;
				int count2 = Highlighter.sComponents.Count;
				while (j < count2)
				{
					renderers.Add(Highlighter.sComponents[j] as Renderer);
					j++;
				}
				i++;
			}
			Highlighter.sComponents.Clear();
			int childCount = t.childCount;
			if (childCount == 0)
			{
				return;
			}
			for (int k = 0; k < childCount; k++)
			{
				Transform child = t.GetChild(k);
				if (!(child.GetComponent<Highlighter>() != null) && !(child.GetComponent<HighlighterBlocker>() != null))
				{
					this.GrabRenderers(child, renderers);
				}
			}
		}

		// Token: 0x060010AE RID: 4270 RVA: 0x00046510 File Offset: 0x00044710
		private void UpdateShaderParams(bool zt, bool sr)
		{
			if (this.zTest != zt)
			{
				this.zTest = zt;
				int ztest = Highlighter.GetZTest(this.zTest);
				this.opaqueMaterial.SetInt(ShaderPropertyID._ZTest, ztest);
				for (int i = 0; i < this.highlightableRenderers.Count; i++)
				{
					this.highlightableRenderers[i].SetZTestForTransparent(ztest);
				}
			}
			if (this.stencilRef != sr)
			{
				this.stencilRef = sr;
				int num = Highlighter.GetStencilRef(this.stencilRef);
				this.opaqueMaterial.SetInt(ShaderPropertyID._StencilRef, num);
				for (int j = 0; j < this.highlightableRenderers.Count; j++)
				{
					this.highlightableRenderers[j].SetStencilRefForTransparent(num);
				}
			}
		}

		// Token: 0x060010AF RID: 4271 RVA: 0x000465C8 File Offset: 0x000447C8
		private void UpdateColors()
		{
			if (this.once)
			{
				this.currentColor = this.onceColor;
			}
			else if (this.flashing)
			{
				this.currentColor = Color.Lerp(this.flashingColorMin, this.flashingColorMax, 0.5f * Mathf.Sin(Time.realtimeSinceStartup * this.flashingFreq * 6.2831855f) + 0.5f);
			}
			else if (this.transitionValue > 0f)
			{
				this.currentColor = this.constantColor;
				this.currentColor.a = this.currentColor.a * this.transitionValue;
			}
			else
			{
				if (!this.occluder)
				{
					return;
				}
				this.currentColor = this.occluderColor;
			}
			this.opaqueMaterial.SetColor(ShaderPropertyID._Color, this.currentColor);
			for (int i = 0; i < this.highlightableRenderers.Count; i++)
			{
				this.highlightableRenderers[i].SetColorForTransparent(this.currentColor);
			}
		}

		// Token: 0x060010B0 RID: 4272 RVA: 0x000466BC File Offset: 0x000448BC
		private void UpdateTransition()
		{
			if (this.transitionValue != this.transitionTarget)
			{
				if (this.transitionTime <= 0f)
				{
					this.transitionValue = this.transitionTarget;
					return;
				}
				float num = (this.transitionTarget > 0f) ? 1f : -1f;
				this.transitionValue = Mathf.Clamp01(this.transitionValue + num * Time.unscaledDeltaTime / this.transitionTime);
			}
		}

		// Token: 0x060010B1 RID: 4273 RVA: 0x0004672C File Offset: 0x0004492C
		private void FillBufferInternal(CommandBuffer buffer, Highlighter.Mode m, bool depthAvailable)
		{
			this.UpdateRenderers();
			bool flag = this.once || this.flashing || this.transitionValue > 0f;
			bool flag2 = this.occluder && (this.seeThrough || !depthAvailable);
			this.mode = Highlighter.Mode.None;
			if (flag)
			{
				this.mode = (this.seeThrough ? Highlighter.Mode.HighlighterSeeThrough : Highlighter.Mode.Highlighter);
			}
			else if (flag2)
			{
				this.mode = (this.seeThrough ? Highlighter.Mode.OccluderSeeThrough : Highlighter.Mode.Occluder);
			}
			if (this.mode == Highlighter.Mode.None || this.mode != m)
			{
				return;
			}
			if (flag)
			{
				this.UpdateShaderParams(this.seeThrough, true);
			}
			else if (flag2)
			{
				this.UpdateShaderParams(false, this.seeThrough);
			}
			this.UpdateColors();
			for (int i = this.highlightableRenderers.Count - 1; i >= 0; i--)
			{
				HighlighterRenderer highlighterRenderer = this.highlightableRenderers[i];
				if (highlighterRenderer == null)
				{
					this.highlightableRenderers.RemoveAt(i);
				}
				else if (!highlighterRenderer.FillBuffer(buffer))
				{
					this.highlightableRenderers.RemoveAt(i);
					highlighterRenderer.SetState(false);
				}
			}
		}

		// Token: 0x060010B2 RID: 4274 RVA: 0x00046840 File Offset: 0x00044A40
		public static void FillBuffer(CommandBuffer buffer, bool depthAvailable)
		{
			for (int i = 0; i < Highlighter.renderingOrder.Length; i++)
			{
				Highlighter.Mode m = Highlighter.renderingOrder[i];
				foreach (Highlighter highlighter in Highlighter.highlighters)
				{
					highlighter.FillBufferInternal(buffer, m, depthAvailable);
				}
			}
		}

		// Token: 0x060010B3 RID: 4275 RVA: 0x0004688C File Offset: 0x00044A8C
		private static int GetZTest(bool enabled)
		{
			if (!enabled)
			{
				return 4;
			}
			return 8;
		}

		// Token: 0x060010B4 RID: 4276 RVA: 0x00046894 File Offset: 0x00044A94
		private static int GetStencilRef(bool enabled)
		{
			if (!enabled)
			{
				return 0;
			}
			return 1;
		}

		// Token: 0x060010B5 RID: 4277 RVA: 0x0004689C File Offset: 0x00044A9C
		public static void SetZWrite(int value)
		{
			if (Highlighter.zWrite == value)
			{
				return;
			}
			Highlighter.zWrite = value;
			Shader.SetGlobalInt(ShaderPropertyID._HighlightingZWrite, Highlighter.zWrite);
		}

		// Token: 0x060010B6 RID: 4278 RVA: 0x000468BC File Offset: 0x00044ABC
		public static void SetOffsetFactor(float value)
		{
			if (Highlighter.offsetFactor == value)
			{
				return;
			}
			Highlighter.offsetFactor = value;
			Shader.SetGlobalFloat(ShaderPropertyID._HighlightingOffsetFactor, Highlighter.offsetFactor);
		}

		// Token: 0x060010B7 RID: 4279 RVA: 0x000468DC File Offset: 0x00044ADC
		public static void SetOffsetUnits(float value)
		{
			if (Highlighter.offsetUnits == value)
			{
				return;
			}
			Highlighter.offsetUnits = value;
			Shader.SetGlobalFloat(ShaderPropertyID._HighlightingOffsetUnits, Highlighter.offsetUnits);
		}

		// Token: 0x04000AA6 RID: 2726
		public static readonly List<Type> types = new List<Type>
		{
			typeof(MeshRenderer),
			typeof(SkinnedMeshRenderer),
			typeof(SpriteRenderer),
			typeof(ParticleSystemRenderer)
		};

		// Token: 0x04000AA7 RID: 2727
		private const float doublePI = 6.2831855f;

		// Token: 0x04000AA8 RID: 2728
		private readonly Color occluderColor = new Color(0f, 0f, 0f, 0f);

		// Token: 0x04000AA9 RID: 2729
		private const int zTestLessEqual = 4;

		// Token: 0x04000AAA RID: 2730
		private const int zTestAlways = 8;

		// Token: 0x04000AAB RID: 2731
		private static readonly Highlighter.Mode[] renderingOrder = new Highlighter.Mode[]
		{
			Highlighter.Mode.Highlighter,
			Highlighter.Mode.Occluder,
			Highlighter.Mode.HighlighterSeeThrough,
			Highlighter.Mode.OccluderSeeThrough
		};

		// Token: 0x04000AAC RID: 2732
		private static HashSet<Highlighter> highlighters = new HashSet<Highlighter>();

		// Token: 0x04000AAD RID: 2733
		private static int zWrite = -1;

		// Token: 0x04000AAE RID: 2734
		private static float offsetFactor = float.NaN;

		// Token: 0x04000AAF RID: 2735
		private static float offsetUnits = float.NaN;

		// Token: 0x04000AB0 RID: 2736
		[HideInInspector]
		public bool seeThrough;

		// Token: 0x04000AB1 RID: 2737
		[HideInInspector]
		public bool occluder;

		// Token: 0x04000AB2 RID: 2738
		private Transform tr;

		// Token: 0x04000AB3 RID: 2739
		private List<HighlighterRenderer> highlightableRenderers = new List<HighlighterRenderer>();

		// Token: 0x04000AB4 RID: 2740
		private bool renderersDirty;

		// Token: 0x04000AB5 RID: 2741
		private static List<Component> sComponents = new List<Component>(4);

		// Token: 0x04000AB6 RID: 2742
		private Highlighter.Mode mode;

		// Token: 0x04000AB7 RID: 2743
		private bool zTest;

		// Token: 0x04000AB8 RID: 2744
		private bool stencilRef;

		// Token: 0x04000AB9 RID: 2745
		private int _once = -1;

		// Token: 0x04000ABA RID: 2746
		private bool flashing;

		// Token: 0x04000ABB RID: 2747
		private Color currentColor;

		// Token: 0x04000ABC RID: 2748
		private float transitionValue;

		// Token: 0x04000ABD RID: 2749
		private float transitionTarget;

		// Token: 0x04000ABE RID: 2750
		private float transitionTime;

		// Token: 0x04000ABF RID: 2751
		private Color onceColor;

		// Token: 0x04000AC0 RID: 2752
		private float flashingFreq;

		// Token: 0x04000AC1 RID: 2753
		private Color flashingColorMin;

		// Token: 0x04000AC2 RID: 2754
		private Color flashingColorMax;

		// Token: 0x04000AC3 RID: 2755
		private Color constantColor;

		// Token: 0x04000AC4 RID: 2756
		private static Shader _opaqueShader;

		// Token: 0x04000AC5 RID: 2757
		private static Shader _transparentShader;

		// Token: 0x04000AC6 RID: 2758
		private Material _opaqueMaterial;

		// Token: 0x020003D6 RID: 982
		private enum Mode
		{
			// Token: 0x040017ED RID: 6125
			None,
			// Token: 0x040017EE RID: 6126
			Highlighter,
			// Token: 0x040017EF RID: 6127
			Occluder,
			// Token: 0x040017F0 RID: 6128
			HighlighterSeeThrough,
			// Token: 0x040017F1 RID: 6129
			OccluderSeeThrough
		}
	}
}
