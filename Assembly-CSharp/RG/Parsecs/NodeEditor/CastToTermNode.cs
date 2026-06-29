using System;
using NodeEditorFramework;
using RG.Parsecs.EndGameEditor;
using RG.Parsecs.EventEditor;
using RG.Parsecs.GoalEditor;
using UnityEngine;

namespace RG.Parsecs.NodeEditor
{
	// Token: 0x02000212 RID: 530
	[Node(false, "Utility Nodes/Cast To Term Node", new Type[]
	{
		typeof(Goal),
		typeof(SurvivalEvent),
		typeof(ReportEvent),
		typeof(SystemEvent),
		typeof(NodeFunction),
		typeof(SystemStatusEvent),
		typeof(ExpeditionEvent),
		typeof(EndGameCanvas),
		typeof(ConditionEvent)
	})]
	public class CastToTermNode : EventNode
	{
		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x060014D0 RID: 5328 RVA: 0x0005C5EE File Offset: 0x0005A7EE
		public override string GetID
		{
			get
			{
				return "PE_CastToTerm";
			}
		}

		// Token: 0x060014D1 RID: 5329 RVA: 0x0005C5F8 File Offset: 0x0005A7F8
		public override Node Create(Vector2 pos)
		{
			CastToTermNode castToTermNode = ScriptableObject.CreateInstance<CastToTermNode>();
			castToTermNode.rect = new Rect(pos.x, pos.y, 150f, 75f);
			castToTermNode.name = "Cast To Term";
			castToTermNode.CreateInput("Object", ObjectConnection.ID);
			castToTermNode.CreateOutput("Term", "LocalizedString");
			return castToTermNode;
		}

		// Token: 0x060014D2 RID: 5330 RVA: 0x0005C658 File Offset: 0x0005A858
		public override Node Duplicate(Vector2 pos)
		{
			return this.Create(this.rect.position + new Vector2(20f, 20f));
		}

		// Token: 0x060014D3 RID: 5331 RVA: 0x0005C67F File Offset: 0x0005A87F
		public override T GetValue<T>(int output, NodeCanvas canvas)
		{
			if (output != 0)
			{
				throw new NotExistingOutputException(this.GetID, output);
			}
			return base.CastValue<T>(base.GetInputValue<object>(this.Inputs[0], canvas));
		}

		// Token: 0x04000DB7 RID: 3511
		public const string ID = "PE_CastToTerm";

		// Token: 0x04000DB8 RID: 3512
		private const int INPUT_OBJECT_INDEX = 0;

		// Token: 0x04000DB9 RID: 3513
		private const int OUTPUT_CHARACTER_INDEX = 0;

		// Token: 0x04000DBA RID: 3514
		private const string INPUT_OBJECT_NAME = "Object";

		// Token: 0x04000DBB RID: 3515
		private const string OUTPUT_CHARACTER_NAME = "Term";

		// Token: 0x04000DBC RID: 3516
		private const string NODE_NAME = "Cast To Term";
	}
}
