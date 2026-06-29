using System;
using NodeEditorFramework;
using RG.Parsecs.EventEditor;
using RG.Parsecs.GoalEditor;
using RG.Parsecs.NodeEditor;
using UnityEngine;

namespace RG.SecondsRemaster.Nodes
{
	// Token: 0x02000269 RID: 617
	[Node(false, "Lists/Int List", new Type[]
	{
		typeof(SurvivalEvent),
		typeof(SystemEvent),
		typeof(NodeFunction),
		typeof(SystemStatusEvent),
		typeof(ExpeditionEvent),
		typeof(ReportEvent),
		typeof(Goal),
		typeof(ConditionEvent)
	})]
	public class IntListNode : ListNodeTemplate<int>
	{
		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x060016E2 RID: 5858 RVA: 0x00064A5E File Offset: 0x00062C5E
		public override string GetID
		{
			get
			{
				return "SE_IntListNode";
			}
		}

		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x060016E3 RID: 5859 RVA: 0x00064A65 File Offset: 0x00062C65
		protected override string ConnectionType
		{
			get
			{
				return "Int";
			}
		}

		// Token: 0x060016E4 RID: 5860 RVA: 0x00064A6C File Offset: 0x00062C6C
		public override Node Create(Vector2 pos)
		{
			IntListNode intListNode = ScriptableObject.CreateInstance<IntListNode>();
			intListNode.rect = new Rect(pos.x, pos.y, 100f, 85f);
			intListNode.name = "Int list";
			intListNode.CreateInput("Value 0", "Int");
			intListNode.CreateInput("Value 1", "Int");
			intListNode.CreateOutput("List", ListConnection.ID);
			return intListNode;
		}
	}
}
