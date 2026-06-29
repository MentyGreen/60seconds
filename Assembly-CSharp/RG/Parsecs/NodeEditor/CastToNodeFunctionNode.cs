using System;
using NodeEditorFramework;
using RG.Parsecs.EndGameEditor;
using RG.Parsecs.EventEditor;
using RG.Parsecs.GoalEditor;
using UnityEngine;

namespace RG.Parsecs.NodeEditor
{
	// Token: 0x02000211 RID: 529
	[Node(false, "Utility Nodes/Cast To Node Function Node", new Type[]
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
	public class CastToNodeFunctionNode : EventNode
	{
		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x060014CB RID: 5323 RVA: 0x0005C52A File Offset: 0x0005A72A
		public override string GetID
		{
			get
			{
				return "PE_CastToNodeFunction";
			}
		}

		// Token: 0x060014CC RID: 5324 RVA: 0x0005C534 File Offset: 0x0005A734
		public override Node Create(Vector2 pos)
		{
			CastToNodeFunctionNode castToNodeFunctionNode = ScriptableObject.CreateInstance<CastToNodeFunctionNode>();
			castToNodeFunctionNode.rect = new Rect(pos.x, pos.y, 180f, 75f);
			castToNodeFunctionNode.name = "Cast To Node Function";
			castToNodeFunctionNode.CreateInput("Object", ObjectConnection.ID);
			castToNodeFunctionNode.CreateOutput("Node Function", "NodeFunction");
			return castToNodeFunctionNode;
		}

		// Token: 0x060014CD RID: 5325 RVA: 0x0005C594 File Offset: 0x0005A794
		public override Node Duplicate(Vector2 pos)
		{
			return this.Create(this.rect.position + new Vector2(20f, 20f));
		}

		// Token: 0x060014CE RID: 5326 RVA: 0x0005C5BB File Offset: 0x0005A7BB
		public override T GetValue<T>(int output, NodeCanvas canvas)
		{
			if (output != 0)
			{
				throw new NotExistingOutputException(this.GetID, output);
			}
			return base.CastValue<T>(base.GetInputValue<object>(this.Inputs[0], canvas));
		}

		// Token: 0x04000DB1 RID: 3505
		public const string ID = "PE_CastToNodeFunction";

		// Token: 0x04000DB2 RID: 3506
		private const int INPUT_OBJECT_INDEX = 0;

		// Token: 0x04000DB3 RID: 3507
		private const int OUTPUT_CHARACTER_INDEX = 0;

		// Token: 0x04000DB4 RID: 3508
		private const string INPUT_OBJECT_NAME = "Object";

		// Token: 0x04000DB5 RID: 3509
		private const string OUTPUT_CHARACTER_NAME = "Node Function";

		// Token: 0x04000DB6 RID: 3510
		private const string NODE_NAME = "Cast To Node Function";
	}
}
