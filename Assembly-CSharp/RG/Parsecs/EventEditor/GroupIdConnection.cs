using System;
using NodeEditorFramework;
using RG.SecondsRemaster.Survival;
using UnityEngine;

namespace RG.Parsecs.EventEditor
{
	// Token: 0x02000213 RID: 531
	public class GroupIdConnection : IConnectionTypeDeclaration
	{
		// Token: 0x170003E2 RID: 994
		// (get) Token: 0x060014D5 RID: 5333 RVA: 0x0005C6B2 File Offset: 0x0005A8B2
		public string Identifier
		{
			get
			{
				return "GroupId";
			}
		}

		// Token: 0x170003E3 RID: 995
		// (get) Token: 0x060014D6 RID: 5334 RVA: 0x0005C6B9 File Offset: 0x0005A8B9
		public Type Type
		{
			get
			{
				return typeof(TextJournalGroupId);
			}
		}

		// Token: 0x170003E4 RID: 996
		// (get) Token: 0x060014D7 RID: 5335 RVA: 0x0005C6C5 File Offset: 0x0005A8C5
		public Color Color
		{
			get
			{
				return new Color(0f, 0.5882353f, 0.39215687f);
			}
		}

		// Token: 0x170003E5 RID: 997
		// (get) Token: 0x060014D8 RID: 5336 RVA: 0x0005C6DB File Offset: 0x0005A8DB
		public string InKnobTex
		{
			get
			{
				return "Textures/In_Knob.png";
			}
		}

		// Token: 0x170003E6 RID: 998
		// (get) Token: 0x060014D9 RID: 5337 RVA: 0x0005C6E2 File Offset: 0x0005A8E2
		public string OutKnobTex
		{
			get
			{
				return "Textures/In_Knob.png";
			}
		}

		// Token: 0x170003E7 RID: 999
		// (get) Token: 0x060014DA RID: 5338 RVA: 0x0005C6E9 File Offset: 0x0005A8E9
		public string InKnobFilledTex
		{
			get
			{
				return "Textures/In_Knob_Filled.png";
			}
		}

		// Token: 0x170003E8 RID: 1000
		// (get) Token: 0x060014DB RID: 5339 RVA: 0x0005C6F0 File Offset: 0x0005A8F0
		public string OutKnobFilledTex
		{
			get
			{
				return "Textures/In_Knob_Filled.png";
			}
		}

		// Token: 0x04000DBD RID: 3517
		public const string ID = "GroupId";
	}
}
