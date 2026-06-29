using System;
using I2.Loc;
using NodeEditorFramework;
using RG.Parsecs.EventEditor;
using RG.Parsecs.GoalEditor;
using RG.Parsecs.NodeEditor;
using UnityEngine;

namespace RG.SecondsRemaster.Nodes
{
	// Token: 0x0200027C RID: 636
	[Node(false, "Text Nodes/Term List", new Type[]
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
	public class TermListNode : ListNodeTemplate<LocalizedString>
	{
		// Token: 0x1700046F RID: 1135
		// (get) Token: 0x06001782 RID: 6018 RVA: 0x00066CBE File Offset: 0x00064EBE
		public override string GetID
		{
			get
			{
				return "SE_TermsListNode";
			}
		}

		// Token: 0x17000470 RID: 1136
		// (get) Token: 0x06001783 RID: 6019 RVA: 0x00066CC5 File Offset: 0x00064EC5
		protected override string ConnectionType
		{
			get
			{
				return "LocalizedString";
			}
		}

		// Token: 0x06001784 RID: 6020 RVA: 0x00066CCC File Offset: 0x00064ECC
		public override Node Create(Vector2 pos)
		{
			TermListNode termListNode = ScriptableObject.CreateInstance<TermListNode>();
			termListNode.rect = new Rect(pos.x, pos.y, 100f, 85f);
			termListNode.name = "Term list";
			termListNode.CreateInput("Value 0", "LocalizedString");
			termListNode.CreateInput("Value 1", "LocalizedString");
			termListNode.CreateOutput("List", ListConnection.ID);
			return termListNode;
		}
	}
}
