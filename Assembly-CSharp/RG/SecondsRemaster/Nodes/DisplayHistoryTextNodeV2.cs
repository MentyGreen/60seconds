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
	// Token: 0x02000255 RID: 597
	[Node(false, "Text Nodes/Display History Text Node", new Type[]
	{
		typeof(SurvivalEvent),
		typeof(SystemEvent),
		typeof(NodeFunction),
		typeof(SystemStatusEvent),
		typeof(ExpeditionEvent),
		typeof(ReportEvent),
		typeof(Goal)
	})]
	public class DisplayHistoryTextNodeV2 : MessageNode
	{
		// Token: 0x17000447 RID: 1095
		// (get) Token: 0x06001642 RID: 5698 RVA: 0x0006131B File Offset: 0x0005F51B
		public override string GetID
		{
			get
			{
				return "DisplayHistoryTextNodeV2";
			}
		}

		// Token: 0x06001643 RID: 5699 RVA: 0x00061324 File Offset: 0x0005F524
		public override Node Create(Vector2 pos)
		{
			DisplayHistoryTextNodeV2 displayHistoryTextNodeV = ScriptableObject.CreateInstance<DisplayHistoryTextNodeV2>();
			displayHistoryTextNodeV.rect = new Rect(pos.x, pos.y, 300f, 105f);
			displayHistoryTextNodeV.name = "Display History Text";
			displayHistoryTextNodeV.CreateMutliInput("In", "Flow");
			displayHistoryTextNodeV.CreateInput("Display Priority", "Int");
			displayHistoryTextNodeV.CreateOutput("Out", "Flow");
			return displayHistoryTextNodeV;
		}

		// Token: 0x06001644 RID: 5700 RVA: 0x00061395 File Offset: 0x0005F595
		public override Node Duplicate(Vector2 pos)
		{
			DisplayHistoryTextNodeV2 displayHistoryTextNodeV = (DisplayHistoryTextNodeV2)this.Create(this.rect.position + new Vector2(20f, 20f));
			displayHistoryTextNodeV._displayPriority = this._displayPriority;
			return displayHistoryTextNodeV;
		}

		// Token: 0x06001645 RID: 5701 RVA: 0x000613CD File Offset: 0x0005F5CD
		protected override void NodeEnable()
		{
		}

		// Token: 0x06001646 RID: 5702 RVA: 0x000613CF File Offset: 0x0005F5CF
		protected override void NodeGUI()
		{
		}

		// Token: 0x06001647 RID: 5703 RVA: 0x000613D4 File Offset: 0x0005F5D4
		public override void Execute(NodeCanvas canvas)
		{
			base.GetInputValue<int>(this.Inputs[1], ref this._displayPriority, canvas);
			TextJournalContent content = new TextJournalContent(SimpleHistoryManager.Instance.RenderHistoryToString(), this._displayPriority);
			SecondsEventManager.AddJournalContent(base.ParentCanvas, content);
			base.CheckAreAllFlowOutputsConnected();
			this.Outputs[0].GetCustomNodeAcrossConnection<ParsecsNode>().ExecuteWithErrorHandling(canvas);
		}

		// Token: 0x04000EF9 RID: 3833
		public const string ID = "DisplayHistoryTextNodeV2";

		// Token: 0x04000EFA RID: 3834
		private const string NODE_NAME = "Display History Text";

		// Token: 0x04000EFB RID: 3835
		private const string INPUT_IN_NAME = "In";

		// Token: 0x04000EFC RID: 3836
		private const string INPUT_PRIORITY_NAME = "Display Priority";

		// Token: 0x04000EFD RID: 3837
		private const string OUTPUT_OUT_NAME = "Out";

		// Token: 0x04000EFE RID: 3838
		private const int INPUT_IN_INDEX = 0;

		// Token: 0x04000EFF RID: 3839
		private const int INPUT_PRIORITY_INDEX = 1;

		// Token: 0x04000F00 RID: 3840
		private const int OUTPUT_OUT_INDEX = 0;

		// Token: 0x04000F01 RID: 3841
		[SerializeField]
		private int _displayPriority;
	}
}
