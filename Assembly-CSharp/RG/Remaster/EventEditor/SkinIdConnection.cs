using System;
using NodeEditorFramework;
using RG.Remaster.Survival;
using UnityEngine;

namespace RG.Remaster.EventEditor
{
	// Token: 0x02000221 RID: 545
	public class SkinIdConnection : IConnectionTypeDeclaration
	{
		// Token: 0x1700040A RID: 1034
		// (get) Token: 0x06001537 RID: 5431 RVA: 0x0005E154 File Offset: 0x0005C354
		public string Identifier
		{
			get
			{
				return "SkinId";
			}
		}

		// Token: 0x1700040B RID: 1035
		// (get) Token: 0x06001538 RID: 5432 RVA: 0x0005E15B File Offset: 0x0005C35B
		public Type Type
		{
			get
			{
				return typeof(SkinId);
			}
		}

		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x06001539 RID: 5433 RVA: 0x0005E167 File Offset: 0x0005C367
		public Color Color
		{
			get
			{
				return new Color(0.34901962f, 0.49444443f, 0.03137255f);
			}
		}

		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x0600153A RID: 5434 RVA: 0x0005E17D File Offset: 0x0005C37D
		public string InKnobTex
		{
			get
			{
				return "Textures/In_Knob.png";
			}
		}

		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x0600153B RID: 5435 RVA: 0x0005E184 File Offset: 0x0005C384
		public string OutKnobTex
		{
			get
			{
				return "Textures/Out_Knob.png";
			}
		}

		// Token: 0x1700040F RID: 1039
		// (get) Token: 0x0600153C RID: 5436 RVA: 0x0005E18B File Offset: 0x0005C38B
		public string InKnobFilledTex
		{
			get
			{
				return "Textures/In_Knob_Filled.png";
			}
		}

		// Token: 0x17000410 RID: 1040
		// (get) Token: 0x0600153D RID: 5437 RVA: 0x0005E192 File Offset: 0x0005C392
		public string OutKnobFilledTex
		{
			get
			{
				return "Textures/In_Knob_Filled.png";
			}
		}

		// Token: 0x04000E0F RID: 3599
		public const string ID = "SkinId";
	}
}
