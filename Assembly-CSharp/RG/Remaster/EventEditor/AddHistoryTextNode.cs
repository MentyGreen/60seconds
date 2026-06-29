using System;
using I2.Loc;
using NodeEditorFramework;
using RG.Parsecs.EventEditor;
using RG.Parsecs.GoalEditor;
using RG.Parsecs.NodeEditor;
using RG.Remaster.Survival;
using UnityEngine;

namespace RG.Remaster.EventEditor
{
	// Token: 0x02000222 RID: 546
	[Node(false, "Text Nodes/Add History Text Node", new Type[]
	{
		typeof(SurvivalEvent),
		typeof(SystemEvent),
		typeof(NodeFunction),
		typeof(SystemStatusEvent),
		typeof(ExpeditionEvent),
		typeof(ReportEvent),
		typeof(Goal)
	})]
	public class AddHistoryTextNode : MessageNode
	{
		// Token: 0x17000411 RID: 1041
		// (get) Token: 0x0600153F RID: 5439 RVA: 0x0005E1A1 File Offset: 0x0005C3A1
		public override string GetID
		{
			get
			{
				return "EE_AddHistoryTextNode";
			}
		}

		// Token: 0x06001540 RID: 5440 RVA: 0x0005E1A8 File Offset: 0x0005C3A8
		public override Node Create(Vector2 pos)
		{
			AddHistoryTextNode addHistoryTextNode = ScriptableObject.CreateInstance<AddHistoryTextNode>();
			addHistoryTextNode.rect = new Rect(pos.x, pos.y, 300f, 105f);
			addHistoryTextNode.name = "Add History Text";
			addHistoryTextNode.CreateMutliInput("IN", "Flow");
			addHistoryTextNode.CreateInput("Term", "LocalizedString");
			addHistoryTextNode.CreateInput("Use Current Day", "Bool");
			addHistoryTextNode.CreateInput("Day", "Int");
			addHistoryTextNode.CreateOutput("Out", "Flow");
			this._dayToSet = 0;
			return addHistoryTextNode;
		}

		// Token: 0x06001541 RID: 5441 RVA: 0x0005E244 File Offset: 0x0005C444
		public override Node Duplicate(Vector2 pos)
		{
			AddHistoryTextNode addHistoryTextNode = (AddHistoryTextNode)this.Create(this.rect.position + new Vector2(20f, 20f));
			addHistoryTextNode._term = this._term;
			addHistoryTextNode._dayToSet = this._dayToSet;
			addHistoryTextNode._useCurrentDay = this._useCurrentDay;
			return addHistoryTextNode;
		}

		// Token: 0x06001542 RID: 5442 RVA: 0x0005E29F File Offset: 0x0005C49F
		protected override void NodeEnable()
		{
		}

		// Token: 0x06001543 RID: 5443 RVA: 0x0005E2A1 File Offset: 0x0005C4A1
		protected override void OnNodeValidate()
		{
		}

		// Token: 0x06001544 RID: 5444 RVA: 0x0005E2A3 File Offset: 0x0005C4A3
		protected override void NodeGUI()
		{
		}

		// Token: 0x06001545 RID: 5445 RVA: 0x0005E2A8 File Offset: 0x0005C4A8
		public override void Execute(NodeCanvas canvas)
		{
			base.GetInputValue<LocalizedString>(this.Inputs[1], ref this._term, canvas);
			base.GetInputValue<int>(this.Inputs[3], ref this._dayToSet, canvas);
			base.GetInputValue<bool>(this.Inputs[2], ref this._useCurrentDay, canvas);
			SimpleHistoryManager.Instance.AddEntry(this._term, this._dayToSet, this._useCurrentDay);
			this.Outputs[0].GetCustomNodeAcrossConnection<ParsecsNode>().ExecuteWithErrorHandling(canvas);
		}

		// Token: 0x04000E10 RID: 3600
		private const string NODE_NAME = "Add History Text";

		// Token: 0x04000E11 RID: 3601
		private const string INPUT_FLOW = "IN";

		// Token: 0x04000E12 RID: 3602
		private const string INPUT_TERM = "Term";

		// Token: 0x04000E13 RID: 3603
		private const string INPUT_USE_CURRENT_DAY_NAME = "Use Current Day";

		// Token: 0x04000E14 RID: 3604
		private const string INPUT_DAY = "Day";

		// Token: 0x04000E15 RID: 3605
		private const string OUTPUT_FLOW = "Out";

		// Token: 0x04000E16 RID: 3606
		private const int INPUT_FLOW_INDEX = 0;

		// Token: 0x04000E17 RID: 3607
		private const int INPUT_TERM_INDEX = 1;

		// Token: 0x04000E18 RID: 3608
		private const int INPUT_DECISION_INDEX = 2;

		// Token: 0x04000E19 RID: 3609
		private const int INPUT_DAY_INDEX = 3;

		// Token: 0x04000E1A RID: 3610
		private const int OUTPUT_FLOW_INDEX = 0;

		// Token: 0x04000E1B RID: 3611
		[SerializeField]
		private LocalizedString _term;

		// Token: 0x04000E1C RID: 3612
		[SerializeField]
		private bool _useCurrentDay = true;

		// Token: 0x04000E1D RID: 3613
		[SerializeField]
		private int _dayToSet;

		// Token: 0x04000E1E RID: 3614
		public const string ID = "EE_AddHistoryTextNode";
	}
}
