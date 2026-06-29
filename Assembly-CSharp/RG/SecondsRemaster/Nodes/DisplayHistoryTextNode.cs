using System;
using NodeEditorFramework;
using RG.Parsecs.EventEditor;
using RG.Parsecs.GoalEditor;
using RG.Parsecs.NodeEditor;
using RG.Remaster.Survival;
using RG.SecondsRemaster.Survival;
using UnityEngine;

namespace RG.SecondsRemaster.Nodes
{
	// Token: 0x02000254 RID: 596
	[Node(true, "Text Nodes/Display History Text Node", new Type[]
	{
		typeof(SurvivalEvent),
		typeof(SystemEvent),
		typeof(NodeFunction),
		typeof(SystemStatusEvent),
		typeof(ExpeditionEvent),
		typeof(ReportEvent),
		typeof(Goal)
	})]
	public class DisplayHistoryTextNode : MessageNode
	{
		// Token: 0x17000446 RID: 1094
		// (get) Token: 0x0600163D RID: 5693 RVA: 0x00061238 File Offset: 0x0005F438
		public override string GetID
		{
			get
			{
				return "DisplayHistoryTextNode";
			}
		}

		// Token: 0x0600163E RID: 5694 RVA: 0x00061240 File Offset: 0x0005F440
		public override Node Create(Vector2 pos)
		{
			DisplayHistoryTextNode displayHistoryTextNode = ScriptableObject.CreateInstance<DisplayHistoryTextNode>();
			displayHistoryTextNode.rect = new Rect(pos.x, pos.y, 300f, 105f);
			displayHistoryTextNode.name = "Display History Text";
			displayHistoryTextNode.CreateMutliInput("In", "Flow");
			displayHistoryTextNode.CreateOutput("Out", "Flow");
			return displayHistoryTextNode;
		}

		// Token: 0x0600163F RID: 5695 RVA: 0x000612A0 File Offset: 0x0005F4A0
		public override Node Duplicate(Vector2 pos)
		{
			return (DisplayHistoryTextNode)this.Create(this.rect.position + new Vector2(20f, 20f));
		}

		// Token: 0x06001640 RID: 5696 RVA: 0x000612CC File Offset: 0x0005F4CC
		public override void Execute(NodeCanvas canvas)
		{
			TextJournalContent content = new TextJournalContent(SimpleHistoryManager.Instance.RenderHistoryToString(), 0);
			SecondsEventManager.AddJournalContent(base.ParentCanvas, content);
			base.CheckAreAllFlowOutputsConnected();
			this.Outputs[0].GetCustomNodeAcrossConnection<ParsecsNode>().ExecuteWithErrorHandling(canvas);
		}

		// Token: 0x04000EF8 RID: 3832
		public const string ID = "DisplayHistoryTextNode";
	}
}
