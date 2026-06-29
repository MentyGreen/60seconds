using System;
using NodeEditorFramework;
using RG.Remaster.Survival;
using UnityEngine;

namespace RG.Remaster.EventEditor
{
	// Token: 0x02000220 RID: 544
	public class SkinDataListConnection : IConnectionTypeDeclaration
	{
		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x0600152F RID: 5423 RVA: 0x0005E107 File Offset: 0x0005C307
		public string Identifier
		{
			get
			{
				return "SkinDataList";
			}
		}

		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x06001530 RID: 5424 RVA: 0x0005E10E File Offset: 0x0005C30E
		public Type Type
		{
			get
			{
				return typeof(SkinDataList);
			}
		}

		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x06001531 RID: 5425 RVA: 0x0005E11A File Offset: 0x0005C31A
		public Color Color
		{
			get
			{
				return new Color(0.1764706f, 0.49444443f, 0.47058824f);
			}
		}

		// Token: 0x17000406 RID: 1030
		// (get) Token: 0x06001532 RID: 5426 RVA: 0x0005E130 File Offset: 0x0005C330
		public string InKnobTex
		{
			get
			{
				return "Textures/In_Knob.png";
			}
		}

		// Token: 0x17000407 RID: 1031
		// (get) Token: 0x06001533 RID: 5427 RVA: 0x0005E137 File Offset: 0x0005C337
		public string OutKnobTex
		{
			get
			{
				return "Textures/Out_Knob.png";
			}
		}

		// Token: 0x17000408 RID: 1032
		// (get) Token: 0x06001534 RID: 5428 RVA: 0x0005E13E File Offset: 0x0005C33E
		public string InKnobFilledTex
		{
			get
			{
				return "Textures/In_Knob_Filled.png";
			}
		}

		// Token: 0x17000409 RID: 1033
		// (get) Token: 0x06001535 RID: 5429 RVA: 0x0005E145 File Offset: 0x0005C345
		public string OutKnobFilledTex
		{
			get
			{
				return "Textures/In_Knob_Filled.png";
			}
		}

		// Token: 0x04000E0E RID: 3598
		public const string ID = "SkinDataList";
	}
}
