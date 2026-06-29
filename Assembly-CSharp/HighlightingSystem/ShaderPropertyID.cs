using System;
using UnityEngine;

namespace HighlightingSystem
{
	// Token: 0x0200017D RID: 381
	public static class ShaderPropertyID
	{
		// Token: 0x1700035B RID: 859
		// (get) Token: 0x060010E7 RID: 4327 RVA: 0x00047914 File Offset: 0x00045B14
		// (set) Token: 0x060010E8 RID: 4328 RVA: 0x0004791B File Offset: 0x00045B1B
		public static int _MainTex { get; private set; }

		// Token: 0x1700035C RID: 860
		// (get) Token: 0x060010E9 RID: 4329 RVA: 0x00047923 File Offset: 0x00045B23
		// (set) Token: 0x060010EA RID: 4330 RVA: 0x0004792A File Offset: 0x00045B2A
		public static int _Color { get; private set; }

		// Token: 0x1700035D RID: 861
		// (get) Token: 0x060010EB RID: 4331 RVA: 0x00047932 File Offset: 0x00045B32
		// (set) Token: 0x060010EC RID: 4332 RVA: 0x00047939 File Offset: 0x00045B39
		public static int _Cutoff { get; private set; }

		// Token: 0x1700035E RID: 862
		// (get) Token: 0x060010ED RID: 4333 RVA: 0x00047941 File Offset: 0x00045B41
		// (set) Token: 0x060010EE RID: 4334 RVA: 0x00047948 File Offset: 0x00045B48
		public static int _Intensity { get; private set; }

		// Token: 0x1700035F RID: 863
		// (get) Token: 0x060010EF RID: 4335 RVA: 0x00047950 File Offset: 0x00045B50
		// (set) Token: 0x060010F0 RID: 4336 RVA: 0x00047957 File Offset: 0x00045B57
		public static int _ZTest { get; private set; }

		// Token: 0x17000360 RID: 864
		// (get) Token: 0x060010F1 RID: 4337 RVA: 0x0004795F File Offset: 0x00045B5F
		// (set) Token: 0x060010F2 RID: 4338 RVA: 0x00047966 File Offset: 0x00045B66
		public static int _StencilRef { get; private set; }

		// Token: 0x17000361 RID: 865
		// (get) Token: 0x060010F3 RID: 4339 RVA: 0x0004796E File Offset: 0x00045B6E
		// (set) Token: 0x060010F4 RID: 4340 RVA: 0x00047975 File Offset: 0x00045B75
		public static int _Cull { get; private set; }

		// Token: 0x17000362 RID: 866
		// (get) Token: 0x060010F5 RID: 4341 RVA: 0x0004797D File Offset: 0x00045B7D
		// (set) Token: 0x060010F6 RID: 4342 RVA: 0x00047984 File Offset: 0x00045B84
		public static int _HighlightingBlur1 { get; private set; }

		// Token: 0x17000363 RID: 867
		// (get) Token: 0x060010F7 RID: 4343 RVA: 0x0004798C File Offset: 0x00045B8C
		// (set) Token: 0x060010F8 RID: 4344 RVA: 0x00047993 File Offset: 0x00045B93
		public static int _HighlightingBlur2 { get; private set; }

		// Token: 0x17000364 RID: 868
		// (get) Token: 0x060010F9 RID: 4345 RVA: 0x0004799B File Offset: 0x00045B9B
		// (set) Token: 0x060010FA RID: 4346 RVA: 0x000479A2 File Offset: 0x00045BA2
		public static int _HighlightingBuffer { get; private set; }

		// Token: 0x17000365 RID: 869
		// (get) Token: 0x060010FB RID: 4347 RVA: 0x000479AA File Offset: 0x00045BAA
		// (set) Token: 0x060010FC RID: 4348 RVA: 0x000479B1 File Offset: 0x00045BB1
		public static int _HighlightingBufferTexelSize { get; private set; }

		// Token: 0x17000366 RID: 870
		// (get) Token: 0x060010FD RID: 4349 RVA: 0x000479B9 File Offset: 0x00045BB9
		// (set) Token: 0x060010FE RID: 4350 RVA: 0x000479C0 File Offset: 0x00045BC0
		public static int _HighlightingBlurred { get; private set; }

		// Token: 0x17000367 RID: 871
		// (get) Token: 0x060010FF RID: 4351 RVA: 0x000479C8 File Offset: 0x00045BC8
		// (set) Token: 0x06001100 RID: 4352 RVA: 0x000479CF File Offset: 0x00045BCF
		public static int _HighlightingBlurOffset { get; private set; }

		// Token: 0x17000368 RID: 872
		// (get) Token: 0x06001101 RID: 4353 RVA: 0x000479D7 File Offset: 0x00045BD7
		// (set) Token: 0x06001102 RID: 4354 RVA: 0x000479DE File Offset: 0x00045BDE
		public static int _HighlightingZWrite { get; private set; }

		// Token: 0x17000369 RID: 873
		// (get) Token: 0x06001103 RID: 4355 RVA: 0x000479E6 File Offset: 0x00045BE6
		// (set) Token: 0x06001104 RID: 4356 RVA: 0x000479ED File Offset: 0x00045BED
		public static int _HighlightingOffsetFactor { get; private set; }

		// Token: 0x1700036A RID: 874
		// (get) Token: 0x06001105 RID: 4357 RVA: 0x000479F5 File Offset: 0x00045BF5
		// (set) Token: 0x06001106 RID: 4358 RVA: 0x000479FC File Offset: 0x00045BFC
		public static int _HighlightingOffsetUnits { get; private set; }

		// Token: 0x06001107 RID: 4359 RVA: 0x00047A04 File Offset: 0x00045C04
		public static void Initialize()
		{
			if (ShaderPropertyID.initialized)
			{
				return;
			}
			ShaderPropertyID._MainTex = Shader.PropertyToID("_MainTex");
			ShaderPropertyID._Color = Shader.PropertyToID("_Color");
			ShaderPropertyID._Cutoff = Shader.PropertyToID("_Cutoff");
			ShaderPropertyID._Intensity = Shader.PropertyToID("_Intensity");
			ShaderPropertyID._ZTest = Shader.PropertyToID("_ZTest");
			ShaderPropertyID._StencilRef = Shader.PropertyToID("_StencilRef");
			ShaderPropertyID._Cull = Shader.PropertyToID("_Cull");
			ShaderPropertyID._HighlightingBlur1 = Shader.PropertyToID("_HighlightingBlur1");
			ShaderPropertyID._HighlightingBlur2 = Shader.PropertyToID("_HighlightingBlur2");
			ShaderPropertyID._HighlightingBuffer = Shader.PropertyToID("_HighlightingBuffer");
			ShaderPropertyID._HighlightingBufferTexelSize = Shader.PropertyToID("_HighlightingBufferTexelSize");
			ShaderPropertyID._HighlightingBlurred = Shader.PropertyToID("_HighlightingBlurred");
			ShaderPropertyID._HighlightingBlurOffset = Shader.PropertyToID("_HighlightingBlurOffset");
			ShaderPropertyID._HighlightingZWrite = Shader.PropertyToID("_HighlightingZWrite");
			ShaderPropertyID._HighlightingOffsetFactor = Shader.PropertyToID("_HighlightingOffsetFactor");
			ShaderPropertyID._HighlightingOffsetUnits = Shader.PropertyToID("_HighlightingOffsetUnits");
			ShaderPropertyID.initialized = true;
		}

		// Token: 0x04000B07 RID: 2823
		private static bool initialized;
	}
}
