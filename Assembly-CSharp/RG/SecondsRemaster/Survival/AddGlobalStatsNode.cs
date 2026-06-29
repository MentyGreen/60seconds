using System;
using NodeEditorFramework;
using RG.Parsecs.Common;
using RG.Parsecs.EndGameEditor;
using RG.Parsecs.EventEditor;
using RG.Parsecs.GoalEditor;
using RG.Parsecs.NodeEditor;
using UnityEngine;

namespace RG.SecondsRemaster.Survival
{
	// Token: 0x020002D2 RID: 722
	[Node(false, "Global Stats/Add Global Stat Node", new Type[]
	{
		typeof(Goal),
		typeof(SurvivalEvent),
		typeof(ReportEvent),
		typeof(SystemEvent),
		typeof(NodeFunction),
		typeof(ExpeditionEvent),
		typeof(SystemStatusEvent),
		typeof(EndGameCanvas)
	})]
	public class AddGlobalStatsNode : EventNode
	{
		// Token: 0x17000498 RID: 1176
		// (get) Token: 0x0600194D RID: 6477 RVA: 0x0006E20A File Offset: 0x0006C40A
		public override string GetID
		{
			get
			{
				return "GE_AddGlobalStatsNode";
			}
		}

		// Token: 0x0600194E RID: 6478 RVA: 0x0006E214 File Offset: 0x0006C414
		public override Node Create(Vector2 pos)
		{
			AddGlobalStatsNode addGlobalStatsNode = ScriptableObject.CreateInstance<AddGlobalStatsNode>();
			addGlobalStatsNode.rect = new Rect(pos.x, pos.y, 300f, 200f);
			addGlobalStatsNode.name = "Add global stat";
			addGlobalStatsNode.CreateMutliInput("In", "Flow");
			addGlobalStatsNode.CreateInput("Value", "Int");
			addGlobalStatsNode.CreateOutput("Out", "Flow");
			return addGlobalStatsNode;
		}

		// Token: 0x0600194F RID: 6479 RVA: 0x0006E285 File Offset: 0x0006C485
		public override Node Duplicate(Vector2 pos)
		{
			AddGlobalStatsNode addGlobalStatsNode = (AddGlobalStatsNode)this.Create(pos + new Vector2(20f, 20f));
			addGlobalStatsNode._key = this._key;
			addGlobalStatsNode._value = this._value;
			return addGlobalStatsNode;
		}

		// Token: 0x06001950 RID: 6480 RVA: 0x0006E2BF File Offset: 0x0006C4BF
		protected override void NodeEnable()
		{
		}

		// Token: 0x06001951 RID: 6481 RVA: 0x0006E2C1 File Offset: 0x0006C4C1
		protected override void NodeGUI()
		{
		}

		// Token: 0x06001952 RID: 6482 RVA: 0x0006E2C3 File Offset: 0x0006C4C3
		protected override void OnNodeValidate()
		{
		}

		// Token: 0x06001953 RID: 6483 RVA: 0x0006E2C8 File Offset: 0x0006C4C8
		public override void Execute(NodeCanvas canvas)
		{
			base.GetInputValue<int>(this.Inputs[1], ref this._value, canvas);
			StatsManager.Instance.AddGlobalData(this._key, this._value);
			this.Outputs[0].GetCustomNodeAcrossConnection<ParsecsNode>().ExecuteWithErrorHandling(canvas);
		}

		// Token: 0x04001316 RID: 4886
		public const string ID = "GE_AddGlobalStatsNode";

		// Token: 0x04001317 RID: 4887
		public const string NODE_NAME = "Add global stat";

		// Token: 0x04001318 RID: 4888
		private const int INPUT_FLOW_INDEX = 0;

		// Token: 0x04001319 RID: 4889
		private const int INPUT_VALUE_INDEX = 1;

		// Token: 0x0400131A RID: 4890
		private const int OUTPUT_FLOW_INDEX = 0;

		// Token: 0x0400131B RID: 4891
		private const string INPUT_FLOW_NAME = "In";

		// Token: 0x0400131C RID: 4892
		private const string OUTPUT_FLOW_NAME = "Out";

		// Token: 0x0400131D RID: 4893
		private const string INPUT_STAT_VALUE = "Value";

		// Token: 0x0400131E RID: 4894
		private const int MINIMAL_VALUE = 1;

		// Token: 0x0400131F RID: 4895
		[SerializeField]
		private string _key;

		// Token: 0x04001320 RID: 4896
		[SerializeField]
		private int _value = 1;
	}
}
